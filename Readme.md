# CloudQA Selenium Automation Assessment

## Overview

This project automates three fields on the CloudQA Automation Practice Form using C# and Selenium WebDriver.

The automation performs the following actions:
- Enters a value in the First Name field.
- Selects the Male radio button in the Gender field.
- Selects India from the State dropdown.
- Verifies each field and displays PASS or FAIL results.

## Tech Stack

- C#
- .NET
- Selenium WebDriver
- ChromeDriver
- Selenium Support (SelectElement and WebDriverWait)

## Website

[CloudQA Automation Practice Form](https://app.cloudqa.io/home/AutomationPracticeForm)

## Features

- Configurable URL through command-line arguments.
- Multiple fallback locators for each field.
- Reusable `FindElementWithFallback()` method.
- Explicit wait of 10 seconds.
- Verification of First Name, Gender, and State.
- Exception handling using try-catch-finally.
- Automatic browser cleanup after execution.

## Locator Strategy

The automation uses multiple locator strategies to locate the required elements.

### First Name
- XPath based on the label relationship.
- XPath based on input type.
- ID fallback.

### Gender
- XPath based on the Male option text.
- XPath based on the Gender label.
- ID fallback.

### State
- XPath based on the State label.
- Name attribute.
- ID fallback.

The program tries the locators one by one and returns a matching visible element when exactly one element is found.

## How to Run

### Prerequisites

- .NET SDK installed.
- Google Chrome installed.
- Selenium WebDriver NuGet packages installed.

### Run the project

Clone the repository:

```bash
git clone https://github.com/YOUR_USERNAME/CloudQA-Selenium-Assessment.git
```

Navigate to the project directory:

```bash
cd CloudQA-Selenium-Assessment
```

Run the automation using the default URL:

```bash
dotnet run
```

Run the automation with a configurable URL:

```bash
dotnet run "https://app.cloudqa.io/home/AutomationPracticeForm"
```

## Expected Output

```text
First Name: PASS
Gender: PASS
State: PASS

OVERALL: PASS
```

## Verification

The program verifies:
- First Name value equals `Sandipan`.
- Male radio button is selected.
- State dropdown selected option equals `India`.

The overall result is PASS only when all three verifications succeed.

## Limitations

- The fallback locators are designed for the current form structure and common changes.
- Significant changes to the webpage structure or labels may require updating the locator strategies.
- The program currently uses fixed test values: `Sandipan`, `Male`, and `India`.
- The automation is intended for the specified practice form.

## Author

Sandipan Nayek

GitHub: [SandipanNayek](https://github.com/SandipanNayek)