using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MatchingEngine.Tests.Pages;

public class RepertoireManagementPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public RepertoireManagementPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By AdditionalFeatures =>
        By.XPath("//*[self::h2 or self::h3][contains(normalize-space(),'Additional Features')]");

    private By ProductsSupported =>
        By.XPath("//*[contains(normalize-space(),'Products Supported')]");

    private By SupportedProductsHeading =>
        By.XPath("//*[contains(normalize-space(),'There are several types of Product Supported:')]");

    public void OpenProductsSupported()
    {
        var section = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(AdditionalFeatures));

        ((IJavaScriptExecutor)driver)
            .ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", section);

        var productsLink = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(ProductsSupported));

        productsLink.Click();
    }

    public string GetSupportedProductsText()
    {
        var heading = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(SupportedProductsHeading));

        return heading.FindElement(
            By.XPath("./following-sibling::*[1]"))
            .Text;
    }
}