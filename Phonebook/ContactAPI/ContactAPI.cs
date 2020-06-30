using System;
using System.Net.Http;

// ReSharper disable once IdentifierTypo
namespace Phonebook.ContactAPI
{
    // ReSharper disable once InconsistentNaming
    public class ContactAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44354/")
            };
            return client;
        }
    }
}
