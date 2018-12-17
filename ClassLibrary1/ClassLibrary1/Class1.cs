using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimHomWork2
{
	[TestFixture]
	public class Class1
	{
		IWebDriver driver;
		IJavaScriptExecutor jexec;
		string downloadDirectoryPath;

		[SetUp]
		public void SetUp()
		{
			downloadDirectoryPath = PrepareDownloadDirectory();
			var options = new ChromeOptions();
			options.AddUserProfilePreference("download.default_directory", downloadDirectoryPath);

			driver = new ChromeDriver(options);
			driver.Manage().Window.Maximize();
			//driver = new ChromeDriver();
			jexec = (IJavaScriptExecutor)driver;
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
		}

		[Test]
		public void DownloadFile()
		{
			//IJavaScriptExecutor jexec;
			var options = new ChromeOptions();
			driver.Navigate().GoToUrl("https://unsplash.com/search/photos/test");

			//locators for use
			var picture = By.CssSelector("#gridMulti > div:nth-child(2) > div:nth-child(12) > div");
			var download = By.XPath("//span[@class='_2Aga-']");
			//var dowload2 = driver.FindElements(By.XPath("//*[@id='gridMulti']/div[2]/div[12]/div/figure/div[1]/div/div[3]/div[2]"));

			jexec.ExecuteScript("window.scrollBy(0, 8000);");
			System.Threading.Thread.Sleep(2000);

			//jexec.ExecuteScript($"arguments[1].click()", picture);

			driver.FindElement(picture).Click();
			driver.FindElement(download).Click();
			System.Threading.Thread.Sleep(10000);

			//jexec.ExecuteScript($"arguments[0].click()", download); 


			Assert.That(Directory.GetFiles(downloadDirectoryPath), Is.Not.Empty);
			/*
			 * чомусь не виходить клікнути джаваскріптом, не шарю шо не так
			 */
		}

		private string PrepareDownloadDirectory()
		{
			var downloadDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Downloads", DateTime.Now.ToString("yy-MM-dd HH-mm-ss"));

			Directory.CreateDirectory(downloadDirectoryPath);

			return downloadDirectoryPath;
		}

	}
}
