using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_8
{
    public class Employee
    {
        public static int EmplCount = 0; // Количество сотрудников
        public int ID { get; set; } // ID сотрудника
        public string FName { get; set; } // Имя сотрудника
        public string LName { get; set; } // Фамилия сотрудника
        public int Age { get; set; } // Возраст сотрудника
        public int Salary { get; set; } // Зарплата сотрудника
        public string Departament { get; set; }  // Департамент в котором работает сотрудлник
        public int Projects { get; set; } // Количество проектов сотрудника

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Employee()
        {

        }

        /// <summary>
        /// Клгсьоуктор с параметрами
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="FName">Имя</param>
        /// <param name="LName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Salary">Зарпалата</param>
        /// <param name="Departament">Департамент</param>
        /// <param name="Projects">Количество проектов</param>
        public Employee(int ID, string FName, string LName, int Age, int Salary, string Departament, int Projects)
        {
            this.ID = ID;
            this.FName = FName;
            this.LName = LName;
            this.Age = Age;
            this.Salary = Salary;
            this.Departament = Departament;
            this.Projects = Projects;
            EmplCount++;
        }
        
        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            FormattableString pattern = $"{ID, -12}{"|", -4}{FName, -20}{LName, -20}{Age,-10}{Salary,-20}{Departament,-20}{Projects,-4}";
            return pattern.ToString();
        }

    }
}
