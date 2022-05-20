# AC2022

### **Session 1 - 04.04.2022**

We have discussed about automation:
  1. What is it
  2. Why is important
  3. Types of automation testing

The presentation was sent via email at the end of the course.

### **Session 2 - 11.04.2022 - Let's write our first UI automation test**

**Scope:** This session scope was to create a unit test project, install all the dependencies and write a simple test.

How to create a unit test project:
  1. Open Visual studio
  2. Click File > New > Project
  3. Search for unit test: Unit Test Project(.NET Framework)
  4. Add project Name
  5. Click "Create" button
  
At this moment, we have created a unit test project and should have a default test class: UnitTest1. 
The class has the following particularities: 
1. It has [TestClass] annotation that identifies the class to be a test one. Without this annotation, the test under this class cannot     be recognized and there for cannot run the tests within it.
2. The class contains a test method that has a [TestMethod] annotation. This help the method to be recognized as an test method and run it accordingly.
  
Find more info on: https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019
  
Let's import the needed packages in order to open a chrome browser using our test. Do a right click on the created project and 
click Manage Nuget Packages. On the opened window, select Browse tab and search for Selenium.Webdriver and install it in your project. Then search for Selenium.Webdriver.ChromeDriver and install the latest version. 

Be aware that if the local installed Chrome is not the same with the installed package, it will trigger a compatibility error when running the test.
  
Let's write some code :) 
  
**REMEMBER: THE TEST DOES WHAT YOU TELL IT TO DO.**
Thats why, selenium manipulates the elements in DOM as a human would do. For now, we will use click and sendkeys.
  
Our first automated test case will try to login into application. For this, we will need to write code for the next steps:
  1. Open the browser
  2. Maximize the browser window
  3. Navigate to the application URL
  4. Click Sign in button
  5. Fill user email
  6. Fill user password
  7. Click Sign in button 
  8. Assert that the current user label contains our email
    
Open the UnitTest1 class and let instantiate our driver to open the Chrome browser.

```csharp  
      var driver = new ChromeDriver(); // open chrome browser
      driver.Manage().Window.Maximize(); // maximize the window
      driver.Navigate().GoToUrl("OUR URL"); // access the SUT(System Under Test) url. In our case http://a.testaddressbook.com/
```

Until now, we have covered the first 3 steps of our test. Let's complete our test:

```csharp  
      var btnSignIn = driver.FindElement(By.Id("sign-in"));
      signIn.Click();
      Thread.Sleep(2000);
      driver.FindElement(By.Id("session_email")).SendKeys("email that was used for creating the account");
      driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("password used to create the account");
      driver.FindElement(By.Name("commit")).Click();
      Assert.IsTrue(driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text.Equals("email that was used for creating the account"));
      driver.Quit(); // close the browser
```

Clarification :)
  1. WebElement represents a DOM element. WebElements can be found by searching from the document root using a WebDriver instance. WebDriver API provides built-in methods to find the WebElements which are based on different properties like ID, Name, Class, XPath, CSS Selectors, link Text, etc.
  2. There are some ways of optimizing our selectors used to identify the elements in page.  
      a. For the CssSelector, the simplest way is to use the following format: tagname[attribute='attributeValue'].  
      b. For the XPath, the simplest way is to use the following format: //tagname[@attribute='attributeValue'].  
    Let's take for example the Sign in button:
    
```csharp    
    <input type="submit" name="commit" value="Sign in" class="btn btn-primary" data-test="submit" data-disable-with="Sign in">
```

    Explained: 
```csharp    
    <input(this is the tagname) 
        type(this is the attribute)="submit"(this is the value) --> The CssSelector would be: input[type='submit']
        name(this is the attribute)="commit"(this is the value) --> The XPath would be: //input[@name='commit']
        value(this is the attribute)="Sign in"(this is the value) --> The CssSelector would be: input[value='Sign in']
        class(this is the attribute)="btn btn-primary"(this is the value) --> The XPath would be: //input[@class='btn btn-primary']
        data-test(this is the attribute)="submit"(this is the value) --> The CssSelector would be: input[data-test='submit']
        data-disable-with(this is the attribute)="Sign in"(this is the value) --> The Xpath would be: //input[data-disabled-with='Sign in']
    >
```   

### **Session 3 - 18.04.2022 - Let's refine/refactor our code**

**Scope:** This session scope was to use locators strategy, MSTest methods to initialize/clean up our test data and to get rid of our duplicate code.

