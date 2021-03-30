//этот класс создан, чтобы одинаковый код из помощников переместить сюда
//Базовый класс для помощников 

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
    public class HelperBase
    {
        //вот это самое поле driver для хелперов
        protected IWebDriver driver;
        protected ApplicationManager manager;

        //конструктор. На вход принимает ссылку на Application manager
        public HelperBase(ApplicationManager manager)
        {
            //сохраняем ссылку на manager, чтобы каждый помощник про него знал
            this.manager = manager;
            //получает ссылку на Driver у менеджера и все этой ссылкой могут пользоваться
            driver = manager.Driver;
        }
    }
}