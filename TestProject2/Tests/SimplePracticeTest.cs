using LoginTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject2.Data;
using TestProject2.Models;
using TestProject2.PageObjects;

namespace TestProject2.Tests
{
    public class SimplePracticeTest
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("headless"); // Ejecutar en modo headless  
            options.AddArgument("no-sandbox");
            options.AddArgument("disable-dev-shm-usage");

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://secure.simplepractice.com");
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [Test]
        [TestCaseSource(typeof(SimplePracticeData), nameof(SimplePracticeData.ValidUser))]
        public void SimpleTest(User simpleUser, Client client)
        {
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.Login(simpleUser);
            HomePage homePage = new HomePage(_driver);
            homePage.CreateClient();
            homePage.FillClientDetails(client);
            homePage.NavigateToClientsPage();
            ClientsPage clientsPage = new ClientsPage(_driver);
            clientsPage.SearchClientByName(client.FirstName + " " + client.LastName);
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
