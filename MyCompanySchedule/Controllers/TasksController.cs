using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCompanySchedule.ActionFilters;
using MyCompanySchedule.Entities;
using MyCompanySchedule.ExtentionMethods;
using MyCompanySchedule.Repositories;
using MyCompanySchedule.ViewModels.Shared;
using MyCompanySchedule.ViewModels.Tasks;
using System.Linq.Expressions;

namespace MyCompanySchedule.Controllers
{
    [AuthenticationFilter]
    public class TasksController : Controller
    {
        public IActionResult Index(IndexVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            TasksRepository repo = new TasksRepository();
            UserToTasksRepository userToTasksRepo = new UserToTasksRepository();

            model.Pager ??= new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 5 : model.Pager.ItemsPerPage;

            List<int> assignedTaskIds = userToTasksRepo.GetAll(i => i.UserId == loggedUser.Id).Select(i => i.TodolistId).ToList();

            model.Filter ??= new FilterVM();
            Expression<Func<ToDoList, bool>> filter = i =>
                        (i.OwnerId == loggedUser.Id || assignedTaskIds.Contains(i.Id)) &&
                        (string.IsNullOrEmpty(model.Filter.Title)|| i.Title.Contains(model.Filter.Title)) &&
                        (model.Filter.IsDone == null || i.IsDone.Equals(model.Filter.IsDone)) &&
                        (model.Filter.OwnerTasks==false || i.OwnerId == loggedUser.Id);


            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) 
                                                                  / (double)model.Pager.ItemsPerPage);

            model.Items = repo.GetAll(filter,
                                        i => i.Id,
                                        model.Pager.Page,
                                        model.Pager.ItemsPerPage);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CreateVM model = new CreateVM();
            model.OwnerId = loggedUser.Id;

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            if (model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TasksRepository repo = new TasksRepository();
            ToDoList item = new ToDoList();
            item.OwnerId = model.OwnerId;
            item.Title = model.Title;
            item.Deadline = model.Deadline;

            repo.Save(item);

            return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            TasksRepository repo = new TasksRepository();
            ToDoList item = repo.GetFirstOrDefault(t => t.Id == id);

            if (item == null)
                return RedirectToAction("Index", "Tasks");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.OwnerId = item.OwnerId;
            model.Title = item.Title;
            model.Deadline = item.Deadline;
            model.IsDone = item.IsDone;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TasksRepository repo = new TasksRepository();
            ToDoList item = repo.GetFirstOrDefault(t => t.Id == model.Id);

            item.Id = model.Id;
            item.OwnerId = model.OwnerId;
            item.Title = model.Title;
            item.Deadline = model.Deadline;
            item.IsDone = model.IsDone;

            repo.Save(item);

            return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public IActionResult Assign(int Id)
        {
            UserToTasksRepository userToTasksRepository = new UserToTasksRepository();
            TasksRepository repo = new TasksRepository();
            UsersRepository usersRepository = new UsersRepository();
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            AssignVM model = new AssignVM();

            model.ToDoList = repo.GetFirstOrDefault(i => i.Id == Id);

            model.Assignments = userToTasksRepository.GetAll(i => i.TodolistId == Id);

            List<int> usersAssignedList = model.Assignments.Select(i => i.UserId).ToList();

            usersAssignedList.Add(model.ToDoList.OwnerId);
            model.Users = usersRepository.GetAll(i => !usersAssignedList.Contains(i.Id) && i.CompanyId == loggedUser.CompanyId);
            return View(model);
        }
        [HttpPost]
        public IActionResult Assign(AssignVM model)
        {
            UserToTasksRepository userToTasksRepository = new UserToTasksRepository();

            foreach(int userId in model.UserIds)
            {
                UserToTask item = new UserToTask();
                item.UserId = userId;
                item.TodolistId = model.ToDoListId;

                userToTasksRepository.Save(item);
            }
            return RedirectToAction("Assign", "Tasks", new {Id = model.ToDoListId});
        }
        [HttpGet]
        public IActionResult RevokeAssignment(int id)
        {
            UserToTasksRepository userToTasksRepository = new UserToTasksRepository();
            UserToTask item = userToTasksRepository.GetFirstOrDefault(i => i.Id == id);

            userToTasksRepository.Delete(item);
            return RedirectToAction("Assign", "Tasks", new {Id = item.TodolistId });
        }
    }
}
