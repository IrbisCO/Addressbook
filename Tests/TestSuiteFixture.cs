using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [SetUpFixture]
    /// <summary>
    /// Общая ситуация для всех тестов
    /// </summary>
    public class TestSuiteFixture
    {
        /// <summary>
        /// поле app типа ApplicationManager
        /// Чтобы все видели, делаем глобальной - static
        /// </summary>
        
        [OneTimeSetUp]
        ///метод для инициализации (драйвер, ссылка на главную, помощники)
        public void InitApplicationManager()
        {
            ///создается AppManager через поле app, обращаясь к GetInstance 
            ApplicationManager app = ApplicationManager.GetInstance();
            ///так как переход на главную и авторизация происходят всегда, то эта часть вынесена сюда
            ///через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app,Groups)
            app.Navigator.GoToHomePage();
            ///передается один объект AccountData
            ///говорим тесту, что у него есть помощник
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
