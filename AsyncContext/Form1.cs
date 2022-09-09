using System.Diagnostics;
namespace AsyncContext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void ReportThread()
        {
            Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
        }

        private async void BtnGo_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            lbxList.Items.Add("Starting call...");

            ReportThread();

            int count = await CallClient(client);

            ReportThread();

            lbxList.Items.Add("Results in...");

            lbxList.Items.Add($"Found {count} BBCs");

            static async Task<int> CallClient(HttpClient client)
            {
                var response = await client.GetAsync(new Uri("https://bbc.co.uk"))
                                           .ConfigureAwait(false);

                ReportThread();

                int count = CountInstances(response.Content.ReadAsStream());
                
                return count;
            }
        }

        private static int CountInstances(Stream stream)
        {
            int count = 0;

            var reader = new StreamReader(stream);

            var line = reader.ReadLine();

            while (line != null)
            {
                Thread.Sleep(5);
                count += line.Contains("BBC") ? 1 : 0;
                line = reader.ReadLine();
            }

            return count;
        }
    }
}