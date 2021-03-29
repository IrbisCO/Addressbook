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
    public class NavigationHelper : HelperBase
    {
        //создается поле типа IWebDriver, но теперь это в HelperBase
        //создаем поле типа string для baseURL
        private string baseURL;

        //чтобы было видно driver. надо создать конструктор, в качестве параметра передается driver, только назвать его соотвественно
        //и теперь, в отличии от LoginHelper, тут передается два параметра
        //так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        public NavigationHelper(IWebDriver driver, string baseURL) : base(driver)
        {
            //в поле присваиваем значение, которое передано как параметр
            this.baseURL = baseURL;
        }

        //страница со входом
        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        //Страница групп
        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
    }
}
