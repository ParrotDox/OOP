using PointLibrary;

namespace UnitTestsPoint
{
    public class Point_tests
    {
        //Тесты на проверку класса Point

        //Проверка конструктора при ожидаемых данных
        [Theory]
        [InlineData(100, 100)]
        [InlineData(-100, 100)]
        [InlineData(100, -100)]
        [InlineData(-100, -100)]
        [InlineData(0, 0)]
        [InlineData(1.2, 5.2)]
        public void Constructor_Point_ValuesAreProper<T>(T x, T y)
        {
            //Arrange

            //Act
            var point = new Point<T>(x, y);
            //Assert
            Assert.NotNull(point);
            Assert.Equal(Convert.ToDouble(x), point.X);
            Assert.Equal(Convert.ToDouble(y), point.Y);
        }

        //Проверка конструктора при неверных параметрах
        [Theory]
        [InlineData("string", "string")]
        [InlineData('3', '1')]
        [InlineData(null, null)]
        public void Constructor_Point_ValuesAreWrong<T>(T x, T y)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<Exception>(() => new Point<T>(x, y));
        }

        //Проверка пустого конструктора
        [Fact]
        public void Constructor_Point_Empty()
        {
            //Arrange
            Point<double> point = new Point<double>();
            //Act
            //Assert
            Assert.NotNull(point);
            Assert.Equal(0, point.X);
            Assert.Equal(0, point.Y);
        }

        //Проверка метода FindDistance
        [Fact]
        public void Find_Distance()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            Point<double> point2 = new Point<double>(3, 4);
            double expectedResult = 5;
            //Act
            double result = point1.FindDistance(point2);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        //Проверка приведений
        [Fact]
        public void TypeCast_INT()
        {
            //Arrange
            Point<double> point = new Point<double>(3.45, 4);
            int expectedX = 3;
            //Act
            int result = (int)point;
            //Assert
            Assert.IsAssignableFrom<int>((int)point);
            Assert.IsType<int>((int)point);
            Assert.Equal(expectedX, result);
        }
        [Fact]
        public void TypeCast_DOUBLE()
        {
            //Arrange
            Point<double> point = new Point<double>(3.45, 4.78);
            double expectedX = 4.78;
            //Act
            double result = point;
            //Assert
            Assert.IsAssignableFrom<double>(result);
            Assert.IsType<double>(result);
            Assert.Equal(expectedX, result);
        }
        //Бинарные операции
        [Fact]
        public void BinaryOperation_Increment_POINT_POINT()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            Point<double> point2 = new Point<double>(3, 4);
            double expectedResult = 5;
            //Act
            double result = point1 + point2;
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void BinaryOperation_Increment_POINT_INT()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            int value = 2;
            double expectedResult = 2;
            //Act
            double result = (point1 + value).X;
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void BinaryOperation_Increment_INT_POINT()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            int value = 2;
            double expectedResult = 2;
            //Act
            double result = (value + point1).X;
            //Assert
            Assert.Equal(expectedResult, result);
        }
        //Унарные операции
        [Fact]
        public void UnaryOperation_Increment()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            double expectedX = 1;
            //Act
            ++point1;
            //Assert
            Assert.Equal(expectedX, point1.X);
        }
        [Fact]
        public void UnaryOperation_Decrement()
        {
            //Arrange
            Point<double> point1 = new Point<double>(0, 0);
            double expectedX = -1;
            //Act
            --point1;
            //Assert
            Assert.Equal(expectedX, point1.X);
        }

        //Тесты на проверку класса PointArray
        //Проверка конструктора при ожидаемых данных
        [Theory]
        [InlineData(5, false)]
        [InlineData(0, false)]
        [InlineData(50, false)]
        public void Constructor_PointArray_ValuesAreProper<T>(T x, bool y) 
        {
            //Arrange
            //Act
            var pointArray = new PointArray<T>(x, y);
            //Assert
            Assert.NotNull(pointArray);
            Assert.Equal(Convert.ToUInt32(x), pointArray.length);
        }
        //Проверка конструктора при неверных параметрах
        [Theory]
        [InlineData(-20, false)]
        [InlineData(0.35, false)]
        [InlineData(-0.23, false)]
        [InlineData("Abobroo!", false)]
        [InlineData('-', false)]
        public void Constructor_PointArray_ValuesAreWrong<T>(T x, bool y)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<Exception>(() => new PointArray<T>(x, y));
        }
        //Проверка пустого конструктора
        [Fact]
        public void Constructor_PointArray_Empty()
        {
            //Arrange
            PointArray<double> pointArray = new PointArray<double>();
            //Act
            //Assert
            Assert.NotNull(pointArray);
            Assert.Equal((uint)Convert.ToInt32(0), pointArray.length);
        }
        //Проверка индексатора
        [Fact]
        public void Indexator_PointArray()
        {
            //Arrange
            PointArray<double> pointArray = new PointArray<double>(5, false);
            //Act
            Point<double> currentPoint = pointArray[0];
            //Assert
            Assert.IsType<Point<double>>(pointArray[2]);
            Assert.Equal(pointArray[0], currentPoint);
        }
    }
}