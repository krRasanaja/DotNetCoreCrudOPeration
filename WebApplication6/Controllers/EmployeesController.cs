using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }
        
        public  IActionResult Index()
        {

            List<Departments> depList = _context.departments.ToList();
            ViewBag.ListOfDepartments = new SelectList(depList, "DepartmentID", "DepartmentName");
            return View(_context.Employees.ToList());
        }

        public JsonResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var emp = JsonConvert.SerializeObject(new Employees());
                return Json(emp);
            }
            else
            {
                var emp = JsonConvert.SerializeObject(_context.Employees.Find(id));
                return Json(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Employees employees)
        {
            if(ModelState.IsValid)
            {
                if (employees.EmployeeId == 0)
                    _context.Add(employees);
                else
                    _context.Update(employees);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
