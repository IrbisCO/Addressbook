/// Удаление группы

using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            /// Данные для заполнения группы при создании группы для удаления
            GroupData group = new GroupData("aaa");
            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Groups.Remove(1, group);
        }
    }
}
