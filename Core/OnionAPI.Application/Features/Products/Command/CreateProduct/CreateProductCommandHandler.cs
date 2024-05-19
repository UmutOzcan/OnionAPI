using MediatR;
using OnionAPI.Application.Features.Products.Rules;
using OnionAPI.Application.Interfaces.UnitOfWork;
using OnionAPI.Domain.Entities;
namespace OnionAPI.Application.Features.Products.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductRules _productRules;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules)
    {
        _unitOfWork = unitOfWork;
        _productRules = productRules;

    }
    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

        //if (products.Any(x => x.Title == request.Title))
        //    throw new Exception("Aynı başlıkta ürün olamaz"); böyle yapmak yerine productRulesdan çekeriz

        await _productRules.ProductTitleMustNotBeSame(products,request.Title);


        Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);

        await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);
        if (await _unitOfWork.SaveAsync() > 0) // save işlemi gerçekleştiyse 0dan büyük olur product oluşmadan productcategory oluşturamayız
        {
            foreach (var categoryId in request.CategoryIds)
                await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                {
                    ProductId = product.Id,
                    CategoryId = categoryId,
                });

            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}
