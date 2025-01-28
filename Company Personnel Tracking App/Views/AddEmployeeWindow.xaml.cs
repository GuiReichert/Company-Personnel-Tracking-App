using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Win32;

namespace Company_Personnel_Tracking_App.Views
{
    public partial class AddEmployeeWindow : Window
    {
        CompanyPersonnelTrackingContext db = new CompanyPersonnelTrackingContext();
        List<Position> positions = new List<Position>();
        public EmployeeDetailModel updtEmployee = new EmployeeDetailModel();

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();

            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;


            if (updtEmployee != null && updtEmployee.Id != 0)
            {
                cmbDepartment.SelectedValue = updtEmployee.DepartmentId;
                cmbPosition.SelectedValue = updtEmployee.PositionId;
                txtUserNo.Text = updtEmployee.UserNo.ToString();
                txtPassword.Text = updtEmployee.Password;
                txtName.Text = updtEmployee.Name;
                txtSurname.Text = updtEmployee.Surname;
                txtSalary.Text = updtEmployee.Salary.ToString();
                txtAdress.AppendText(updtEmployee.Address);
                picker1.SelectedDate = updtEmployee.Birthday;
                chisAdmin.IsChecked = updtEmployee.isAdmin;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(@"Images/" + updtEmployee.ImagePath,UriKind.RelativeOrAbsolute);
                image.EndInit();
                EmployeeImage.Source = image;
            }  


        }
        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)     //Faz com que sempre que SÓ POSSAMOS ESCOLHER POSIÇÕES QUE ESTEJAM DENTRO DAQUELE DEPARTAMENTO!
        {
            
            if (cmbDepartment.SelectedIndex != -1)
            {
                int SelectedDepId = (int)cmbDepartment.SelectedValue;
                cmbPosition.ItemsSource = positions.Where(x => x.DepartmentId == SelectedDepId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }

        }


        OpenFileDialog dialog = new OpenFileDialog();                       // Classe que faz o usuario selecionar um ARQUIVO
        private void btnChoose_Click(object sender, RoutedEventArgs e)      //adicionar uma imagem ao funcionario
        {
            if (dialog.ShowDialog() == true)            //dialog.ShowDialog(): abre o explorador de arquivos para o usuario selecionar algo. CASO ELE SELECIONE, retorna TRUE, se não, retorna FALSE!
            {
                BitmapImage img = new BitmapImage();            // BitmapImage: representa uma imagem no formato de bitmap. Usado para carregar e exibir uma imagem em controles de interface do usuário
                img.BeginInit();                                //inicia o processamento do bitmap (UriSource só pode ser usado enquanto o bitmap estiver inicializado!)
                img.UriSource = new Uri(dialog.FileName);       // atribui o URIsource (referencia da imagem) a uma nova instancia de URI, usando o caminho do arquivo pego pelo "OpenFileDialog". URI: identifica a localização de um recurso / arquivo / site (parecido com URL)
                img.EndInit();                                  //finaliza o processamento do bitmap


                EmployeeImage.Source = img;
                txtImage.Text = dialog.FileName;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserNo.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtName.Text.Trim() == "" ||             //retira os campos vazios
                txtSurname.Text.Trim() == "" || txtSalary.Text.Trim() == "" || cmbDepartment.SelectedIndex == -1
                || cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("You must fill all the necessary areas");
            }
            else
            {

                if (updtEmployee != null && updtEmployee.Id != 0)                      //para ATUALIZAR uma pessoa
                {
                    Employee NewInfoemployee = db.Employees.Find(updtEmployee.Id);      //outra forma de atualizar (pegando diretamente o objeto e alterando cada uma das informações dele, para depois usar um "SaveChanges"

                    List<Employee> list = db.Employees.Where(x=> x.UserNo == int.Parse(txtUserNo.Text) && x.Id != NewInfoemployee.Id).ToList();

                    if (list.Count > 0)
                    {
                        MessageBox.Show("This UserNo is unavailable");
                    }
                    else
                    {



                        if (txtImage.Text.Trim() != "")
                        {
                            File.Delete(@"Images/" + NewInfoemployee.ImagePath);
                            string filename = "";
                            string Unique = Guid.NewGuid().ToString();
                            filename += Unique + System.IO.Path.GetFileName(txtImage.Text);
                            File.Copy(txtImage.Text, @"Images/" + filename);
                            NewInfoemployee.ImagePath = filename;
                        }
                        NewInfoemployee.Name = txtName.Text;
                        NewInfoemployee.UserNo = int.Parse(txtUserNo.Text);
                        NewInfoemployee.Password = txtPassword.Text;
                        NewInfoemployee.Surname = txtSurname.Text;
                        NewInfoemployee.Salary = int.Parse(txtSalary.Text);
                        NewInfoemployee.DepartmentId = (int)cmbDepartment.SelectedValue;
                        NewInfoemployee.PositionId = (int)cmbPosition.SelectedValue;

                        TextRange text = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd);
                        NewInfoemployee.Address = text.Text;

                        NewInfoemployee.Birthday = picker1.SelectedDate;
                        NewInfoemployee.IsAdmin = chisAdmin.IsChecked;

                        db.SaveChanges();
                        MessageBox.Show("Employee updated successfully");
                    }
                }
                else                                                            //Para ADICIONAR uma pessoa nova
                {

                    bool isUnique;
                    var Uniquelist = db.Employees.Where(x => x.UserNo == int.Parse(txtUserNo.Text)).ToList();

                    if (Uniquelist.Count > 0)
                    {
                        MessageBox.Show("This UserNo is unavailable");
                    }
                    else
                    {


                        Employee NewEmployee = new Employee();
                        NewEmployee.Name = txtName.Text;
                        NewEmployee.UserNo = int.Parse(txtUserNo.Text);
                        NewEmployee.Password = txtPassword.Text;
                        NewEmployee.Surname = txtSurname.Text;
                        NewEmployee.Salary = int.Parse(txtSalary.Text);
                        NewEmployee.DepartmentId = (int)cmbDepartment.SelectedValue;
                        NewEmployee.PositionId = (int)cmbPosition.SelectedValue;

                        TextRange text = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd);      //txtAdress é uma "Richtextbox" (controle no WPF que permite editar e exibir texto formatado (com estilos, cores, etc.)).
                        NewEmployee.Address = text.Text;                                                                    //por isso precisamos extrair EXPLICITAMENTE o conteudo do texto usando esse "TextRange"!

                        NewEmployee.Birthday = picker1.SelectedDate;
                        NewEmployee.IsAdmin = chisAdmin.IsChecked;


                        if (txtImage.Text.Trim() != "")
                        {


                            string filename = "";
                            string Unique = Guid.NewGuid().ToString();          //Guid: Gera um identificador UNICO GLOBAL (nesse caso, serve para que não haja conflito com o nome dos arquivos salvos caso eles possuam o mesmo nome!)
                            filename += Unique + dialog.SafeFileName;            //SaveFileName: Obtem APENAS O NOME DO ARQUIVO, SEM o seu caminho completo.
                            NewEmployee.ImagePath = filename;
                            File.Copy(txtImage.Text, @"Images/" + filename);   //File.Copy: Copia o arquivo da localização(txtImage.text) para o diretorio (definido no segundo parametro)
                        }

                        db.Employees.Add(NewEmployee);
                        db.SaveChanges();

                        
                        MessageBox.Show("Employee added successfully");

                        txtName.Clear();
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        picker1.SelectedDate = DateTime.Today;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.SelectedIndex = -1;
                        cmbPosition.ItemsSource = positions;
                        txtAdress.Document.Blocks.Clear();                  // dá Clear no RichTextBox
                        chisAdmin.IsChecked = false;
                        EmployeeImage.Source = new BitmapImage();
                        txtImage.Clear();

                    }


                }
            }






        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);   // "impede que o usuário digite qualquer coisa que não seja um número.
                                                                // Usa uma expressão regular para validar o texto digitado e cancela a
                                                                // entrada se encontrar caracteres inválidos." - CHATGPT
        }





        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("You must provide an UserNo");
            }
            else
            {
                if (updtEmployee != null && updtEmployee.Id != 0)
                {
                    bool isUnique;
                    var Uniquelist = db.Employees.Where(x => x.UserNo == int.Parse(txtUserNo.Text) && x.Id != updtEmployee.Id).ToList();

                    if (Uniquelist.Count > 0)
                    {
                        MessageBox.Show("This UserNo is unavailable");
                    }
                    else
                    {
                        MessageBox.Show("This UserNo is available");
                    }
                }
                else
                {



                    bool isUnique;
                    var Uniquelist = db.Employees.Where(x => x.UserNo == int.Parse(txtUserNo.Text)).ToList();

                    if (Uniquelist.Count > 0)
                    {
                        MessageBox.Show("This UserNo is unavailable");
                    }
                    else
                    {
                        MessageBox.Show("This UserNo is available");
                    }
                }
            }
        }

    }
}
