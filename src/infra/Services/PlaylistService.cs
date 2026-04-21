using domain.Entities;
using domain.Interfaces.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace infra.Services
{
    /// <summary>
    /// Utiliza Selenium para importar vídeos de uma playlist do youtube
    /// </summary>
    public class PlaylistService : IPlaylistService
    {
        private readonly ChromeDriver _driver;
        private readonly WebDriverWait _wait;

        public PlaylistService()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("intl.accept_languages", "pt-BR,pt");
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        public Playlist ScrapVideos(string playlistUrl)
        {
            var playlist = ScrapPlaylist(playlistUrl);

            ScrollToBottom(_driver);

            var videoUrls = _driver
                .FindElements(By.XPath("//ytd-playlist-video-renderer"))
                .Select(el => el.FindElements(By.XPath(".//a[@id='video-title']")) is { Count: > 0 } links
                    ? links[0].GetAttribute("href")
                    : null)
                .Where(url => !string.IsNullOrEmpty(url))
                .Cast<string>()
                .ToList();

            playlist.Videos = videoUrls.Select(url =>
            {
                _driver.Navigate().GoToUrl(url);

                var descAccordion = _wait.Until(d =>
                {
                    var els = d.FindElements(By.XPath("//*[@id='description-inner']"));
                    return els.Count > 0 && els[0].Displayed ? els[0] : null;
                });
                descAccordion!.Click();

                var descEls = _driver.FindElements(By.XPath("//*[@id='expanded']/yt-attributed-string/span/span[1]"));
                return new Playlist.Video
                {
                    Url = url,
                    Title = GetVideoTitle(),
                    Description = descEls.Count > 0 ? descEls[0].Text : null
                };
            }).ToList();

            _driver.Close();
            _driver.Dispose();

            return playlist;
        }

        private string? GetPlaylistDescription()
        {
            var buttons = _driver.FindElements(By.XPath("//*[@id='page-header']/yt-page-header-renderer/yt-page-header-view-model/div[2]/div[1]/div/yt-description-preview-view-model/truncated-text/button"));
            if (buttons.Count == 0)
                return null;

            buttons[0].Click();

            var messageEls = _driver.FindElement(By.XPath("//*[@id='message']"));
            return messageEls.Text;
        }

        private string GetVideoTitle()
        {
            var el = _driver.FindElement(By.XPath("//*[@id='title']/h1/yt-formatted-string"));
            return el.GetAttribute("title")!;
        }

        private static void ScrollToBottom(IWebDriver driver)
        {
            var js = (IJavaScriptExecutor)driver;
            long lastHeight = -1;

            while (true)
            {
                js.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight)");
                Thread.Sleep(1500);

                long newHeight = Convert.ToInt64(js.ExecuteScript("return document.documentElement.scrollHeight"));
                if (newHeight == lastHeight) break;
                lastHeight = newHeight;
            }
        }

        private Playlist ScrapPlaylist(string playlistUrl)
        {
            playlistUrl = Uri.UnescapeDataString(playlistUrl);
            _driver.Navigate().GoToUrl(playlistUrl);

            _wait.Until(d =>
                d.FindElements(By.XPath("//yt-dynamic-text-view-model/h1/span")).Count > 0 ||
                d.FindElements(By.TagName("yt-alert-renderer")).Count > 0
            );

            var alert = _driver.FindElements(By.CssSelector("yt-alert-renderer yt-formatted-string"));
            if (alert.Count > 0)
                throw new InvalidOperationException(alert[0].Text);

            var playlistTitle = _driver
                .FindElement(By.XPath("//yt-dynamic-text-view-model/h1/span"))
                .Text;

            return new()
            {
                Url = playlistUrl,
                Title = playlistTitle,
                Description = GetPlaylistDescription(),
            };
        }

    }
}
