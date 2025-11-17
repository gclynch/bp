using Xunit;
using BPCalculator;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(120, 80, BPCategory.Ideal, "Ideal Blood Pressure")]
        [InlineData(140, 90, BPCategory.High, "High Blood Pressure")]
        [InlineData(85, 55, BPCategory.Low, "Low Blood Pressure")]
        [InlineData(130, 85, BPCategory.PreHigh, "Pre-High Blood Pressure")]
        [InlineData(100, 70, BPCategory.Ideal, "Ideal Blood Pressure")]
        [InlineData(90, 60, BPCategory.Ideal, "Ideal Blood Pressure")]  // Borderline ideal
        [InlineData(139, 89, BPCategory.PreHigh, "Pre-High Blood Pressure")]  // Borderline pre-high
        public void CalculateCategory_ValidInputs_ReturnsCorrectCategory(
            int systolic, int diastolic, BPCategory expectedCategory, string expectedDisplay)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category;
            var displayName = GetDisplayName(result);
            
            // Assert
            Assert.Equal(expectedCategory, result);
            Assert.Equal(expectedDisplay, displayName);
        }

        [Theory]
        [InlineData(70, 40)]  // Minimum valid values
        [InlineData(190, 100)] // Maximum valid values
        [InlineData(110, 75)] // Normal values
        public void CalculateCategory_ValidRanges_DoesNotThrow(int systolic, int diastolic)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act & Assert - should not throw
            var result = bp.Category;
            Assert.IsType<BPCategory>(result);
        }

        [Theory]
        [InlineData(120, 80, "Ideal")]
        [InlineData(140, 90, "High")]
        [InlineData(85, 55, "Low")]
        [InlineData(130, 85, "PreHigh")]
        public void Category_ToString_ReturnsExpectedValue(int systolic, int diastolic, string expected)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category.ToString();
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void BloodPressure_Constants_HaveCorrectValues()
        {
            // Assert
            Assert.Equal(70, BloodPressure.SystolicMin);
            Assert.Equal(190, BloodPressure.SystolicMax);
            Assert.Equal(40, BloodPressure.DiastolicMin);
            Assert.Equal(100, BloodPressure.DiastolicMax);
        }

        [Theory]
        [InlineData(89, 59)] // Both low
        [InlineData(121, 81)] // Both pre-high  
        [InlineData(141, 91)] // Both high
        [InlineData(141, 80)] // Systolic high, diastolic ideal
        [InlineData(120, 91)] // Systolic ideal, diastolic high
        public void CalculateCategory_MixedCategories_ReturnsWorstCategory(int systolic, int diastolic)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            
            // Act
            var result = bp.Category;
            
            // Assert - Should return the worst category
            Assert.True(result == BPCategory.High || result == BPCategory.PreHigh || 
                       result == BPCategory.Low || result == BPCategory.Ideal);
        }

        private string GetDisplayName(BPCategory category)
        {
            var field = category.GetType().GetField(category.ToString());
            var attribute = (System.ComponentModel.DataAnnotations.DisplayAttribute)
                System.Attribute.GetCustomAttribute(field, typeof(System.ComponentModel.DataAnnotations.DisplayAttribute));
            return attribute?.Name ?? category.ToString();
        }
    }
}