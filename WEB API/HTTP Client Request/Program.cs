using System.Text.Json;
using System.Text.Json.Serialization;

public class ResponseObject
{
    public int userId { get; set; }

    public int id { get; set; }

    public string? title { get; set; }

    public bool completed { get; set; }
}
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

        try
        {
            HttpResponseMessage result = await client.GetAsync($"https://jsonplaceholder.typicode.com/todos/{count}");
            result.EnsureSuccessStatusCode();

            string responseBody = await result.Content.ReadAsStringAsync();

            ResponseObject? resp = JsonSerializer.Deserialize<ResponseObject>(responseBody);

            if (resp != null)
                WriteToFile(resp);

            throw new Exception();
        }
        catch
        {
            throw new Exception("Couldn't get response from client");
        }
        finally
        {
            client.Dispose();
        }
    }

    static void WriteToFile(ResponseObject resp)
    {
        string[] textBody = $"{resp.userId}+{resp.id}+{resp.title}+{resp.completed}+".Split('+');

       for (int i = 0; i < textBody.Length; i++)
            Console.WriteLine(textBody[i]);

        File.AppendAllLines(filePath, textBody);

    }
}
