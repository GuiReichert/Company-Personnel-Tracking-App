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

namespace Company_Personnel_Tracking_App.Views
{
    /// <summary>
    /// Interaction logic for DepartmentList.xaml
    /// </summary>
    public partial class DepartmentList : UserControl
    {


        public DepartmentList()
        {
            InitializeComponent();
            using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
            {
                List<Department> list = db.Departments.OrderBy(x => x.DepartmentName).ToList();
                gridDepartment.ItemsSource = list;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepPage = new AddDepartmentWindow();
            addDepPage.ShowDialog();
            using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
            {
                List<Department> list = db.Departments.OrderBy(x => x.DepartmentName).ToList();
                gridDepartment.ItemsSource = list;
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Department dpt = (Department)gridDepartment.SelectedItem;                   // pega o departamento selecionado
            AddDepartmentWindow page = new AddDepartmentWindow();
            page.department = dpt;                                                      //atribui o valor desse departamento para a variavel na outra classe (AddDepartmentWindow)
            page.ShowDialog();
            using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
            {
                List<Department> list = db.Departments.OrderBy(x => x.DepartmentName).ToList();
                gridDepartment.ItemsSource = list;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this department?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Department department = (Department)gridDepartment.SelectedItem;
                using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
                {
                    db.Departments.Remove(department);
                    db.SaveChanges();
                }
                MessageBox.Show("Department deleted successfully");


                using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
                {
                    List<Department> list = db.Departments.OrderBy(x => x.DepartmentName).ToList();
                    gridDepartment.ItemsSource = list;
                }

            }
        }
    }
}
