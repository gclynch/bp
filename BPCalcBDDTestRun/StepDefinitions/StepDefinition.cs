/*using System;
	using TechTalk.SpecFlow;

		namespace BPCalcBDDTestRun.StepDefinitions
{
		[Binding]
		public class StepDefinitions
		{
			private readonly ScenarioContext _scenarioContext;
	
			public StepDefinitions(ScenarioContext scenarioContext)
			{
				_scenarioContext = scenarioContext;
			}
			
[Given(@"a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
public void Givenapersonwithsystolicbloodpressureanddiastolicbloodpressure(int args1,int args2)
{
	_scenarioContext.Pending();
}

[When(@"their blood pressure is measured")]
public void Whentheirbloodpressureismeasured()
{
	_scenarioContext.Pending();
}

[Then(@"the result should be High Blood Pressure")]
public void ThentheresultshouldbeHighBloodPressure()
{
	_scenarioContext.Pending();
}

[Given(@"a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
public void Givenapersonwithsystolicbloodpressureanddiastolicbloodpressure1(int args1,int args2)
{
	_scenarioContext.Pending();
}

[When(@"their blood pressure is measured")]
public void Whentheirbloodpressureismeasured1()
{
	_scenarioContext.Pending();
}

[Then(@"the result should be Pre-High Blood Pressure")]
public void ThentheresultshouldbePreHighBloodPressure1()
{
	_scenarioContext.Pending();
}

[Given(@"a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
public void Givenapersonwithsystolicbloodpressureanddiastolicbloodpressure2(int args1,int args2)
{
	_scenarioContext.Pending();
}

[When(@"their blood pressure is measured")]
public void Whentheirbloodpressureismeasured2()
{
	_scenarioContext.Pending();
}

[Then(@"the result should be Ideal Blood Pressure")]
public void ThentheresultshouldbeIdealBloodPressure2()
{
	_scenarioContext.Pending();
}

[Given(@"a person with systolic blood pressure (.*) and diastolic blood pressure (.*)")]
public void Givenapersonwithsystolicbloodpressureanddiastolicbloodpressure3(int args1,int args2)
{
	_scenarioContext.Pending();
}

[When(@"their blood pressure is measured")]
public void Whentheirbloodpressureismeasured3()
{
	_scenarioContext.Pending();
}

[Then(@"the result should be Low Blood Pressure")]
public void ThentheresultshouldbeLowBloodPressure3()
{
	_scenarioContext.Pending();
}

		}
	}*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
namespace BPCalcBDDTestRun.StepDefinitions
{
    [TestClass]
    public class StepDefinitions
    {
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
            result = MeasureBloodPressure(systolicPressure, diastolicPressure);
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