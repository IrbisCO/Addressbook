﻿///Класс для описания полей при создании групп

using System;

namespace WebAddressbookTests.Model
{
    /// <summary>
    /// Указываем, что класс наследует IEquatable - функция сравнения. Этот класс можно сравнивать с дургими объектами типа GroupData
    /// Все это нужно для корректного сравнения списков. Состоит из 3 частей: 
    /// 1.Написать : IEquatable<GroupData> 
    /// 2.Создать метод public bool Equals(GroupData other)
    /// 3.Добавить метод public int GetHashCode()
    /// 
    /// Чтобы отсортировать списки, необходимо описать как сравниваются между собой элементы типа GroupData
    /// 1. Добавляем IComparable<GroupData>:
    /// 2. Создаем метод public int CompareTo(GroupData other)
    /// </summary>
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        /// <summary>
        /// поле для названия группы
        /// </summary>
        private string name;
        /// <summary>
        /// создаем с дефолтным пустым значением. Следовательно, конструктор создавать не надо, 
        /// туда будет по умолчанию передана пустая строка
        /// </summary>
        private string header = "";
        private string footer = "";

        /// <summary>
        /// конструктор. Принимает на вход только параметр name
        /// можно сделать несколько конструкторов. Например во втором хотим сделать все 3 поля обязательными
        /// оставляем первый конструктор для обратной совместимости
        /// Но он теперь не нужен, так как нет смысла передавать каждое значение
        /// </summary>
        /// <param name="name">Название группы</param>
        public GroupData(string name)
        {
            ///в поле присваиваем значение, которое передано как параметр
            this.name = name;
        }

        /// <summary>
        /// Функция, которая реализует сравнение. В качестве параметра принимает второй объект типа GroupData
        /// </summary>
        /// <param name="other">второй объект типа GroupData, с которым сравниваем текущий объект</param>
        /// <returns></returns>
        public bool Equals(GroupData other)
        {
            /// 1 базовая проверка. Если объект, с которым мы сравниваем, это NULL
            if (other is null)
            {
                /// false потому что текущий объект точно есть и точно не равен NULL
                return false;
            }
            /// 2 базовая проверка. Если объект, с которым мы сравниваем, это тот же объект и они совпадают (две ссылки указывают на один и тот же объект)
            if (ReferenceEquals(this, other))
            {
                /// true потому что никаких проверок не надо, так как объект сравниваем сам с собой 
                return true;
            }
            /// Проверка по смыслу. Сравниваем только имена. Для контактов имя+фамилия
            return Name == other.Name;
        }

        /// <summary>
        /// Помимо стандартного метода Equals нужен еще и GetHashCode
        /// Метод предназначен для оптимизации сравнения
        /// Сначала объекты сравниваются по хэш-кодам, а потом, если равны, с помощью метода Equals
        /// Так как метод переопределяет стандартный метод, определенный в базовом классе требуется override
        /// </summary>
        /// <returns>Возвращает имя. Для контактов надоимя+фамилия</returns>
        public override int GetHashCode()
        {
            /// так как в сравнении участвует Name, то и хэш-коды вычисляются по именам
            return Name.GetHashCode();
        }

        /// <summary>
        /// Возвращает строковое представление объектов типа GroupData
        /// Так как метод переопределяет стандартный метод, определенный во всех классах, требуется override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /// Возвращает имя. Для контактов имя+фамилия
            return "name = " + Name;
        }
        /// <summary>
        /// Метод для сравнения
        /// Возвращает 1, если текущий объект this больше, чем переданный в качестве параметра объект GroupData other
        /// Возвращает 0, если объекты равны
        /// Возвращает -1, если текущий объект this меньше, чем переданный в качестве параметра объект GroupData other
        /// </summary>
        /// <param name="other">Второй объект типа GroupData, с которым сравниваем текущий объект</param>
        /// <returns></returns>
        public int CompareTo(GroupData other)
        {
            /// 1. Стандартная проверка. Если второй объект равен NULL
            if(other is null)
            {
                /// Однозначно текущий объект больше
                return 1;
            }
            /// 2. Сравнение по смыслу. Сравниваем Name
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// свойства
        /// </summary>
        public string Name
        {
            ///get - возвращает
            get
            {
                return name;
            }
            ///set - устанавливает
            set
            {
                name = value;
            }
        }
        /// <summary>
        /// если нужно поменять это дополнительное необязательное значние, испоьзуют свойства
        /// </summary>
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        /// <summary>
        /// если нужно поменять это дополнительное необязательное значние, испоьзуют свойства
        /// </summary>
        public string Footer
        {
            get
            {
                return footer;
            }
            set
            {
                footer = value;
            }
        }
    }
}
