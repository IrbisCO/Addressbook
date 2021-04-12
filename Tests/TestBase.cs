///Базовый класс всея тестов

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    /// <summary>
    /// перенесли это + SetUp + TearDown из основных тестов, так как это повторяется
    /// </summary>
    public class TestBase
    {
        /// <summary>
        /// так как все ссылки перенесены в ApplicationManager, то создаем ссылку на него
        /// </summary>
        protected ApplicationManager app;

        [SetUp]
        ///метод для инициализации (драйвер, ссылка на главную, помощники)
        public void SetupApplicationManager()
        {
            ///инициализация ApplicationManager, так как все теперь там
            ///обращаемся к GetInstance, чтобы получить доступ к единственному экземпляру ApplicationManager
            app = ApplicationManager.GetInstance();
        }
    }
}
