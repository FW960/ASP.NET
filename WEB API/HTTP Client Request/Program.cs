
class Program
{
    static string filePath = @"C:\Users\windo\source\repos\ASP.NET\WEB API\HTTP Client Request\result.txt";

    static void Main()
    {

        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        fs.Close();

        int count = 4;

        while (count != 14)
        {
            Task.Run(() => GetPageBody(count)).Wait();

            count++;
        }
    }

    static async Task GetPageBody(int count)
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage result = await client.GetAsync($"https://jsonplaceholder.typicode.com/todos/{count}");
        result.EnsureSuccessStatusCode();

        string responseBody = await result.Content.ReadAsStringAsync();

        Parse(responseBody);
    }

    static void Parse(string responseBody)
    {
        string text = "";

        for (int i = 0; i < responseBody.Length; i++)
        {
            if (responseBody[i] == ':')
            {
                i = i + 2;

                for (int j = i; j < responseBody.Length; j++)
                {
                    if (j == responseBody.Length - 2)
                    {
                        text += "+";

                        i = j;
                        break;
                    }

                    if (responseBody[j] == '"')
                        continue;

                    if (responseBody[j] == ',')
                    {
                        text += "+";

                        i = j;
                        break;
                    }

                    text += responseBody[j];

                }

            }
        }

        string[] result = text.Split('+');

        WriteToFile(result);
    }

    static void WriteToFile(string[] result) => File.AppendAllLines(filePath, result);

}
