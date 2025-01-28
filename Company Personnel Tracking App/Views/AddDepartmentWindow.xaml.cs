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

namespace Company_Personnel_Tracking_App.Views
{
    public partial class AddDepartmentWindow : Window
    {
        public Department department {  get; set; }
        public AddDepartmentWindow()
        {
            InitializeComponent();
        }

        private void txtDepartmentName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtDepartmentName.Text.Trim() == "")
            {
                MessageBox.Show("You must provide a name for the Department");
            }
            else
            {
                if (department != null && department.Id != 0)                                   // Código para ALTERAR um departamento
                {
                    using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
                    {
                        Department update = new Department();
                        update.DepartmentName = txtDepartmentName.Text;
                        update.Id = department.Id;
                        db.Departments.Update(update);
                        db.SaveChanges();
                        MessageBox.Show("Department updated successfully");
                    }
                }
                else                                                                             //Código para CRIAR um novo departamento
                {

                    using (CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext())
                    {
                        Department dpt = new Department();
                        dpt.DepartmentName = txtDepartmentName.Text;
                        db.Departments.Add(dpt);
                        db.SaveChanges();
                        txtDepartmentName.Clear();
                        MessageBox.Show("Department added succesfully");
                    }
                }

            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (department != null && department.Id != 0)
            {
                txtDepartmentName.Text = department.DepartmentName;
            }
        }
    }
}
