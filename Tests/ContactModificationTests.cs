/// Модафикация контакта

using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ///новые данные для модификации. Взяли из ContactCreationTests и заменили название на newData
            ContactData newData = new ContactData("Name1", "Surname1");
            /// Добавил данные для contact, чтобы заполнять контакт, если его нет. Возможно есть вариант лучше
            ContactData contact = new ContactData("Name", "Surname");
            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Contacts.Modify(1, newData, contact);
        }
    }
}
