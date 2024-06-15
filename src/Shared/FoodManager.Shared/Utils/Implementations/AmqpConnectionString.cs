using FoodManager.Shared.Utils.Interfaces;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace FoodManager.Shared.Utils.Implementations;

public class AmqpConnectionStringBuilder : IAmqpConnectionString
{
    private const string HostArg = "host";
    private const string UserNameArg = "username";
    private const string PortArg = "port";
    private const string UserPasswordArg = "password";
    private const string QueueNameArg = "queue";

    private readonly Dictionary<string, string> _dict;

    private AmqpConnectionStringBuilder(Dictionary<string, string> dict)
    {
        _dict = dict;
    }

    public string GetHost()
    {
        return _dict.TryGetValue(HostArg, out var host) ? host : throw new InvalidConfigurationException();
    }

    public string GetQueueName()
    {
        return _dict.TryGetValue(QueueNameArg, out var queueName) ? queueName : throw new InvalidConfigurationException();
    }

    public string GetUserName()
    {
        return _dict.TryGetValue(UserNameArg, out var userName) ? userName : throw new InvalidConfigurationException();
    }
    public string GetUserPassword()
    {
        return _dict.TryGetValue(UserPasswordArg, out var userPassword) ? userPassword : throw new InvalidConfigurationException();
    }

    public static IAmqpConnectionString Parse(string connectionString)
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

        return new AmqpConnectionStringBuilder(result);
    }

    public int GetPort()
    {
        string port = _dict.TryGetValue(PortArg, out port) ? port : throw new InvalidConfigurationException();

        return int.TryParse(port, out int servicePort) ? servicePort : throw new InvalidConfigurationException();
    }
}
