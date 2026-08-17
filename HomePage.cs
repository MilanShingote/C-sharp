using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MatchingEngine.Tests.Pages;

public class HomePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By ModulesMenu =>
        By.XPath("//a[contains(normalize-space(),'Solutions')]");

    private By RepertoireManagementModule =>
        By.XPath("//a[contains(normalize-space(),'Repertoire Management')]");

    public void Open()
    {
        driver.Navigate().GoToUrl("https://www.matchingengine.com/");
        Thread.Sleep(3000);

        File.WriteAllText(
        "homepage.html",
        driver.PageSource);
    }

    public void OpenRepertoireManagementModule()
    {
        var modules = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                ModulesMenu));

        modules.Click();

        var repertoire = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                RepertoireManagementModule));

        repertoire.Click();
    }
}