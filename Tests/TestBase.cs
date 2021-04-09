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
            ///инициализация ApplicationManager, так как все теперь там
            ///обращаемся к GetInstance, чтобы получить доступ к единственному экземпляру ApplicationManager
            app = ApplicationManager.GetInstance();
        }
    }
}
