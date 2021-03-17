using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessPaymentController
    {

       [HttpPost]
        public async Task<bool>  Process(int PaymentId)
        {
          var rdn = new Random();
          await Task.Delay(1000);
          return rdn.Next(10) %2 == 0;
        }
    
    }
}