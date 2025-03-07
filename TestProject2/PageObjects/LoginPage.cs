using NUnit.Framework;
using OpenQA.Selenium;
using TestProject2.Models;

namespace LoginTest.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement UsernameTextBox => driver.FindElement(By.Id("user_email"));
        IWebElement PasswordTextBox => driver.FindElement(By.Id("user_password"));
        IWebElement LoginSubmit => driver.FindElement(By.Id("submitBtn"));
        IWebElement IndexHeaderContainer => driver.FindElement(By.Id("submitBtn"));

        public void Login(User user)
        {
            UsernameTextBox.Clear();
            UsernameTextBox.SendKeys(user.Username);
            PasswordTextBox.Clear();
            PasswordTextBox.SendKeys(user.Password);
            LoginSubmit.Submit();
            var isLoginSuccessfull = IndexHeaderContainer.Displayed;
            Assert.IsTrue(isLoginSuccessfull);
        }
    }
}
