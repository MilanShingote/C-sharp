using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpanishPointAutomation.Pages;

namespace SpanishPointAutomation.Tests
{
    [TestFixture]
    public class MatchingEngineTests
    {
        private IWebDriver driver = null!;
        private HomePage homePage = null!;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);

            homePage = new HomePage(driver);
        }

        [Test]
        public void VerifyDistributionProcessingSolution()
        {
            // Visit Matching Engine
            homePage.Open();

            // Expand Solutions
            homePage.ExpandSolutions();

            // Get Solutions list
            var actualSolutions = homePage.GetSolutions();

            var expectedSolutions = new List<string>
            {
                "Repertoire management",
                "Repertoire and usage matching",
                "Data ingestion and integration",
                "Distribution processing",
                "Member management",
                "Member self service"
            };

            // Assert Solutions list
            Assert.That(
                actualSolutions,
                Is.EquivalentTo(expectedSolutions),
                "The Solutions list does not contain the expected solutions.");

            // Click Distribution Processing
            DistributionProcessingPage distributionPage =
                homePage.ClickDistributionProcessing();

            // Scroll to All-in-one solution for scale
            distributionPage.ScrollToAllInOneSection();

            // Assert section is displayed
            Assert.That(
                distributionPage.IsAllInOneSectionDisplayed(),
                Is.True,
                "The 'All-in-one solution for scale' section was not displayed.");

            // Get section content
            string sectionText = distributionPage.GetSectionText();

            // Assert section content
            Assert.That(
                sectionText,
                Does.Contain("Distribute royalty payments quickly"));

            Assert.That(
                sectionText,
                Does.Contain("Provide full detail of music usage to members"));

            Assert.That(
                sectionText,
                Does.Contain("Reduce cost-to-distribution ratios"));

            Assert.That(
                sectionText,
                Does.Contain("Prevent missing payments"));

            Assert.That(
                sectionText,
                Does.Contain("Comply with international standards"));

            Assert.That(
                sectionText,
                Does.Contain("Run analytics and queries"));

            Assert.That(
                sectionText,
                Does.Contain("Manage different collection share pictures"));

            Assert.That(
                sectionText,
                Does.Contain("Minimise manual intervention"));

            Assert.That(
                sectionText,
                Does.Contain("Tackle fluctuating data volumes"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}