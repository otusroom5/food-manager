using FoodManager.WebUI.Models;

namespace FoodManager.WebUI.Services.Interfaces;

public interface IAccountService
{
    public Task<(string Token, string Role)> LogInAsync(string login, string password);
}