Locators attributes strategy: 

ID – unique, safest, fastest locator option and should always be your first choice 
	
	//input[@id = "session_email"] - “session_password" 
	 

Name - it also has same speed as of like ID 
	
	//input[@name = "session[email]"] -  “session[password]” 
 

Class Name (simple / composed) - Fast, Consistent as it doesn’t change much 
	
	//div[@class = "row"]//a - (Sign up button) 
 

Link Text (a.href) 
	
	//a[contains (text(), 'Sign up')] 
 

CSS Selector - slower and more resource consuming option but it gives more flexibility 
	
	input[data-test='email'] 
	
	input[type='password'] 
	
	tagname[attribute='attributeValue'] 
 

Xpath - slowest and the most “expensive” 
	
Most flexible in order to build reliable web element locators.

Very slow locator since in order to locate the element it needs to traverse the whole DOM of the page which is a time consuming operation.

Absolute XPath (direct way, select the element from the root node) / 

Relative XPath (anywhere at the webpage) // 
	
	//input[@value='Sign in'] 

	//tagname[@attribute='attributeValue'] 
	
	(input, button, label) | (id, name, class name) 
	
	//input[starts-with(@type, 'email')] 
	
	//input[@type = "email" and @name = "email"] 
	
	//input[@type = "email" or @name = "email"] 


 

XPath methods: 

Following - all following elements of the current node  

	//div[@id='clearance']//following::div 
	
	//div[@class='container']//following::div 

Ancestor - all ancestors element (grandparent, parent, etc.) on the current node 
	
	//input[@type="email"]/ancestor::form 
	//input[@type="submit"]/ancestor::form 

Child - all children elements of the current node

	//div[@id='clearance']//child::div 

Preceding - all nodes that come before the current node 

	//input[@value='Sign in']//preceding::input 
	

Following-sibling - following siblings of the context node 

	//div[@id='clearance']//following-sibling::input 

Parent - parent of the current node 

	//input[@type='submit']/parent::div 

Descendant - descendants of the current node 

	//div[@id='clearance']//descendant::div
	
	//element.name[@attribute.name=“attribute.value“]/method::element.name 

	(following-sibling) 

Try to use these element’s in order if possible in order to consistently have good tests which will reduce brittleness and increase maintainability.. 

XPath and CSS Selectors are extremely powerful but are normally not the best option to use for both speed and brittleness reasons. 

One of a test case component is the prerequisite: conditions that must be met before the test case can be run.
Our code is testing login scenarios and we need to see what are the prerequisites.
We have identified the following steps that need to be execute before running the test:

```csharp
            var driver = new ChromeDriver(); // open the browser
            driver.Manage().Window.Maximize(); // maximize the window 
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/"); // access the SUT url
            driver.FindElement(By.Id("sign-in")).Click(); // click on sign in button in order to be redirected to the login page
            Thread.Sleep(2000); // THIS sleep is a bad practice AND NEEDS TO BE DELETED AND WE WILL BURN IT WITH FIRE IN THE NEAR FUTURE
```

Also, after the test has finished running, we need to clean up the operations that we made in our test in order to not impact further test. Remember, each test is independent and should not influence the result of other tests. In our test, the clean up would mean to close the browser.

```csharp
            driver.Quit();
```

MSTest provides a way to declare methods to be called by the test runner before or after running a test.

```csharp
            [TestInitialize]
            public void TestInitialize()
            {
            }

            [TestCleanup]
            public void TestCleanup()
            {
            }
```

The method decorated by [TestInitialize] is called before running each test of the class. The method decorated by [TestCleanup] is called after running each test of the class.

First, we need to remove the init/clean up steps and to move it the according method. Let's add one more test.
At this point, our tests should look like this: 

