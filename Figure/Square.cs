using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theory
{
    class Square : Figure
    {
        //поле size. Важный атрибут
        private int size;
        //конструктор 
        public Square(int size)
        {
            //this.size ссылается на поле size. Второй size - локальная переменная. Это дает возможность
            //в Figure задать значение локальной переменной (размер)
            this.size = size;
        }
        /* Вместо это безобразия есть property, который делает то же самое, но короче
         * 
        //создаем новый метод для проверки размера
        public int getSize()
        {
            //возвращаем размер. То есть как бы сравнивает указанный с имеющимся
            return size;
        }
        //метод для изменения размера
        public void setSize(int size)
        {
            this.size = size;
        }
        */

        //а вот и сам property
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
    }
}
