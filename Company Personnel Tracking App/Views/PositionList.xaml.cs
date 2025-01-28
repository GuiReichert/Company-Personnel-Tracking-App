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
    public partial class PositionList : UserControl
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        public PositionList()
        {
            InitializeComponent();

            FillGrid();
        }
        void FillGrid()                         // Função para MOSTRAR os valores da tabela
        {

            var positionlist = db.Positions.Include(x => x.Department).Select(x => new
            {
                PositionId = x.Id,
                PositionName = x.PositionName,
                DepartmentName = x.Department.DepartmentName,
                DepartmentId = x.DepartmentId
            }).OrderBy(x => x.PositionName).ToList();

            List<PositionModel> modellist = new List<PositionModel>();
            foreach (var position in positionlist)
            {
                PositionModel model = new PositionModel();
                model.Id = position.PositionId;
                model.PositionName = position.PositionName;
                model.DepartmentName = position.DepartmentName;
                model.DepartmentId = position.DepartmentId;

                modellist.Add(model);
            }


            gridPosition.ItemsSource = modellist;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPositionWindow addPositionWindow = new AddPositionWindow();
            addPositionWindow.ShowDialog();
            FillGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PositionModel currentPosition = (PositionModel)gridPosition.SelectedItem;



            if (currentPosition != null && currentPosition.Id != 0)
            {
                AddPositionWindow updatePositionWindow = new AddPositionWindow();
                updatePositionWindow.model = currentPosition;
                updatePositionWindow.ShowDialog();
                FillGrid();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)      //MÉTODO USANDO TRIGGERS (Não precisamos fazer nada no código, apenas criá-la no SSMS)
        {
            PositionModel positionModel = (PositionModel)gridPosition.SelectedItem;
            if (positionModel != null && positionModel.Id != 0)
            {
                Position deletePosition = db.Positions.Find(positionModel.Id);
                db.Positions.Remove(deletePosition);
                db.SaveChanges();
                MessageBox.Show("Position deleted successfully");
                FillGrid();
            }

        }
    }
}
