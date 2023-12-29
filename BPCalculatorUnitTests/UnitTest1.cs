using Microsoft.VisualStudio.TestTools.UnitTesting;

using BPCalculator;

namespace BPUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
         [TestMethod]
        public void LowBloodPressure()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 71, Diastolic = 41 };
            Assert.AreEqual(bp.Category, BPCategory.Low);
        }
        [TestMethod]
        public void LowBloodPressure_LowerValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 70, Diastolic = 40 };
            Assert.AreEqual(bp.Category, BPCategory.Low);
        }

        [TestMethod]
        public void LowBloodPressure_UpperValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 89, Diastolic = 59 };
            Assert.AreEqual(bp.Category, BPCategory.Low);
        }

        [TestMethod]
        public void IdealBloodPressure()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 91, Diastolic = 61 };
            Assert.AreEqual(bp.Category, BPCategory.Ideal);
        }
        [TestMethod]
        public void IdealBloodPressure_LowerValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 90, Diastolic = 60 };
            Assert.AreEqual(bp.Category, BPCategory.Ideal);
        }

        [TestMethod]
        public void IdealBloodPressure_UpperValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 119, Diastolic = 79 };
            Assert.AreEqual(bp.Category, BPCategory.Ideal);
        }

        [TestMethod]
        public void PreHighBloodPressure()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 121, Diastolic = 81 };
            Assert.AreEqual(bp.Category, BPCategory.PreHigh);
        }
        [TestMethod]

        public void PreHighBloodPressure_LowerValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 120, Diastolic = 80 };
            Assert.AreEqual(bp.Category, BPCategory.PreHigh);
        }

        [TestMethod]
        public void PreHighBloodPressure_UpperValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 139, Diastolic = 89 };
            Assert.AreEqual(bp.Category, BPCategory.PreHigh);
        }
        [TestMethod]
        public void HighBloodPressure()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 141, Diastolic = 91 };
            Assert.AreEqual(bp.Category, BPCategory.High);
        }
        [TestMethod]
        public void HighBloodPressure_LowerValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 140, Diastolic = 90 };
            Assert.AreEqual(bp.Category, BPCategory.High);
        }

        [TestMethod]
        public void HighBloodPressure_UpperValues()
        {
            BloodPressure bp = new BloodPressure() { Systolic = 190, Diastolic = 100 };
            Assert.AreEqual(bp.Category, BPCategory.High);
        }


        [TestMethod]
        public void InvalidRange_LowerValues()
        {
        Random r = new Random();
        int rSysytolic = r.Next(0, 69);
        int rDiastolic = r.Next(0, 39);
        BloodPressure bp = new BloodPressure() { Systolic = rSysytolic, Diastolic = rDiastolic };
        Assert.AreEqual(bp.Category, BPCategory.Empty);
        }
        [TestMethod]
        public void InvalidRange_HigherValues()
        {
            Random r = new Random();
            int rSysytolic = r.Next(191, 100000);
            int rDiastolic = r.Next(101, 100000);
            BloodPressure bp = new BloodPressure() { Systolic = rSysytolic, Diastolic = rDiastolic };
            Assert.AreEqual(bp.Category, BPCategory.Empty);
        }
    }
}
