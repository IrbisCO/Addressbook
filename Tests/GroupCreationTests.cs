//7 видео 1 урока 13 минута

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            //передаем значения в зависимости от конструктора. Если выбран первый (где только name, то передаем только "aaa"
            //а если выбран второй, то надо передать все  три значения (теперь в нем нет смысла)
            //FillGroupForm(new GroupData("aaa", "sss", "ddd"));

            //1. Инициализация тестовых данных
            //HO если значений много, то можно обойтись без конструктора и передать параметры следующим образом:
            //создается группа с неким именем ("ааа")
            //теперь конструктор из GroupData со всеми параметрами не нужен, так как он слишком громоздкий
            GroupData group = new GroupData("aaa");
            //поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнен дефолтными значениями
            group.Header = "sss";
            group.Footer = "ddd";
            //логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            //через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app,Groups)
            //2. Единсвенное действие
            //все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);
        }

        [Test]
        //описание см GroupCreationTest()
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            app.Groups.Create(group);
        }
    }
}
