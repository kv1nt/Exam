using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EpamTask.LogFiles;
using EpamTask.Models;
using EpamTask.Repositories.Implementation;

namespace EpamTask.Controllers
{
    [LoggingFilter]
    public class ProfessorController : Controller
    {
        readonly SubjectRepository _subjectRepository = new SubjectRepository();
        readonly ProfessorRepository _professorRepository = new ProfessorRepository();

        // GET: Professor

        public ActionResult GetSubjectForProfessor()
        {
            ViewBag.Subjects = _subjectRepository.GetAll();
            return View();
        }

        public ActionResult GetAllProfessors()
        {
            ViewBag.AllProfessors = _professorRepository.GetAll();
            return View(ViewBag.AllProfessors);
        }

        // GET: Professor/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {          
            ViewBag.Subject = _subjectRepository.Get(id);
            return View(ViewBag.Subject);
        }
     
        [HttpPost]
        public ActionResult CreateProfessor()
        {
            string info = Request["ProfessorInfo"];
            int Id = Convert.ToInt32(Request["SubjectId"]);
            _professorRepository.Create(info, Id);
           return RedirectToAction("GetAllProfessors");
        }
    }
}
