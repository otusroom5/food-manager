using FoodUserNotifier.Infrastructure.Sources.Contracts.Requests;
using Microsoft.AspNetCore.Http;

namespace FoodUserNotifier.Infrastructure.Sources.Extensions;

internal static class FindRecepientRequestExtensions
{
    public static QueryString ToQueryString(this FindRecepientRequest request)
    {

        var pairs = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("Contact", request.Contact),
            new KeyValuePair<string, string>("ContactType", Enum.GetName(request.ContactType))
        };


        return QueryString.Create(pairs);
    }
}
