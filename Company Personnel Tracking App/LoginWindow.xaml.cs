using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    
    public partial class LoginWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(chckGuest.IsChecked == true)
            {
                this.Visibility = Visibility.Collapsed;
                MainWindow main = new MainWindow();

                Employee admin = db.Employees.FirstOrDefault(x => x.UserNo == 1)!;
                UserStatic.EmployeeId = admin.Id;
                UserStatic.UserNo = admin.UserNo;
                UserStatic.Name = admin.Name;
                UserStatic.Surname = admin.Surname;
                UserStatic.isAdmin = (bool)admin.IsAdmin;
                main.ShowDialog();
            }
            else if(txtUserNo.Text.Trim() =="" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please fill both fields");
            }
            else
            {
                Employee employee = db.Employees.FirstOrDefault(x => x.UserNo == int.Parse(txtUserNo.Text) && x.Password.Equals(txtPassword.Text));
                if(employee != null && employee.Id != 0)
                {
                    this.Visibility = Visibility.Collapsed;
                    MainWindow main = new MainWindow();
                    UserStatic.EmployeeId = employee.Id;
                    UserStatic.UserNo = employee.UserNo;
                    UserStatic.Name = employee.Name;
                    UserStatic.Surname = employee.Surname;
                    UserStatic.isAdmin = (bool)employee.IsAdmin;
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("UserNo or Password is incorrect");
                }
            }

        }

        private void txtUserNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void chckGuest_Checked(object sender, RoutedEventArgs e)
        {
            txtUserNo.IsEnabled = false;
            txtPassword.IsEnabled = false;

        }

        private void chckGuest_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserNo.IsEnabled = true;
            txtPassword.IsEnabled = true;
        }
    }
}
