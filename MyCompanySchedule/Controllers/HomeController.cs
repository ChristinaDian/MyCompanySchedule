using Microsoft.AspNetCore.Mvc;
using MyCompanySchedule.ActionFilters;
using MyCompanySchedule.Entities;
using MyCompanySchedule.ExtentionMethods;
using MyCompanySchedule.Repositories;
using MyCompanySchedule.ViewModels.Home;
using System.Diagnostics;

namespace MyCompanySchedule.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
        [AuthenticationFilter]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            MyDbContext context = new MyDbContext();
            User loggedUser = context.Users.Where(u=>u.Username == model.Username &&
                                   u.Password == model.Password).FirstOrDefault();

            if (loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterVM model = new RegisterVM();

            return View(model);
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            CompaniesRepository companiesRepository = new CompaniesRepository();
            Company company = new Company();
            company.Name = model.CompanyName;
            company.Subscription = "free";
            companiesRepository.Save(company);

            UsersRepository repo = new UsersRepository();

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.CompanyId = company.Id;
            item.Role = "Owner";

            repo.Save(item);
            User loggedUser = repo.GetFirstOrDefault(u => u.Username == model.Username &&
                                   u.Password == model.Password);
            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }
        [AuthenticationFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Home", "Home");
        }
    }
}