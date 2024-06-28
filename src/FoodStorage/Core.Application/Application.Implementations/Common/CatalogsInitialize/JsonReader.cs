using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FoodStorage.Application.Implementations.Common.CatalogsInitialize;

internal interface IJsonReader
{
    public Task<IReadOnlyList<TObject>> ReadAsync<TObject>(string filePath);
}

internal class JsonReader : IJsonReader
{
    private readonly ILogger<JsonReader> _logger;

    public JsonReader(ILogger<JsonReader> logger)
    {
        _logger = logger;
    }

    public async Task<IReadOnlyList<TObject>> ReadAsync<TObject>(string filePath)
    {
        string jsonName = Path.GetFileName(filePath);

        _logger.LogTrace("Reading file '{jsonName}.json'", jsonName);

        List<TObject> result = new();

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("File '{jsonName}.json' not exists", jsonName);
            return result;
        }

        try
        {
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.None);

            List<TObject> objects = await JsonSerializer.DeserializeAsync<List<TObject>>(fs, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            result.AddRange(objects);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error reading file '{jsonName}.json': '{error}'", jsonName, ex.Message);
            throw;
        }

        _logger.LogTrace("Reading file '{jsonName}.json' completed", jsonName);

        return result;
    }
}
