using System.Text;
using System.Text.Json;

namespace SmartAssistant.Services;

public static class Serializer
{
    public static string SaveDirectory { get; set; } = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Smart Assistant");

    public static async Task SaveJsonToFile<T>(string fileName, T objectToSerialize)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(objectToSerialize, options);

        Directory.CreateDirectory(SaveDirectory);
        await File.WriteAllTextAsync(Path.Combine(SaveDirectory, fileName), jsonString);
    }

    public static async Task<T> DeserializeJsonFile<T>(string fileName)
    {
        var jsonString = await File.ReadAllTextAsync(Path.Combine(SaveDirectory, fileName));
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        var result = await JsonSerializer.DeserializeAsync<T>(stream);
        return result;
    }
}