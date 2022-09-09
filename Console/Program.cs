WithAsync();
Console.WriteLine("Doing something in the meantime...");
Thread.Sleep(2000);
ReportThread();
Console.WriteLine();


static void ReportThread()
{
    Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
}

static async void WithAsync()
{
    var client = new HttpClient();

    Console.WriteLine("Starting call...");

    ReportThread();

    var response = await client.GetAsync(new Uri("https://bbc.co.uk"));

    ReportThread();

    Console.WriteLine("Results in...");

    Console.WriteLine($"{new StreamReader(response.Content.ReadAsStream()).ReadLine()?.Substring(0, 80) ?? ""}...");
}
