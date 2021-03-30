//Нужен для управления помощниками

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
    public class ApplicationManager
    {
        //взяли все ссылки на помощников из TestBase

        //поле driver. Используется в помощниках
        protected IWebDriver driver;
        //поле baseURL, задается ниже
        protected string baseURL;

        //это ссылки на помощников
        //добавляем поле loginHelper, которое используется в методе SetUp для логина (имя должно отличаться (L и l)
        protected LoginHelper loginHelper;
        //добавляем поле navigator, которое используется в методе SetUp для навигации (имя должно отличаться (N и n)
        protected NavigationHelper navigator;
        //добавляем поле groupHelper, которое используется в методе SetUp для всего по группам (имя должно отличаться (G и g)
        protected GroupHelper groupHelper;

        //Инициализируем помощников с помощью конструктора (тоже просто скопировали из TestBase)
        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";

            //вместо ссылки на драйвер (в скобках) передается ссылка на ApplicationManager
            //подключаем помощника по логинам из LoginHelper
            loginHelper = new LoginHelper(this);
            //подключаем помощника по навигации и NavgationHelper
            navigator = new NavigationHelper(this, baseURL);
            //подключаем помощника по всему остальному из GroupHelper
            groupHelper = new GroupHelper(this);
        }

        //property для HelperBase для получения ссылки на driver
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        //Метод для остановки браузера
        public void Stop()
        {
            {
                try
                {
                    driver.Quit();
                }
                catch (Exception)
                {
                    // Ignore errors if unable to close the browser
                }
            }
        }

        //для доступа тестов к АппМанагеру делаем проперти только с set-тером
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }
        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }
}
