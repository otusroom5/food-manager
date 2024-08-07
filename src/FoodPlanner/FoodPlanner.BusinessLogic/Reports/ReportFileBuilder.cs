﻿using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.DataAccess.Interfaces;
using System.Text;

namespace FoodPlanner.BusinessLogic.Reports;

public class ReportFileBuilder : IReportFileBuilder
{
    private readonly ReportFile _reportFile;
    private readonly IStorageRepository _storageRepository;
    private readonly ISupplierRepository _supplierRepository;

    public ReportFileBuilder(IUnitOfWork unitOfWork)
    {
        _storageRepository = unitOfWork.GetStorageRepository();
        _supplierRepository = unitOfWork.GetSupplierRepository();

        _reportFile = new ReportFile();
    }

    public IReportFileBuilder BuildHeader()
    {
        var htmlContent = new StringBuilder();
        htmlContent.AppendLine("<head><meta charset=utf-8></head>");
        htmlContent.AppendLine("<body>");
        htmlContent.AppendLine("<div style = 'border: 0px; background-color: #FFFFFF; font-family: Arial, sans-serif;' >");
        htmlContent.AppendLine("<h1> Товары с заканчивающимся сроком использования </h1>");
        htmlContent.AppendLine("<table style = 'width: 100%; border-collapse: collapse;'>");
        htmlContent.AppendLine("<thead>");
        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray' > Наименование товара </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray' > Годен до </th>");
        htmlContent.AppendLine("</tr><hr/>");
        htmlContent.AppendLine("</thead>");
        htmlContent.AppendLine("<tbody>");

        _reportFile.Header = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder BuildBody(int daysBeforeExpired)
    {
        var htmlContent = new StringBuilder();
        foreach (var item in _storageRepository.GetExpiredProductsAsync(daysBeforeExpired).Result)
        {
            htmlContent.AppendLine("<tr>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.Product.Name + " </td>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.ExpiryDate + " </td>");
            htmlContent.AppendLine("</tr>");
        }
        htmlContent.AppendLine("</tbody>");
        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("</div>");
        htmlContent.AppendLine("</body>");

        _reportFile.Body = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder BuildBodyDistrubution(ExpireProduct products)
    {
        var htmlContent = new StringBuilder();
        foreach (var item in products.ProductItems)
        {
            htmlContent.AppendLine("<tr>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.ProductName + " </td>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.ExpiryDate + " </td>");
            htmlContent.AppendLine("</tr>");
        }
        htmlContent.AppendLine("</tbody>");
        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("</div>");
        htmlContent.AppendLine("<i>Отчет сформирован сформирован автоматически в: " + products.OccuredOn + "</i>");
        htmlContent.AppendLine("</body>");        

        _reportFile.Body = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder BuildBodyDistrubution(ProductAlmostOver product)
    {
        var htmlContent = new StringBuilder();
        htmlContent.AppendLine("<head><meta charset=utf-8></head>");
        htmlContent.AppendLine("<body>");
        htmlContent.AppendLine("<div style = 'border: 0px; background-color: #FFFFFF; font-family: Arial, sans-serif;' >");
        htmlContent.AppendLine("<h1> Продукт который скоро закончится </h1>");
        htmlContent.AppendLine("<table style = 'width: 100%; border-collapse: collapse;'>");
        htmlContent.AppendLine("<thead>");
        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray'> Наименование продукта </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray'> Минимальный остаток на день </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray'> Количество оставшегося продукта </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGray'> Единица измерения </th>");
        htmlContent.AppendLine("</tr><hr/>");
        htmlContent.AppendLine("</thead>");
        htmlContent.AppendLine("<tbody>");

        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>" + product.ProductName + " </td>");
        htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>" + product.MinAmountPerDay + " </td>");
        htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>" + product.Amount + " </td>");
        htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>" + product.Unit + " </td>");
        htmlContent.AppendLine("</tr>");
       
        htmlContent.AppendLine("</tbody>");
        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("<i>Отчет сформирован сформирован автоматически в: " + product.OccuredOn + "</i>");
        htmlContent.AppendLine("</div>");
        htmlContent.AppendLine("</body>");

        _reportFile.Body = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder AddActualFoodPrices(int daysBeforeExpired)
    {
        var htmlContent = new StringBuilder();

        htmlContent.AppendLine("</br>");
        htmlContent.AppendLine("<table style = 'width: 100%; border-collapse: collapse;'>");
        htmlContent.AppendLine("<thead>");
        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGreen'> Магазин </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGreen'> Наименование товара </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd; background-color:LightGreen'> Актуальная цена </th>");
        htmlContent.AppendLine("</tr><hr/>");
        htmlContent.AppendLine("</thead>");
        htmlContent.AppendLine("<tbody>");

        foreach (var productItem in _storageRepository.GetExpiredProductsAsync(daysBeforeExpired).Result.Select(productItem => productItem.Product))
        {
            var priceEntity = _supplierRepository.GetActualProductPriceAsync(productItem.Id).Result;
            if (priceEntity != null)
            {
                htmlContent.AppendLine("<tr>");
                htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + _supplierRepository.GetShopDetailsAsync(priceEntity.ShopId).Result?.Name + " </td>");
                htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + productItem.Name + " </td>");
                htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + decimal.Round(priceEntity.Price, 2, MidpointRounding.AwayFromZero) + " </td>");
                htmlContent.AppendLine("</tr>");
            }
        }
        htmlContent.AppendLine("</tbody>");
        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("</div>");
        htmlContent.AppendLine("</body>");

        _reportFile.Body += htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder BuildFooter()
    {
        _reportFile.Footer += $"&copy; {DateTime.Now.Date.Year} - FoodManager";
        return this;
    }

    public string Build()
    {
        return _reportFile.ToString();
    }
}