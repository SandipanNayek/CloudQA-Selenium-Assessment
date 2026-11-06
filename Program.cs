
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        string url = args.Length > 0
            ? args[0]
            : "https://app.cloudqa.io/home/AutomationPracticeForm";

        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl(url);

            // First Name - fallback locators
            By[] firstNameLocators =
            {
                By.XPath("//label[normalize-space()='First Name']/following-sibling::input[1]"),
                By.XPath("//label[normalize-space()='First Name']/following::input[@type='text'][1]"),
                By.Id("fname")
            };

            IWebElement firstName =
                FindElementWithFallback(driver, firstNameLocators);

            firstName.Clear();
            firstName.SendKeys("Sandipan");


            // Gender - fallback locators
            By[] maleLocators =
            {
                By.XPath("//span[normalize-space()='Male']/preceding-sibling::input[@type='radio']"),
                By.XPath("//label[normalize-space()='Gender']/following::input[@type='radio'][1]"),
                By.Id("male")
            };

            IWebElement male =
                FindElementWithFallback(driver, maleLocators);

            male.Click();


            // State - fallback locators
            By[] stateLocators =
            {
                By.XPath("//label[normalize-space()='State']/following::select[1]"),
                By.XPath("//select[@name='State']"),
                By.Id("state")
            };

            IWebElement stateElement =
                FindElementWithFallback(driver, stateLocators);

            SelectElement stateDropdown =
                new SelectElement(stateElement);

            stateDropdown.SelectByText("India");


            // Verification
            bool namePassed =
                firstName.GetAttribute("value") == "Sandipan";

            bool genderPassed = male.Selected;

            bool statePassed =
                stateDropdown.SelectedOption.Text == "India";


            Console.WriteLine(
                $"First Name: {(namePassed ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"Gender: {(genderPassed ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"State: {(statePassed ? "PASS" : "FAIL")}");

            bool allPassed =
                namePassed && genderPassed && statePassed;

            Console.WriteLine(
                allPassed ? "\nOVERALL: PASS" : "\nOVERALL: FAIL");

            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nOVERALL: FAIL");
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            driver.Quit();
        }
    }


    // Reusable fallback locator method
    
static IWebElement FindElementWithFallback(
    IWebDriver driver,
    By[] locators)
{
    WebDriverWait wait =
        new WebDriverWait(driver, TimeSpan.FromSeconds(10));

    return wait.Until(d =>
    {
        foreach (By locator in locators)
        {
            try
            {
                var elements = d.FindElements(locator);

                if (elements.Count == 1 && elements[0].Displayed)
                {
                    return elements[0];
                }
            }
            catch (StaleElementReferenceException)
            {
                // DOM changed; try again
            }
        }

        return null;
    });
}

}
