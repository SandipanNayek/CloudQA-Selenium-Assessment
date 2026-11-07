# CloudQA Assessment 1 – Selenium Automation

## Overview

This project automates the testing of three fields on the CloudQA Automation Practice Form using **C# and Selenium WebDriver**.

The primary objective is to build a resilient automation script that continues to work when the page undergoes minor changes, such as:

- Fields being reordered.
- `id`, `name`, or other attributes changing.
- Slight modifications to the HTML structure.
- Similar-looking form fields appearing on the page.

The solution uses multiple fallback locator strategies instead of depending on a single fixed XPath or CSS selector.

## Application Under Test

**URL:**  
https://app.cloudqa.io/home/AutomationPracticeForm

## Video Demonstration

**Assessment 1 – Execution Walkthrough**

[▶ Watch Assessment 1 Video](https://drive.google.com/file/d/1DPnhzAM69pzeQxIkxIukvZKGHwKc2R9h/view?usp=sharing)

The video demonstrates:

- Project setup and structure.
- Resilient locator implementation.
- Selenium automation execution.
- Automated interaction with form fields.
- Field-level validation.
- Overall PASS/FAIL results.

## Technologies Used

- C#
- .NET
- Selenium WebDriver
- Selenium Support
- Google Chrome

## Test Scenarios

| Field | Test Action | Expected Result |
|---|---|---|
| First Name | Enter `Sandipan` | Text box contains the entered name |
| Gender | Select Male | Male radio button is selected |
| State | Select India | India is selected when available |

The program verifies each field and displays its individual test result along with the overall execution status.

## Resilient Locator Strategy

To improve reliability against page modifications, the program uses multiple locator strategies with fallback mechanisms.

### First Name

- Label-to-input association using the `for` attribute.
- Label-based sibling and parent container locators.
- Placeholder and `aria-label` attributes.
- Fallback to ID and name attributes.

### Gender

- Label-to-radio association.
- Radio button value attribute.
- Sibling and nested label relationships.
- Fallback to ID and Gender container locators.

### State

- Label-to-dropdown association.
- Parent container and following dropdown locators.
- Dropdown attributes and option text.
- Fallback to ID-based selection.

The program also uses explicit waits, visibility and enabled-state checks, scrolling, and safe click handling.

## Project Structure

```text
CloudQA-Assessment-1/
├── Program.cs
├── CloudQA-Assessment-1.csproj
├── README.md
└── .gitignore
```

## Prerequisites

Before running the project, ensure that you have:

- .NET SDK installed.
- Google Chrome installed.
- Internet connectivity for accessing the application and Selenium Manager, when required.

## Installation and Setup

### 1. Clone the Repository

```bash
git clone <YOUR_GITHUB_REPOSITORY_URL>
```

### 2. Navigate to the Project Directory

```bash
cd CloudQA-Assessment-1
```

### 3. Install Required Selenium Packages

```bash
dotnet add package Selenium.WebDriver
dotnet add package Selenium.Support
```

### 4. Restore Dependencies

```bash
dotnet restore
```

## Execution

### Run Automation Using the Default URL

```bash
dotnet run
```

### Run Automation With a Configurable URL

```bash
dotnet run -- "https://app.cloudqa.io/home/AutomationPracticeForm"
```

The program accepts a URL as a command-line argument. If no URL is provided, it uses the default CloudQA Automation Practice Form URL.

## Sample Output

```text
First Name: PASS
Gender: PASS
State: PASS

OVERALL: PASS
```

## Execution Status

- `PASS`: The field validation succeeded.
- `FAIL`: The field validation failed or an exception occurred.
- Exit code `0`: Overall test passed.
- Exit code `1`: Overall test failed.

## Key Features

- Configurable application URL.
- Multiple fallback locator strategies.
- Resilient field identification.
- Explicit wait-based element handling.
- Automated interaction with text boxes, radio buttons, and dropdowns.
- Field-level verification.
- Overall PASS/FAIL reporting.
- Exception handling and process exit codes.

## Conclusion

This solution demonstrates resilient browser automation using C# and Selenium WebDriver.

By combining semantic locator strategies, fallback mechanisms, explicit waits, and validation, the program is designed to handle reasonable changes in the form's layout and HTML structure while automating the required fields.

**Assessment 1 Status: Successfully executed on the original CloudQA Automation Practice Form.**

## Video Link

https://drive.google.com/file/d/1DPnhzAM69pzeQxIkxIukvZKGHwKc2R9h/view?usp=sharing
