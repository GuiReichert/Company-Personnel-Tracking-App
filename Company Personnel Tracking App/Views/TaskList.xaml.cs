using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<TaskDetailModel> taskmodel = new List<TaskDetailModel>();
        List<TaskDetailModel> searchlist = new List<TaskDetailModel>();
        List<Position> positions = new List<Position>();
        TaskDetailModel SelectedTask;
        
        public TaskList()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();


        }

        void FillDataGrid()
        {
            taskmodel = db.Tasks.Include(x => x.TaskStateNavigation).Include(x => x.Employee)
                .ThenInclude(x => x.Department).ThenInclude(x => x.Positions).Select(x => new TaskDetailModel()
                {

                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    Name = x.Employee.Name,
                    StateName = x.TaskStateNavigation.StateName,
                    Surname = x.Employee.Surname,
                    TaskContent = x.TaskContent,
                    TaskDeliveryDate = x.TaskDeliveryDate,
                    TaskStartDate = (DateTime)x.TaskStartDate,
                    TaskState = (int)x.TaskState,
                    TaskTitle = x.TaskTitle,
                    UserNo = x.Employee.UserNo,
                    DepartmentId = x.Employee.DepartmentId,
                    PositionId = x.Employee.PositionId
                }).ToList();

            gridTask.ItemsSource = taskmodel;
            searchlist = taskmodel;


            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;

            List<TaskState> taskStates = new List<TaskState>();
            taskStates=db.TaskStates.ToList();
            cmbState.ItemsSource = taskStates;
            cmbState.DisplayMemberPath = "NameState";
            cmbState.SelectedValuePath = "Id";
            cmbState.SelectedIndex = -1;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow newpage = new AddTaskWindow();
            newpage.ShowDialog();
            FillDataGrid();
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int DepId = (int)cmbDepartment.SelectedValue;
            if (cmbDepartment.SelectedIndex != -1)
            {
                cmbPosition.ItemsSource = positions.Where(x=> x.DepartmentId == DepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<TaskDetailModel> search = searchlist;
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
            if (cmbState.SelectedIndex != -1)
                search = search.Where(x => x.TaskState == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (rbStart.IsChecked == true)
                search = search.Where(x => x.TaskStartDate > dpStart.SelectedDate && x.TaskStartDate < dpDelivery.SelectedDate).ToList();
            if (rbDelivery.IsChecked == true)
                search = search.Where(x => x.TaskDeliveryDate > dpStart.SelectedDate && x.TaskDeliveryDate < dpDelivery.SelectedDate).ToList();
            gridTask.ItemsSource = search;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            dpDelivery.SelectedDate = DateTime.Today;
            dpStart.SelectedDate = DateTime.Today;
            cmbDepartment.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            rbDelivery.IsChecked = false;
            rbStart.IsChecked = false;
            gridTask.ItemsSource = taskmodel;
        }
        private void gridTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTask = (TaskDetailModel)gridTask.SelectedItem;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow updtpage = new AddTaskWindow();
            updtpage.model = SelectedTask;
            updtpage.ShowDialog();
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask.Id == 0)
            {
                MessageBox.Show("You must select a task to delete");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this task?","Question", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataBase.Task tasktodelete = db.Tasks.Find(SelectedTask.Id);
                    db.Tasks.Remove(tasktodelete);
                    db.SaveChanges();
                    MessageBox.Show("Task deleted successfully");
                    FillDataGrid();
                }

                
            }
        }
    }
}
