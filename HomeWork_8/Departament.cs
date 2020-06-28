using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_8
{
    public class Departament
    {
        public string Name { get; set; } //Имя департамента
        public DateTime CreationDate { get; set; } // Дата создания
        public int EmployeesCount { get { return employees.Count; } } // Количество сотрудников

        public List<Employee> Emplyees { get { return employees; } } //Свойство для возвращения списка сотрудников

        private List<Employee> employees = new List<Employee>(); // Список сотрудников

        #region Constructors

        //Конструктов без параметров необходим только для сериализации 

        /// <summary>
        /// Конструктор без прараметров 
        /// </summary>
        public Departament()
        {

        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        public Departament(string Name)
        {
            this.Name = Name;
            CreationDate = DateTime.Now;
        }

        #endregion


        #region Add/Delete

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        public void Add(Employee empl)
        {
            employees.Add(empl);
        }

        /// <summary>
        /// Удаление сотрудника из списка по индексу
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        /// <returns></returns>
        public int Delete(int index)
        {
            var empl = employees[index];
            return Delete(empl);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        /// <returns></returns>
        public int Delete(Employee empl)
        {
            if (employees.Contains(empl))
            {
                employees.Remove(empl);
                Employee.EmplCount--;
            }
            else return -1;

            return 0;
        }

        #endregion

        /// <summary>
        /// Проверяет, работает ли сотрудник в данном департаменте
        /// </summary>
        /// <param name="empl">Сотрудник</param>
        /// <returns></returns>
        public bool Contains(Employee empl)
        {
            return employees.Contains(empl);
        }

        /// <summary>
        /// Индексатор для работы со списком сотрудников
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Employee this[int i]
        {
            get
            {
                return employees[i];
            }
        }

        /// <summary>
        /// Переоперделение метода ToString() 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var pattern = $"{Name, -20}{CreationDate,-30}{EmployeesCount, -10}";
            return pattern;
        }

    }
}
