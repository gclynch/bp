using BPCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
namespace BPCalculator;

[Binding]
public class BloodPressureSteps
{
    private BloodPressure? bloodPressure;
    private BPCategory result;

    [Given("a person with systolic blood pressure (.*) and the diastolic blood pressure (.*)")]
    public void GivenAPersonWithBloodPressure(int systolic, int diastolic)
    {
        bloodPressure = new BloodPressure()
        {
            Systolic = systolic,
            Diastolic = diastolic
        };
    }

    [When(@"hbp blood pressure range is measured")]
    public void WhenHbpBloodPressureRangeIsMeasured()
    {
        result = bloodPressure.Category;
        
    }

    [Then(@"the result should be High Blood Pressure")]
    public void ThenTheResultShouldBeHighBloodPressure()
    {
        Assert.AreEqual(BPCategory.High, result);
    }
    

}