```csharp
          namespace UnitTestProject1
          {
              [TestClass]
              public class LoginTests
              {
                  // declare IWebDriver instance variable
                  // use it outside of any methods so that we can use it in various methods
                  private IWebDriver driver;

                  [TestInitialize]
                  public void TestInitialize()
                  {
                       // initialize the needed driver. In our case is ChromeDriver
                       driver = new ChromeDriver();
                       loginPage = new LoginPage(driver);

                       driver.Manage().Window.Maximize();
                       driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

                       var btnSignIn = driver.FindElement(By.Id("sign-in"));
                       btnSignIn.Click();
                       Thread.Sleep(2000); // to be changed, it is not a best practice to use it
                  }

                  [TestMethod]
                  public void User_Should_Login_Successfully()
                  {
                       driver.FindElement(By.Id("session_email")).SendKeys("test@test.test");
                       driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("test");

                       driver.FindElement(By.Name("commit")).Click();

                       Assert.IsTrue(driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text.Equals("test@test.test"));
                  }

                  [TestMethod]
                  public void User_Should_Fail_Login_With_WrongEmail()
                  {
                       driver.FindElement(By.Id("session_email")).SendKeys("test@test.testWrong");
                       driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("test");

                       driver.FindElement(By.Name("commit")).Click();

                       var expectedResult = "Bad email or password.";
                       var actualResult = driver.FindElement(By.CssSelector(".alert-notice")).Text;

                       Assert.AreEqual(expectedResult, actualResult);
                  }

                  [TestCleanup]
                  public void TestCleanup()
                  {
                       driver.Quit();
                  }
              }
          }   
```

ONE OF THE COMMON MISTAKE IS TO DECLARE THE IWebDriver INSTANCE VARIABLE WITHIN THE TEST INIT:

```csharp
        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            var driver = new ChromeDriver();
          //this is a method local variable and cannot be used in other methods outside init. The tests will throw null refrence since it doesn't know about the ChromeDriver instance
        }
```

Our code starts to look cleaner :)

But wait, there is more work to do. Let's say that the login page layout needs to be changed. Our test will fail after this changes.
We have only two tests and will be easy to fix it. But imagine that we have 25 login tests. Is not so funny to update all the tests.

A better approach to script maintenance is to create a separate class file which would find web elements, fill them or verify them. This class can be reused in all the scripts using that element. In future, if there is a change in the web element, we need to make the change in just 1 class file and not 10 different scripts.

This approach is called Page Object Model (POM). It helps make the code more readable, maintainable, and reusable.

Page Object model is an object design pattern in Selenium, where web pages are represented as classes, and the various elements on the page are defined as variables on the class. All possible user interactions can then be implemented as methods on the class.

Let's create the the page object that contains the elements for the login page: LoginPage.cs

Right click on the project > Add > Folder and name it PageObjects

Right click on the PageObjects folder > Add > New Item... > Add a class with name: LoginPage.cs

We need to add the objects that we use in our script in this class: email input, password input, sign in button and create a method to login the user.

Our login page will look like this in the end:

```csharp
      public class LoginPage
      {
          //declare the driver
          private IWebDriver driver;

          //instantiate the driver
          public LoginPage(IWebDriver browser)
          {
              driver = browser;
          }

          //create our elements
          private IWebElement Username()
          {
            return driver.FindElement(By.Id("session_email"));
          }

          private IWebElement Password()
          {
              return driver.FindElement(By.Id("session_password"));
          }

          private IWebElement LoginClick()
          {
              return driver.FindElement(By.Name("commit"));
          }

          public void LoginApplication(string username, string password)
          {
              Username().SendKeys(username);
              Password().SendKeys(password);
              LoginClick().Click();
          }
      }
```

At this point, we need to update our tests:

```csharp
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            var btnSignIn = driver.FindElement(By.Id("sign-in"));
            btnSignIn.Click();
            Thread.Sleep(2000); // to be changed, it is not a best practice to use it
        }

        [TestMethod]
        public void User_Should_Login_Successfully()
        {
            loginPage.LoginApplication("test@test.test", "test");

            Assert.IsTrue(driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text.Equals("test@test.test"));
        }

        [TestMethod]
        public void User_Should_Fail_Login_With_WrongEmail()
        {
            loginPage.LoginApplication("test@test.testWrong", "test");

            var expectedResult = "Bad email or password.";
            var actualResult = driver.FindElement(By.CssSelector(".alert-notice")).Text;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
	
```

### **Session 4 - 02.05.2022 - Refactor LoginTests and Add test for Adding new address**

**Scope:** This session scope was to create a new test class for adding a new address and to keep the code clean.

Let's start with the refactoring left:

 **LoginPage.cs:**
-	Change the methods for declaring the elements using Lambda expression.
-	Rename the elements declaration to be more sugestive and to describe better what type of elements it’s used.
-	Add a new element for the error message used in the User_Should_Fail_Login_With_WrongEmail test.

A this point, the changes look like this:

