﻿using OnionAPI.Domain.Common;

namespace OnionAPI.Domain.Entities;

public class Brand : EntityBase
{
    public Brand()
    {
        
    }

    public Brand(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}
