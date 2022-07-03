using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adspowers.Puppeteer
{
    internal class BrowserControl
    {
        private Browser Browser; 
        public BrowserControl(string address) 
        {
            var options = new ConnectOptions()
            {
                BrowserWSEndpoint = address
            };

            this.Browser = PuppeteerSharp.Puppeteer.ConnectAsync(options).GetAwaiter().GetResult();
        }
        public async void Start(string email, string refcode) 
        {
            var Page = await this.Browser.NewPageAsync();
            await Page.SetViewportAsync(new ViewPortOptions() { Width = 1920, Height = 1080 });

            if (refcode == "")
            {
                await Page.GoToAsync("https://noah.com/", WaitUntilNavigation.Networkidle0);
            }
            else 
            {
                await Page.GoToAsync(refcode, WaitUntilNavigation.Networkidle0);
            }

            var inputTextbox = await Page.XPathAsync("//input[@data-qa='hero-email-input']");
            await inputTextbox.First().ClickAsync();
            await Page.Keyboard.SendCharacterAsync(email);

            var submitButton = await Page.XPathAsync("//button[@data-qa='hero-join-button']");
            await submitButton.First().ClickAsync();
        }

        public async void CheckValidate(string link) 
        {
            var Page = await this.Browser.NewPageAsync();
            await Page.SetViewportAsync(new ViewPortOptions() { Width = 1920, Height = 1080 });

            await Page.GoToAsync(link, WaitUntilNavigation.Networkidle0);
        }
    }
}
