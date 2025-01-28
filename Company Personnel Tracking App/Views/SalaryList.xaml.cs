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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Company_Personnel_Tracking_App.DataBase;
using Company_Personnel_Tracking_App.Model;
using Microsoft.EntityFrameworkCore;

namespace Company_Personnel_Tracking_App.Views
{

    public partial class SalaryList : UserControl
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<SalaryDetailModel> salaries = new List<SalaryDetailModel>();
        List<Position> positions = new List<Position>();
        public SalaryList()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
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

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSalaryWindow addsalarywindow = new AddSalaryWindow();
            addsalarywindow.ShowDialog();
            FillDataGrid();
        }



        void FillDataGrid()
        {
            salaries = db.Salaries.Include(x => x.Employee).Include(x => x.Month).Select(x => new SalaryDetailModel()
            {
                Name = x.Employee.Name,
                Surname = x.Employee.Surname,
                EmployeeId = x.EmployeeId,
                Id = x.Id,
                UserNo = x.Employee.UserNo,
                Amount = x.Amount,
                MonthName = x.Month.MonthName,
                Year = x.Year,
                MonthId = x.MonthId,
                DepartmentId = x.Employee.DepartmentId,
                PositionId = x.Employee.PositionId,
            }).OrderByDescending(x=> x.Year).OrderByDescending(x=> x.MonthId).ToList();

            gridSalary.ItemsSource = salaries;
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int DepId = (int)cmbDepartment.SelectedValue;



            if (cmbDepartment.SelectedIndex != -1)
            {

                cmbPosition.ItemsSource = db.Positions.Where(x => x.DepartmentId == DepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<SalaryDetailModel> search = salaries;
            if (txtUserNo.Text.Trim() != "")
                search = search.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                search = search.Where(x => x.Name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                search = search.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                search = search.Where(x => x.DepartmentId == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                search = search.Where(x => x.PositionId == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (txtYear.Text.Trim() != "")
                search = search.Where(x => x.Year == Convert.ToInt32(txtYear.Text)).ToList();
            if (cmbMonth.SelectedIndex != -1)
                search = search.Where(x => x.MonthId == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            if (txtSalary.Text.Trim() != "")
            {
                if (rbMore.IsChecked == true)
                    search = search.Where(x => x.Amount > Convert.ToInt32(txtSalary.Text)).ToList();
                else if (rbLess.IsChecked == true)
                    search = search.Where(x => x.Amount < Convert.ToInt32(txtSalary.Text)).ToList();
                else
                    search = search.Where(x => x.Amount == Convert.ToInt32(txtSalary.Text)).ToList();

            }
            gridSalary.ItemsSource = search;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUserNo.Clear();
            txtSalary.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtYear.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            rbEquals.IsChecked = false;
            rbLess.IsChecked = false;
            rbMore.IsChecked = false;

            gridSalary.ItemsSource = salaries;
        }
        SalaryDetailModel model = new SalaryDetailModel();
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AddSalaryWindow updtSalaryWindow = new AddSalaryWindow();
            updtSalaryWindow.updtmodel = model;
            updtSalaryWindow.ShowDialog();
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this salary", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (model.Id!= 0)
                {

                    Salary dbsalary = db.Salaries.Find(model.Id);
                    db.Salaries.Remove(dbsalary);
                    db.SaveChanges();
                    MessageBox.Show("Salary deleted successfully");
                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("You must choose a salary to delete");
                }
            }
            else
            {
                
            }
        }

        private void gridSalary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (SalaryDetailModel)gridSalary.SelectedItem;
        }
    }
}
