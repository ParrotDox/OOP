using ClassIerarchyLib;

namespace UnitTests
{
    public class LINQHashTableTests
    {
        private readonly List<int> numbers = new() { 10, 5, 15, 20, 25 };

        //Test for WhereCustom (numbers > 10)
        [Fact]
        public void WhereCustom_ReturnNumbersGreaterThan()
        {
            var result = numbers.WhereCustom(n => n > 10).ToList();

            Assert.Equal(new List<int> { 15, 20, 25 }, result);
        }

        //Test for AggregateCustom (Sum of all numbers)
        [Fact]
        public void AggregateCustom_ReturnSumOfNumbers()
        {
            double result = numbers.AggregateCustom(n => (double)n, (sum, num) => sum + num);

            Assert.Equal(75, result);
        }

        //Test for AggregateCustom (multiplication of all numbers)
        [Fact]
        public void AggregateCustom_ReturnProductOfNumbers()
        {
            //First item that algorithm gets is 0, to prevent result being zero, using ternary operator
            double result = numbers.AggregateCustom(n => (double)n, (result, num) => result == 0 ? num : result * num);

            Assert.Equal(5 * 10 * 15 * 20 * 25, result);
        }

        //Test for OrderByCustom (Descending = false)
        [Fact]
        public void OrderByCustom_SortAscending()
        {
            var result = numbers.OrderByCustom(n => n).ToList();

            Assert.Equal(new List<int> { 5, 10, 15, 20, 25 }, result);
        }

        //Test for OrderByCustom (Descending = true)
        [Fact]
        public void OrderByCustom_SortDescending()
        {
            var result = numbers.OrderByCustom(n => n, true).ToList();

            Assert.Equal(new List<int> { 25, 20, 15, 10, 5 }, result);
        }
    }
}