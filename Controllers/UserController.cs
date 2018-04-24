using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using plane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace plane.Controllers
{
    public class UserController : Controller
    {
        private planeContext _context;
        
        public UserController(planeContext context)
        {
            _context = context;
        }
//this shows the form to login
        [HttpGet]
        [Route("notthisway")]
        public IActionResult Login()
        {
            return PartialView("Login", new LoginViewModel() );
        }
//main index page
        [HttpGet] 
        [Route("")]
        public IActionResult Index() 
        {
            return View("Index");
        }
//this sends the data for validation
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string login_email, string login_password)
        {
            User foundUser = _context.users.SingleOrDefault(user => user.email == login_email);
            var Hasher = new PasswordHasher<User>();
            if (foundUser == null || 0 == Hasher.VerifyHashedPassword(foundUser, foundUser.password, login_password) ) 
            {
                ViewBag.errorMsg = "Invalid login. Please check email or password and try again.";
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("userID", foundUser.userID);
                TempData["String"] = "Login";
                return RedirectToAction("Splash", "Post");//, "Activity");
            }
            
        }

        [HttpGet]
        [Route("user/{userID}")]
        public IActionResult ShowUser(int UserID)
        {
            User foundUser = _context.users.SingleOrDefault(user => user.userID == UserID);
            if (foundUser != null)
            {
                int PostCount = 0;
                int LikeCount = 0;
                List<Post> AllPosts = _context.posts.Where(GS => GS.userID == UserID).ToList();
                List<Fan> AllLikes = _context.fans.Where(GS => GS.userID == UserID).ToList();
                foreach (Post post in AllPosts)
                {
                    PostCount++;
                }
                foreach (Fan like in AllLikes)
                {
                    LikeCount++;
                }
                ViewBag.Likes = LikeCount;
                ViewBag.Posts = PostCount;
                ViewBag.User = foundUser;
                return View("ShowUser");
                // return Content("Found");
            }
            else{
                return Content("Oops, Something went wrong! That User page doesn't exist");
            }
            
            // return View("ShowUser");
        }
// this shows the form for register
        [HttpGet] 
        [Route("user/create")]
        public IActionResult Create()
        {
            return PartialView("Create", new RegisterViewModel() );
        }
// this posts the values to the DB for register
        [HttpPost] 
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User oldUser = _context.users.SingleOrDefault(user => user.email == model.email);
                if (oldUser != null)
                {
                    ViewBag.errorMsg = "This email is already registered! Please go back and log in instead!";
                    return View ("Create", model);
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newUser = new User 
                {
                    alias = model.alias,
                    name = model.name,
                    email = model.email,
                    password = model.password,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                newUser = _context.users.SingleOrDefault(user => user.email == newUser.email);
                HttpContext.Session.SetInt32("userID", newUser.userID);
                
                //need to add confirmation for matching passwords
                TempData["String"] = "Valid Registration"; 
                return RedirectToAction("Splash", "Post");//, "Activity");
                // return Content("Plaeholder for registration");
            }
            else 
            {
                return View("Index");
            }
        }
//Logout
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
        

    }
}