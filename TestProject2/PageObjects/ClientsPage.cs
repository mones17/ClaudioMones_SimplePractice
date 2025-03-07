using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestProject2.PageObjects
{
    public class ClientsPage
    {
        private WebDriverWait _wait;
        private readonly IWebDriver driver;
        public ClientsPage(IWebDriver driver)
        {
            this.driver = driver;
            _wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }
        IWebElement ClientsContactsHeader => driver.FindElement(By.XPath("//h1[text()='Clients and contacts']"));
        IWebElement SearchInput => driver.FindElement(By.ClassName("utility-search"));
        public void SearchClientByName(string clientName)
        {
            _wait.Until(driver => ClientsContactsHeader.Displayed && ClientsContactsHeader.Enabled);
            bool isClientAndContactsPageDisplayed = ClientsContactsHeader.Displayed;
            Assert.IsTrue(isClientAndContactsPageDisplayed, "Clients & Contacts page is displayed");
            SearchInput.SendKeys(clientName);
            SearchInput.SendKeys(Keys.Enter);
            IWebElement clientNameText = driver.FindElement(By.XPath($"//div/a[text()='{clientName}']"));
            bool isClientNameDisplayed = clientNameText.Displayed;
            Assert.IsTrue(isClientNameDisplayed, "Client name is displayed on client list.");
        }
    }
}
