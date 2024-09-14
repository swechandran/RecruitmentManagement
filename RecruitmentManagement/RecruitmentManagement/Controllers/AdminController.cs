using RecruitmentManagement.Models;
using RecruitmentManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RecruitmentManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateAdmin()
        {
            return View();
        }
        private readonly AdminRepository _userRepository;

        public AdminController()
        {
            _userRepository = new AdminRepository();
        }
        // GET: Admin/Users
        public ActionResult Users()
        {
            var users = _userRepository.GetAllUsers();
            return View(users);
        }
        // GET: Admin/Edit
        public ActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            return View(user);
        }
        // POST: Admin/Edit
        [HttpPost]
        public ActionResult Edit(Registeration model)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(model);
                return RedirectToAction("Users");
            }
            return View(model);
        }
        // GET: Admin/Delete
        public ActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            return View(user);
        }
        // POST: Admin/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Users");
        }

        // GET: Enquiry Form Page
        public ActionResult Enquire()
        {
            return View("EnquiryView");
        }
        
        // POST: Enquiry/Submit
        [HttpPost]
        public ActionResult Submit(string name, string email, string message)
        {
            _userRepository.InsertEnquiry(name, email, message);
            return RedirectToAction("Success");
        }

        public ActionResult AdminView()
        {
            List<Enquiry> enquiries = _userRepository.GetAllEnquiries();
            return View(enquiries);
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account"); 
        }
    }
}