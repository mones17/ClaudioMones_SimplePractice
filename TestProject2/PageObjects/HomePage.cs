using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using TestProject2.Models;

namespace TestProject2.PageObjects
{
    public class HomePage
    {
        private WebDriverWait _wait;
        private readonly IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            _wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(30));
        }
        IWebElement CreateButton => driver.FindElement(By.XPath("//button[contains(@class,'create')]"));
        IWebElement CreateClientButton => driver.FindElement(By.XPath("//button/text()[contains(., 'Create client')]/.."));
        IWebElement FirstNameInput => driver.FindElement(By.XPath("//input[contains(@id,'firstName')]"));
        IWebElement LastNameInput => driver.FindElement(By.XPath("//input[contains(@id,'lastName')]"));
        IWebElement PreferedNameInput => driver.FindElement(By.Id("nickname"));
        IWebElement MonthOfBirthDropDown => driver.FindElement(By.XPath("//select[@name='month']"));
        IWebElement DayOfBirthDropDown => driver.FindElement(By.XPath("//select[@name='day']"));
        IWebElement YearOfBirthDropDown => driver.FindElement(By.XPath("//select[@name='year']"));
        IWebElement ReferedBySelectBox => driver.FindElement(By.Id("select-box-el-194"));
        IWebElement WaitListCheckBox => driver.FindElement(By.Id("waitlistCheckbox-mask"));
        IWebElement ClientLocationDropDown => driver.FindElement(By.Id("new-client-office"));
        IWebElement AddEmailButton => driver.FindElement(By.XPath("//text()[contains(.,'Add email')]/.."));
        IWebElement AddPhoneButton => driver.FindElement(By.XPath("//text()[contains(.,'Add phone')]/.."));
        IWebElement ContinueButton => driver.FindElement(By.XPath("//button[contains(text(),'Continue')]"));
        IWebElement ClientsLinkButton => driver.FindElement(By.XPath("//span[text()='Clients' and @class='link-text']"));
        public void CreateClient()
        {
            _wait.Until(driver => CreateButton.Displayed && CreateButton.Enabled);
            bool isCreateButtonDisplayed = CreateButton.Displayed;
            Assert.IsTrue(isCreateButtonDisplayed, "Verify that Create button was displayed.");
            CreateButton.Click();
            _wait.Until(driver => CreateClientButton.Displayed && CreateClientButton.Enabled);
            bool isCreateClientButtonDisplayed = CreateClientButton.Displayed;
            Assert.IsTrue(isCreateClientButtonDisplayed, "Verify that Create client button was displayed.");
            CreateClientButton.Click();
        }
        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            string validMonth = dateOfBirth.Month switch
            {
                1 => "January",
                2 => "February",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "Jun",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
            };
            SelectElement monthDropDown = new SelectElement(MonthOfBirthDropDown);
            monthDropDown.SelectByText(validMonth);
            SelectElement dayDropDown = new SelectElement(DayOfBirthDropDown);
            dayDropDown.SelectByText(dateOfBirth.Day.ToString());
            SelectElement yearDropDown = new SelectElement(YearOfBirthDropDown);
            yearDropDown.SelectByText(dateOfBirth.Year.ToString());
        }
        // This is a method that will cover all related with client details, if there's no needed some of the fields, just leave them empty in the instance
        public void FillClientDetails(Client client)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            _wait.Until(driver => FirstNameInput.Displayed && FirstNameInput.Enabled);
            bool isFirstNameInputDisplayed = FirstNameInput.Displayed;
            Assert.IsTrue(isFirstNameInputDisplayed, "Verify that First Name input was displayed.");
            if (!string.IsNullOrEmpty(client.ClientType))
            {
                IWebElement clientTypeRadioButton =
                    driver.FindElement(By.XPath($"//label/text()[contains(.,'{client.ClientType}')]/preceding-sibling::input"));
                clientTypeRadioButton.Click();
            }
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(client.FirstName);
            LastNameInput.Clear();
            LastNameInput.SendKeys(client.LastName);
            if (!string.IsNullOrEmpty(client.PreferedName))
            {
                PreferedNameInput.Clear();
                PreferedNameInput.SendKeys(client.PreferedName);
            }
            SetDateOfBirth(client.DateOfBirth);
            if (!string.IsNullOrEmpty(client.ReferedBy))
            {
                ReferedBySelectBox.Click();
                IWebElement referedByOption = driver.FindElement(By.XPath($"//div[text()='{client.ReferedBy}']"));
                bool isReferedByOptionDisplayed = referedByOption.Displayed;
                Assert.IsTrue(isReferedByOptionDisplayed, $"Verify that Refered By option '{client.ReferedBy}' was displayed.");
                referedByOption.Click();
            }
            if (client.WaitList)
            {
                WaitListCheckBox.Click();
            }
            if (!string.IsNullOrEmpty(client.ClientStatus))
            {
                IWebElement clientStatusRadioButton =
                    driver.FindElement(By.XPath($"//label/text()[contains(.,'{client.ClientStatus}')]/preceding-sibling::input"));
                clientStatusRadioButton.Click();
            }
            if (!string.IsNullOrEmpty(client.Location))
            {
                SelectElement clientLocationSelector = new SelectElement(ClientLocationDropDown);
                clientLocationSelector.SelectByText(client.Location);
            }
            js.ExecuteScript("arguments[0].scrollIntoView();", AddPhoneButton);
            // This is a dynamic way to add emails and phones, if there are no emails or phones, the method will not add them, if they are multiple it will add them all.
            if (client.Email != null)
            {
                int i = 1;
                foreach (var email in client.Email)
                {
                    // Adding the email
                    AddEmailButton.Click();
                    IWebElement emailInput = driver.FindElement(By.XPath($"(//input[@name='email'])[{i.ToString()}]"));
                    _wait.Until(driver => emailInput.Displayed && emailInput.Enabled);
                    emailInput.SendKeys(email.Contact);
                    // Adding the email type by click on the selector
                    IWebElement emailTypeDropDown = driver.FindElement(By.XPath
                        ($"((//div[@class='contact-details-section-container']//tbody)[1]//td[2]//button)[{i.ToString()}]"));
                    emailTypeDropDown.Click();
                    // Select the valid option based on the instance
                    IWebElement emailTypeOption = 
                        driver.FindElement(By.XPath($"//span[text()='{email.Type}']/../parent::div[contains(@class,'item ember-view')]/button"));
                    js.ExecuteScript("window.scrollBy(0, 20);");
                    _wait.Until(driver => emailTypeOption.Displayed && emailTypeOption.Enabled);
                    Thread.Sleep(3000);
                    emailTypeOption.Click();
                    // Adding the email permission by click on the selector
                    IWebElement emailPermissionDropDown = driver.FindElement(By.XPath
                        ($"((//div[@class='contact-details-section-container']//tbody)[1]//td[3]//button)[{i.ToString()}]"));
                    emailPermissionDropDown.Click();
                    // Select the valid option based on the instance
                    IWebElement emailPermissionOption = 
                        driver.FindElement(By.XPath($"//span[text()='{email.Permission}']/../parent::div[contains(@class,'item ember-view')]/button"));
                    js.ExecuteScript("window.scrollBy(0, 20);");
                    _wait.Until(driver => emailPermissionOption.Displayed && emailPermissionOption.Enabled);
                    Thread.Sleep(3000);
                    emailPermissionOption.Click();
                    i++;
                }
            }
            if (client.Phone != null)
            {
                int i = 1;
                foreach (var phone in client.Phone)
                {
                    // Adding the phone
                    AddPhoneButton.Click();
                    IWebElement phoneInput = driver.FindElement(By.XPath($"(//input[@name='phone'])[{i.ToString()}]"));
                    _wait.Until(driver => phoneInput.Displayed && phoneInput.Enabled);
                    phoneInput.SendKeys(phone.Contact);
                    // Adding the phone type by click on the selector
                    IWebElement phoneTypeDropDown = driver.FindElement(By.XPath
                        ($"((//div[@class='contact-details-section-container']//tbody)[2]//td[2]//button)[{i.ToString()}]"));
                    phoneTypeDropDown.Click();
                    // Select the valid option based on the instance
                    IWebElement phoneTypeOption = 
                        driver.FindElement(By.XPath($"//span[text()='{phone.Type}']/../parent::div[contains(@class,'item ember-view')]/button"));
                    js.ExecuteScript("window.scrollBy(0, 20);");
                    _wait.Until(driver => phoneTypeOption.Displayed && phoneTypeOption.Enabled);
                    Thread.Sleep(3000);
                    phoneTypeOption.Click();
                    // Adding the phone permission by click on the selector
                    IWebElement phonePermissionDropDown = driver.FindElement(By.XPath
                        ($"((//div[@class='contact-details-section-container']//tbody)[2]//td[3]//button)[{i.ToString()}]"));
                    phonePermissionDropDown.Click();
                    // Select the valid option based on the instance
                    IWebElement phonePermissionOption = 
                        driver.FindElement(By.XPath($"//span[text()='{phone.Permission}']/../parent::div[contains(@class,'item ember-view')]/button"));
                    js.ExecuteScript("window.scrollBy(0, 30);");
                    _wait.Until(driver => phonePermissionOption.Displayed && phonePermissionOption.Enabled);
                    Thread.Sleep(3000);
                    phonePermissionOption.Click();
                    i++;
                }
            }
            // Creation of dynamic element instead of use 3 different elements just for 1 word difference.
            string reminderOptions = "//span[text()='?']/../preceding-sibling::div/span";
            if (client.UpcommingAppointments)
            {
                IWebElement upcomingAppointmentsToogleButton = driver.FindElement(By.XPath(reminderOptions.Replace("?", "Upcoming appointments")));
                js.ExecuteScript("arguments[0].scrollIntoView();", upcomingAppointmentsToogleButton);
                upcomingAppointmentsToogleButton.Click();
            }
            if (client.IncompleteDocuments)
            {
                IWebElement incompleteDocumentsToogleButton = driver.FindElement(By.XPath(reminderOptions.Replace("?", "Incomplete documents")));
                js.ExecuteScript("arguments[0].scrollIntoView();", incompleteDocumentsToogleButton);
                incompleteDocumentsToogleButton.Click();
            }
            if (client.Cancellations)
            {
                IWebElement cancellationsDocumentsToogleButton = driver.FindElement(By.XPath(reminderOptions.Replace("?", "Cancellations")));
                js.ExecuteScript("arguments[0].scrollIntoView();", cancellationsDocumentsToogleButton);
                cancellationsDocumentsToogleButton.Click();
            }
            ContinueButton.Click();
        }
        public void NavigateToClientsPage()
        {
            _wait.Until(driver => ClientsLinkButton.Displayed && ClientsLinkButton.Enabled);
            bool isClientsLinkButtonDisplayed = ClientsLinkButton.Displayed;
            Assert.IsTrue(isClientsLinkButtonDisplayed, "Verify that Clients link button was displayed.");
            ClientsLinkButton.Click();
        }
    }
}
