using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Genre")]//attributes
  public class Genre
    {
        public int Id { get; set; }

        [MaxLength(64)] 
        [Required]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
