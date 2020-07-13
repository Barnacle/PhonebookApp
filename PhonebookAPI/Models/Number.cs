using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable once IdentifierTypo
namespace PhonebookAPI.Models
{
    public class Number
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string PhoneNumber { get; set; }
        public long ContactId { get; set; }
    }
}
