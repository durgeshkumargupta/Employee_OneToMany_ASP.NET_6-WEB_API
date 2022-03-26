using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOneToMany
{
    public class Address
    {

        public int Id { get; set; }
        public string City { get; set; }    
        public string Pin { get; set; } 

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
