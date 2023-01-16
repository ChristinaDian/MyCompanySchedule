using Microsoft.AspNetCore.Mvc;
using MyCompanySchedule.ActionFilters;
using MyCompanySchedule.Entities;
using MyCompanySchedule.ExtentionMethods;
using MyCompanySchedule.Repositories;
using MyCompanySchedule.ViewModels.Shared;
using MyCompanySchedule.ViewModels.Users;
using System.Linq.Expressions;

namespace MyCompanySchedule.Controllers
{
    [AuthenticationFilter]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {
            UsersRepository repo = new UsersRepository();
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            model.Pager ??= new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 5 : model.Pager.ItemsPerPage;
           
            model.Filter ??=new FilterVM();  
            Expression<Func<User, bool>> filter = i =>
                                (i.CompanyId==loggedUser.CompanyId) &&
                                (string.IsNullOrEmpty(model.Filter.Username) || i.Username.Contains(model.Filter.Username)) &&
                                (string.IsNullOrEmpty(model.Filter.FirstName) || i.FirstName.Contains(model.Filter.FirstName)) &&
                                (string.IsNullOrEmpty(model.Filter.LastName) || i.LastName.Contains(model.Filter.LastName));
           
            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)model.Pager.ItemsPerPage);
            model.Items = repo.GetAll(filter, i=>i.Id, model.Pager.Page, model.Pager.ItemsPerPage);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateVM model = new CreateVM();
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser.Role == "Employee")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return RedirectToAction("Index", "Users");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            if(loggedUser.Role == "Employee")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return RedirectToAction("Index", "Users");
            }
            UsersRepository repo = new UsersRepository();

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.CompanyId = loggedUser.CompanyId;
            item.WorkingHoursPerMonth = model.WorkingHoursPerMonth;
            item.Role = model.Role;
            item.Department = model.Department;

            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = repo.GetFirstOrDefault(u => u.Id == id);
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser.Role == "Employee")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return RedirectToAction("Index", "Users");
            }
            if (item == null)
                return RedirectToAction("Index", "Users");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;   
            model.LastName = item.LastName;
            model.CompanyId = item.CompanyId;
            model.WorkingHoursPerMonth = item.WorkingHoursPerMonth;
            model.Role = item.Role;
            model.Department = item.Department;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            if (loggedUser.Role == "Employee")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return RedirectToAction("Index", "Users");
            }
            UsersRepository repo = new UsersRepository();

            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.CompanyId = model.CompanyId;
            item.WorkingHoursPerMonth = model.WorkingHoursPerMonth;
            item.Role = model.Role;
            item.Department = model.Department;

            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }
        public IActionResult Delete(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = new User();
            item.Id = id;

            repo.Delete(item);

            return RedirectToAction("Index", "Users");
        }
    }
}
