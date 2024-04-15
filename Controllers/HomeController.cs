using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AuthSystem.Data;

namespace AuthSystem.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _dbContext; 

        public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            _logger = logger;
            this._userManager = userManager;
            _dbContext = dbContext; 
        }

        public async Task<IActionResult> Index()
        {
            ViewData["UserID"]= _userManager.GetUserId(this.User);
            var user = await _userManager.GetUserAsync(User);
            ViewData["Email"] = user.Email;
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;



            var userId = _userManager.GetUserId(this.User);
            var URL_list = _dbContext.Additionals.Where(n => n.UserId == userId).ToList();
            return View(URL_list);
 
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
                var user = await _userManager.GetUserAsync(User);


                var additionalItem = new Additional
                {
                    Title = itemName,
                    Value = itemValue,
                    UserId = await _userManager.GetUserIdAsync(user) 
                };


                _dbContext.Additionals.Add(additionalItem);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("YourFormView");
            }
        }

        

        [HttpPost]
        public IActionResult Edit(int editItemId, string editItemName, string editItemValue)
        {
            var userId = _userManager.GetUserId(this.User);

           
                var note = _dbContext.Additionals.FirstOrDefault(n => n.UserId == userId && n.Id== editItemId);
                if (note != null)
                {
                    // Update the additional list for different urls
                    note.Title = editItemName;
                    note.Value = editItemValue;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index), "Home");
                }
            
            return Content("Error updating record.");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
 
            var itemToDelete = _dbContext.Additionals.Find(id);
            if (itemToDelete != null)
            {
                _dbContext.Additionals.Remove(itemToDelete);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                return NotFound();
            }
        }
    }
}
