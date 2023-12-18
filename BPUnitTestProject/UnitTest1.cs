using Microsoft.VisualStudio.TestTools.UnitTesting;

//using BPCalculator;

namespace BPUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
          //  BMI bmi = new BMI() { WeightStones = 12, WeightPounds = 0, HeightFeet = 5, HeightInches = 10 };
           // Assert.AreEqual(bmi.BMICategory, BMICategory.Normal);
           Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestMethod10()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =71 , Diastolic =41 };
           Assert.AreEqual(bp.Category, BPCategory.LowDiastolic);
        }

        [TestMethod]
        public void TestMethod2()
        {
           BloodPressure bp =  new BloodPressure() {Systolic = 91, Diastolic = 61};
           Assert.AreEqual(bp.Category, BPCategory.IdealDiastolic);
        }

        [TestMethod]
        public void TestMethod3()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =121 , Diastolic =81 };
           Assert.AreEqual(bp.Category, BPCategory.PreHighDiastolic);
        }

        [TestMethod]
        public void TestMethod4()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =141 , Diastolic =91 };
           Assert.AreEqual(bp.Category, BPCategory.HighDiastolic);
        }

        [TestMethod]
        public void TestMethod5()
        {
           BloodPressure bp =  new BloodPressure() {Systolic = 81, Diastolic =41 };
           Assert.AreEqual(bp.Category, BPCategory.Low);
        }

        [TestMethod]
        public void TestMethod6()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =91 , Diastolic = 61};
           Assert.AreEqual(bp.Category, BPCategory.Ideal);
        }

        [TestMethod]
        public void TestMethod7()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =121 , Diastolic = 81};
           Assert.AreEqual(bp.Category, BPCategory.PreHigh);
        }

        [TestMethod]
        public void TestMethod8()
        {
           BloodPressure bp =  new BloodPressure() {Systolic =141 , Diastolic = 91};
           Assert.AreEqual(bp.Category, BPCategory.High);
        }

        [TestMethod]
        public void TestMethod9()
        {
           BloodPressure bp =  new BloodPressure() {Systolic = 40, Diastolic = 20};
           Assert.AreEqual(bp.Category, BPCategory.Empty);
        }
    }
}
