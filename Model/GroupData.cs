///Класс для описания полей при создании групп

using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressbookTests.Model
{
    [Table(Name = "group_list")]
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
        /// конструктор. Принимает на вход только параметр name
        /// можно сделать несколько конструкторов. Например во втором хотим сделать все 3 поля обязательными
        /// оставляем первый конструктор для обратной совместимости
        /// Но он теперь не нужен, так как нет смысла передавать каждое значение
        /// </summary>
        /// <param name="name">Название группы</param>
        public GroupData(string name)
        {
            ///в поле присваиваем свойство
            Name = name;
        }

        public GroupData()
        {
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
        /// 
        /// Строит по объекто более простую сущность - хэш. В данном случае это число
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
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer;
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
            if (other is null)
            {
                /// Однозначно текущий объект больше
                return 1;
            }
            /// 2. Сравнение по смыслу. Сравниваем Name
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// свойства (короткая запись). Поля создаются автоматически
        /// + название колонки в бд
        /// </summary>
        [Column(Name = "group_name")]
        public string Name { get; set; }

        /// <summary>
        /// если нужно поменять это дополнительное необязательное значние, испоьзуют свойства
        /// + название колонки в бд
        /// </summary>
        [Column(Name = "group_header")]
        public string Header { get; set; }

        /// <summary>
        /// если нужно поменять это дополнительное необязательное значние, испоьзуют свойства
        /// + название колонки в бд
        /// </summary>
        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        /// <summary>
        /// Свойство ID. Для определения элемента не только по имени, но и по ID
        /// + название колонки в бд, PK, Уник ID
        /// </summary>
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        /// <summary>
        /// Получение данных из БД для групп
        /// </summary>
        /// <returns></returns>
        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        /// <summary>
        /// Получаем нужный список контактов в группах
        /// </summary>
        /// <returns></returns>
        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts from gcr in db.GCR.Where(p => p.GroupId == Id 
                        && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00") select c).Distinct().ToList();
            }
        }
    }
}
