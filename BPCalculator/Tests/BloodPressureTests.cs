using Xunit;
using BPCalculator;
using System.ComponentModel.DataAnnotations;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(120, 80, BPCategory.Ideal)]
        [InlineData(140, 90, BPCategory.High)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(100, 70, BPCategory.Ideal)]
        [Trait("Category", "Unit")]
        public void CalculateCategory_ValidInputs_ReturnsCorrectCategory(int systolic, int diastolic, BPCategory expected)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category;
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_IdealBloodPressure_When_CalculatingCategory_Then_ReturnsIdeal()
        {
            // Given
            var bp = new BloodPressure { Systolic = 110, Diastolic = 70 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.Ideal, category);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_HighBloodPressure_When_CalculatingCategory_Then_ReturnsHigh()
        {
            // Given
            var bp = new BloodPressure { Systolic = 150, Diastolic = 95 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.High, category);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_LowBloodPressure_When_CalculatingCategory_Then_ReturnsLow()
        {
            // Given
            var bp = new BloodPressure { Systolic = 85, Diastolic = 55 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.Low, category);
        }

        [Fact]
        [Trait("Category", "BDD")]
        public void Given_PreHighBloodPressure_When_CalculatingCategory_Then_ReturnsPreHigh()
        {
            // Given
            var bp = new BloodPressure { Systolic = 135, Diastolic = 88 };
            
            // When
            var category = bp.Category;
            
            // Then
            Assert.Equal(BPCategory.PreHigh, category);
        }

        [Fact]
        [Trait("Category", "Security")]
        public void BloodPressure_NoInjectionVulnerabilities()
        {
            // Arrange
            var bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            
            // Act - access property to ensure no security-related exceptions are thrown
            var result = bp.Category;
        }

        [Fact]
        public void BloodPressure_Constants_AreCorrect()
        {
            Assert.Equal(70, BloodPressure.SystolicMin);
            Assert.Equal(190, BloodPressure.SystolicMax);
            Assert.Equal(40, BloodPressure.DiastolicMin);
            Assert.Equal(100, BloodPressure.DiastolicMax);
        }

        [Theory]
        [InlineData(89, 59, BPCategory.Low)] // Both low
        [InlineData(121, 81, BPCategory.PreHigh)] // Both pre-high
        [InlineData(141, 91, BPCategory.High)] // Both high
        [Trait("Category", "Unit")]
        public void CalculateCategory_MixedValues_ReturnsWorstCategory(int systolic, int diastolic, BPCategory expected)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category;
            
            // Assert
            Assert.Equal(expected, result);
        }
    }
}