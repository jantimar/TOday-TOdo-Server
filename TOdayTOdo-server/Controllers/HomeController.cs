using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOdayTOdo_server.Models;
using TOdayTOdo_server.Filters;

namespace TOdayTOdo_server.Controllers
{
    public class HomeController : Controller
    {
        //Trieda zabezpecujuca databazu
        private DatabaseContext Database = new DatabaseContext();

        public String Index()
        {
            return "Run";
        }

        // zobrazenie vsetkych errorov
        [HttpGet]
        public ActionResult AllErrors()
        {
            List<MyError> allErrors = null;
            try
            {
                allErrors = Database.Errors.ToList();
            }
            catch (Exception exception)
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json(allErrors, JsonRequestBehavior.AllowGet);
        }
        
        // zobrazenie vsetkych taskov
        [HttpGet]
        public ActionResult AllTasks()
        {
            List<Task> allTasks = null;
            try
            {
                allTasks = Database.Tasks.ToList();
            }
            catch (Exception exception)
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json(allTasks, JsonRequestBehavior.AllowGet);
        }

        // pridanie noveho tasku
        [HttpPost]
        public ActionResult SendTask(String Content)
        {
            Task newTask = null;
            try
            {
                newTask = new Task { Content = Content, Date = DateTime.Now };
                Database.Tasks.Add(newTask);
                Database.SaveChanges();
            }
            catch (Exception exception) 
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json(newTask, JsonRequestBehavior.AllowGet);
        }

        // odstranenie erroru
        [HttpPost]
        public ActionResult DeleteError(int UUID)
        {
            MyError error = null;
            try
            {
                var errors = from e in Database.Errors where e.UUID == UUID select e;
                error = errors.First();
                Database.Errors.Remove(error);
                Database.SaveChanges();
            }
            catch (Exception exception)
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json(error, JsonRequestBehavior.AllowGet);
        }

        // odstranenie erroru
        [HttpPost]
        public ActionResult DeleteAllError()
        {
            try
            {
                var errors = from e in Database.Errors select e;
                foreach (MyError error in errors)
                {
                    Database.Errors.Remove(error);                
                }
                Database.SaveChanges();
            }
            catch (Exception exception)
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        // odstranenie tasku
        [HttpPost]
        public ActionResult DeleteTask(int UUID)
        {
            Task task = null;
            try
            {
                var tasks = from t in Database.Tasks where t.UUID == UUID select t;
                task = tasks.First();
                Database.Tasks.Remove(task);
                Database.SaveChanges();
            }
            catch (Exception exception)
            {
                Database.Errors.Add(new MyError { Description = exception.Message, Date = DateTime.Now });
                Database.SaveChanges();
            }
            return Json(task, JsonRequestBehavior.AllowGet);
        }
    }
}
