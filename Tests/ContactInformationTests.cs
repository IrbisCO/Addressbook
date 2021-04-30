/// Тест проверяет, что для некоторого отдельно взятого контакта информация на главной странице (в таблице) 
/// соответствует информации, представленной в форме редактирования контакта (где задаются все его свойства).

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {

        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            /// Verification
            /// 
            /// Сравнивается имя и фамилия, так как в ContactData только они и сравниваются в методе Equals
            Assert.AreEqual(fromTable, fromForm);
            /// Сравниваем адрес
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            /// Сравниваем телефоны
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }
    }
}