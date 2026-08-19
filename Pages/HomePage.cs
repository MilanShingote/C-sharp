using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpanishPointAutomation.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By SolutionsMenu =>
            By.XPath("//header//*[normalize-space()='Solutions']");

        private By DistributionProcessing =>
            By.XPath("//a[normalize-space()='Distribution processing' or normalize-space()='Distribution Processing']");

        public void Open()
{
    driver.Navigate().GoToUrl("https://www.matchingengine.com/");

    HandleCookieConsent();
}

        public void ExpandSolutions()
        {
            IWebElement solutions = wait.Until(driver =>
            {
                var element = driver.FindElement(SolutionsMenu);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            });

            solutions.Click();
        }

        public List<string> GetSolutions()
{
    var elements = driver.FindElements(
        By.XPath(
            "//header//a[" +
            "normalize-space()='Repertoire management' or " +
            "normalize-space()='Repertoire and usage matching' or " +
            "normalize-space()='Data ingestion and integration' or " +
            "normalize-space()='Distribution processing' or " +
            "normalize-space()='Member management' or " +
            "normalize-space()='Member self service'" +
            "]"));

    return elements
        .Select(x => x.Text.Trim())
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Distinct()
        .ToList();
}

private void HandleCookieConsent()
{
    try
    {
        var acceptButton = wait.Until(driver =>
        {
            var buttons = driver.FindElements(
                By.XPath(
                    "//*[contains(@id,'CybotCookiebotDialog')]//button[" +
                    "contains(translate(normalize-space(.), " +
                    "'ABCDEFGHIJKLMNOPQRSTUVWXYZ', " +
                    "'abcdefghijklmnopqrstuvwxyz'), 'accept') or " +
                    "contains(translate(normalize-space(.), " +
                    "'ABCDEFGHIJKLMNOPQRSTUVWXYZ', " +
                    "'abcdefghijklmnopqrstuvwxyz'), 'allow')" +
                    "]"));

            return buttons.FirstOrDefault(button =>
                button.Displayed && button.Enabled);
        });

        if (acceptButton != null)
        {
            acceptButton.Click();
        }

        wait.Until(driver =>
        {
            var dialogs = driver.FindElements(
                By.XPath("//*[contains(@id,'CybotCookiebotDialog')]"));

            return dialogs.All(dialog => !dialog.Displayed);
        });
    }
    catch (WebDriverTimeoutException)
    {
        // Cookie dialog did not appear or disappeared automatically.
    }
}

        public DistributionProcessingPage ClickDistributionProcessing()
        {
            IWebElement link = wait.Until(driver =>
            {
                var element = driver.FindElement(DistributionProcessing);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            });

            link.Click();

            return new DistributionProcessingPage(driver);
        }
    }
}