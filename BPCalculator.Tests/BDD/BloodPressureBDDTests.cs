using Xunit;
using BPCalculator;

namespace BPCalculator.Tests.BDD
{
    public class BloodPressureBDDTests
    {
        [Fact]
        public void GivenLowValues_WhenICheckCategory_ThenItIsLow()
        {
            // Given
            var bp = new BloodPressure { Systolic = 85, Diastolic = 55 };

            // When
            var category = bp.Category;

            // Then
            Assert.Equal(BPCategory.Low, category);
        }

        [Fact]
        public void GivenIdealValues_WhenICheckCategory_ThenItIsIdeal()
        {
            var bp = new BloodPressure { Systolic = 110, Diastolic = 75 };
            var category = bp.Category;
            Assert.Equal(BPCategory.Ideal, category);
        }

        [Fact]
        public void GivenPreHighValues_WhenICheckCategory_ThenItIsPreHigh()
        {
            var bp = new BloodPressure { Systolic = 130, Diastolic = 85 };
            var category = bp.Category;
            Assert.Equal(BPCategory.PreHigh, category);
        }

        [Fact]
        public void GivenHighValues_WhenICheckCategory_ThenItIsHigh()
        {
            var bp = new BloodPressure { Systolic = 150, Diastolic = 95 };
            var category = bp.Category;
            Assert.Equal(BPCategory.High, category);
        }
    }
}