```csharp
        private IWebElement TxtEmail => driver.FindElement(By.Id("session_email"));

        private IWebElement TxtPassword => driver.FindElement(By.Id("session_password"));

        private IWebElement BtnLogin => driver.FindElement(By.Name("commit"));

        private IWebElement LblErrorMessage => driver.FindElement(By.CssSelector(".alert-notice"));

        public void LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }

        public string ErrorMessage => LblErrorMessage.Text;
```

**LoginTests.cs**

-	In this class only the assert has to be changed.

```csharp
        [TestMethod]
        public void User_Should_Fail_Login_With_WrongEmail()
        {
            loginPage.LoginApplication("test@test.testWrong", "test");

            Assert.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }
```

Now let's write another login test with correct email and wrong password:

```csharp
        [TestMethod]
        public void User_Should_Fail_Login_With_WrongPassword()
        {
            loginPage.LoginApplication("test@test.test", "testWrong");

            Assert.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }
```
<br>

**Now, moving to the second part of the session, let's write the test for adding a new address**



In order to add a new address, we will need a write code for the next steps:

1. Open the browser
2. Maximize the page
3. Navigate to the application URL
4. Click Sign in button (NavigateToLoginPage method)
5. Fill user email and password, then click Sign in button (LoginApplication method)
6. Navigate to addresses page
7. Navigate to add address page
8. Complete the form with mandatory fields and click Save button
9. Assert that the success message is shown

At step 5, use login actions (fill user email and password, click Sign in button) that is in LoginPage.cs:

```csharp
        public void LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }
```

<br>

In order to navigate to addresses page, we need to create a page object HomePage.cs that contains elements and method for this page:

```csharp
     public class HomePage
        {
            private IWebDriver driver;

            public HomePage(IWebDriver browser)
            {
                driver = browser;
            }

            private IWebElement BtnAddresses => driver.FindElement(By.CssSelector("a[data-test=addresses]"));

            public void NavigateToAddressesPage()
            {
                BtnAddresses.Click();
            }
         }
```

<br>

Next step is to navigate to add address page. For this, we need another page object AddressesPage.cs which contains New Address button declaration and method to click on the element:

```csharp
      public class AddressesPage
        {
            private IWebDriver driver;

            public AddressesPage(IWebDriver browser)
            {
                driver = browser;
            }

            private IWebElement BtnNewAddress => driver.FindElement(By.CssSelector("a[data-test=create]"));

            public AddAddressPage NavigateToAddAddressPage()
            {
                BtnNewAddress.Click();
                return new AddAddressPage(driver);
            }
        }
```

<br>

For step 8 (Complete the form with mandatory fields and click Save button), we will create a page object that contains the elements for the add address page: AddAddressPage.cs. We need to add the objects that we use in our script in this class: first name, last name, address, city, zip code, create address button. Also, create a method to add the address. Our add address page will look like this:

```csharp
        public class AddAddressPage
            {
                private IWebDriver driver;

                public AddAddressPage(IWebDriver browser)
                {
                    driver = browser;
                }

                private IWebElement TxtFirstName => driver.FindElement(By.Id("address_first_name"));

                private IWebElement TxtLastName => driver.FindElement(By.CssSelector("input[name='address[last_name]']"));

                private IWebElement TxtAddress1 => driver.FindElement(By.XPath("//input[@name='address[address1]']"));

                private IWebElement TxtCity => driver.FindElement(By.Id("address_city"));

                private IWebElement TxtZipCode => driver.FindElement(By.Id("address_zip_code"));

                private IWebElement BtnCreateAddress => driver.FindElement(By.XPath("//input[@value='Create Address']"));

                public void AddAddress(string firstName, string lastName, string address1, string city, string zipCode)
                {
                    TxtFirstName.SendKeys(firstName);
                    TxtLastName.SendKeys(lastName);
                    TxtAddress1.SendKeys(address1);
                    TxtCity.SendKeys(city);
                    TxtZipCode.SendKeys(zipCode);
                    BtnCreateAddress.Click();
                }
            }
```

<br>

Let's create class AddAddressTests.cs which will contain our test:

