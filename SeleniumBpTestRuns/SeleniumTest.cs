/*using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumBpTestRuns
{
    [TestClass]
    public class UnitTest1
    { 
        IWebDriver driver;

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
}*/


/*new program


*//**//*--------------------------------------------------------------------------------------------------------------------------------------------*//**//*
using Microsoft.VisualStudio.TestTools.UnitTesting;

// NuGet install Selenium WebDriver package and Support Classes

using OpenQA.Selenium;

// NuGet install Chrome Driver
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

// run 2 instances of VS to do run Selenium tests against localhost
// instance 1 : run web app e.g. on IIS Express
// instance 2 : from Test Explorer run Selenium test
// or use the dotnet vstest task
// e.g. dotnet vstest seleniumtest\bin\debug\netcoreapp2.1\seleniumtest.dll /Settings:seleniumtest.runsettings

namespace SeleniumTest
{
    [TestClass]
    public class SeleniumTest
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner 
        // release task to point to run settings file
        // also webAppUri overriden to use pipeline variable

        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            // read URL from SeleniumTest.runsettings (configure run settings)
            this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();

            this.webAppUri = "http://localhost:53135/";
        }

        [TestMethod]
        public void BPCategory()
        {

            String chromeDriverPath = Environment.GetEnvironmentVariable("ChromeWebDriver");
            if (chromeDriverPath is null)
            {
                //chromeDriverPath = "C:\Users\CS2110TX\Downloads\chromedriver.exe";                 // for IDE
                chromeDriverPath = @"C:\Users\CS2110TX\Downloads\chromedriver.exe";

            }

            using (IWebDriver driver = new ChromeDriver(chromeDriverPath))
            {
                // any exception below results in a test fail

                // navigate to URI for pressure check
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


                // get systolic value in element
                IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
                systolicPressure.SendKeys("180");

                // get diastolic value in element
                IWebElement Diastolicpressure = driver.FindElement(By.Id("BP_Diastolic"));
                Diastolicpressure.SendKeys("90");

                // submit button element
                IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
                submitButton.Click();


                // explictly wait for result with "BPCategoryValue" item
                IWebElement BPCategoryValue = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(c => c.FindElement(By.XPath("//*[@id='form1']/div[4]")));

                // item comes back like "BPCategoryValue: High Blood Pressure"
                String bp = BPCategoryValue.Text.ToString();

                StringAssert.Contains(bp, "High Blood Pressure");

                driver.Quit();

                // alternative - use Cypress or Playright
            }
        }
    }
}*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTest
{
    [TestClass]
    public class SeleniumTest
    {
        String test_url = "http://localhost:53135/";
        String HBP = "High Blood Pressure";
        String PHBP = "Pre-High Blood Pressure";
        String IBP= "Ideal Blood Pressure";
        String LBP = "Low Blood Pressure";
        

        [TestMethod]
        public void HBPCategory()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver(@"C:\Users\CS2110TX\Downloads\chromedriver.exe");

            driver.Navigate().GoToUrl(test_url);

            // get systolic value in element
            IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
            System.Threading.Thread.Sleep(4000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("150");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(4000);
            diastolicPressure.Click();  
            diastolicPressure.Clear();
            diastolicPressure.SendKeys("90");

            // submit button element
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
            submitButton.Click();


            // Verified Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("//*[@id='form1']/div[4]"));
            String getText = itemtext.Text;
            Assert.IsTrue(HBP.Contains(getText));

            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            System.Threading.Thread.Sleep(4000);

            Console.WriteLine("HBP Test Passed");
        
        driver.Quit();
        }
        //BP : SP/DP mmHG
        //      100/80 eg.
        //    70-190/ 40-100 
        // SP>>DP
        // HBP: 140-190/90-100
        // PHBP: 120-140/ 80-90
        // IBP: 90-120/60-80
        // LOW: 70-90/40-60
        [TestMethod]
        public void PHBPCategory()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver(@"C:\Users\CS2110TX\Downloads\chromedriver.exe");

            driver.Navigate().GoToUrl(test_url);

            // get systolic value in element
            IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
            System.Threading.Thread.Sleep(4000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("130");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(4000);
            diastolicPressure.Click();
            diastolicPressure.Clear();
            diastolicPressure.SendKeys("85");

            // submit button element
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
            submitButton.Click();


            // Verified Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("//*[@id='form1']/div[4]"));
            String getText = itemtext.Text;
            Assert.IsTrue(PHBP.Contains(getText));

            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            System.Threading.Thread.Sleep(4000);

            Console.WriteLine("PHBP Test Passed");

            driver.Quit();
        }
    }
}
