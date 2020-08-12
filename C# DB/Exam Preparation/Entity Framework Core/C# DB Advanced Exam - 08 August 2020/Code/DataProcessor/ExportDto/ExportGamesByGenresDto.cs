using System.Collections;
using System.Collections.Generic;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ExportDto
{
   public class ExportGamesByGenresDto
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
        public ICollection<GameDto> Games { get; set; }
    }

    public class GameDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Developer { get; set; }

        public string[] Tags { get; set; }
    }
}