```csharp
        [TestClass]
        public class AddAddressTests
        {
            private IWebDriver driver;
            private AddAddressPage addAddressPage;

            [TestInitialize]
            public void TestInitialize()
            {
                driver = new ChromeDriver();

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

                var btnSignIn = driver.FindElement(By.Id("sign-in"));
                btnSignIn.Click();
                Thread.Sleep(2000);

                var loginPage = new LoginPage(driver);
                loginPage.LoginApplication("test@test.test", "test");

                var homePage = new HomePage(driver);
                homePage.NavigateToAddressesPage();

                var addressesPage = new AddressesPage(driver);
                Thread.Sleep(2000);

                addAddressPage = addressesPage.NavigateToAddAddressPage();
                Thread.Sleep(2000);
            }

            [TestMethod]
            public void User_Should_Add_Address_Successfully()
            {
                addAddressPage.AddAddress("AC FN", "AC LN", "AC address1", "AC city", "AC zipcode");
            }

            [TestCleanup]
            public void TestCleanup()
            {
                driver.Quit();
            }
        }
```

### **Session 5 - 09.05.2022 - Finish AddAddressTests, create BO class and change the wait strategy**

**Scope:** This session scope was to finish the test for adding an address, to create a BO model and to replace the Thread.Sleep with efficient waits.

Let's start with adding the elements left in the AddAddressPage. After that, complete the AddAddress method:

```csharp
        public class AddAddressPage
        {
            private IWebDriver driver;

            public AddAddressPage(IWebDriver browser)
            {
                driver = browser;
            }

            private IWebElement TxtFirstName => driver.FindElement(By.Id("address_first_name"));

            private IWebElement TxtLastName => driver.FindElement(By.CssSelector("input[name='address[last_name]']"));

            private IWebElement TxtAddress1 => driver.FindElement(By.XPath("//input[@name='address[address1]']"));

            private IWebElement TxtCity => driver.FindElement(By.Id("address_city"));

            private IWebElement DdlState => driver.FindElement(By.Id("address_state"));

            private IWebElement TxtZipCode => driver.FindElement(By.Id("address_zip_code"));

            private IList<IWebElement> LstCountry => driver.FindElements(By.CssSelector("input[type=radio]"));

            private IWebElement TxtBirthday => driver.FindElement(By.Id("address_birthday"));

            private IWebElement ClColor => driver.FindElement(By.Id("address_color"));

            private IWebElement BtnCreateAddress => driver.FindElement(By.XPath("//input[@value='Create Address']"));
            
            public void AddAddress(string firstname, string lastName, string address1, string city, string zipCode)
            {
                TxtFirstName.SendKeys(firstName);
                TxtLastName.SendKeys(lastName);
                TxtAddress1.SendKeys(address1);
                TxtCity.SendKeys(city);

                // select from drop-down
                var selectState = new SelectElement(DdlState);
                selectState.SelectByText("Hawaii");

                TxtZipCode.SendKeys(zipCode);

                // select radio button value -> country
                LstCountry[1].Click();

                TxtBirthday.SendKeys("15071999");

                // select color from color picker
                var js = (IJavaScriptExecutor)driver;
                // js.ExecuteScript(script, arguments);
                js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, "#FF0000");

                BtnCreateAddress.Click();
            }
        }
```

<br>

Find Elements command takes in By object as the parameter and returns a list of web elements. It returns an empty list if there are no elements found using the given locator strategy and locator value. Below is the syntax of find elements command. private IList LstCountry => driver.FindElements(By.CssSelector("input[type=radio]"));

The 'Select' class in Selenium WebDriver is used for selecting and deselecting option in a dropdown. The objects of Select type can be initialized by passing the dropdown webElement as parameter to its constructor. var selectState = new SelectElement(DdlState); selectState.SelectByText("Hawaii");

JavaScriptExecutor is an Interface that helps to execute JavaScript through Selenium Webdriver.

Syntax:
```csharp
        var js = (IJavaScriptExecutor) driver; 
        js.ExecuteScript(Script,Arguments);
```

Script – This is the JavaScript that needs to execute.

Arguments – It is the arguments to the script. It's optional.

```csharp
        var js = (IJavaScriptExecutor) driver;
        js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, "#FF0000");
```

Now, let's create another class because, after pressing on the add address button, we will be redirected to another page: AddressDetailsPage.cs.


```csharp
        namespace ACAutomation.PageObjects
        {
            public class AddressDetailsPage
            {
                private IWebDriver driver;

                public AddressDetailsPage(IWebDriver browser)
                {
                    driver = browser;
                }

                // locator to be added when "add" functionality will work
                public IWebElement LblSuccess => driver.FindElement(By.Id(""));
            }
        }
```

In the AddAddressPage, let's change the way of navigating to the AddressDetailsPage after adding the address:

