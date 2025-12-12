using Xunit;
using BPCalculator;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(80, 70, BPCategory.Low)]
        public void Category_ReturnsLow(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(expected, bp.Category);
        }

        [Theory]
        [InlineData(100, 70, BPCategory.Ideal)]
        [InlineData(110, 75, BPCategory.Ideal)]
        public void Category_ReturnsIdeal(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(expected, bp.Category);
        }

        [Theory]
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(125, 88, BPCategory.PreHigh)]
        public void Category_ReturnsPreHigh(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(expected, bp.Category);
        }

        [Theory]
        [InlineData(150, 95, BPCategory.High)]
        [InlineData(140, 70, BPCategory.High)]
        public void Category_ReturnsHigh(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(expected, bp.Category);
        }
    }
}
