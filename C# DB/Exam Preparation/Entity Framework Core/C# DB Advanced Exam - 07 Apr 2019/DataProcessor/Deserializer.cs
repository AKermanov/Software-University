using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Cinema.DataProcessor
{
    using System;
    using System.Collections;
    using Cinema.Data;
    using Cinema.DataProcessor.ExportDto;
    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            //Slavi's solution
            var moviesDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);

            var movies = new List<Movie>();

            var sb = new StringBuilder();
            foreach (var dto in moviesDtos)
            {
                if (IsValid(dto))
                {
                    var movie = new Movie
                    {
                        Title = dto.Title,
                        Genre = dto.Genre,
                        Duration = dto.Duration,
                        Rating = dto.Rating,
                        Director = dto.Director

                    };
                    movies.Add(movie);

                    sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();





            /*
            var moviesDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);

            var movies = new List<Movie>();
            var sb = new StringBuilder();

            foreach (var movieDto in moviesDtos)
            {
                var movieExists = movies.Any(t => t.Title == movieDto.Title);
                var isValidDto = IsValid(movieDto);
                var isValidEnum = Enum.TryParse(typeof(Genre), movieDto.Genre, out object genre);

                if (movieExists || !isValidDto || !isValidEnum)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = Mapper.Map<Movie>(movieDto);

                movies.Add(movie);

                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            var result = sb.ToString();

            return result;
            */
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var objects = JsonConvert.DeserializeObject<ImportHallSeatDto[]>(jsonString);

            var sb = new StringBuilder();
            foreach (var dto in objects)
            {
                if (IsValid(dto))
                {
                    var hall = new Hall
                    {
                        Name = dto.Name,
                        Is4Dx = dto.Is4Dx,
                        Is3D = dto.Is3D
                    };

                    //трябва да създадем залата и след това да сложим seats
                    context.Halls.Add(hall);
                    AddSeatsInDatabse(context, hall.Id, dto.Seats);
                    var projectionType = GetProjectionType(hall);
                    sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }


            context.SaveChanges();

            return sb.ToString().TrimEnd();


            /*
            var hallSeatDtos = JsonConvert.DeserializeObject<ImportHallSeatDto[]>(jsonString);
            var halls = new List<Hall>();

            var sb = new StringBuilder();

            foreach (var hallSeatDto in hallSeatDtos)
            {
                if (!IsValid(hallSeatDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall
                {
                    Name = hallSeatDto.Name,
                    Is3D = hallSeatDto.Is3D,
                    Is4Dx = hallSeatDto.Is4Dx,
                };

                for (int i = 0; i < hallSeatDto.Seats; i++)
                {
                   hall.Seats.Add(new Seat());
                }

                halls.Add(hall);

                string status = "";

                if (hall.Is4Dx)
                {
                    status = hall.Is3D ? "4Dx/3D" : "4Dx";
                }
                else if (hall.Is3D)
                {
                    status = "3D";
                }
                else
                {
                    status = "Normal";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, status, hall.Seats.Count));
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString();
            */
        }

        private static object GetProjectionType(Hall hall)
        {
            var res = "normal";

            if (hall.Is4Dx && hall.Is4Dx)
            {
                res = "4Dx/3D";
            }
            else if (hall.Is3D)
            {
                res = "3D";
            }
            else if (hall.Is4Dx)
            {
                res = "4Dx";
            }
            return res;
        }

        private static void AddSeatsInDatabse(CinemaContext context, int hallId, int seatsCount)
        {
            var seats = new List<Seat>();
            for (int i = 0; i < seatsCount; i++)
            {
                seats.Add(new Seat { HallId = hallId });
            }
            context.AddRange(seats);
            context.SaveChanges();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportProjectionDto[]), new XmlRootAttribute("Projections"));

            var objectcts = (ImportProjectionDto[])serializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();

            foreach (var dto in objectcts)
            {
                if (IsValid(dto) && IsValidMovieId(context, dto.MovieId) && IsValidHallId(context, dto.HallId))
                {
                    var projection = new Projection
                    {
                        MovieId = dto.MovieId,
                        HallId = dto.HallId,
                        DateTime = DateTime.ParseExact(dto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    };

                    context.Projections.Add(projection);
                    var dateTimeRes = projection.DateTime.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture);
                    sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, dateTimeRes));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();






            /*
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectionDto[]), new XmlRootAttribute("Projections"));

            var projectionsDto = (ImportProjectionDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var projections = new List<Projection>();
            var sb = new StringBuilder();

            foreach (var projectionDto in projectionsDto)
            {
                var movie = context.Movies.Find(projectionDto.MovieId);
                var hall = context.Halls.Find(projectionDto.HallId);

                if (movie == null || hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = DateTime.ParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };

                projections.Add(projection);

                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();
            return sb.ToString();
            */
        }

        private static bool IsValidHallId(CinemaContext context, int hallId)
        {
            return context.Halls.Any(m => m.Id == hallId);
        }

        private static bool IsValidMovieId(CinemaContext context, int movieId)
        {
            return context.Movies.Any(m => m.Id == movieId);
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportTicketDto[]), new XmlRootAttribute("Customers"));

            var objects = (ImportTicketDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();

            foreach (var dto in objects)
            {
                if (IsValid(dto))
                {
                    var customer = new Customer
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        Balance = dto.Balance
                        
                    };

                    context.Customers.Add(customer);
                    AddCustomerTickets(context, customer.Id, dto.Tickets);
                    sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, dto.FirstName, dto.LastName, dto.Tickets.Length));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();



            /*
            var xmlSerializer = new XmlSerializer(typeof(ImportTicketDto[]), new XmlRootAttribute("Customers"));

            var customersDto = (ImportTicketDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var customers = new List<Customer>();
            var sb = new StringBuilder();

            foreach (var customerDto in customersDto)
            {
                var projections = context.Projections.Select(x => x.Id).ToArray();
                var projectionExists = projections.Any(x => customerDto.Tickets.Any(s => s.ProjectionId != x));

                if (!IsValid(customerDto) && customerDto.Tickets.All(IsValid)
                    && projectionExists)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                };

                foreach (var ticket in customerDto.Tickets)
                {
                    customer.Tickets.Add(new Ticket
                    {
                        ProjectionId = ticket.ProjectionId,
                        Price = ticket.Price
                    });
                }

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));

                customers.Add(customer);
            
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
            return sb.ToString();
            */
        }

        private static void AddCustomerTickets(CinemaContext context, int customerId, ImportTicketArrayDto[] dtoTickets)
        {
            var tickets = new List<Ticket>();
            foreach (var dto in dtoTickets)
            {
                if (IsValid(dto))
                {
                    var ticket = new Ticket
                    {
                        ProjectionId = dto.ProjectionId,
                        CustomerId = customerId,
                        Price = dto.Price
                    };

                    tickets.Add(ticket);
                }
            }

            context.Tickets.AddRange(tickets);
            context.SaveChanges();
        }

        private static bool IsValid(object dto)
        {
            //1:00:18
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}