```csharp
        public AddressDetailsPage AddAddress(string firstname, string lastName, string address1, string city, string zipCode)
        {
            ....
            return new AddressDetailsPage(driver);
        }
```

We need to also modify in the other classes the way of navigating to the other pages:

```csharp
        public AddressesPage NavigateToAddressesPage()
        {
            BtnAddresses.Click();

            return new AddressesPage(driver);
        }
```

```csharp
        public HomePage LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            return new HomePage(driver);
        }
```
<br>

***Parametrize AddAddress method in an efficient way***

To parametrize AddAddress method in an efficient way, we can create a business object class called AddAddressBO.cs which will contain the objects needed in the process of adding an address:

```csharp
        public class AddAddressBO
        {
            public string TxtFirstName = "AC FN";
            public string TxtLastName = "AC LN";
            public string TxtAddress1 = "AC address1";
            public string TxtCity = "AC city";
            public string TxtState = "Hawaii";
            public string TxtZipCode = "AC zipcode";
            public string TxtBirthdate = "11072000";
            public string TxtColor = "#FF0000";
        }
```

Then, use it in the AddAddress method as a parameter and to access its properties:

```csharp
        public AddressDetailsPage AddAddress(AddAddressBO address)
        {
            TxtFirstName.SendKeys(address.TxtFirstName);
            TxtLastName.SendKeys(address.TxtLastName);
            ... and so on
        }
```

```csharp
        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            addAddressPage.AddAddress(new AddAddressBO());

            //var addressDetailsPage = new AddressDetailsPage(driver);
            //Assert.AreEqual("Message to be added when the site works", addressDetailsPage.LblSuccess.Text);
        }
```

<br>

***Wait strategy***

There are explicit and implicit waits in Selenium Web Driver. Waiting is having the automated task execution elapse a certain amount of time before continuing with the next step.

You should choose to use Explicit or Implicit Waits.

**• Thread.Sleep**

In particular, this pattern of sleep is an example of explicit waits. So this isn’t actually a feature of Selenium WebDriver, it’s a common feature in most programming languages though.

Thread.Sleep() does exactly what you think it does, it sleeps the thread.

Example:

```csharp
        Thread.Sleep(2000);
```
Warning! Using Thread.Sleep() can leave to random failures (server is sometimes slow), you don't have full control of the test and the test could take longer than it should. It is a good practice to use other types of waits.

**• Implicit Wait**

WebDriver will poll the DOM for a certain amount of time when trying to find an element or elements if they are not immediately available

Example:
```csharp
         driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
```
• Explicit Wait - Wait for a certain condition to occur before proceeding further in the code

In practice, we recommend that you use Web Driver Wait in combination with methods of the Expected Conditions class that reduce the wait. If the element appeared earlier than the time specified during Web Driver wait initialization, Selenium will not wait but will continue the test execution.

Example 1:
```csharp
         var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
         wait.Until(ExpectedConditions.ElementIsVisible(firstName));
```
Example 2:

```csharp
        public AddAdressPage(IWebDriver browser)
        {
            driver = browser;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(FirstName));
        }

        private By FirstName = By.Id("address_first_name");
        private IWebElement TxtFirstName => driver.FindElement(FirstName);
```

<br>

**NOTE:** Check the commit "Changed the way of using waits." to see the changes made in the tests for waiting the elements, the package instaled, and the WaitHelpers.cs

### **Session 6 - 16.05.2022 - Work with grids. Write test for Edit Address functionality**

**Scope:** This session scope was to handle grid in our automation script. At this point, we have tests for the Login and for Add new address functionalities, but the application has also other functionalities implemented, like: edit an existing address and delete address. This can be done in the Addresses page. Within this page we have a grid that contains a list of addresses(table row > tr) and each address contains it's details(table data > td) and the possibility to view/edit/delete it.

- Today we will create a test for editing an existing address. Given that the add new address flow is malfunctioning, it would be a shame to delete the data left in the application. (Eventually we will handle the delete flow next session, but without confirming the actual deletion, upon the confirmation we will select the cancel option, but we will learn how to interact with browser alerts).
- Concerning the edit address flow, we need to prepare our ground a bit, so first we will refactor a bit AddAddressBO.cs. This way it will be easier for us to use the Business Object later on when editing an address.

After making the changes the **AddAddressBO.cs** will look like this:

```csharp
        public class AddAddressBO
        {
            public string FirstName { get; set; } = "AC FN Default";
            public string LastName { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Color { get; set; }
        }
```
<br>

