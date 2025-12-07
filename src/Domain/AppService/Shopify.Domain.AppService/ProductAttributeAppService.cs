using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;
using Shopify.Domain.Core.ProductAttributeAgg.AppService;
using Shopify.Domain.Core.ProductAttributeAgg.Dto;
using Shopify.Domain.Core.ProductAttributeAgg.Service;

namespace Shopify.Domain.AppService;

public class ProductAttributeAppService(IProductAttributeService productAttributeService) : IProductAttributeAppService
{
    public async Task<Result<bool>> Add(string name, CancellationToken cancellationToken)
    {
        //Add validation
        var result = await productAttributeService.Add(name, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("عملیات افرودن با شکست مواجه شد");
        }
        return Result<bool>.Failure("عملیات افرودن انجام شد");
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await productAttributeService.Delete(id, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("عملیات حذف با شکست مواجه شد");
        }
        return Result<bool>.Failure("عملیات حذف انجام شد");
    }

    public async Task<ICollection<ProductAttributeDto>> GetAll(CancellationToken cancellationToken)
    {
        return await productAttributeService.GetAll(cancellationToken);
    }

    public async Task<Result<bool>> Update(int id, string name, CancellationToken cancellationToken)
    {
        var result = await productAttributeService.Update(id,name, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("عملیات اپدیت با شکست مواجه شد");
        }
        return Result<bool>.Failure("عملیات اپدیت انجام شد");
    }
}