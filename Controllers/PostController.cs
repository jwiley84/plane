using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using plane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace plane.Controllers
{
    public class PostController : Controller
    {
        private planeContext _context;
        
        public PostController(planeContext context)
        {
            _context = context;
        }
//Main Page
        [HttpGet]
        [Route("Splash")]
        public IActionResult Splash()
        {
            int? userID = HttpContext.Session.GetInt32("userID");
            if (userID != null)
            {
                User user = _context.users.SingleOrDefault(usr => usr.userID == HttpContext.Session.GetInt32("userID"));
        if (user != null) 
        {
        ViewBag.usr = user.alias;
        }
        List<Post> AllPosts = _context.posts
                          .Include(GS => GS.author)
                          .Include(GS => GS.fans)
                  .ThenInclude(fan => fan.user).ToList();
		
                List<Dictionary<string, object>> PostList = new List<Dictionary<string, object>>();
                


                foreach (Post post in AllPosts)
                {   if (post.deleted != 1) 
                    {
                      bool owned = false;
                      bool liked = false;   
                      int likes = 0;
                      User ownerUser = _context.users.SingleOrDefault(usr => usr.userID == post.userID);
                      string owner = ownerUser.alias;
                      if (HttpContext.Session.GetInt32("userID") == post.userID)
                      {
                        owned = true;
                      }
                      foreach (var like in post.fans)
                      {
                        ++likes;
                      }
                      Dictionary<string, object> newPost = new Dictionary<string, object>();
                      newPost.Add("postID", post.postID);
                      newPost.Add("postContent", post.postContent);
                      newPost.Add("userID", post.userID);
                      newPost.Add("owner", owner);
                      newPost.Add("owned", owned);
                      newPost.Add("likes", likes);
                      newPost.Add("liked", liked);
                      PostList.Add(newPost);
                    }
                   
                }
                var sort = PostList.OrderByDescending(x => x.ContainsKey("likes") ? x["likes"] : string.Empty);
                ViewBag.posts = sort;
                return View("Splash");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

//Post form for create message
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(PostViewModel model)
        {
          int? userID = HttpContext.Session.GetInt32("userID");
          if (userID != null) 
            {
              if (ModelState.IsValid)
              {
              Post newSession = new Post{
                postContent = model.postContent,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
              //   deleted = 0,
                userID = (int)HttpContext.Session.GetInt32("userID")
              };
              _context.posts.Add(newSession);
              _context.SaveChanges();
            // return Content("this is borked");
              return RedirectToAction("Splash", "Post");
              }
              else
              {
              return Content("THIS IS THE ERORR");
              }
            }   
          else
            {
                return RedirectToAction("Index", "User");
            }
        }
//Post form for like
    [HttpGet]
    [Route("Like/{PostID}")]
    public IActionResult Like(int PostID)
    {
        Fan newFan = new Fan
        {
            userID = (int)HttpContext.Session.GetInt32("userID"),
            postID = PostID
        };
        _context.fans.Add(newFan);
        _context.SaveChanges();
        return RedirectToAction("Splash", "Post");
    }
//post form for delete FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIISSSSSSSSSSSSSSSSSSSS
        [HttpGet]
        [Route("delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            Post deleteTarget = _context.posts.SingleOrDefault(
            gs => gs.userID == (int)HttpContext.Session.GetInt32("userID") &&
            gs.postID == PostId);
          if (deleteTarget != null)
          {
            //SRSLY. My biggest Hiccup. I'm going batty.
            //this does NOT deleted from the DB. I suck at DBs. What it DOES do is hide it from the user.
            //If ever build a list of 'what weird ideas users have EVER had', ans shows the user, I'll fix it then.
            deleteTarget.deleted = 1;
            // _context.posts.Remove(deleteTarget);
            _context.SaveChanges();
          }
          return RedirectToAction("Splash");
        }
//Show individual message
        [HttpGet]
        [Route("Post/{PostId}")]
        public IActionResult Display(int PostId)
        {
          int? userID = HttpContext.Session.GetInt32("userID");
            if (userID != null)
            {
                List<User> perFan = new List<User>();
                var thisPost =  _context.posts.Where(a => a.postID == PostId)
                                                .Include(gs => gs.fans)
                                                .ThenInclude(u => u.user).SingleOrDefault();
                var owned = _context.users.SingleOrDefault(u => u.userID == thisPost.userID);
                if (owned != null && thisPost != null) 
                {
                    ViewBag.Post = thisPost;
                    ViewBag.Owner = owned.alias;
                    foreach (Fan fan in thisPost.fans)
                    {
                        if (!perFan.Any(f => f.userID == fan.userID))
                        {
                            perFan.Add(fan.user);
                        }
                    }
                  ViewBag.nerdList = perFan;
                  return View("DisplayPost");
                }
                else 
                { 
                    return RedirectToAction("Splash", "Post");
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}

    
