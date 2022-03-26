using EmployeeOneToMany.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeOneToMany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly DataContext dataContext;
        public EmployeeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Address>>> Get()
        {
            var adddress=await dataContext.Address.ToListAsync();
            var employye2=await dataContext.Employee.ToListAsync();
            foreach (Address address1 in adddress)
            {
                address1.Employee = dataContext.Employee.Where(e => e.EmployeeId == address1.Employee.EmployeeId).FirstOrDefault();
            }
            return Ok(await dataContext.Address.ToListAsync());

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Address address)
        {

            dataContext.Address.Add(address);
            dataContext.SaveChanges();
            return Ok(address);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var adddress = await dataContext.Address.FindAsync(id);
            var employye = await dataContext.Employee.FindAsync(id);
            if (adddress==null && employye==null)
            {
                return BadRequest("Employee Not Found");
            }

            dataContext.Employee.Remove(employye);
            dataContext.Address.Remove(adddress);
            dataContext.SaveChanges();
            return Ok("Data Deleted");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,Address address)
        {
            var addr=await dataContext.Address.FindAsync(id);
            var emp = await dataContext.Employee.FindAsync(id);
            if (addr == null && emp==null)
                return BadRequest("Data Not Available");

            addr.City = address.City;
            addr.Pin=address.Pin;

            emp.Name=address.Employee.Name;
            emp.CompanyName=address.Employee.CompanyName;
            emp.EmployeeId=addr.Employee.EmployeeId;

            dataContext.SaveChanges();
            return Ok(addr);

        }
    }
}
