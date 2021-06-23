/// Базовый класс для помощников 
/// этот класс создан, чтобы одинаковый код из помощников переместить сюда

using OpenQA.Selenium;
using System;
using System.Linq;
using System.Text;

namespace WebAddressbookTests.AppManager
{
    public class HelperBase
    {
        /// <summary>
        /// вот это самое поле driver для хелперов
        /// создается поле типа IWebDriver
        /// </summary>
        protected IWebDriver driver;
        protected ApplicationManager manager;
        private bool acceptNextAlert = true;

        /// <summary>
        /// конструктор. На вход принимает ссылку на Application manager
        /// </summary>
        /// <param name="manager"></param>
        public HelperBase(ApplicationManager manager)
        {
            ///сохраняем ссылку на manager, чтобы каждый помощник про него знал
            this.manager = manager;
            ///получает ссылку на Driver у менеджера и все этой ссылкой могут пользоваться
            driver = manager.Driver;
        }

        /// <summary>
        /// Перенесен из GroupHelper
        /// Метод делается из обычного Clear-SendKeys выносом By.Name("group_name") и group.Name в локальные переменные,
        /// заменой ими в основной части и рефакторингом
        /// Делается один раз и используется для всех форм
        /// было так:
        /// driver.FindElement(By.Name("group_name")).Clear();
        /// driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        /// </summary>
        /// <param name="locator">бывший By.Name("group_name"), по которому искалось поле, куда потом записывалось значение</param>
        /// <param name="text">бывший group.Name, где передавалось значение для хедера\футера</param>
        public void Type(By locator, string text)
        {
            ///Если в поле text (значение футера\хедера или любого друго поля не NULL, то его надо очистить и запонить, иначе не трогать
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }

        /// <summary>
        /// Проверка наличия элемента
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        /// <summary>
        /// Для закрытия алёрта при удалении контактов
        /// Если надоест, это можно убрать
        /// и использовать driver.SwitchTo().Alert().Accept(); в ContactHelper
        /// </summary>
        /// <returns></returns>
        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        public static Random rnd = new Random();

        /// <summary>
        /// Рандомайзер
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int max)
        {
            // Генератор, где можно указать свой набор данных
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=-";
            return new string(Enumerable.Repeat(chars, max)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Генерация числа. Даже работает;)
        /// Возможно есть более корректный вариант, чем перечисление
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomDay()
        {
            string[] RD = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", 
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", 
                "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            int DD = rnd.Next(RD.Length);
            return RD[DD];
        }

        /// <summary>
        /// Генерация месяца. Даже работает;)
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomMonth()
        {
            string[] RM = { "January", "February", "March", "April", "May", "June", "July", 
                            "August", "September", "October", "November", "December" };
            int MM = rnd.Next(RM.Length);
            return RM[MM];
        }

        /// <summary>
        /// Генерация года. Пока не рабтает :(
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomYear()
        {
            int year = rnd.Next(1900, 2021);
            return year;
        }
    }
}