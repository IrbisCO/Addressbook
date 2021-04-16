///  Просто теория

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;


namespace Theory
{
    [TestClass]
    public class Cycles
    {
        [TestMethod]
        public void TestMethod1() //вариант с for
        {
            /// массив строк
            string[] s = new string[] { "I", "want", "to", "sleep" };

            /// s.Length - длина массива
            for (int i = 0; i < s.Length; i++)
            {
                /// s[i] - обращение к элементам массива по индексу
                Console.Out.Write(s[i] + "\n");
            }
            
        }

        [TestMethod]
        public void TestMethod2() // аналогично прошлому, только foreach
        {
            /// массив строк
            string[] s = new string[] { "I", "want", "to", "sleep" };

            /// аналог первого
            /// string element in s - для каждого элемента массива S
            foreach (string element in s)
            {
                /// выводить ELEMENT
                Console.Out.Write(element + "\n");
            }
        }

        //[TestMethod]
        public void TestMethod3() // вариант с while для поиска элемента, который когда-нибудь появится 
        {
            IWebDriver driver = null;
            /// счетчик попыток
            int attepmt = 0;
            /// Пока элемент с ID = test не найден (счетчик равен 0) и количество попыток меньше 60...
            while (driver.FindElements(By.Id("test")).Count == 0 && attepmt < 60)
            {
                /// ...ждать 1 секунду и снова искать
                System.Threading.Thread.Sleep(1000);
                attepmt++;
            } 
            /// а дальше уже дургая часть кода
        }

        //[TestMethod]
        public void TestMethod4() // аналогично прошлому, но сначала ждем секунду, а потом ищем
        {
            IWebDriver driver = null;
            /// счетчик попыток
            int attepmt = 0;

            do
            /// Пока элемент с ID = test не найден (счетчик равен 0) и количество попыток меньше 60...
            {
                /// ...ждать 1 секунду и снова искать
                System.Threading.Thread.Sleep(1000);
                attepmt++;
            }
            while (driver.FindElements(By.Id("test")).Count == 0 && attepmt < 60);
            /// а дальше уже дургая часть кода
        }
    }
}
