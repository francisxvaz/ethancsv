using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CsvHelper;
using CsvHelper.Configuration;

namespace EthanCommunion.Service.Controllers
{
    public class ValuesController : ApiController
    {
        private string _appFolderPath = string.Empty;
        public ValuesController()
        {
            _appFolderPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data","guests.csv");
        }
        

        // GET api/<controller>
        public IEnumerable<Guest> Get()
        {
            using (var textReader = new StreamReader(_appFolderPath))
            {
                var csv = new CsvReader(textReader,new CsvConfiguration
                {
                    HasExcelSeparator = false
                });
                var records = csv.GetRecords<Guest>();
                return records.ToList();
            }

        }

        // GET api/<controller>/5
        public Guest Get(string id)
        {
            var allGuests = Get();
            return allGuests.FirstOrDefault(x => x.Id == id);
        }

        // POST api/<controller>
        public void Post([FromBody]Guest guest)
        {
            guest.Id = Guid.NewGuid().ToString("n").Substring(0, 10); 

            using (var textWriter = new StreamWriter(_appFolderPath, true))
            {
                var csv = new CsvWriter(textWriter,new CsvConfiguration
                {
                  TrimFields  = true,
                  TrimHeaders = true
                });
                csv.WriteRecord(guest);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class Guest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public bool Accept { get; set; }
    }

    public sealed class GuestMap : CsvClassMap<Guest>
    {
        public GuestMap()
        {
            Map(m => m.Accept);
        }
    }
}