using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Company_Personnel_Tracking_App.DataBase;
using Company_Personnel_Tracking_App.Model;
using Company_Personnel_Tracking_App.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Company_Personnel_Tracking_App.Views
{
    public partial class AddTaskWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<Employee> employeelist = new List<Employee>();
        List<Position> positions = new List<Position>();
        public TaskDetailModel model = new TaskDetailModel();
        public AddTaskWindow()
        {
            InitializeComponent();
            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;


        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            employeelist = db.Employees.OrderBy(x => x.Name).ToList();
            gridEmployee.ItemsSource = employeelist;

            if (model !=  null && model.Id != 0)
            {
                txtName.Text = model.Name;
                txtSurname.Text = model.Surname;
                txtUserNo.Text = model.UserNo.ToString();
                txtTitle.Text = model.TaskTitle;
                txtContent.Text = model.TaskContent;
            }


        }


        int EmployeeId;
        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = (Employee)gridEmployee.SelectedItem;
            txtUserNo.Text = employee.UserNo.ToString();
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surname;
            EmployeeId = employee.Id;


        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int DepId = (int)cmbDepartment.SelectedValue;

            if (cmbDepartment.SelectedIndex!= -1)
            {
                cmbPosition.ItemsSource = positions.Where(x => x.DepartmentId == DepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeId == 0)
            {
                MessageBox.Show("You must select an employee from the table");
            }
            else if (txtTitle.Text.Trim() =="" || txtContent.Text.Trim() =="")
            {
                MessageBox.Show("Please fill the necessary fields");
            }
            else
            {
                if (model != null && model.Id != 0)
                {
                    DataBase.Task updtTask = db.Tasks.Find(model.Id);
                    updtTask.EmployeeId = EmployeeId;
                    updtTask.TaskTitle = txtTitle.Text;
                    updtTask.TaskContent = txtContent.Text;

                    db.SaveChanges();
                    MessageBox.Show("Task updated successfully");
                }
                else
                {



                    DataBase.Task addtask = new DataBase.Task();

                    addtask.EmployeeId = EmployeeId;
                    addtask.TaskStartDate = DateTime.Now;
                    addtask.TaskTitle = txtTitle.Text;
                    addtask.TaskContent = txtContent.Text;
                    addtask.TaskState = Definitions.TaskStates.OnEmployee;

                    db.Tasks.Add(addtask);
                    db.SaveChanges();

                    MessageBox.Show("Task added successfully");

                    EmployeeId = 0;
                    txtContent.Clear();
                    txtTitle.Clear();
                    txtUserNo.Clear();
                    txtName.Clear();
                    txtSurname.Clear();
                    cmbDepartment.SelectedIndex = -1;
                    cmbPosition.SelectedIndex = -1;

                }
            }

        }
    }
}
