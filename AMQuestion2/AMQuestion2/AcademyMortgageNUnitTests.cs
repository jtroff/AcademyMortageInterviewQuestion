using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace AMQuestion2
{
    class AcademyMortgageNUnitTests
    {
        IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            //Browser supports Chrome and Firefox drivers
            driver = new ChromeDriver();
        }

        [Test]
        public void NewsCEOTest()
        {
            //Navigate to Academy Mortgage and maximize the window
            driver.Url = "https://academymortgage.com/";
            driver.Manage().Window.Maximize();
            //Set Timeout wait to 5 seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Find and select All News from the webpage
            driver.FindElement(By.Id("cm-dropdown5")).Click();
            driver.FindElement(By.CssSelector("a[href='/news/all-news']")).Click();
            //Verify that the element for the CEO tile is present
            bool elementPresent = IsElementPresent(By.CssSelector
                    (
                    "a[href='https://academymortgage.com/news/company-news/detail/2019/08/27/james-mac-pherson-named-ceo-of-academy-mortgage']"
                    ));
            Assert.AreEqual(true, elementPresent);
            //Verify that the body contains James Mac Pherson
            var body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("James Mac Pherson"));
        } 

        [Test]
        public void LoanOfficerTest()
        {
            //Navigate to Academy Mortgage and maximize the window
            driver.Url = "https://academymortgage.com/";
            driver.Manage().Window.Maximize();
            //Set Timeout wait to 5 seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Navigate to the Find a Loan Officer page
            driver.FindElement(By.CssSelector("a[href='/find-a-loan-officer']")).Click();
            //Set Parent element of loan officer
            var loanOfficerParentElement = driver.FindElement(By.Name("lo-zip"));
            //Find and select the zip code radius under the loan officer side
            var zipRadiusElement = loanOfficerParentElement.FindElement(By.Id("Radius"));
            SelectElement oSelect = new SelectElement(zipRadiusElement);
            //Set radius value
            oSelect.SelectByValue("25");
            //Find zip code text box and enter in value under the loan officer side
            var zipElement = loanOfficerParentElement.FindElement(By.Id("Zip"));
            zipElement.SendKeys("84005");
            //Search for loan officer
            loanOfficerParentElement.FindElement(By.Id("searchBtn")).Click();
            //Verify that the body contains Matt Keyes and his corresponding NMLS
            var body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("Matt Keyes"));
            Assert.IsTrue(body.Text.Contains("NMLS #398768"));
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }

        //Helper function to verify if element is present on page
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Helper function to allow selection of dropdown items
        public IWebElement SelectElement { get; }

    }
}
