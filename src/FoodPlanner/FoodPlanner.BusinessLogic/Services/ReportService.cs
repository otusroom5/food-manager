﻿using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportService : IReportService
{
    private readonly IReportFileBuilder _reportFileBuilder;
    private readonly IPdfService _pdfService;
    private readonly ILogger<ReportService> _logger;

    public ReportService(IReportFileBuilder reportFileBuilder,
         IPdfService pdfService,
         ILogger<ReportService> logger)
    {
        _reportFileBuilder = reportFileBuilder;
        _pdfService = pdfService;       
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public Report Create(string reportName, string reportDescription, Guid userId)
    {
        try
        {
            return Report.CreateNew(ReportId.CreateNew(), ReportName.FromString(reportName), reportDescription, UserId.FromGuid(userId));
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public async Task<byte[]> GenerateReportFileAsync(int daysBeforeExpired, bool includeActualPrices)
    {
        try
        {
            var htmlContent = string.Empty;
            if (includeActualPrices)
            {
                htmlContent = _reportFileBuilder
                    .BuildHeader()
                    .BuildBody(daysBeforeExpired)
                    .AddActualFoodPrices(daysBeforeExpired)
                    .BuildFooter()
                    .Build();
            }
            else
            {
                htmlContent = _reportFileBuilder
                  .BuildHeader()
                  .BuildBody(daysBeforeExpired)
                  .BuildFooter()
                  .Build();
            }                       

            return await _pdfService.CreatePDFAsync(htmlContent);
        }
        catch (Exception exception)
        {
            LogError("Generate", exception);
            throw;
        }
    }

    public async Task<byte[]> GenerateReportFileDistributionAsync(ExpireProduct products)
    {
        try
        {
            string htmlContent = _reportFileBuilder
                 .BuildHeader()
                 .BuildBodyDistrubution(products)
                 .BuildFooter()
                 .Build();

            return await _pdfService.CreatePDFAsync(htmlContent);
        }
        catch (Exception exception)
        {
            LogError("Generate", exception);
            throw;
        }
    }

    public async Task<byte[]> GenerateReportFileDistributionAsync(ProductAlmostOver product)
    {
        try
        {
            string htmlContent = _reportFileBuilder                 
                 .BuildBodyDistrubution(product)
                 .BuildFooter()
                 .Build();

            return await _pdfService.CreatePDFAsync(htmlContent);
        }
        catch (Exception exception)
        {
            LogError("Generate", exception);
            throw;
        }
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
