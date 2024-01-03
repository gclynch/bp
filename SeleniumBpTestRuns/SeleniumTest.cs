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
            System.Threading.Thread.Sleep(2000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("150");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(2000);
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
            System.Threading.Thread.Sleep(2000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("130");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(2000);
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
        [TestMethod]
        public void IBPCategory()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver(@"C:\Users\CS2110TX\Downloads\chromedriver.exe");

            driver.Navigate().GoToUrl(test_url);

            // get systolic value in element
            IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
            System.Threading.Thread.Sleep(2000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("115");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(2000);
            diastolicPressure.Click();
            diastolicPressure.Clear();
            diastolicPressure.SendKeys("75");

            // submit button element
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
            submitButton.Click();


            // Verified Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("//*[@id='form1']/div[4]"));
            String getText = itemtext.Text;
            Assert.IsTrue(IBP.Contains(getText));

            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine("IBP Test Passed");

            driver.Quit();
        }
        [TestMethod]
        public void LBPCategory()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver(@"C:\Users\CS2110TX\Downloads\chromedriver.exe");

            driver.Navigate().GoToUrl(test_url);

            // get systolic value in element
            IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
            System.Threading.Thread.Sleep(2000);
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys("80");

            // get diastolic value in element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            System.Threading.Thread.Sleep(2000);
            diastolicPressure.Click();
            diastolicPressure.Clear();
            diastolicPressure.SendKeys("50");

            // submit button element
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
            submitButton.Click();


            // Verified Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("//*[@id='form1']/div[4]"));
            String getText = itemtext.Text;
            Assert.IsTrue(LBP.Contains(getText));

            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine("LBP Test Passed");

            driver.Quit();
        }
    }
}
