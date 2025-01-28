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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Company_Personnel_Tracking_App.Views
{
    public partial class PermissionList : UserControl
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<PermissionDetailModel> permissionmodels = new List<PermissionDetailModel>();
        List<Position> positions = new List<Position>();
        PermissionDetailModel model;

        public PermissionList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPermissionWindow addPermissionWindow = new AddPermissionWindow();
            addPermissionWindow.ShowDialog();
            FillDataGrid();
        }

        void FillDataGrid()
        {
            permissionmodels = db.Permissions.Include(x => x.Employee).Include(x=> x.PermissionStateNavigation).Select(x => new PermissionDetailModel()
            {
                DayAmount = x.PermissionAmount,
                EmployeeId = x.EmployeeId,
                DepartmentId = x.Employee.DepartmentId,
                EndDate = x.PermissionEndDate,
                Explanation = x.PermissionExplanation,
                Id = x.Id,
                Name = x.Employee.Name,
                Surname = x.Employee.Surname,
                PermissionState = x.PermissionState,
                PositionId = x.Employee.PositionId,
                StartDate = x.PermissionStartDate,
                StateName = x.PermissionStateNavigation.StateName,
                UserNo = x.UserNo
            }).OrderByDescending(x=> x.StartDate).ToList();
            gridPermission.ItemsSource = permissionmodels;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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

            List<PermissionState> permissionStates = db.PermissionStates.ToList();
            cmbState.ItemsSource = permissionStates;
            cmbState.DisplayMemberPath = "PermissionState";
            cmbState.SelectedValuePath = "Id";
            cmbState.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<PermissionDetailModel> search = permissionmodels;
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
            if (rbStart.IsChecked == true)
                search = search.Where(x => x.StartDate > dpStart.SelectedDate && x.StartDate < dpEnd.SelectedDate).ToList();
            if (rbEndDate.IsChecked == true)
                search = search.Where(x => x.EndDate > dpStart.SelectedDate && x.EndDate < dpEnd.SelectedDate).ToList();

            if (cmbState.SelectedIndex != -1)
                search = search.Where(x => x.PermissionState == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (txtDayAmount.Text.Trim() != "")
                search = search.Where(x => x.DayAmount == Convert.ToInt32(txtDayAmount.Text)).ToList();
            gridPermission.ItemsSource = search;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDayAmount.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtUserNo.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            dpEnd.SelectedDate = DateTime.Today;
            dpStart.SelectedDate = DateTime.Today;
            rbEndDate.IsChecked = false;
            rbStart.IsChecked = false;
            gridPermission.ItemsSource = permissionmodels;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (gridPermission.SelectedItem == null)
            {
                MessageBox.Show("Please select a permission to update");
            }
            else
            {
                AddPermissionWindow updwindow = new AddPermissionWindow();
                updwindow.model = (PermissionDetailModel)gridPermission.SelectedItem;
                updwindow.ShowDialog();
                FillDataGrid();
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (model != null && model.Id != 00 && model.PermissionState == Definitions.PermissionStates.OnEmployee)
            {
                Permission permissiontoapprove = db.Permissions.Find(model.Id);
                permissiontoapprove.PermissionState = Definitions.PermissionStates.Approved;
                db.SaveChanges();
                MessageBox.Show("Permission approved successfully");
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Please select a valid permission to approve");
            }
        }

        private void gridPermission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (PermissionDetailModel)gridPermission.SelectedItem;
        }

        private void btnDisapprove_Click(object sender, RoutedEventArgs e)
        {
            if (model != null && model.Id != 00 && model.PermissionState == Definitions.PermissionStates.OnEmployee)
            {
                Permission permissiontodisapprove = db.Permissions.Find(model.Id);
                permissiontodisapprove.PermissionState = Definitions.PermissionStates.Disapproved;
                db.SaveChanges();
                MessageBox.Show("Permission disapproved successfully");
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Please select a valid permission to disapprove");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (model != null && model.Id != 0)
            {
                if(MessageBox.Show("Are you sure you want to delete this permission?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Permission permissiontodelete = db.Permissions.Find(model.Id);
                    db.Permissions.Remove(permissiontodelete);
                    db.SaveChanges();
                    MessageBox.Show("Permission deleted successfully");
                    FillDataGrid();
                }

            }
            else
            {
                MessageBox.Show("Plase select a permission to delete");
            }
        }
    }
}
