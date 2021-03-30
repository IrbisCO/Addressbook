//Базовый класс

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    //перенесли это + SetUp + TearDown из основных тестов, так как это повторяется
    public class TestBase
    {
        //так как все ссылки перенесены в ApplicationManager, то создаем ссылку на него
        protected ApplicationManager app;

        [SetUp]
        //метод для инициализации (драйвер, ссылка на главную, помощники)
        public void SetupTest()
        {
            //инициализация ApplicationManager, так как все теперь там
            app = new ApplicationManager();
            //так как переход на главную и авторизация происходят всегда, то эта часть вынесена сюда
            //через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app,Groups)
            app.Navigator.GoToHomePage();
            //передается один объект AccountData
            //говорим тесту, что у него есть помощник
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        //останавливает браузер в конце
        public void TeardownTest()
        {
            //ccылка на AppManager
            app.Stop();
        }
    }
}
