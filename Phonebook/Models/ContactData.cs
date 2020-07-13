using System.Collections.Generic;

// ReSharper disable once IdentifierTypo
namespace Phonebook.Models
{
    public class ContactData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<NumberData> Number { get; set; } = new List<NumberData>();

        internal void CreatePhoneNumbers(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                Number.Add(new NumberData());
            }
        }
    }
}
