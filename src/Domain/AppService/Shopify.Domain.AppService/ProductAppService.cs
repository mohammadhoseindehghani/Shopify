using Microsoft.Extensions.Logging;
using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Service;

namespace Shopify.Domain.AppService;

public class ProductAppService(IProductService productService, ILogger<ProductAppService> logger) : IProductAppService
{
    public async Task<Result<bool>> Add(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        //validation
        var result = await productService.Add(createProductDto, cancellationToken);
        if (!result)
        {
            logger.LogError($"خطا در افزودن محصول. ورودی داده شده: {createProductDto}");
            return Result<bool>.Failure("خطا در اینجاد محصول");
        }
        logger.LogInformation($"محصول جدید با عنوان {createProductDto.Title} با موفقیت ایجاد شد.");
        return Result<bool>.Success(result,"محصول با موفقیت اینجاد شد");

    }

    public async Task<Result<ProductDetailDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var product =  await productService.GetById(id, cancellationToken);
        if (product is null)
            return Result<ProductDetailDto>.Failure("محصول یافت نشد");
        return Result<ProductDetailDto>.Success(product);
    }

    public async Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken)
    {
        return await productService.GetAll(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken)
    {
        return await productService.GetActiveProducts(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken)
    {
        return await productService.GetSpecialProducts(cancellationToken);
    }

    public async Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken)
    {
        return await productService.GetProductsForAdmin(cancellationToken);
    }

    public async Task<Result<bool>> ChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken)
    {
        var result =await productService.ChangeCategory(productId, newCategoryId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("خطا در عملیات");
        }
        logger.LogWarning($"دسته‌بندی محصول {productId} با موفقیت به دسته‌بندی {newCategoryId} تغییر یافت.");
        return Result<bool>.Success(result);
    }

    public async Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken)
    {
        return await productService.GetProductsByCategory(categoryId, cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken)
    {
        return await productService.SearchProducts(keyword, cancellationToken);
    }

    public async Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken)
    {
        return await productService.GetProductsWithAttributes(cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await productService.Delete(id, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("عملیات حذف با شکست مواجه شد");
        }

        return Result<bool>.Success(result, "حذف با موفقیت انجام شد");
    }

    public async Task<int> ProductCount(CancellationToken cancellationToken)
    {
        return await productService.ProductCount(cancellationToken);
    }

    public async Task<int> GetProductsInStock(CancellationToken cancellationToken)
    {
        return await productService.GetProductsInStock(cancellationToken);
    }

    public async Task<int> GetProductsRunningLow(CancellationToken cancellationToken)
    {
        return await productService.GetProductsRunningLow(cancellationToken);
    }

    public async Task<int> GetProductsOutOfStock(CancellationToken cancellationToken)
    {
        return await productService.GetProductsOutOfStock(cancellationToken);
    }

    public async Task<Result<bool>> EditProduct(int id, EditProductDto editDto, CancellationToken cancellationToken)
    {
        var oldStock = await productService.GetCurrentStockQuantity(id, cancellationToken);
        var newStock = editDto.StockQuantity;

        var result = await productService.EditProduct(id, editDto, cancellationToken);

        if (!result)
        {
            logger.LogError($"ویرایش محصول {id} با شکست مواجه شد. داده‌های ورودی: {editDto}");
            return Result<bool>.Failure("ادیت انجام نشد");
        }

        if (newStock < 5 && oldStock >= 5)
        {
            logger.LogWarning($" موجودی محصول {id} به سطح هشدار رسید. مقدار جدید: {newStock} (قبلی: {oldStock})");
        }
        else if (newStock > oldStock + 100)
        {
            logger.LogWarning($" افزایش قابل توجه موجودی محصول {id} از {oldStock} به {newStock} ثبت شد.");
        }

        logger.LogInformation($"محصول {id} با موفقیت ویرایش شد عنوان جدید: {editDto.Title}");
        return Result<bool>.Success(result, "ادیت انجام شد");
    }

    public async Task<Result<bool>> EditImg(int id, string imgUrl, CancellationToken cancellationToken)
    {
        if (imgUrl.Length<5 || string.IsNullOrEmpty(imgUrl))
        {
            return Result<bool>.Failure("فرمنت ورودی عکس درست نیست");
        }

        var result = await productService.EditImg(id, imgUrl, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("ویرایش عکس با شکست مواجه شد");
        }
        return Result<bool>.Success(result,"ویرایش عکس انجام شد");
    }
}