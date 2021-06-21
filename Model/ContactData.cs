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
        /// Поле всей поле всей информации о контакте
        /// </summary>
        private string contactDetailes;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Никнейм
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Комания
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// ЗАголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Свойство ID. Для определения элемента не только по имени, но и по ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Secondary Address
        /// </summary>
        public string SecondaryAddress { get; set; }


        /// <summary>
        /// Secondary Home Phone
        /// </summary>
        public string SecondaryHomePhone { get; set; }


        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// E-mail #1
        /// </summary>
        public string Email1 { get; set; }

        /// <summary>
        /// E-mail #2
        /// </summary>
        public string Email2 { get; set; }

        /// <summary>
        /// E-mail #3
        /// </summary>
        public string Email3 { get; set; }

        /// <summary>
        /// Homepage (E-mail)
        /// </summary>
        public string Homepage { get; set; }

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
        /// День рождения
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// Месяц рождения
        /// </summary>
        public string MonthOfBirth { get; set; }

        /// <summary>
        /// Год рождения
        /// </summary>
        public string YearhOfBirth { get; set; }

        /// <summary>
        /// День юбилея
        /// </summary>
        public string AnniversaryDay { get; set; }

        /// <summary>
        /// Месяц юбилея
        /// </summary>
        public string MonthOfAnniversary { get; set; }

        /// <summary>
        /// Год юбилея
        /// </summary>
        public string YearOfAnniversary { get; set; }


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
        /// Fax
        /// </summary>
        public string Fax { get; set; }

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
            return ("\r\n" + FirstName + " " + MiddleName + " " + Lastname + "\r\n"
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
                        + "Homepage:" + "\r\n" + Homepage + "\r\n\r\n\r\n"
                        //+ "Birthday " + Birthday + ". " + MonthOfBirth + " " + YearhOfBirth + "\r\n"
                        //+ "Anniversary " + AnniversaryDay + ". " + MonthOfAnniversary + " " + YearOfAnniversary + "\r\n\r\n"
                        + (SecondaryAddress) + "\r\n\r\n"
                        + "P: " + (SecondaryHomePhone) + "\r\n\r\n"
                        + Notes).Trim();
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
    }
}
