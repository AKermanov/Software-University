namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {

            var prisoners = context
                .Prisoners
                .ToArray()
                .Where(x => ids.Contains(x.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(off => new
                    {
                        OfficerName = off.Officer.FullName,
                        Department = off.Officer.Department.Name
                    })
                    .OrderBy(x => x.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(x => x.Officer.Salary)
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();
              

            string json = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return json;
            ;

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonersInboxDto[]), new XmlRootAttribute("Prisoners"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var arrayOfNames = prisonersNames.Split(",").ToArray();
            StringWriter stringWriter = new StringWriter(sb);

            using (stringWriter)
            {
                var prosoners = context.Prisoners
                    .Where(x => arrayOfNames.Contains(x.FullName))
                    .ToArray()
                    .Select(x => new ExportPrisonersInboxDto
                    {
                        Id = x.Id,
                        Name = x.FullName,
                        IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                        EncrMessages = x.Mails.Select(j => new EncryptedMessages
                        {
                          DesctiptionReverse = Reverse(j.Description)

                        }).ToArray()
                    })
                    .OrderBy(x=>x.Name)
                    .ThenBy(x=>x.Id)
                    .ToArray();

                     xmlSerializer.Serialize(stringWriter, prosoners, namespaces);
            }

           

            return sb.ToString().TrimEnd();
        }

        public static string Reverse(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}