using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Company_Personnel_Tracking_App.DataBase;
using Company_Personnel_Tracking_App.Model;

namespace Company_Personnel_Tracking_App.Views
{

    public partial class AddSalaryWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<Position> positions = new List<Position>();
        List<Employee> employeelist = new List<Employee>();
        public SalaryDetailModel updtmodel = new SalaryDetailModel();
        public AddSalaryWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Employee> employeelist = db.Employees.ToList();
            gridEmployee.ItemsSource = employeelist;

            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;

            List<Month> months = db.Months.ToList();
            cmbMonth.ItemsSource = months;
            cmbMonth.DisplayMemberPath = "MonthName";
            cmbMonth.SelectedValuePath = "Id";
            cmbMonth.SelectedIndex = -1;

            if(updtmodel!=null && updtmodel.Id != 0)
            {
                txtName.Text = updtmodel.Name;
                txtSurname.Text = updtmodel.Surname;
                txtSalary.Text = updtmodel.Amount.ToString();
                txtYear.Text = updtmodel.Year.ToString();
                EmployeeId = updtmodel.EmployeeId;
                cmbMonth.SelectedValue = updtmodel.MonthId;
                txtUserNo.Text = updtmodel.UserNo.ToString();
            }


        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        int EmployeeId;
        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = (Employee)gridEmployee.SelectedItem;

            txtUserNo.Text = employee.UserNo.ToString();
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surname;
            txtYear.Text = DateTime.Now.Year.ToString();
            txtSalary.Text = employee.Salary.ToString();
            EmployeeId = employee.Id;




        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int DepId = (int)cmbDepartment.SelectedValue;



            if (cmbDepartment.SelectedIndex != -1)
            {
                gridEmployee.ItemsSource = employeelist.Where(x => x.DepartmentId == DepId);

                cmbPosition.ItemsSource = db.Positions.Where(x => x.DepartmentId == DepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void cmbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridEmployee.ItemsSource = employeelist.Where(x => x.PositionId == (int)cmbPosition.SelectedValue).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtSalary.Text.Trim()=="" || txtYear.Text.Trim()==""||cmbMonth.SelectedIndex == -1 || txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("You must fill all the necessary fields");
            }
            else
            {

                if (updtmodel != null && updtmodel.Id != 0)
                {
                    Salary salary = db.Salaries.Find(updtmodel.Id);

                    int oldsalary = salary.Amount;

                    salary.Amount = int.Parse(txtSalary.Text);
                    salary.EmployeeId = EmployeeId;
                    salary.MonthId = (int)cmbMonth.SelectedValue;
                    salary.Year = int.Parse(txtYear.Text);
                    db.SaveChanges();
                    if (oldsalary < salary.Amount)
                    {
                        Employee employee = db.Employees.Find(EmployeeId);
                        employee.Salary = salary.Amount;
                        db.SaveChanges();
                    }

                    MessageBox.Show("Salary updated successfully");

                }
                else
                {
                    Salary NewSalary = new Salary();

                    NewSalary.EmployeeId = EmployeeId;
                    NewSalary.Amount = int.Parse(txtSalary.Text);
                    NewSalary.MonthId = (int)cmbMonth.SelectedValue;
                    NewSalary.Year = int.Parse(txtYear.Text);

                    db.Salaries.Add(NewSalary);
                    db.SaveChanges();

                    MessageBox.Show("Salary added successfully");
                   
                    txtName.Clear();
                    txtSurname.Clear();
                    txtUserNo.Clear();
                    txtSalary.Clear();
                    txtYear.Clear();
                    cmbDepartment.SelectedIndex = -1;
                    cmbMonth.SelectedIndex = -1;
                    cmbPosition.SelectedIndex = -1;
                }
            }
            
            
        }
    }
}
