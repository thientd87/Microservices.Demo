﻿using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogDbContext
    {
        IMongoCollection<Product> Products { get; set; }
    }
}