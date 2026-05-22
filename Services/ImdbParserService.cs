using Microsoft.Playwright;

namespace IMDbTop250Scraper.Services
{
    public class ImdbParserService
    {
        public async Task<List<Movie>> ParseAsync(string url)
        {
            using var playwright =
                await Playwright.CreateAsync();

            await using var browser =
                await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = true
                    });

            var context =
                await browser.NewContextAsync(
                    new BrowserNewContextOptions
                    {
                        UserAgent =
                            "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",

                        ViewportSize = new ViewportSize
                        {
                            Width = 1920,
                            Height = 1080
                        },

                        Locale = "en-US"
                    });

            var page =
                await context.NewPageAsync();

            await page.GotoAsync(
                url,
                new PageGotoOptions
                {
                    WaitUntil =
                        WaitUntilState.DOMContentLoaded,

                    Timeout = 60000
                });

            await page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);

            await page.WaitForFunctionAsync(@"
() => document.querySelectorAll(
'li.ipc-metadata-list-summary-item'
).length >= 250
");

            // cookie popup
            var acceptButton =
                page.Locator("button:has-text('Accept')");

            if(await acceptButton.CountAsync() > 0)
            {
                await acceptButton.First.ClickAsync(
                    new()
                    {
                        Timeout = 3000
                    });
            }

            var movies =
                await page.EvaluateAsync<Movie[]>(@"
() => {
    return Array.from(
        document.querySelectorAll(
            'li.ipc-metadata-list-summary-item'
        )
    ).map((movie, index) => {

        const titleElement =
            movie.querySelector('h3.ipc-title__text');

        const ratingElement =
            movie.querySelector('.ipc-rating-star--rating');

        const voteCountElement =
            movie.querySelector('.ipc-rating-star--voteCount');

        const yearElement =
            movie.querySelector('li.ipc-inline-list__item');

        const linkElement =
            movie.querySelector('a');

        const imgElement =
            movie.querySelector('img.ipc-image');

        const poster =
            imgElement ? imgElement.src : '';

        const posterBig =
            poster.replace(/\._V1_.*\.jpg/, '._V1_.jpg');

        return {
            position: index + 1,

            title:
                titleElement
                ? titleElement.innerText
                : '',

            rating:
                ratingElement
                ? ratingElement.innerText
                : '',

            voteCount:
                voteCountElement
                ? voteCountElement.innerText
                : '',

            year:
                yearElement
                ? yearElement.innerText
                : '',

            poster:
                posterBig,

            link:
                linkElement
                ? linkElement.href
                : ''
        };
    });
}
");

            return movies.ToList();
        }
    }
}