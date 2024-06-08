using GCTL_Assignment.Models;
using GCTL_Assignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCTL_Assignment.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext db;
        private readonly IWebHostEnvironment env;
        public EmployeesController(EmployeeDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            var data = await db.Employees.ToListAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            var model = new EmployeeInputModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeInputModel model, string act = "")
        {
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee
                    {
                        EmployeeName = model.EmployeeName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Gender = model.Gender,
                        JoinDate = model.JoinDate,
                        Salary = model.Salary,
                        JobTitle = model.JobTitle,
                        OnAvailable = model.OnAvailable,
                    };
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fn = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string sp = Path.Combine(env.WebRootPath, "Pictures", fn);
                    FileStream fs = new FileStream(sp, FileMode.Create);
                    await model.Picture.CopyToAsync(fs);
                    fs.Close();
                    employee.Picture = fn;
                    await db.Employees.AddAsync(employee);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var a = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (a == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditModel
            {
                EmployeeId = a.EmployeeId,
                EmployeeName = a.EmployeeName,
                Email = a.Email,
                Phone = a.Phone,
                Gender = a.Gender,
                JoinDate = a.JoinDate,
                Salary = a.Salary,
                JobTitle = a.JobTitle,
                OnAvailable = a.OnAvailable,
            };
            ViewBag.CurrentPic = a.Picture;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditModel model, string act = "")
        {
            var a = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);
            if (a == null)
            {
                return NotFound();
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    a.EmployeeName = model.EmployeeName;
                    a.Email = model.Email;
                    a.Phone = model.Phone;
                    a.Gender = model.Gender;
                    a.JoinDate = model.JoinDate;
                    a.Salary = model.Salary;
                    a.JobTitle = model.JobTitle;
                    a.OnAvailable = model.OnAvailable;
                    if (model.Picture != null)
                    {
                        string ext = Path.GetExtension(model.Picture.FileName);
                        string fn = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string sp = Path.Combine(env.WebRootPath, "Pictures", fn);
                        FileStream fs = new FileStream(sp, FileMode.Create);
                        await model.Picture.CopyToAsync(fs);
                        fs.Close();
                        a.Picture = fn;
                    }
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = a.Picture;
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var a = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            ViewBag.CurrentPic = a.Picture;
            return View(a);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int id)
        {
            var a = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (a == null)
            {
                return NotFound();
            }
            db.Employees.Remove(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



    }
}
