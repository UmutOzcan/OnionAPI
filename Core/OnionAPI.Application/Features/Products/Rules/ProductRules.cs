using OnionAPI.Application.Bases;
using OnionAPI.Application.Features.Products.Exceptions;
using OnionAPI.Domain.Entities;

namespace OnionAPI.Application.Features.Products.Rules
{
    public class ProductRules : BaseRules
    {
        // Productlarımın içinde request title ile aynı title varsa hata ver.
        public Task ProductTitleMustNotBeSame(IList<Product> products, string requestTitle)
        {
            if (products.Any(x => x.Title == requestTitle)) throw new ProductTitleMustNotBeSameException();
            return Task.CompletedTask;
        }
    }
}
