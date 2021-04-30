///Класс для описания полей при создании контактов

using System;
using System.Text.RegularExpressions;

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
        /// Поле всех телефонов
        /// </summary>
        private string allPhones;
        /// <summary>
        /// Поле всех мэйлов
        /// </summary>
        private string allEmails;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Свойство ID. Для определения элемента не только по имени, но и по ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// мэйл 1
        /// </summary>
        public string Email1 { get; set; }

        /// <summary>
        /// мэйл 2
        /// </summary>
        public string Email2 { get; set; }

        /// <summary>
        /// мэйл 3
        /// </summary>
        public string Email3 { get; set; }

        /// <summary>
        /// Все мэйлы
        /// </summary>
        public string AllEmails
        {
            get
            {
                /// Если поле allEmails установлено
                if (allEmails != null)
                {
                    /// Возвращаем его же
                    return allEmails;
                }
                /// Иначе склеиваем данные
                else
                {
                    /// Trim - убирает лишние пробелы в начале и в конце 
                    return (CleanUp(Email1) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        /// <summary>
        /// Домашний телефон
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Мобильный телефон
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Рабочий телефон
        /// </summary>
        public string WorkPhone { get; set; }

        /// <summary>
        /// Все телефоны
        /// </summary>
        public string AllPhones
        {
            get
            {
                /// Если поле allPhones установлено
                if (allPhones != null)
                {
                    /// Возвращаем его же
                    return allPhones;
                }
                /// Иначе склеиваем данные
                else
                {
                    /// Trim - убирает лишние пробелы в начале и в конце 
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        /// <summary>
        /// Убирает лишние символы (пробелы, скобки и тд)
        /// </summary>
        /// <param name="homePhone"></param>
        /// <returns></returns>
        private string CleanUp(string phone)
        {
            /// Если строка пустая 
            if (phone == null || phone == "")
            {
                return "";
            }
            /// Убираем лишние символы. Можно добавить больше при необходимости. В конце переносим строку
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public ContactData()
        {
        }

        /// <summary>
        /// конструктор. Принимает на вход параметры name\surname
        /// можно сделать несколько конструкторов. Например во втором сделать все поля обязательными
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        public ContactData(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        /// <summary>
        /// Функция, которая реализует сравнение. В качестве параметра принимает второй объект типа ContactData
        /// </summary>
        /// <param name="other">второй объект типа ContactData, с которым сравниваем текущий объект</param>
        /// <returns></returns>
        public bool Equals(ContactData other)
        {
            /// 1 базовая проверка. Если объект, с которым мы сравниваем, это NULL
            if (ReferenceEquals(other, null))
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
            return FirstName == other.FirstName && SecondName == other.SecondName;
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
            return (FirstName + SecondName).GetHashCode();
        }

        /// <summary>
        /// Возвращает строковое представление объектов типа GroupData
        /// Так как метод переопределяет стандартный метод, определенный во всех классах, требуется override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /// Возвращает имя. Для контактов имя+фамилия
            return "Fullname = " + FirstName + " " + SecondName;
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
            if (ReferenceEquals(other, null))
            {
                /// Однозначно текущий объект больше
                return 1;
            }
            /// 2. Сравнение по смыслу. Сравниваем Name и Surname
            if (Equals(this.SecondName, other.SecondName))
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return SecondName.CompareTo(other.SecondName);
        }
    }
}
