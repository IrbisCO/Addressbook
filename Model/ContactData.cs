///Класс для описания полей при создании контактов

using System;

namespace WebAddressbookTests.Model
{
    /// <summary>
    /// Указываем, что класс наследует IEquatable - функция сравнения. Этот класс можно сравнивать с дургими объектами типа ContactData
    /// Все это нужно для корректного сравнения списков. Состоит из 3 частей: 
    /// 1.Написать : IEquatable<GroupData> 
    /// 2.Создать метод public bool Equals(ContactData other)
    /// 3.Добавить метод public int GetHashCode()
    /// 
    /// Чтобы отсортировать списки, необходимо описать как сравниваются между собой элементы типа ContactData
    /// 1. Добавляем IComparable<ContactData>:
    /// 2. Создаем метод public int CompareTo(ContactData other)
    /// </summary>
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        /// <summary>
        /// пара полей для имени\фамилии
        /// </summary>
        private string name;
        private string surname;

        /// <summary>
        /// конструктор. Принимает на вход параметры name\surname
        /// можно сделать несколько конструкторов. Например во втором сделать все поля обязательными
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        public ContactData(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        /// <summary>
        /// Функция, которая реализует сравнение. В качестве параметра принимает второй объект типа ContactData
        /// </summary>
        /// <param name="other">второй объект типа ContactData, с которым сравниваем текущий объект</param>
        /// <returns></returns>
        public bool Equals(ContactData other)
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
            return Surname == other.Surname && Name == other.Name;
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
            return (Surname + " " + Name).GetHashCode();
        }

        /// <summary>
        /// Возвращает строковое представление объектов типа GroupData
        /// Так как метод переопределяет стандартный метод, определенный во всех классах, требуется override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /// Возвращает имя. Для контактов имя+фамилия
            return "Contact: " + Surname + " " + Name;
        }
        /// <summary>
        /// Метод для сравнения
        /// Возвращает 1, если текущий объект this больше, чем переданный в качестве параметра объект ContactData other
        /// Возвращает 0, если объекты равны
        /// Возвращает -1, если текущий объект this меньше, чем переданный в качестве параметра объект ContactData other
        /// </summary>
        /// <param name="other">Второй объект типа ContactData, с которым сравниваем текущий объект</param>
        /// <returns></returns>
        public int CompareTo(ContactData other)
        {
            /// 1. Стандартная проверка. Если второй объект равен NULL
            if (other is null)
            {
                /// Однозначно текущий объект больше
                return 1;
            }
            /// 2. Сравнение по смыслу. Сравниваем Name
            return (Surname + " " + Name).CompareTo(other.Surname + " " + Name);
        }

        /// <summary>
        /// свойства
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// свойства
        /// </summary>
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
    }
}
