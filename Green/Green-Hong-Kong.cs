using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// Green-Hong-Kong

namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@company.co";
        private string _mailFrom = "admin@Mycompany.co";

        public void Sent (string subject, string message)
        {
            Debug.WriteLine($"Sending mail from {_mailFrom} to {_mailTo}.");
            Debug.WriteLine($"Subject is {subject}.");
            Debug.WriteLine($"Message is {message}.");
        }
    }
}
