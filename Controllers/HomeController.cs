using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _dbContext; // Inject AuthDbContext


        public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            _logger = logger;
            this._userManager = userManager;
            _dbContext = dbContext; // Inject AuthDbContext

        }

        public async Task<IActionResult> Index()
        {
            ViewData["UserID"]= _userManager.GetUserId(this.User);
            var user = await _userManager.GetUserAsync(User);
            ViewData["Email"] = user.Email;
            ViewData["LinkedinURL"] = user.LinkedinURL;
            ViewData["FacebookURL"] = user.FacebookURL;
             
            var userId = _userManager.GetUserId(this.User);

            var listtt = _dbContext.Additionals.Where(n => n.UserId == userId).ToList();

            
            return View(listtt);


             
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Save(string itemName, string itemValue)
        {

            if (ModelState.IsValid)
            {
                // Get the current user
                var user = await _userManager.GetUserAsync(User);

                // Create a new Additional object
                var additionalItem = new Additional
                {
                    Title = itemName,
                    Value = itemValue,
                    UserId = await _userManager.GetUserIdAsync(user) // Associate additional item with the current user
                };

                // Add the new additional item to the context and save changes


                _dbContext.Additionals.Add(additionalItem);
                await _dbContext.SaveChangesAsync();

                // Redirect to the home page or wherever appropriate
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If model state is not valid, return to the form
                return View("YourFormView");
            }
        }

    }
}
