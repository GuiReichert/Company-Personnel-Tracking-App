using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Company_Personnel_Tracking_App.DataBase;
using Company_Personnel_Tracking_App.Model;
using Microsoft.EntityFrameworkCore;

namespace Company_Personnel_Tracking_App.Views
{

    public partial class EmployeeList : UserControl
    {
        public EmployeeList()
        {
            InitializeComponent();
        }
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<Position> positions = new List<Position>();
        List<EmployeeDetailModel> employeemodel = new List<EmployeeDetailModel>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailModel model = (EmployeeDetailModel)gridEmployee.SelectedItem;
            AddEmployeeWindow window = new AddEmployeeWindow();
            window.updtEmployee = model;
            window.ShowDialog();
            FillDataGrid();

        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedDepId = Convert.ToInt32(cmbDepartment.SelectedValue);
            if (cmbDepartment.SelectedIndex != -1)
            {


                cmbPosition.ItemsSource = db.Positions.Where(x => x.DepartmentId == SelectedDepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        void FillDataGrid()
        {
            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;

            employeemodel = db.Employees.Include(x => x.Position).Include(x => x.Department).Select(x => new EmployeeDetailModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Birthday = (DateTime)x.Birthday,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName,
                PositionId = x.PositionId,
                PositionName = x.Position.PositionName,
                isAdmin = (bool)x.IsAdmin,
                Password = x.Password,
                Salary = x.Salary,
                Surname = x.Surname,
                UserNo = x.UserNo,
                ImagePath = x.ImagePath

            }).ToList();


            gridEmployee.ItemsSource = employeemodel;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)                  // MÉTODO SEM USAR TRIGGERS!
        {
            EmployeeDetailModel employeeDetailModel = (EmployeeDetailModel)gridEmployee.SelectedItem;
            if (employeeDetailModel != null && employeeDetailModel.Id != 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this employee?","Question",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Employee deleteEmployee = db.Employees.Find(employeeDetailModel.Id);
                    List<DataBase.Task> deleteTasks = db.Tasks.Where(x=> x.Id == employeeDetailModel.Id).ToList();
                    List<Permission> deletePermissions = db.Permissions.Where(x=> x.Id == employeeDetailModel.Id).ToList();
                    List<Salary> deleteSalaries = db.Salaries.Where(x=> x.Id != employeeDetailModel.Id).ToList();


                    foreach (DataBase.Task task in deleteTasks)                                                             // Temos que criar uma lista para cada tabela e um loop foreach para retirá-lo do database junto
                    {
                        db.Tasks.Remove(task);
                    }
                    foreach (Permission permission in deletePermissions)
                    {
                        db.Permissions.Remove(permission);
                    }
                    foreach(Salary salary in deleteSalaries)
                    {
                        db.Salaries.Remove(salary);
                    }
                    db.SaveChanges();

                    db.Employees.Remove(deleteEmployee);
                    db.SaveChanges();

                    MessageBox.Show("Employee deleted successfully");
                    FillDataGrid();
                }
            }
        }
    }
}
