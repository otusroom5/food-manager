using System.Text;
using FoodManager.Shared.Utils.Interfaces;

namespace FoodManager.Shared.Utils.Implementations;

public class ServiceConnectionBuilder : IServiceConnection
{
    private const string HostArg = "host";
    private const string PortArg = "port";
    private const string SchemaArg = "schema";

    private readonly Dictionary<string, string> _dict;

    private ServiceConnectionBuilder(Dictionary<string, string> dict)
    {
        _dict = dict;
    }

    public ServiceConnectionBuilder()
    {
        _dict = new Dictionary<string, string>();
    }

    public ServiceConnectionBuilder SetHost(string host)
    {
        _dict[HostArg] = host;
        return this;
    }

    public string GetHost(string defaultHost = "")
    {
        if (!_dict.TryGetValue(HostArg, out var host))
        {
            return defaultHost;
        }

        return host;
    }

    public ServiceConnectionBuilder SetPort(int port)
    {
        _dict[PortArg] = port.ToString();
        return this;
    }

    public int GetPort(int defaultPort = 0)
    {
        if (!_dict.TryGetValue(PortArg, out var port))
        {
            return defaultPort;
        }

        return int.Parse(port);
    }

    public string GetSchema(string defaultSchema = "")
    {
        if (!_dict.TryGetValue(SchemaArg, out var schema))
        {
            return defaultSchema;
        }

        return schema;
    }

    public ServiceConnectionBuilder SetSchema(string schema)
    {
        _dict[SchemaArg] = schema;
        return this;
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        foreach (var item in _dict)
        {
            result.AppendJoin(";", $"{item.Key}={item.Value}");
        }

        return result.ToString();
    }

    public static IServiceConnection Parce(string connectionString)
    {
        var result = new Dictionary<string, string>();


        foreach (var keyValue in connectionString.ToLower().Split(';'))
        {
            var tokens = keyValue.Split('=');

            if (tokens.Length != 2)
            {
                continue;
            }

            result.Add(tokens[0], tokens[1]);
        }

        return new ServiceConnectionBuilder(result);
    }
}