Let’s refactor also the Add Address test:

```csharp
        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "AC LN",
                Address1 = "AC address1",
                City = "AC city",
                State = "Hawaii",
                ZipCode = "AC zipcode",
                Color = "#FF0000"
            };

            addEditAddressPage = addressesPage.NavigateToAddAddressPage();
            var addressDetailsPage = addEditAddressPage.AddEditAddress(inputData);

            //Assert.AreEqual("Message to be added", addressDetailsPage.LblSuccess.Text);
        }
```
<br>

We also need to adapt a little the **[TestInitialize]** so we can use it in both tests (adding and editing an address).

```csharp
        [TestClass]
        public class AddAddressTests
        {
            private IWebDriver driver;
            private AddAddressPage addAddressPage;
            private AddressesPage addressesPage;

            [TestInitialize]
            public void TestInitialize()
            {
                driver = new ChromeDriver();

                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

                var btnSignIn = driver.FindElement(By.Id("sign-in"));
                btnSignIn.Click();

                var loginPage = new LoginPage(driver);
                var homePage = loginPage.LoginApplication("test@test.test", "test");
                addressesPage = homePage.NavigateToAddressesPage();   
            }

```
<br>

**Let’s run the test in order to make sure we didn’t break anything.**

Now we go to **AddressesPage.cs** and we have to add the web elements for the edit address button and the **NavigateToEditAddressesPage()** method.

To improve the things, we can rename the AddAddress() method to AddEditAddress() and the AddAddressPage.cs to AddEditAddressPage.cs.

```csharp
        public class AddEditAddressPage
        {
            private IWebDriver driver;

            public AddEditAddressPage(IWebDriver browser)
            {
                driver = browser;
            }

            private By FirstName = By.Id("address_first_name");
            private IWebElement TxtFirstName => driver.FindElement(FirstName);

            private By LastName = By.CssSelector("input[name='address[last_name]']");
            private IWebElement TxtLastName => driver.FindElement(LastName);

            private By Address1 = By.XPath("//input[@name='address[address1]']");
            private IWebElement TxtAddress1 => driver.FindElement(Address1);

            private By City = By.Id("address_city");
            private IWebElement TxtCity => driver.FindElement(City);

            private By State = By.Id("address_state");
            private IWebElement DdlState => driver.FindElement(State);

            private By ZipCode = By.Id("address_zip_code");
            private IWebElement TxtZipCode => driver.FindElement(ZipCode);

            private By Country = By.CssSelector("input[type=radio]");
            private IList<IWebElement> LstCountry => driver.FindElements(Country);

            private By Color = By.Id("address_color");
            private IWebElement ClColor => driver.FindElement(Color);

            private By Submit = By.XPath("//input[@data-test='submit']");
            private IWebElement BtnSubmit => driver.FindElement(Submit);

            public AddressDetailsPage AddEditAddress(AddAddressBO address)
            {
                WaitHelpers.WaitForElementToBeVisible(driver, Submit);

                TxtFirstName.Clear();
                TxtFirstName.SendKeys(address.FirstName);
                TxtLastName.Clear();
                TxtLastName.SendKeys(address.LastName);
                TxtAddress1.SendKeys(address.Address1);
                TxtCity.Clear();
                TxtCity.SendKeys(address.City);

                // select from drop-down
                var selectState = new SelectElement(DdlState);
                selectState.SelectByText(address.State);

                TxtZipCode.SendKeys(address.ZipCode);

                // select radio button value -> country
                LstCountry[1].Click();

                // select color from color picker
                var js = (IJavaScriptExecutor)driver;
                // js.ExecuteScript(script, arguments);
                js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, address.Color);

                BtnSubmit.Click();

                return new AddressDetailsPage(driver);
            }
        }
```
<br>

Now we go to **AddAddressPage.cs** class to adapt a few things so that we can use the same elements in Adding and Editing tests.

**One change we can make is for the CreateAddress button. If we inspect the CreateAddress and UpdateAddress buttons we can see that they have a few things in common. This means we can identify the two elements by the same XPath. Also we rename it to be more generic.**

After making the changes the AddAdressPage.cs will look like this:

