///Класс для описания полей при создании групп

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{ 
    public class GroupData
    {
        private string name;
        /// <summary>
        /// создаем с дефолтным пустым значением. Следовательно, конструктор создавать не надо, 
        /// туда будет по умолчанию передана пустая строка
        /// </summary>
        private string header = "";
        private string footer = "";

        /// <summary>
        /// конструктор. Принимает на вход только параметр name
        /// </summary>
        /// <param name="name"></param>
        public GroupData(string name)
        {
            ///в поле присваиваем значение, которое передано как параметр
            this.name = name;
        }

        //можно сделать несколько конструкторов. Например во втором хотим сделать все 3 поля обязательными
        //оставляем первый конструктор для обратной совместимости
        //Но он теперь не нужен, так как нет смысла передавать каждое значение
        /*public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }*/

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
