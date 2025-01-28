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
    /// <summary>
    /// Interaction logic for AddPositionWindow.xaml
    /// </summary>
    public partial class AddPositionWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();

        public PositionModel model = new PositionModel();
        public AddPositionWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = db.Departments.ToList().OrderBy(x => x.DepartmentName);
            cmbDepartment.ItemsSource = list;
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            if (model != null &&  model.Id != 0)
            {
                cmbDepartment.SelectedValue = model.DepartmentId;
                txtPosition.Text = model.PositionName;
            }


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDepartment.SelectedIndex == -1 || txtPosition.Text.Trim() == "")
            {
                MessageBox.Show("You must provide a Department and a Position");
            }
            else
            {
                if (model != null && model.Id != 0)
                {
                    UpdatePosition();
                }
                else
                {
                    AddPosition();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        void AddPosition()
        {
            Position newposition = new Position();
            newposition.PositionName = txtPosition.Text;
            newposition.DepartmentId = (int)cmbDepartment.SelectedValue;
            db.Positions.Add(newposition);
            db.SaveChanges();

            cmbDepartment.SelectedIndex = -1;
            txtPosition.Clear();
            MessageBox.Show("Position added successfully");
        }

        void UpdatePosition()
        {
            Position updtPosition = new Position();
            updtPosition.PositionName = txtPosition.Text;
            updtPosition.DepartmentId = (int)cmbDepartment.SelectedValue;
            updtPosition.Id = model.Id;

            db.Positions.Update(updtPosition);                                      //Ele sabe qual posição atualizar ATRAVÉS DO ID que é IGUAL! (linha de cima)
            db.SaveChanges();

            MessageBox.Show("Position updated successfully");
        }

    }
}
