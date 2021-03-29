using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

//этот класс создан, чтобы одинаковый код из помощников переместить сюда

namespace WebAddressbookTests
{
    public class HelperBase
    {
        //вот это самое поле driver для хелперов
        protected IWebDriver driver;

        //конструктор. На вход принимает ссылку на driver, через который управляем браузером...
        public HelperBase(IWebDriver driver)
        {
            //...и присваивает ее в поле
            this.driver = driver;
        }
    }
}