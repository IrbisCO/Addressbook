///Базовый класс всея тестов

using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using WebAddressbookTests.AppManager;

namespace WebAddressbookTests.Tests
{
    /// <summary>
    /// перенесли это + SetUp + TearDown из основных тестов, так как это повторяется
    /// </summary>
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;
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
