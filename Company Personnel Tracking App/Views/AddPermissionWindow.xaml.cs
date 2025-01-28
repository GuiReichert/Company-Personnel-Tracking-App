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

    public partial class AddPermissionWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        public PermissionDetailModel model;
        public AddPermissionWindow()
        {
            InitializeComponent();
        }
        TimeSpan tspermissionday = new TimeSpan();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
            if(model != null && model.Id != 0)
            {
                txtUserNo.Text = model.UserNo.ToString();
                txtExplanation.Text = model.Explanation.ToString();
                txtDayAmount.Text = model.DayAmount.ToString();
                dpStart.SelectedDate = model.StartDate;
                dpEnd.SelectedDate = model.EndDate;
            }
        }

        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpEnd.SelectedDate != null)
            {
                tspermissionday = (TimeSpan)(dpEnd.SelectedDate - dpStart.SelectedDate);
                txtDayAmount.Text = tspermissionday.TotalDays.ToString();
            }
        }

        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStart.SelectedDate != null)
            {
                tspermissionday = (TimeSpan)(dpEnd.SelectedDate - dpStart.SelectedDate);
                txtDayAmount.Text = tspermissionday.TotalDays.ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if(txtDayAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the permission start and end dates");
            }
            else if (int.Parse(txtDayAmount.Text) <= 0)
            {
                MessageBox.Show("Permission day must be higher than zero");
            }
            else if (txtExplanation.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the explanation field");
            }
            else
            {
                if (model != null && model.Id != 0)
                {
                    Permission updtpermission = db.Permissions.Find(model.Id);

                    updtpermission.PermissionStartDate = (DateTime)dpStart.SelectedDate;
                    updtpermission.PermissionEndDate = (DateTime)dpEnd.SelectedDate;
                    updtpermission.PermissionAmount = int.Parse(txtDayAmount.Text);
                    updtpermission.PermissionExplanation = txtExplanation.Text;
                    db.SaveChanges();
                    MessageBox.Show("Permission updated successfully");
                }
                else
                {
                    Permission addPermission = new Permission();
                    addPermission.Id = 0;
                    addPermission.EmployeeId = UserStatic.EmployeeId;
                    addPermission.UserNo = UserStatic.UserNo;
                    addPermission.PermissionExplanation = txtExplanation.Text;
                    addPermission.PermissionStartDate = (DateTime)dpStart.SelectedDate;
                    addPermission.PermissionState = Definitions.PermissionStates.OnEmployee;
                    addPermission.PermissionEndDate = (DateTime)dpEnd.SelectedDate;
                    addPermission.PermissionAmount = int.Parse(txtDayAmount.Text);
                    addPermission.PermissionExplanation = txtExplanation.Text;

                    db.Permissions.Add(addPermission);
                    db.SaveChanges();
                    MessageBox.Show("Permission added successfully");
                }
                dpStart.SelectedDate = DateTime.Today;
                dpEnd.SelectedDate = DateTime.Today;
                txtDayAmount.Clear();
                txtExplanation.Clear();

            }
        }
    }
}
