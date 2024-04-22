﻿using MediatR;
using OnionAPI.Application.Interfaces.UnitOfWork;
using OnionAPI.Domain.Entities;
namespace OnionAPI.Application.Features.Products.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);
        
        await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);
        if(await _unitOfWork.SaveAsync() > 0) // save işlemi gerçekleştiyse 0dan büyük olur product oluşmadan productcategory oluşturamayız
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
