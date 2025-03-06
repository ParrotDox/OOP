using ClassIerarchyLib;

namespace UnitTests
{
    public class LINQHashTableTests
    {
        [Fact]
        public void WhereCustom_ShouldFilterElementsByCondition()
        {
            // Arrange
            var table = new NewCustomHashTable<int, int>(10);
            table.Add(1, 10);
            table.Add(2, 20);
            table.Add(3, 30);
            table.Add(4, 40);

            // Act
            var result = table.WhereCustom(pair => pair.Value > 20).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Key == 3 && p.Value == 30);
            Assert.Contains(result, p => p.Key == 4 && p.Value == 40);
            Assert.DoesNotContain(result, p => p.Key == 1 && p.Value == 10);
            Assert.DoesNotContain(result, p => p.Key == 2 && p.Value == 20);
        }

        [Fact]
        public void AggregateCustom_ShouldSumValuesCorrectly()
        {
            // Arrange
            var table = new NewCustomHashTable<int, int>(10);
            table.Add(1, 10);
            table.Add(2, 20);
            table.Add(3, 30);

            // Act
            double sum = table.AggregateCustom(pair => pair.Value, (total, next) => total + next);

            // Assert
            Assert.Equal(60, sum);
        }

        [Fact]
        public void OrderByCustom_ShouldSortAscendingByDefault()
        {
            // Arrange
            var table = new NewCustomHashTable<int, int>(10);
            table.Add(1, 30);
            table.Add(2, 10);
            table.Add(3, 20);

            // Act
            var result = table.OrderByCustom(pair => pair.Value).ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(10, result[0].Value); // 10
            Assert.Equal(20, result[1].Value); // 20
            Assert.Equal(30, result[2].Value); // 30
        }

        [Fact]
        public void OrderByCustom_ShouldSortDescendingWhenSpecified()
        {
            // Arrange
            var table = new NewCustomHashTable<int, int>(10);
            table.Add(1, 30);
            table.Add(2, 10);
            table.Add(3, 20);

            // Act
            var result = table.OrderByCustom(pair => pair.Value, descending: true).ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(30, result[0].Value); // 30
            Assert.Equal(20, result[1].Value); // 20
            Assert.Equal(10, result[2].Value); // 10
        }
    }
}