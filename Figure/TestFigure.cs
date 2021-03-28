using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Addressbook
{
    [TestClass]
    public class TestFigure
    {
        [TestMethod]
        public void TestSquare()
        {
            Square s1 = new Square(5);
            Square s2 = new Square(10);
            Square s3 = s1;

            //С учетом property из Square, код будет переписан
            /* 
            //Проверяем, что размер первого квадрата равен 5. Ошибка, если не указать getSize
            Assert.AreEqual(s1.getSize(), 5);
            Assert.AreEqual(s2.getSize(), 10);
            //Ошибка, сли указать s1(для s3)
            Assert.AreEqual(s3.getSize(), 5);
            //изменили размер s3
            s3.setSize(15);
            //проверяем, что s1 (тк оно равно s3) тоже теперь равно 15
            Assert.AreEqual(s1.getSize(), 15);
            */

            //А вот и новый вариант с учетом property
            //Проверяем, что размер первого квадрата равен 5. Ошибка, если не указать getSize
            Assert.AreEqual(s1.Size, 5);
            Assert.AreEqual(s2.Size, 10);
            //Ошибка, сли указать s1(для s3)
            Assert.AreEqual(s3.Size, 5);
            //изменили размер s3
            s3.Size = 15;
            //проверяем, что s1 (тк оно равно s3) тоже теперь равно 15
            Assert.AreEqual(s1.Size, 15);
        }

        [TestMethod]
        public void TestCircle()
        {
            //все аналогично квадрату
            Circle s1 = new Circle(5);
            Circle s2 = new Circle(10);
            Circle s3 = s1;

            Assert.AreEqual(s1.Radius, 5);
            Assert.AreEqual(s2.Radius, 10);

            Assert.AreEqual(s3.Radius, 5);

            s3.Radius = 15;

            Assert.AreEqual(s1.Radius, 15);

            //проверка на цвет. Задаем значение и сравниваем текущее состояние с ожидаемым
            s2.Colored = true;
            Assert.AreEqual(s2.Colored, true);
        }
    }
}
