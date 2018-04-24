using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using plane.Models;

namespace plane.Controllers
{
    public class HomeController : Controller
    {
        

        private planeContext _context;

        public HomeController(planeContext context)
        {
         _context = context;

        }
        [HttpGet]
        [Route("home/index")]
        public IActionResult Index()
        {   
            List<User> AllUsers = _context.users.ToList();
            ViewBag.AllUsers = AllUsers;
            return View();
        }
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }

// One post can be liked by many users. (Fan)
// One user can like many posts.
//One person can author many posts.
//One post has one author/user.


}
