using Microsoft.Win32;
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

namespace HomeWork_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] sortParams; //Параметры сортировки для SortBox
        bool isEmpl = true; // Для отображения состояния отображения Сотрудники/Департаменты
        Random rnd = new Random(); 

        InfSystem ourInfSyst = new InfSystem(); //Информационная система

        /// <summary>
        /// Показывает пердупреждение о потере несохраненных данных
        /// </summary>
        /// <returns>Результат ответа пользователя</returns>
        private MessageBoxResult ShowWarning()
        {
            return MessageBox.Show("Все несохраненные данные будут утеряны. Продолжить?", "Warning" ,MessageBoxButton.YesNo);
        }

        /// <summary>
        /// Заполняет данный департамент заданным количеством сотрудников
        /// </summary>
        /// <param name="dep">Департамент для заполнеиня</param>
        /// <param name="employeeCount">Количестов сотрудников</param>
        private void FillDepartament(Departament dep, int employeeCount)
        {
            for(int i = 0; i < employeeCount; i++)
            {
                var ID = dep.EmployeesCount + 1;
                var FName = "FName_" + i;
                var LName = "LName_" + i;
                var Age = rnd.Next(22, 50);
                var Salary = rnd.Next(2, 20) * 10000;
                var Departament = dep.Name;
                var Projects = rnd.Next(1, 12);

                var empl = new Employee(ID, FName, LName, Age, Salary, Departament, Projects);

                dep.Add(empl);
            }
        }

        /// <summary>
        /// Создает заданное кол-во департаментов
        /// </summary>
        /// <param name="count">Количество департаментов</param>
        private void CreateDepartaments(int count)
        {
            string depName = "Departament_";

            for(int i = 0; i < count; i++)
            {
                var dep = new Departament(depName + i);
                var emplCount = rnd.Next(5, 21);

                FillDepartament(dep, emplCount);
                
                ourInfSyst.AddDepartament(dep);
            }
        }

        /// <summary>
        /// Выводит в Content_lb список всех департаментов
        /// </summary>
        private void ShowAllDepartamens()
        {
            Content_lb.Items.Clear();

            var pattern = $"{"ID", -2}{"|",-4}{"Name", -30}{"CreationDate",-20}{"EmployeeCount",-10}";
            OutputPattern_lbl.Content = pattern;

            var depList = ourInfSyst.Departaments;

            for(int i =0; i < depList.Count; i++)
            {
                Content_lb.Items.Add($"{i,-3}{"|",-4}" + depList[i]);
            }
        }

        /// <summary>
        /// Выводит в Content_lb список сотрудников
        /// </summary>
        private void ShowAllEmployees()
        {
            Content_lb.Items.Clear();
            
            var pattern = $"{"Global ID"}{"|",-4}{"Local ID"}{"|",-4}{"FName",-20}{"LName", -20}{"Age",-10}{"Salary", -20}{"Departament", -20}{"Projects"}";

            OutputPattern_lbl.Content = pattern;

            List<Employee> emplList;

            if (ourInfSyst.Employees == null) emplList = ourInfSyst.GetEmployees();
            else emplList = ourInfSyst.Employees;

            for(int i =0; i < emplList.Count(); i++)
            {
                Content_lb.Items.Add($"{i,-12}{"|",-4}"+ emplList.ElementAt(i).ToString());
            }
        }

        /// <summary>
        /// Задает параметры SortBox для сортировки сотрудников
        /// </summary>
        private void SetEmployeeSortParams()
        {
            sortParams = new string[]{"None", "By FName", "By LName", "By Age", "By Departament", "By Salary", "By Projects"};
            FirstSortBox_cb.ItemsSource = sortParams;
            SecondSortBox_cb.ItemsSource = sortParams;
        }

        /// <summary>
        /// Задает параметры SortBox для сортировки департаментов
        /// </summary>
        private void SetDepartamentSortParams()
        {
            sortParams = new string[]{"None", "By Name", "By CreationTime", "By EmployeeCount"};
            FirstSortBox_cb.ItemsSource = sortParams;
            SecondSortBox_cb.ItemsSource = sortParams;
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчки нажатия на кнопку Fill Random
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillRandom_Click(object sender, RoutedEventArgs e)
        {
            ourInfSyst.Clear();

            SetEmployeeSortParams();
            CreateDepartaments(3);
            Dep_btn.BorderBrush = Brushes.Gray;
            Empl_btn.BorderBrush = Brushes.Green;

            Empl_btn.BorderThickness = new Thickness(3);
            Dep_btn.BorderThickness = new Thickness(1);
            ShowAllEmployees();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Сотрудники
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowEmpl_Click(object sender, RoutedEventArgs e)
        {
            isEmpl = true;

            if (ourInfSyst.DepatramentsCount == 0) CreateDepartaments(3);

            SetEmployeeSortParams();

            Dep_btn.BorderBrush = Brushes.Gray;
            Empl_btn.BorderBrush = Brushes.Green;

            Empl_btn.BorderThickness = new Thickness(3);
            Dep_btn.BorderThickness = new Thickness(1);

            ShowAllEmployees();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Департаменты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAllDeps_Click(object sender, RoutedEventArgs e)
        {
            isEmpl = false;
            if (ourInfSyst.DepatramentsCount == 0) CreateDepartaments(3);

            Empl_btn.BorderBrush = Brushes.Gray;
            Dep_btn.BorderBrush = Brushes.Green;

            Empl_btn.BorderThickness = new Thickness(1);
            Dep_btn.BorderThickness = new Thickness(3);

            SetDepartamentSortParams();
            ShowAllDepartamens();
        }

        #region CoboBox Events

        /// <summary>
        /// Оработчик события Lost Focus у FirstSortBox_cb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstSortBox_cb_LostFocus(object sender, RoutedEventArgs e)
        {
            var firstIndex = FirstSortBox_cb.SelectedIndex;
            var secondIndex = SecondSortBox_cb.SelectedIndex;
            
            if (isEmpl)
            {
                ourInfSyst.SortEmployee(firstIndex, secondIndex);
                ShowAllEmployees();
            }
            else
            {
                ourInfSyst.SortDepartament(firstIndex, secondIndex);
                ShowAllDepartamens();
            }                
        }

        /// <summary>
        /// Обработчик события Lost Focus у SecondSortBox_cb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondSortBox_cb_LostFocus(object sender, RoutedEventArgs e)
        {
            var firstIndex = FirstSortBox_cb.SelectedIndex;
            var secondIndex = SecondSortBox_cb.SelectedIndex;

            if(isEmpl)
            {
                ourInfSyst.SortEmployee(firstIndex, secondIndex);
                ShowAllEmployees();
            }
            else
            {
                ourInfSyst.SortDepartament(firstIndex, secondIndex);
                ShowAllDepartamens();
            }                
        }

        #endregion

        #region Open/Save

        /// <summary>
        /// Обработчик нажатия на кнопку Open XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenXML_Click(object sender, RoutedEventArgs e)
        {
            var warningRes = ShowWarning();
            if(warningRes == MessageBoxResult.Yes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files | *.xml";

                var result = ofd.ShowDialog();

                if (result == true)
                {
                    var path = ofd.FileName;
                    var openRes = ourInfSyst.OpenXML(path);

                    if (openRes == 0)
                    {
                        ShowAllEmployees();
                    }
                    else MessageBox.Show("Не удалось открыть файл");
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Save as XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveXML_Click(object sender, RoutedEventArgs e)
        {
            if (ourInfSyst.DepatramentsCount == 0)
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML Files | *.xml";

                var result = sfd.ShowDialog();

                if (result == true)
                {
                    var path = sfd.FileName;
                    var res = ourInfSyst.SaveAsXML(path);

                    if (res == 0) MessageBox.Show("Файл успешно сохранен.");
                    else MessageBox.Show("При сохранении возникла ошибка.");
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Open JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenJSON_Click(object sender, RoutedEventArgs e)
        {
            var warningRes = ShowWarning();

            if (warningRes == MessageBoxResult.Yes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "JSON Files | *.json";

                var resul = ofd.ShowDialog();

                if (resul == true)
                {
                    var path = ofd.FileName;
                    var openRes = ourInfSyst.OpenJSON(path);

                    if (openRes == 0) ShowAllEmployees();
                    else
                    {
                        MessageBox.Show("Не удалось открыть файл");
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Save as JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJSON_Click(object sender, RoutedEventArgs e)
        {
            if (ourInfSyst.DepatramentsCount == 0)
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "JSON Files | *.json";

                var result = sfd.ShowDialog();

                if (result == true)
                {
                    var path = sfd.FileName;
                    var res = ourInfSyst.SaveAsJSON(path);

                    if (res == 0) MessageBox.Show("Файл успешно сохранен.");
                    else MessageBox.Show("При сохранении возникла ошибка.");
                }
            }
        }

        #endregion

    }
}
