using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BasketSaucedemo {
    public class Tests {
        IWebDriver driver;
        [SetUp]
        public void Setup() {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            IWebElement login = driver.FindElement(By.Id("user-name"));
            login.SendKeys("problem_user");
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("secret_sauce");
            IWebElement loginBtn = driver.FindElement(By.Id("login-button"));
            loginBtn.Click();
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", driver.Url);
        }
        [TearDown]
        public void CleanUp() {
            driver.Quit();
        }

        [Test]
        public void PutProductAtTheBusket() {
            IWebElement AddToCard = driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            AddToCard.Click();
            IWebElement GoToTheBasket = driver.FindElement(By.ClassName("shopping_cart_link"));
            GoToTheBasket.Click();
            Assert.That(driver.FindElement(By.ClassName("inventory_item_name")).Displayed);
        }

        [Test]
        public void RemoveProduct() {
            IWebElement AddToCard = driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            AddToCard.Click();
            IWebElement AddToCardNewProduct = driver.FindElement(By.Id("add-to-cart-sauce-labs-onesie"));
            AddToCardNewProduct.Click();
            IWebElement GoToTheBasket = driver.FindElement(By.ClassName("shopping_cart_link"));
            GoToTheBasket.Click();
            Assert.That(driver.FindElements(By.ClassName("cart_item")).Count == 2);
            Assert.IsNotNull(driver.FindElement(By.Id("item_0_title_link")));
            IWebElement RemoveProduct = driver.FindElement(By.Id("remove-sauce-labs-bike-light"));
            RemoveProduct.Click();
            Assert.That(driver.FindElements(By.ClassName("cart_item")).Count == 1);
            Assert.That(driver.FindElements(By.Id("item_0_title_link")).Count == 0);
        }

        [Test]
        public void CheckoutProduct() {
            IWebElement AddToCard = driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            AddToCard.Click();
            IWebElement GoToTheBasket = driver.FindElement(By.ClassName("shopping_cart_link"));
            GoToTheBasket.Click();
            IWebElement CheckoutBtn = driver.FindElement(By.Id("checkout"));
            CheckoutBtn.Click();
            IWebElement FirstName = driver.FindElement(By.Id("first-name"));
            FirstName.SendKeys("Maksim");
            IWebElement PostalCode = driver.FindElement(By.Id("first-name"));
            PostalCode.SendKeys("20-V54");
            IWebElement ContinueBtn = driver.FindElement(By.Id("continue"));
            ContinueBtn.Click();
            IWebElement error = driver.FindElement(By.CssSelector("#checkout_info_container > div > form > div.checkout_info > div.error-message-container.error > h3"));
            Assert.AreEqual("Error: Last Name is required", error.Text);
            Assert.AreNotEqual("Epic sandface:Username and password do not match any any users in this service", error.Text);
            Assert.That(error.Displayed);
        }

        [Test]
        public void ContinueShopping() {
            IWebElement AddToCard = driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            AddToCard.Click();
            IWebElement GoToTheBasket = driver.FindElement(By.ClassName("shopping_cart_link"));
            GoToTheBasket.Click();
            IWebElement SauceLabs = driver.FindElement(By.Id("item_0_title_link"));
            IWebElement ContinueShoppingBtn = driver.FindElement(By.Id("continue-shopping"));
            ContinueShoppingBtn.Click();
            Assert.IsFalse(driver.FindElements(By.ClassName("cart_item")).Count == 2);
        }
    }
}