﻿using OnionAPI.Domain.Common;

namespace OnionAPI.Domain.Entities;

public class Category : EntityBase
{
    public Category()
    {
        
    }

    public Category(int parentId, string name, int priorty)
    {
        ParentId = parentId;
        Name = name;
        Priorty = priorty;
    }

    public int ParentId { get; set; }
    public string Name { get; set; }
    public int Priorty { get; set; }
    public ICollection<Detail> Details { get; set; } // OneToMany
    public ICollection<ProductCategory> ProductCategories { get; set; } // ManyToMany - ara tablo ile

}
