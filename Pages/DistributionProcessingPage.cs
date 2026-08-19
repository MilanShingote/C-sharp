using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpanishPointAutomation.Pages
{
    public class DistributionProcessingPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public DistributionProcessingPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By AllInOneHeading =>
            By.XPath("//h2[normalize-space()='All-in-one solution for scale']");

        public void ScrollToAllInOneSection()
        {
            IWebElement section = wait.Until(driver =>
            {
                var element = driver.FindElement(AllInOneHeading);

                return element.Displayed
                    ? element
                    : null;
            });

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block: 'center'});",
                section);
        }

        public string GetSectionText()
        {
            IWebElement heading = wait.Until(driver =>
            {
                var element = driver.FindElement(AllInOneHeading);

                return element.Displayed
                    ? element
                    : null;
            });

            IWebElement section = heading.FindElement(
                By.XPath("./ancestor::section[1]"));

            return section.Text;
        }

        public bool IsAllInOneSectionDisplayed()
        {
            return wait.Until(driver =>
            {
                var element = driver.FindElement(AllInOneHeading);

                return element.Displayed;
            });
        }
    }
}