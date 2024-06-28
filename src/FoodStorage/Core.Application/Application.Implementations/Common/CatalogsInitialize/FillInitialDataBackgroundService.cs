using FoodStorage.Application.Services;
using FoodStorage.Application.Services.RequestModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodStorage.Application.Implementations.Common.CatalogsInitialize;

public sealed class FillInitialDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<FillInitialDataBackgroundService> _logger;

    private IJsonReader _jsonReader;
    private IUnitService _unitService;
    private IProductService _productService;

    private const string UnitsFileName = "Units";
    private const string ProductsFileName = "Products";

    public FillInitialDataBackgroundService(IServiceProvider services, ILogger<FillInitialDataBackgroundService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _services.CreateScope();

        _jsonReader = scope.ServiceProvider.GetRequiredService<IJsonReader>();
        _unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
        _productService = scope.ServiceProvider.GetRequiredService<IProductService>();

        await InitUnits();
        await InitProducts();
    }

    private async Task InitUnits()
    {
        _logger.LogTrace("Begin initializing units");

        try
        {
            string filePath = GetInitJsonPath(UnitsFileName);
            IReadOnlyList<UnitCreateRequestModel> units = await _jsonReader.ReadAsync<UnitCreateRequestModel>(filePath);

            if (units.Any())
            {
                var unitsFromBase = await _unitService.GetAllAsync();

                List<string> unitIds = units.Select(u => u.Id).ToList();
                List<string> dbUnitIds = unitsFromBase.Where(u => unitIds.Contains(u.Id))
                                                      .Select(u => u.Id)
                                                      .ToList();

                List<UnitCreateRequestModel> unitsExists = units.Where(u => dbUnitIds.Contains(u.Id)).ToList();
                List<UnitCreateRequestModel> unitsToAdd = units.Except(unitsExists).ToList();

                foreach (var unit in unitsToAdd)
                {
                    await _unitService.CreateAsync(unit);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Units initializing error: '{error}'.", ex.Message);
            return;
        }

        _logger.LogTrace("Units initializing completed");
    }

    private async Task InitProducts()
    {
        _logger.LogTrace("Begin initializing products");

        try
        {
            string filePath = GetInitJsonPath(ProductsFileName);
            IReadOnlyList<ProductCreateRequestModel> products = await _jsonReader.ReadAsync<ProductCreateRequestModel>(filePath);

            if (products.Any())
            {
                var productsFromBase = await _productService.GetAllAsync();

                List<string> productNames = products.Select(p => p.Name).ToList();
                List<string> dbProductNames = productsFromBase.Where(p => productNames.Contains(p.Name))
                                                              .Select(p => p.Name)
                                                              .ToList();

                List<ProductCreateRequestModel> productsExists = products.Where(p => dbProductNames.Contains(p.Name)).ToList();
                List<ProductCreateRequestModel> productsToAdd = products.Except(productsExists).ToList();

                foreach (var product in productsToAdd)
                {
                    await _productService.CreateAsync(product);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Products initializing error: '{error}'.", ex.Message);
            return;
        }

        _logger.LogTrace("Products initializing completed");
    }

    private static string GetInitJsonPath(string jsonName)
    {
        return Path.Combine(Environment.CurrentDirectory, "Initialize", $"{jsonName}.json");
    }
}
