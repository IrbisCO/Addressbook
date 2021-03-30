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

        //метод принимает один параметр типа AccountData, назвать можно как угодно (хоть AccountData irbiska)
        public void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            //в поля вносятся значения свойств объекта (account.Username). Передаем параметр как объект
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).Clear();
            //в поля вносятся значения свойств объекта (account.Password)
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
    }
}
