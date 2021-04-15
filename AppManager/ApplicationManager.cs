///Нужен для управления помощниками

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace WebAddressbookTests.AppManager
{
    public class ApplicationManager
    {
        ///взяли все ссылки на помощников из TestBase

        /// <summary>
        /// поле driver. Используется в помощниках
        /// </summary>
        protected IWebDriver driver;
        /// <summary>
        /// поле baseURL, задается ниже
        /// </summary>
        protected string baseURL;

        ///это ссылки на помощников
        /// <summary>
        /// добавляем поле loginHelper, которое используется в методе SetUp для логина (имя должно отличаться (L и l)
        /// </summary>
        protected LoginHelper loginHelper;
        /// <summary>
        /// добавляем поле navigator, которое используется в методе SetUp для навигации (имя должно отличаться (N и n)
        /// </summary>
        protected NavigationHelper navigator;
        /// <summary>
        /// добавляем поле groupHelper, которое используется в методе SetUp для всего по группам (имя должно отличаться (G и g)
        /// </summary>
        protected GroupHelper groupHelper;
        /// <summary>
        /// добавляем поле contactHelper, которое используется в методе SetUp для всего по группам (имя должно отличаться (C и c)
        /// </summary>
        protected ContactHelper contactHelper;

        /// <summary>
        /// Единственный экземпляр ApllicationManager
        /// Специальный объект, который будет устанавливать соотвествие между текущим потоком и объктом типа ApplicationManager
        /// модификатор readonly добавлен по желанию VS, можно убрать если что
        /// </summary>
        private static readonly ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        /// <summary>
        /// Инициализируем помощников с помощью конструктора (тоже просто скопировали из TestBase)
        /// Конструктор приватный, чтобы за пределами класса ApplicationManager никто не мог создать другие объекты
        /// </summary>
        private ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";

            ///вместо ссылки на драйвер (в скобках) передается ссылка на ApplicationManager
            ///подключаем помощника по логинам из LoginHelper
            loginHelper = new LoginHelper(this);
            ///подключаем помощника по навигации и NavgationHelper
            navigator = new NavigationHelper(this, baseURL);
            ///подключаем помощника по всему остальному из GroupHelper
            groupHelper = new GroupHelper(this);
            ///подключаем помощника по всему остальному из ContactHelper
            contactHelper = new ContactHelper(this);
        }

        /// <summary>
        /// Деструктор. Нужен для избавления от метода Stop() и остановки браузера
        /// </summary>
        ~ApplicationManager()
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

        /// <summary>
        /// Singleton вместо глобальной переменной. Нужен для парраллельного запуска тестов
        /// При первом запуске инициализируется, запускается браузер и все такое, а потом уже используется ранее созданный объект
        /// </summary>
        /// <returns></returns>
        public static ApplicationManager GetInstance()
        {
            ///если для текщего потока внутри хранилища ничего не создано...
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                ///так как переход на главную и авторизация происходят всегда, то эта часть вынесена сюда
                ///через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app,Groups)
                newInstance.Navigator.GoToHomePage();
                ///...то нужно его создать
                ///и присвоить новый созданный объект в хранилище ThreadLocal
                app.Value = newInstance;

            }
            ///возвращается это самое значение (выше написано какое)
            return app.Value;
        }

        /// <summary>
        /// property для HelperBase для получения ссылки на driver
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        /// <summary>
        /// для доступа тестов к ApplicationManager делаем проперти только с set-тером
        /// 1. Доступ к авторизации
        /// </summary>
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        /// <summary>
        /// 2. Доступ к навигации по странице
        /// </summary>
        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }
        /// <summary>
        /// 3. Доступ к списку групп
        /// </summary>
        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
        /// <summary>
        /// 4. Доступ к списку контактов
        /// </summary>
        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
    }
}
