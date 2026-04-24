using Xunit;

namespace Controllers.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_Passes()
        {
            // Arrange
            int a = 1;
            int b = 1;
            
            // Act
            int result = a + b;
            
            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Test2_Passes()
        {
            // Arrange
            string text = "Hello World";
            
            // Act
            int length = text.Length;
            
            // Assert
            Assert.Equal(11, length);
        }

        [Fact]
        public void Test3_Passes()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };
            
            // Act
            int count = list.Count;
            
            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public void Test4_AssertTrue()
        {
            // Arrange & Act
            bool condition = true;
            
            // Assert
            Assert.True(condition);
        }

        [Fact]
        public void Test5_AssertNotNull()
        {
            // Arrange & Act
            object obj = new object();
            
            // Assert
            Assert.NotNull(obj);
        }
    }
}
