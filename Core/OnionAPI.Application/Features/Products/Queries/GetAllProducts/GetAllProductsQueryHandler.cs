﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionAPI.Application.DTOs;
using OnionAPI.Application.Interfaces.AutoMapper;
using OnionAPI.Application.Interfaces.UnitOfWork;
using OnionAPI.Domain.Entities;

namespace OnionAPI.Application.Features.Products.Queries.GetAllProducts;

// Mediatr kullanarak handle etme
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {   //eager loading icin include ile ekledim
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include:x=>x.Include(b=>b.Brand));

        var brand = _mapper.Map<BrandDto, Brand>(new Brand());
        var map = _mapper.Map<GetAllProductsQueryResponse, Product>(products);
        foreach (var item in map)
            item.Price -= (item.Price * item.Discount / 100);

        //throw new Exception("napıyon birader");
        return map;
    }
}
