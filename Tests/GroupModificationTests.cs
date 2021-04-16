/// Модафикация группы

using NUnit.Framework;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            ///новые данные для модификации. Взяли из GroupCreationTests и заменили название на newData
            GroupData newData = new GroupData("www");
            /// Добавил данные для group, чтобы заполнять контакт, если его нет. Возможно есть вариант лучше
            GroupData group = new GroupData("aaa");
            ///поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнены дефолтными значениями
            ///если написано NULL, то с полем не выполняется каких-либо действий
            newData.Header = null;
            newData.Footer = null;
            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Groups.Modify(0, newData, group);
        }
    }
}
