using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.WebApi.Extensions;

public static class UserRoleExtension
{
    public const string AdministrationRole = "administrator";
    public const string CookerRole = "cooker";
    public const string ManagerRole = "manager";

    /// <summary>
    /// This method returns symbolic alias of enumeration type
    /// </summary>
    /// <returns>string</returns>
    /// 
    public static string ConvertToString(this UserRole role) => role switch
    {
        UserRole.Cooker => CookerRole,
        UserRole.Manager => ManagerRole,
        _ => AdministrationRole
    };
}
