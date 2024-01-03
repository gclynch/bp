//using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using BPCalculator;
using System;

[Binding]
public class BPCalculatorSteps
{

    private BloodPressure BPCalculator;
    //BPCalculator == "";
    //BPCalculator.BloodPressure bp = new BPCalculator.BloodPressure();
    private BPCategory Category;

    [Given(@"a blood pressure with systolic value (\d+) and diastolic value (\d+)")]
    public void GivenABloodPressureWithSystolicValueAndDiastolicValue(int systolic, int diastolic)
    {
        BPCalculator = new BloodPressure
        {
            Systolic = 90,
            Diastolic = 76
        };
    }

    [When(@"I categorize the blood pressure")]
    public void WhenICategorizeTheBloodPressure()
    {
        Category = BPCalculator.Category;
    }

    [Then(@"the category should be low")]
    public void ThenTheCategoryShouldBe(string expectedCategory)
    {
        var expectedBPCategory = Enum.Parse<BPCategory>(expectedCategory);
        Assert.AreEqual(expectedBPCategory, Category);
    }
}

