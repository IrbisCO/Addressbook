using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class GroupData
    {
        private string name;
        //создаем с дефолтным пустым значением. Следовательно, конструктор создавать не надо, туда
        //будет по умолчанию передана пустая строка
        private string header = "";
        private string footer = "";

        //конструктор. Принимает на вход только параметр name
        public GroupData(string name)
        {
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
        
        //свойства
        public string Name
        {
            //get - возвращает
            get
            {
                return name;
            }
            //set - устанавливает (или восстанавливает, хз)
            set
            {
                name = value;
            }
        }
        //если нужно поменять это дополнительное необязательное значние, испоьзуют свойства
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
