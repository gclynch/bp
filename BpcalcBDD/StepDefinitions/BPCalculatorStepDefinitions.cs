using TechTalk.SpecFlow;
using NUnit.Framework;

[Binding]
public class BloodPressureSteps
{
    private int systolicPressure;
    private int diastolicPressure;
    private string result;

    [Given("a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
    public void GivenAPersonWithBloodPressure(int systolic, int diastolic)
    {
        systolicPressure = systolic;
        diastolicPressure = diastolic;
    }

    [When("their blood pressure is measured")]
    public void WhenTheirBloodPressureIsMeasured()
    {
        // Implementation for measuring blood pressure
        result = MeasureBloodPressure(systolicPressure, diastolicPressure);
    }

    [Then("the result should be High Blood Pressure")]
    public void ThenTheResultShouldBeHighBloodPressure()
    {
        NUnit.Framework.Assert.Equals("High Blood Pressure", result);
    }

    private string MeasureBloodPressure(int systolic, int diastolic)
    {
        // Placeholder logic for measuring blood pressure
        // You should implement the actual logic based on your system
        if (systolic >= 140 && diastolic >= 90)
        {
            return "High Blood Pressure";
        }
        else
        {
            return "Normal";
        }
    }
}
