using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Company_Personnel_Tracking_App.Model
{
    public class EmployeeDetailModel
    {
        public int Id { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public string PositionName {  get; set; }
        public string DepartmentName { get; set; }
        public int Salary {  get; set; }
        public bool isAdmin {  get; set; }
        public DateTime Birthday { get; set; }
        public string Address {  get; set; }
        public string Password {  get; set; }

        public string? ImagePath {  get; set; }


    }
}
