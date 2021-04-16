
using System;
///Класс для описания полей при создании групп
namespace WebAddressbookTests.Model
{
    /// <summary>
    /// Указываем, что класс наследует IEquatable - функция сравнения. Этот класс можно сравнивать с дургими объектами типа GroupData
    /// Все это нужно для корректного сравнения списков. Состоит из 3 частей: 
    /// 1.Написать : IEquatable<GroupData> 
    /// 2.Создать метод public bool Equals(GroupData other)
    /// 3.Добавить метод public int GetHashCode()
    /// </summary>
    public class GroupData : IEquatable<GroupData> 
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
        /// <param name="other">второй объект типа GroupData</param>
        /// <returns></returns>
        public bool Equals(GroupData other)
        {
            /// 1 базовая проверка. Если объект, с которым мы сравниваем, это NULL
            if (Object.ReferenceEquals(other, null))
            {
                /// false потому что текущий объект точно есть и точно не равен NULL
                return false;
            }
            /// 2 базовая проверка. Если объект, с которым мы сравниваем, это тот же объект и они совпадают (две ссылки указывают на один и тот же объект)
            if (Object.ReferenceEquals(this, other))
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
        /// </summary>
        /// <returns></returns>
        public int GetHashCode()
        {
            /// так как в сравнении участвует Name, то и хэш-коды вычисляются по именам
            return Name.GetHashCode();
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
