///Класс для описания полей при создании контактов

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;

namespace WebAddressbookTests.Model
{
    [Table(Name = "addressbook")]
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
        /// Поле всей поле всей информации о контакте
        /// </summary>
        private string contactDetailes;
 
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
                    return ((Email1) + (Email2) + (Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
       
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
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(SecondaryHomePhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        /// <summary>
        /// Просмотр детальной информации о контакте
        /// </summary>
        public string ContactDetails
        {
            get
            {
                if (contactDetailes != null)
                {
                    return contactDetailes;
                }
                else
                {
                    return 
                        (FirstName + " " + MiddleName + " " + Lastname + "\r\n"
                        + Nickname + "\r\n"
                        + Title + "\r\n"
                        + Company + "\r\n"
                        + Address + "\r\n\r\n"
                        + "H: " + HomePhone + "\r\n"
                        + "M: " + MobilePhone + "\r\n"
                        + "W: " + WorkPhone + "\r\n"
                        + "F: " + Fax + "\r\n\r\n"
                        + Email1 + "\r\n"
                        + Email2 + "\r\n"
                        + Email3 + "\r\n"
                        + "Homepage:" + "\r\n" + Homepage + "\r\n\r\n"
                        + "Birthday " + Birthday + ". " + MonthOfBirth + " " + YearhOfBirth + "\r\n"
                        + "Anniversary " + AnniversaryDay + ". " + MonthOfAnniversary + " " + YearOfAnniversary + "\r\n\r\n"
                        + (SecondaryAddress) + "\r\n\r\n"
                        + "P: " + (SecondaryHomePhone) + "\r\n\r\n"
                        + Notes).Trim();
                }
            }
            set
            {
                contactDetailes = value;
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
            /// Убираем лишние символы. Можно добавить больше при необходимости. Заменяеые значения в [], это пробел, тире, скобки. В конце переносим строку
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
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
            Lastname = secondName;
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
            return FirstName == other.FirstName && Lastname == other.Lastname;
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
            return (FirstName + Lastname).GetHashCode();
        }

        //TODO: Сделать более корректно описание имя-имя и тд
        /// <summary>
        /// Возвращает строковое представление объектов типа GroupData
        /// Так как метод переопределяет стандартный метод, определенный во всех классах, требуется override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "\nFirstName = " + FirstName + "\r\n" + "MiddleName = " + MiddleName + "\r\n" + "Lastname = " + Lastname + "\r\n"
                        + "Nickname = " + Nickname + "\r\n" + "Title = " + Title + "\r\n" + "Company = " + Company + "\r\n" + "Address = " + Address + "\r\n"
                        + "HomePhone = " + HomePhone + "\r\n" + "MobilePhone = " + MobilePhone + "\r\n" + "WorkPhone = " + WorkPhone + "\r\n" + "Fax = " + Fax + "\r\n"
                        + "Email1 = " + Email1 + "\r\n" + "Email2 = " + Email2 + "\r\n" + "Email3 = " + Email3 + "\r\n" + "Homepage =" + Homepage + "\r\n"
                        + "Birthday = " + Birthday + "\r\n" + "MonthOfBirth = " + MonthOfBirth + "\r\n" + "YearhOfBirth = " + YearhOfBirth + "\r\n"
                        + "Anniversary = " + AnniversaryDay + "\r\n" + "MonthOfAnniversary = " + MonthOfAnniversary + "\r\n" + "YearOfAnniversary = " + YearOfAnniversary + "\r\n"
                        + "SecondaryAddress = " + (SecondaryAddress) + "\r\n" + "SecondaryHomePhone = " + (SecondaryHomePhone) + "\r\n" + "Notes = " + Notes;
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
            if (Equals(this.Lastname, other.Lastname))
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return Lastname.CompareTo(other.Lastname);
        }

        /// <summary>
        /// Имя
        /// </summary>
        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Никнейм
        /// </summary>
        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Комания
        /// </summary>
        [Column(Name = "company")]
        public string Company { get; set; }

        /// <summary>
        /// ЗАголовок
        /// </summary>
        [Column(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Column(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Домашний телефон
        /// </summary>
        [Column(Name = "home")]
        public string HomePhone { get; set; }

        /// <summary>
        /// Мобильный телефон
        /// </summary>
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Рабочий телефон
        /// </summary>
        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        [Column(Name = "fax")]
        public string Fax { get; set; }

        /// <summary>
        /// E-mail #1
        /// </summary>
        [Column(Name = "email")]
        public string Email1 { get; set; }

        /// <summary>
        /// E-mail #2
        /// </summary>
        [Column(Name = "email2")]
        public string Email2 { get; set; }

        /// <summary>
        /// E-mail #3
        /// </summary>
        [Column(Name = "email3")]
        public string Email3 { get; set; }

        /// <summary>
        /// Homepage (E-mail)
        /// </summary>
        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        [Column(Name = "bday")]
        public string Birthday { get; set; }

        /// <summary>
        /// Месяц рождения
        /// </summary>
        [Column(Name = "bmonth")]
        public string MonthOfBirth { get; set; }

        /// <summary>
        /// Год рождения
        /// </summary>
        [Column(Name = "byear")]
        public string YearhOfBirth { get; set; }

        /// <summary>
        /// День юбилея
        /// </summary>
        [Column(Name = "aday")]
        public string AnniversaryDay { get; set; }

        /// <summary>
        /// Месяц юбилея
        /// </summary>
        [Column(Name = "amonth")]
        public string MonthOfAnniversary { get; set; }

        /// <summary>
        /// Год юбилея
        /// </summary>
        [Column(Name = "ayear")]
        public string YearOfAnniversary { get; set; }        

        /// <summary>
        /// Secondary Address
        /// </summary>
        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }

        /// <summary>
        /// Secondary Home Phone
        /// </summary>
        [Column(Name = "phone2")]
        public string SecondaryHomePhone { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [Column(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Свойство ID. Для определения элемента не только по имени, но и по ID
        /// </summary>
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        /// <summary>
        /// Получение данных из БД для контактов 
        /// </summary>
        /// <returns></returns>
        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }
    }
}
