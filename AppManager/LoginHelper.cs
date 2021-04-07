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
            /// в поля вносятся значения свойств объекта (account.Username). Передаем параметр как объект
            Type(By.Name("user"), account.Username);
            /// в поля вносятся значения свойств объекта (account.Password)
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
    }
}
