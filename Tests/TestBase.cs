///Базовый класс всея тестов

using NUnit.Framework;
using System;
using System.Text;
using WebAddressbookTests.AppManager;

namespace WebAddressbookTests.Tests
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

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        public static Random rnd = new Random();

        /// <summary>
        /// Рандомайзер
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int max)
        {
            /// Число от 0 до max
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            /// Формирование строки
            StringBuilder builder = new StringBuilder();
            /// Генерация l различных символов
            for (int i = 0; i < l; i++)
            {
                /// Добавление случайного числа в builder
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            /// Возвращаем слцчайную строку
            return builder.ToString();
        }
    }
}
