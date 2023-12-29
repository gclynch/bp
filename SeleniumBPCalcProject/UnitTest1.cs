using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[TestClass]
public class BloodPressureTests
{
    IWebDriver? driver;

    [TestInitialize]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [TestMethod]
    public void TestHighBloodPressure()
    {
        TestBloodPressureCategory(160, 95, "High Blood Pressure");
    }

    [TestMethod]
    public void TestPreHighBloodPressure()
    {
        TestBloodPressureCategory(130, 85, "Pre-High Blood Pressure");
    }

    [TestMethod]
    public void TestIdealBloodPressure()
    {
        TestBloodPressureCategory(110, 70, "Ideal Blood Pressure");
    }

    [TestMethod]
    public void TestLowBloodPressure()
    {
        TestBloodPressureCategory(80, 50, "Low Blood Pressure");
    }

    private void TestBloodPressureCategory(int systolic, int diastolic, string expectedCategory)
    {
        // Navigate to a webpage or perform any necessary setup
        driver.Navigate().GoToUrl("http://localhost:53135/");

        // Input systolic and diastolic values
        IWebElement systolicInput = driver.FindElement(By.Id("BP_Systolic"));
        IWebElement diastolicInput = driver.FindElement(By.Id("BP_Diastolic"));

        systolicInput.SendKeys(systolic.ToString());
        diastolicInput.SendKeys(diastolic.ToString());

        // Trigger blood pressure categorization (you may need to adjust this based on your application)
        IWebElement categorizeButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
        categorizeButton.Click();

        // Wait for a few seconds to see the results (you might want to use explicit waits in a real scenario)
        System.Threading.Thread.Sleep(3000);

        // Verify the result
        IWebElement resultElement = driver.FindElement(By.Id("result"));
        string actualResult = resultElement.Text;

        Assert.AreEqual(expectedCategory, actualResult, $"Expected category: {expectedCategory}, Actual category: {actualResult}");
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Close the browser
        driver.Quit();
    }
}