```csharp
 public class AddAddressPage
    {
        private IWebDriver driver;

        public AddAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

        private By FirstName = By.Id("address_first_name");
        private IWebElement TxtFirstName => driver.FindElement(FirstName);

        private By LastName = By.CssSelector("input[name='address[last_name]']");
        private IWebElement TxtLastName => driver.FindElement(LastName);

        private By Address1 = By.XPath("//input[@name='address[address1]']");
        private IWebElement TxtAddress1 => driver.FindElement(Address1);

        private By City = By.Id("address_city");
        private IWebElement TxtCity => driver.FindElement(City);

        private By State = By.Id("address_state");
        private IWebElement DdlState => driver.FindElement(State);

        private By ZipCode = By.Id("address_zip_code");
        private IWebElement TxtZipCode => driver.FindElement(ZipCode);

        private By Country = By.CssSelector("input[type=radio]");
        private IList<IWebElement> LstCountry => driver.FindElements(Country);

        private By Color = By.Id("address_color");
        private IWebElement ClColor => driver.FindElement(Color);

        private By Submit = By.XPath("//input[@data-test='submit']");
        private IWebElement BtnSubmit => driver.FindElement(Submit);

        public AddressDetailsPage AddEditAddress(AddAddressBO address)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, Submit);

            TxtFirstName.Clear();
            TxtFirstName.SendKeys(address.FirstName);
            TxtLastName.Clear();
            TxtLastName.SendKeys(address.LastName);
            TxtAddress1.SendKeys(address.Address1);
            TxtCity.Clear();
            TxtCity.SendKeys(address.City);

            // select from drop-down
            var selectState = new SelectElement(DdlState);
            selectState.SelectByText(address.State);

            TxtZipCode.SendKeys(address.ZipCode);

            // select radio button value -> country
            LstCountry[1].Click();

            // select color from color picker
            var js = (IJavaScriptExecutor)driver;
            // js.ExecuteScript(script, arguments);
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, address.Color);

            BtnSubmit.Click();

            return new AddressDetailsPage(driver);
        }
    }
```
<br>

Now, let’s add the test for editing an existing address. We will write the test in AddAddressTests so we can re-use the [TestInitialize] and [TestCleanup] that we have already wtitten.

```csharp
        [TestMethod]
        public void User_Should_Edit_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                FirstName = "AC First Name1 edit 1",
                LastName = "AC edit",
                Address1 = "AC address1 edit",
                City = "AC city edit",
                State = "Arizona",
                ZipCode = "AC zipc",
                Color = "#17A2B8"
            };

            addEditAddressPage = addressesPage.NavigateToEditAddressPage(inputData.FirstName);

            var addressDetailsPage = addEditAddressPage.AddEditAddress(inputData);

            Assert.AreEqual("Address was successfully updated.", addressDetailsPage.NoticeText);
        }
```
<br>

In order to make the assert, we need to add another element in the **AddressDetailsPage** class.

```csharp
        public class AddressDetailsPage
        {
            private IWebDriver driver;

            public AddressDetailsPage(IWebDriver browser)
            {
                driver = browser;
            }

            // locator to be added when "add" functionality will work
            public IWebElement LblSuccess => driver.FindElement(By.Id(""));

            private IWebElement LblNotice => driver.FindElement(By.CssSelector("div[data-test=notice]"));

            public string NoticeText => LblNotice.Text;
        }
```
<br>
<br>
<br>
<br>
<br>
<br>
<br>

### **References**

Getting started: 
- https://www.automatetheplanet.com/getting-started-webdriver/ 
- official documentation: https://www.selenium.dev/documentation/en/

Page object model 
- https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/ 
- https://www.automatetheplanet.com/page-object-pattern/ 
- https://huddle.eurostarsoftwaretesting.com/selenium-page-objects-page-object-model/ 
- https://testautomationu.applitools.com/test-automation-framework-csharp/chapter3.html

Waits: 
- https://www.toolsqa.com/selenium-webdriver/c-sharp/advance-explicit-webdriver-waits-in-c/ 
- https://www.lambdatest.com/blog/explicit-fluent-wait-in-selenium-c/ 
- https://dzone.com/articles/selenium-c-tutorial-using-explicit-and-fluent-wait 
- https://alexsiminiuc.medium.com/c-expected-conditions-are-deprecated-so-what-b451365adc24 
- https://testautomationu.applitools.com/test-automation-framework-csharp/chapter12.html

Others: 
- Select dropdown - https://www.toolsqa.com/selenium-webdriver/c-sharp/dropdown-multiple-select-operations-in-c/ 
- Javascript executor - https://www.c-sharpcorner.com/article/execution-of-selenium-web-driver-using-c-sharp-javascript/
