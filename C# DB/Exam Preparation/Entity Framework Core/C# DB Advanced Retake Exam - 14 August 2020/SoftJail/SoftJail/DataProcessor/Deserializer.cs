namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var departmentsDto = JsonConvert.DeserializeObject<ImportDepartmentsCellsDto[]>(jsonString);
            var listOfDepartments = new List<Department>();

            foreach (var department in departmentsDto)
            {
                var cellsCount = department.Cells.Count();

                if (!IsValid(department) || cellsCount == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var curentDepartment = new Department
                {
                    Name = department.Name,
                };

                foreach (var cell in department.Cells)
                {
                    if (!IsValid(cell))
                    {
                        sb.AppendLine("Invalid Data");
                        continue;
                    }

                    var currentCell = new Cell
                    {
                        CellNumber = cell.CellNumber,
                        HasWindow = cell.HasWindow
                    };

                    curentDepartment.Cells.Add(currentCell);
                }

                if (cellsCount != curentDepartment.Cells.Count)
                {
                    continue;
                }

                listOfDepartments.Add(curentDepartment);
                sb.AppendLine($"Imported {curentDepartment.Name} with {curentDepartment.Cells.Count} cells");
            }

            context.Departments.AddRange(listOfDepartments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var prisonersDto = JsonConvert.DeserializeObject<ImportPrisonersMailsDto[]>(jsonString);
            var listPrisonersMail = new List<Prisoner>();

            foreach (var prisoner in prisonersDto)
            {
                var initialMailCount = prisoner.Mails.Count();

                if (!IsValid(prisoner))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime realiseDate;

                var prisonerRealiceDate = DateTime.TryParseExact(prisoner.ReleaseDate,
                                                                 "dd/MM/yyyy",
                                                                 CultureInfo.InvariantCulture,
                                                                 DateTimeStyles.None,
                                                                 out realiseDate);

                var currentPrisoner = new Prisoner
                {
                    FullName = prisoner.FullName,
                    Nickname = prisoner.Nickname,
                    Age = prisoner.Age,
                    IncarcerationDate = DateTime.ParseExact(prisoner.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = realiseDate,
                    Bail = prisoner.Bail,
                    CellId = prisoner.CellId

                };

                foreach (var mail in prisoner.Mails)
                {
                    if (!IsValid(mail))
                    {
                        continue;
                    }

                    var currentMail = new Mail
                    {
                        Description = mail.Description,
                        Sender = mail.Sender,
                        Address = mail.Address
                    };

                    currentPrisoner.Mails.Add(currentMail);
                }

                if (initialMailCount != currentPrisoner.Mails.Count)
                {
                    continue;
                }

                listPrisonersMail.Add(currentPrisoner);

                sb.AppendLine($"Imported {currentPrisoner.FullName} {currentPrisoner.Age} years old");
            }

            context.Prisoners.AddRange(listPrisonersMail);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var officersDto = DeserializeObject<ImportOfficersPrisonersDto>("Officers", xmlString);

            StringBuilder sb = new StringBuilder();

            List<Officer> validOfficer = new List<Officer>();

         

            foreach (var officer in officersDto)
            {

                if (!IsValid(officer) || !IsValidWepon(officer) || !IsValidPosition(officer) )
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var currentOfficer = new Officer
                {
                    FullName = officer.FullName,
                    Salary = officer.Salary,
                    Position = (Position)Enum.Parse(typeof(Position), officer.Position),
                    Weapon = (Weapon)Enum.Parse(typeof(Weapon), officer.Weapon),
                    DepartmentId = officer.DepartmentId
                };

                foreach (var prisoner in officer.Prisoners)
                {
                    /*
                    var prisonnerExist = context.Prisoners.FirstOrDefault(x => x.Id == prisoner.Id);

                    if (prisonnerExist == null)
                    {
                        continue;
                    }

                    */
                    var prisonersOfficers = new OfficerPrisoner
                    {
                        OfficerId = currentOfficer.Id,
                        PrisonerId = prisoner.Id
                    };

                    currentOfficer.OfficerPrisoners.Add(prisonersOfficers);
                }

                validOfficer.Add(currentOfficer);
                sb.AppendLine($"Imported {currentOfficer.FullName} ({currentOfficer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(validOfficer);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValidWepon(ImportOfficersPrisonersDto officer)
        {
            if (officer.Weapon == "Knife" || 
                officer.Weapon == "FlashPulse" || 
                officer.Weapon == "ChainRifle" || 
                officer.Weapon == "Pistol" || 
                officer.Weapon == "Sniper")
            {
                return true;
            }
            return false;
        }

        private static bool IsValidPosition(ImportOfficersPrisonersDto officer)
        {
            
            if (officer.Position == "Overseer" ||
                officer.Position == "Guard" ||
                officer.Position == "Watcher" ||
                officer.Position == "Labour" 
                )
            {
                return true;
            }
            return false;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

        private static T[] DeserializeObject<T>(string rootElement, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElement));
            var deserializedDtos = (T[])xmlSerializer.Deserialize(new StringReader(xmlString));
            return deserializedDtos;
        }
    }
}