namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private int systolicPressure;
        private int diastolicPressure;
        private string result="";

        [Given("a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
        public void GivenAPersonWithBloodPressure(int systolic, int diastolic)
        {
            systolicPressure = systolic;
            diastolicPressure = diastolic;
        }

        [When("their blood pressure is measured")]
        public void WhenTheirBloodPressureIsMeasured()
        {
            string result = MeasureBloodPressure(systolicPressure, diastolicPressure);
        }

        [Then("the result should be High Blood Pressure")]
        public void ThenTheResultShouldBeHighBloodPressure()
        {
            Assert.Equals("High Blood Pressure", result);
        }

        [Then("the result should be Pre-High Blood Pressure")]
        public void ThenTheResultShouldBePreHighBloodPressure()
        {
            Assert.Equals("Pre-High Blood Pressure", result);
        }

        [Then("the result should be Ideal Blood Pressure")]
        public void ThenTheResultShouldBeIdealBloodPressure()
        {
            Assert.Equals("Ideal Blood Pressure", result);
        }

        [Then("the result should be Low Blood Pressure")]
        public void ThenTheResultShouldBeLowBloodPressure()
        {
            Assert.Equals("Low Blood Pressure", result);
        }

        private string MeasureBloodPressure(int systolic, int diastolic)
        {
            // Placeholder logic for measuring blood pressure
            // You should implement the actual logic based on your system
            if (systolic >= 140 && diastolic >= 90)
            {
                return "High Blood Pressure";
            }
            else if (systolic >= 130 && diastolic >= 85)
            {
                return "Pre-High Blood Pressure";
            }
            else if (systolic >= 90 && diastolic >= 60)
            {
                return "Ideal Blood Pressure";
            }
            else
            {
                return "Low Blood Pressure";
            }
        }
    }
}
