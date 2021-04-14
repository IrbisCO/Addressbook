/// Создание группы
/// 7 видео 1 урока 13 минута

using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            ///передаем значения в зависимости от конструктора. Если выбран первый (где только name, то передаем только "aaa"
            /// а если выбран второй, то надо передать все  три значения (теперь в нем нет смысла)
            /// FillGroupForm(new GroupData("aaa", "sss", "ddd"));
            GroupData group = new GroupData("aaa");
            /// поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнен дефолтными значениями
            group.Header = "sss";
            group.Footer = "ddd";
            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Groups)
            /// Единсвенное действие
            /// все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);
        }

        [Test]
        /// описание см GroupCreationTest()
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            app.Groups.Create(group);
        }
    }
}
