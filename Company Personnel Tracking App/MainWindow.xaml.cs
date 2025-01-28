using System.Text;
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
using Company_Personnel_Tracking_App.ViewModels;
using Company_Personnel_Tracking_App.Views;

namespace Company_Personnel_Tracking_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void PersonnelMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = string.Empty;
            if (!UserStatic.isAdmin)
            {
                btnPermissions.IsEnabled = false;
                btnSalary.IsEnabled = false;
            }
        }

        private void btnDepartment_Click(object sender, RoutedEventArgs e)      //USER CONTROLS SÃO CHAMADOS DESSA FORMA! (precisamos criar um 
        {                                                      // "Window Resources" no front-end, criar um "Data Template" e um "ContentControl"!)
            lblWindowName.Content = "Department List";
            DataContext = new DepartmentViewModel();            //Para abrir "Windows"(janelas), basta criar uma nova intancia dela e usar ".ShowDialog()"!
                                                                //(ex: AddDepartmentWindow newWindow = new AddDepartmentWindow(); newWindow.ShowDialog();
        }                                                       // Show x ShowDialog: Show retorna INSTANTANEAMENTE assim que a janela for aberta, enquanto ShowDialog só retornará QUANDO A JANELA ABERTA FOR FECHADA!!

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Position List";
            DataContext = new PositionViewModel();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Employee List";
            DataContext = new EmployeeViewModel();
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Task List";
            DataContext = new TaskViewModel();                    
        }

        private void btnSalary_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Salary List";
            DataContext = new SalaryViewModel();
        }

        private void btnPermissions_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Permission List";
            DataContext = new PermissionViewModel();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Visibility = Visibility.Visible;
        }
    }
}