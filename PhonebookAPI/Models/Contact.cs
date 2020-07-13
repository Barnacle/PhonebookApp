using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable once IdentifierTypo
namespace PhonebookAPI.Models
{
    public class Contact
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        public List<Number> Number { get; set; } = new List<Number>();
    }
}
