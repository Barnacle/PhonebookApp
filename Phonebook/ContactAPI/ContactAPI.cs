using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

// ReSharper disable once IdentifierTypo
namespace Phonebook.ContactAPI
{
    // ReSharper disable once InconsistentNaming
    public class ContactAPI
    {
        public HttpClient Initial()
        {
            var name = Dns.GetHostName(); // get container id
            var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44335/") // IIS
                //BaseAddress = new Uri("http://localhost:8080/")
                //BaseAddress = new Uri("http://192.168.99.100:8080")
            };

            return client;
        }
    }
}
