namespace BDDTestCasesforBP.StepDefinitions
{ 
using NUnit.Framework;
using TechTalk.SpecFlow;

[Binding]
    public class CalculatorStepDefinitions
    {
        private int firstNumber;
        private int secondNumber;
        private int result;

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            firstNumber = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            secondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            result = AddNumbers(firstNumber, secondNumber);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            Assert.AreEqual(expectedResult, result);
        }

        private int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }

}

