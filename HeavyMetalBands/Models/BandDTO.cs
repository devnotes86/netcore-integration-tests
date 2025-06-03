using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavyMetalBands.Models
{
    public class BandDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Band name is required.")]
        public string BandName { get; set; }

        [Range(1900, 2100, ErrorMessage = "Enter a valid year.")]
        public int YearCreated { get; set; }

          
        public string BandNameUppercase { get; set; }
    }

}
