using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HomeWork_8
{
    class InfSystem
    {
        List<Departament> depatraments = new List<Departament>(); // Список департаментов
        public int DepatramentsCount { get { return depatraments.Count; } } //Количестов департаментов
        public List<Departament> Departaments { get{ return depatraments; } } // Свойство возвращающее список департаментов

        public List<Employee> Employees { get; set; } // Список сотрудников
        public int EmployeesCount
        {
            get
            {
                int count = 0;
                foreach (var dep in depatraments)
                {
                    count += dep.EmployeesCount;
                }
                return count;
            }
        }   // Количество сотрудников

        #region Add/Delete/Clear

        /// <summary>
        /// Добавление департамента
        /// </summary>
        /// <param name="dep">Департамент</param>
        public void AddDepartament(Departament dep)
        {
            depatraments.Add(dep);
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="dep">Департамент</param>
        /// <returns></returns>
        public int DeleteDepartament(Departament dep)
        {
            if (depatraments.Contains(dep)) depatraments.Remove(dep);
            else return -1;

            return 0;
        }
    
        /// <summary>
        /// Ужадение департамента по индексу
        /// </summary>
        /// <param name="i">Индекс департамента</param>
        /// <returns></returns>
        public int DeleteDepartament(int i)
        {
            var dep = depatraments[i];

            return DeleteDepartament(dep);
        }

        /// <summary>
        /// Добавление сотрудника в департамент
        /// </summary>
        /// <param name="departamentIndex">Индекс департамента</param>
        /// <param name="empl">Сотрудник</param>
        /// <returns></returns>
        public int AddEmplyee(int departamentIndex, Employee empl)
        {
            if (departamentIndex > DepatramentsCount - 1) return -1;

            depatraments[departamentIndex].Add(empl);

            return 0;
        }

        /// <summary>
        /// Удаление сотрудника по индексу из департамента
        /// </summary>
        /// <param name="departamentIndex">Индекс департамента</param>
        /// <param name="empl">Индекс сотрудника</param>
        /// <returns></returns>
        public int DeleteEmployee(int departamentIndex, Employee empl)
        {
            if (departamentIndex > DepatramentsCount - 1) return -1;

            if (!depatraments[departamentIndex].Contains(empl)) return -1;

            depatraments[departamentIndex].Delete(empl);

            return 0;
        }

        public void Clear()
        {
            depatraments.Clear();
        }

        #endregion

        #region Sort

        /// <summary>
        /// Сортировка списка сотрудникво по двум параметрам
        /// </summary>
        /// <param name="firstP">Первый парметр</param>
        /// <param name="secondP">Второй парметр</param>
        public void SortEmployee(int firstP = 0, int secondP = 0)
        {
            var list = GetEmployees();
            Employees = SortEmployeeBy(list, firstP, secondP);
        }

        /// <summary>
        /// Сортировка списка департаметров по двум параметрам
        /// </summary>
        /// <param name="firstP">Первый параметр</param>
        /// <param name="secondP">Второй параметр</param>
        public void SortDepartament(int firstP = 0, int secondP = 0)
        {
            var list = depatraments;
            depatraments = SortDepartamentsBy(list, firstP, secondP);
        }

        /// <summary>
        /// Сортировка департаментов по двум параметрам
        /// </summary>
        /// <param name="list">Список для сортировки</param>
        /// <param name="firstP">Первый параметр</param>
        /// <param name="secondP">Второй параметр</param>
        /// <returns></returns>
        public List<Departament> SortDepartamentsBy(List<Departament> list, int firstP = 0, int secondP = 0)
        {
            switch (firstP)
            {
                case 1:
                    return DepatramentsThenBy(list.OrderBy(x => x.Name), secondP);
                case 2:
                    return DepatramentsThenBy(list.OrderBy(x => x.CreationDate), secondP);
                case 3:
                    return DepatramentsThenBy(list.OrderBy(x => x.EmployeesCount), secondP);
                default:
                    return list;
            }
        }

        /// <summary>
        /// Сортировка сортудников по двум параметрам
        /// </summary>
        /// <param name="list">Список для сортировки</param>
        /// <param name="firstParam">Первый параметр</param>
        /// <param name="secondParam">Второй параметр</param>
        /// <returns></returns>
        private List<Employee> SortEmployeeBy(List<Employee> list ,int firstParam, int secondParam)
        {
            switch (firstParam)
            {
                case 1:
                    return EmployeeThenBy(list.OrderBy(x => x.FName), secondParam);
                case 2:
                    return EmployeeThenBy(list.OrderBy(x => x.LName), secondParam);
                case 3:
                    return EmployeeThenBy(list.OrderBy(x => x.Age), secondParam);
                case 4:
                    return EmployeeThenBy(list.OrderBy(x => x.Departament), secondParam);
                case 5:
                    return EmployeeThenBy(list.OrderBy(x => x.Salary), secondParam);
                case 6:
                    return EmployeeThenBy(list.OrderBy(x => x.Projects), secondParam);
                default:
                    return list;
            }
        }

        /// <summary>
        /// Сортировка департаментов по второму параметру
        /// </summary>
        /// <param name="list">Список для сортировки</param>
        /// <param name="param">Параметр сортировки</param>
        /// <returns></returns>
        public List<Departament> DepatramentsThenBy(IOrderedEnumerable<Departament> list, int param)
        {
            switch (param)
            {
                case 1:
                    return list.ThenBy(x => x.Name).ToList();
                case 2:
                    return list.ThenBy(x => x.CreationDate).ToList();
                case 3:
                    return list.ThenBy(x => x.EmployeesCount).ToList();
                default:
                    return list.ToList();
            }
        }

        /// <summary>
        /// Сортировка сотрудников по второму параметру
        /// </summary>
        /// <param name="list">Список для сортировки</param>
        /// <param name="param">Параметр для сортировки</param>
        /// <returns></returns>
        private List<Employee> EmployeeThenBy(IOrderedEnumerable<Employee> list, int param)
        {
            switch (param)
            {
                case 1:
                    return list.ThenBy(x => x.FName).ToList();
                case 2:
                    return list.ThenBy(x => x.LName).ToList();
                case 3:
                    return list.ThenBy(x => x.Age).ToList();
                case 4:
                    return list.ThenBy(x => x.Departament).ToList();
                case 5:
                    return list.ThenBy(x => x.Salary).ToList();
                case 6:
                    return list.ThenBy(x => x.Projects).ToList();
                default:
                    return list.ToList();
            }

        }

        #endregion

        /// <summary>
        /// Возвращает списов сотрудников
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees()
        {
            var emplList = new List<Employee>();
            foreach(var dep in depatraments)
            {
                emplList.AddRange(dep.Emplyees);
            }

            return emplList;
        }

        #region Save/Opne

        /// <summary>
        /// Сохранение в XML-файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public int SaveAsXML(string path)
        {
            try
            {
                using (Stream fstream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Departament>));
                    xmlSerializer.Serialize(fstream, depatraments);
                }
            }
            catch
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Сохранение в JSON-файл
        /// </summary>
        /// <param name="path">Пукть к файлу</param>
        /// <returns></returns>
        public int SaveAsJSON(string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(depatraments);
                File.WriteAllText(path, json);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Открытие XML-файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public int OpenXML(string path)
        {
            try
            {
                using (Stream fstream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Departament>));
                    depatraments = xmlSerializer.Deserialize(fstream) as List<Departament>;
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Открытие JSON-файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public int OpenJSON(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                depatraments = JsonConvert.DeserializeObject<List<Departament>>(json);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        #endregion
    }
}
