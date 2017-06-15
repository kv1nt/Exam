using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EpamTask.LogFiles;
using EpamTask.Models;
using EpamTask.Repositories.Implementation;
using NLog;

namespace EpamTask.Controllers
{
    [LoggingFilter]
    public class StudentController : Controller
    {
        readonly StudentRepository _studentRepository = new StudentRepository();
        readonly ExamRepository _examRepository = new ExamRepository();
        readonly AlIExamInfoRepository _allExamInfoRepository = new AlIExamInfoRepository();

        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Studentsss = _studentRepository.GetAll();         
            return View(ViewBag.Students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Det = _studentRepository.Get(id);
            return View(ViewBag.Det);
        }

        public ActionResult GetAllInfoExam(int id)
        {
            ViewBag.ExamInfo = _allExamInfoRepository.Get(id);
            return View(ViewBag.ExamInfo);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(AllModels _allModels) 
        {
            try
            {
                _allExamInfoRepository.CreateExam(_allModels);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
               string grade = Request.Form["Grade"];
                _examRepository.Update(id, grade);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _allExamInfoRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
