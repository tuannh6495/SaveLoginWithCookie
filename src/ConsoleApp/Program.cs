using Microsoft.Playwright;

class Program
{
    static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();

        // Khởi tạo trình duyệt và context đầu tiên
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        // Truy cập trang web
        await page.GotoAsync("https://sosanhnha.com/");

        Console.WriteLine("Vui lòng đăng nhập thủ công. Nhấn Enter khi đã đăng nhập xong.");
        Console.ReadLine();

        // Lưu trạng thái storage (bao gồm cả cookies)
        await context.StorageStateAsync(new BrowserContextStorageStateOptions
        {
            Path = "state.json"
        });


        await browser.CloseAsync();

        // Khởi tạo trình duyệt và context đầu tiên
        await using var browser2 = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
        });
        var context2 = await browser2.NewContextAsync();
        var page2 = await context2.NewPageAsync();
        // Khởi tạo trình duyệt và context mới với trạng thái đã lưu
        var newContext = await browser2.NewContextAsync(new BrowserNewContextOptions
        {
            StorageStatePath = "state.json"
        });

        var newPage = await newContext.NewPageAsync();
        await newPage.GotoAsync("https://sosanhnha.com/");

        Console.WriteLine("Đã tự động đăng nhập bằng trạng thái đã lưu. Kiểm tra trình duyệt mới.");
        Console.ReadLine();
    }
}