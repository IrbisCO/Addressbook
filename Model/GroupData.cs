///Класс для описания полей при создании групп

namespace WebAddressbookTests
{
    public class GroupData
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
