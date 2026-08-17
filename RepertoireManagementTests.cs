using NUnit.Framework;
using OpenQA.Selenium;
using MatchingEngine.Tests.Pages;
using MatchingEngine.Tests.Utilities;

namespace MatchingEngine.Tests.Tests;

public class RepertoireManagementTests
{
    private IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        driver = DriverFactory.CreateChromeDriver();
    }

    [Test]
    public void ShouldDisplaySupportedProducts()
    {
        var homePage = new HomePage(driver);
        var repertoirePage = new RepertoireManagementPage(driver);

        homePage.Open();

        homePage.OpenRepertoireManagementModule();

        repertoirePage.OpenProductsSupported();

        var supportedProducts =
            repertoirePage.GetSupportedProductsText();

        Assert.That(
            supportedProducts,
            Does.Contain("Equities"));

        Assert.That(
            supportedProducts,
            Does.Contain("Bonds"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}