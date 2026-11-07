using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        // Configurable URL via command-line arguments
        string url = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : "https://app.cloudqa.io/home/AutomationPracticeForm";

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");

        using IWebDriver driver = new ChromeDriver(options);

        try
        {
            driver.Navigate().GoToUrl(url);

            // =================================================================
            // 1. FIRST NAME (Text Box)
            // =================================================================
            By[] firstNameLocators =
            {
                // 1. Linked via label's @for attribute (survives randomized IDs and reordered HTML)
                By.XPath("//input[@id = //label[contains(translate(normalize-space(.), 'FIRSTNAME', 'firstname'), 'first name')]/@for]"),
                // 2. Direct sibling or nested inside the First Name field container
                By.XPath("//label[contains(translate(normalize-space(.), 'FIRSTNAME', 'firstname'), 'first name')]/following-sibling::input[1]"),
                By.XPath("//label[contains(translate(normalize-space(.), 'FIRSTNAME', 'firstname'), 'first name')]/parent::*//input[not(@type) or translate(@type, 'TEXT', 'text')='text']"),
                // 3. Placeholder, aria-label, name, or id attributes
                By.XPath("//input[contains(translate(@placeholder, 'FIRSTNAME', 'firstname'), 'first name') or contains(translate(@aria-label, 'FIRSTNAME', 'firstname'), 'first name')]"),
                By.XPath("//label[contains(translate(normalize-space(.), 'FIRSTNAME', 'firstname'), 'first name')]/following::input[not(@type) or translate(@type, 'TEXT', 'text')='text'][1]"),
                By.Id("fname"),
                By.Name("First Name")
            };

            IWebElement firstName = FindElementWithFallback(driver, firstNameLocators);
            ScrollIntoView(driver, firstName);
            firstName.Clear();
            firstName.SendKeys("Sandipan");


            // =================================================================
            // 2. GENDER (Radio Button - Male)
            // =================================================================
            By[] maleLocators =
            {
                // 1. Linked via "Male" label's @for attribute
                By.XPath("//input[@type='radio' and @id = //label[translate(normalize-space(.), 'MALE', 'male')='male']/@for]"),
                // 2. Radio input whose @value is "Male" (immune to label/span changes or reordering)
                By.XPath("//input[@type='radio' and translate(normalize-space(@value), 'MALE', 'male')='male']"),
                // 3. Radio input immediately before or after a <span> or <label> saying "Male"
                By.XPath("//*[self::span or self::label][translate(normalize-space(.), 'MALE', 'male')='male']/preceding-sibling::input[@type='radio'][1]"),
                By.XPath("//*[self::span or self::label][translate(normalize-space(.), 'MALE', 'male')='male']/following-sibling::input[@type='radio'][1]"),
                // 4. Radio input nested inside a <label> saying "Male"
                By.XPath("//label[translate(normalize-space(.), 'MALE', 'male')='male']//input[@type='radio']"),
                // 5. Fallback to ID or first radio inside the Gender container
                By.Id("male"),
                By.XPath("//label[contains(translate(normalize-space(.), 'GENDER', 'gender'), 'gender')]/parent::*//input[@type='radio'][1]"),
                By.XPath("//label[contains(translate(normalize-space(.), 'GENDER', 'gender'), 'gender')]/following::input[@type='radio'][1]")
            };

            IWebElement male = FindElementWithFallback(driver, maleLocators);
            ScrollIntoView(driver, male);
            SafeClick(driver, male);


            // =================================================================
            // 3. STATE (Dropdown)
            // =================================================================
            By[] stateLocators =
            {
                // 1. Linked via "State" label's @for attribute
                By.XPath("//select[@id = //label[contains(translate(normalize-space(.), 'STATE', 'state'), 'state')]/@for]"),
                // 2. Select inside the same parent container as the "State" label
                By.XPath("//label[contains(translate(normalize-space(.), 'STATE', 'state'), 'state')]/parent::*//select"),
                // 3. First select following the "State" label
                By.XPath("//label[contains(translate(normalize-space(.), 'STATE', 'state'), 'state')]/following::select[1]"),
                // 4. Select by @name, @id, or containing the "India" option
                By.XPath("//select[contains(translate(@name, 'STATE', 'state'), 'state') or contains(translate(@id, 'STATE', 'state'), 'state')]"),
                By.XPath("//select[option[translate(normalize-space(.), 'INDIA', 'india')='india']]"),
                By.Id("state")
            };

            IWebElement stateElement = FindElementWithFallback(driver, stateLocators);
            ScrollIntoView(driver, stateElement);

            SelectElement stateDropdown = new SelectElement(stateElement);
            string selectedState = SelectDropdownOptionResilient(stateDropdown, "India");


            // =================================================================
            // 4. VERIFICATION
            // =================================================================
            bool namePassed = string.Equals(
                firstName.GetAttribute("value")?.Trim(),
                "Sandipan",
                StringComparison.Ordinal);

            bool genderPassed = male.Selected;

            bool statePassed = !string.IsNullOrEmpty(selectedState) &&
                string.Equals(
                    stateDropdown.SelectedOption.Text.Trim(),
                    selectedState,
                    StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"First Name: {(namePassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"Gender: {(genderPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"State: {(statePassed ? "PASS" : "FAIL")}");

            bool allPassed = namePassed && genderPassed && statePassed;
            Console.WriteLine(allPassed ? "\nOVERALL: PASS" : "\nOVERALL: FAIL");

            Environment.ExitCode = allPassed ? 0 : 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nOVERALL: FAIL");
            Console.WriteLine($"Error: {ex.Message}");
            Environment.ExitCode = 1;
        }
        finally
        {
            driver.Quit();
        }
    }

    // Reusable fallback locator method
    static IWebElement FindElementWithFallback(IWebDriver driver, By[] locators)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        return wait.Until(d =>
        {
            foreach (By locator in locators)
            {
                try
                {
                    var match = d.FindElements(locator)
                                 .FirstOrDefault(e => e.Displayed && e.Enabled);

                    if (match != null)
                    {
                        return match;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    // DOM changed during lookup; continue trying
                }
            }

            return null;
        })!;
    }

    // Selects "India" if available; otherwise selects the first valid non-placeholder option
    static string SelectDropdownOptionResilient(SelectElement dropdown, string preferredText)
    {
        var preferred = dropdown.Options.FirstOrDefault(o =>
            string.Equals(o.Text.Trim(), preferredText, StringComparison.OrdinalIgnoreCase));

        if (preferred != null)
        {
            dropdown.SelectByText(preferred.Text);
            return preferred.Text.Trim();
        }

        var fallback = dropdown.Options.FirstOrDefault(o =>
            o.Enabled &&
            !string.IsNullOrWhiteSpace(o.Text) &&
            !o.Text.Contains("Select", StringComparison.OrdinalIgnoreCase) &&
            !o.Text.Contains("Choose", StringComparison.OrdinalIgnoreCase) &&
            !o.Text.Contains("--", StringComparison.Ordinal));

        if (fallback != null)
        {
            dropdown.SelectByText(fallback.Text);
            return fallback.Text.Trim();
        }

        return string.Empty;
    }

    static void ScrollIntoView(IWebDriver driver, IWebElement element)
    {
        if (driver is IJavaScriptExecutor js)
        {
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
        }
    }

    static void SafeClick(IWebDriver driver, IWebElement element)
    {
        try
        {
            element.Click();
        }
        catch (ElementClickInterceptedException)
        {
            if (driver is IJavaScriptExecutor js)
            {
                js.ExecuteScript("arguments[0].click();", element);
            }
        }
    }
}