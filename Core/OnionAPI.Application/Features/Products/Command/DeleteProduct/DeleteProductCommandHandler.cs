﻿using MediatR;
using OnionAPI.Application.Interfaces.UnitOfWork;
using OnionAPI.Domain.Entities;

namespace OnionAPI.Application.Features.Products.Command.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x=> x.Id == request.Id && !x.IsDeleted);
        product.IsDeleted = true; // soft delete

        await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
        await _unitOfWork.SaveAsync();
    }
}
