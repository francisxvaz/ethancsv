using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EthanCommunion.Service.Controllers
{
    public class TestController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "Francis",
                "Leena"
            };
        }
    }
}
