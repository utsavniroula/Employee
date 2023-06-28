using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext DbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<EmployeeInformation> employeeList = DbContext.employeeInformation;
            return View(employeeList);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EmployeeInformation obj)
        {
            if(ModelState.IsValid)
            {
                DbContext.employeeInformation.Add(obj);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await DbContext.employeeInformation.FirstOrDefaultAsync(x => x. Id == id);
            if(details != null)
            {
                return View(details);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Details")]
        public async Task<IActionResult> Details(EmployeeInformation model)
        {
            if (ModelState.IsValid)
            {
                var updateInfo = await DbContext.employeeInformation.FindAsync(model.Id);
                if (updateInfo != null)
                {
                    updateInfo.Name = model.Name;
                    updateInfo.Address = model.Address;
                    updateInfo.Salary = model.Salary;

                    await DbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Details");
                }
            }
            else
            {
                return RedirectToAction("Details",model.Id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeInformation obj)
        {
            var deleteId = await DbContext.employeeInformation.FindAsync(obj.Id);
            if (deleteId != null)
            {
                DbContext.employeeInformation.Remove(deleteId);
                await DbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }else 
            {
                return RedirectToAction("Index");
            }
        }
    }
}
