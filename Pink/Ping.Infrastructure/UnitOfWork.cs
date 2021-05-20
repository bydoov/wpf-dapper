using Ping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ping.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }

        public IProductRepository Products { get; }
    }
}
