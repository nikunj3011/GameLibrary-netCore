using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    public class DataStructureController : Controller
    {
        private readonly ILogger logger;

        public DataStructureController(ILogger logger)
        {
            this.logger = logger;
        }
        public IActionResult DynamicArray()
        { 
            var dArray = new DynamicArray<int>(4);
            dArray.add(1);
            dArray.add(2);
            dArray.add(3);
            dArray.set(1, 5);
            dArray.removeAt(1);
            dArray.remove(1);
            Console.WriteLine("\n");
            Console.WriteLine(dArray.get(0));
            Console.WriteLine(dArray.indexOf(3));
            Console.WriteLine(dArray.contains(3));
            Console.WriteLine(dArray.size());
            Console.WriteLine(dArray.isEmpty());
            dArray.clear();
            Console.WriteLine(dArray.isEmpty());
            return Ok();
        }
    }
}
