using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class LoginHelper : HelperBase
    {
        //создается поле типа IWebDriver, но теперь это в HelperBase

        //чтобы было видно driver. надо создать конструктор, в качестве параметра передается driver
        //так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        public LoginHelper (ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// метод принимает один параметр типа AccountData
        /// </summary>
        /// <param name="account"></param>
        public void Login(AccountData account)
        {
            ///Если залогинен 
            if (IsLoggedIn())
            {
                ///Если залогинен под учеткой, переданной в качестве параметра
                if (IsLoggedIn(account))
                {
                    ///то все ок
                    return;
                }
                ///Если неправильная учетка - Logout
                Logout();
            }
            /// в поля вносятся значения свойств объекта (account.Username). Передаем параметр как объект
            Type(By.Name("user"), account.Username);
            /// в поля вносятся значения свойств объекта (account.Password)
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        /// <summary>
        /// Выход
        /// </summary>
        public void Logout()
        {
            ///Проверка залогинен ли
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }

        /// <summary>
        /// Проверка залогинен ли
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
            ///поиск кнопки logout с помощью IsElementPresent
            return IsElementPresent(By.Name("logout"));
        }

        /// <summary>
        /// Проверка залогинен ли под правильной учеткой
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsLoggedIn(AccountData account)
        {
            /// Проверка, что залогинен и имя указанное в скобках равно данным из account 
            return IsLoggedIn()
                && driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text == "(" + account.Username + ")";
        }
    }
}
