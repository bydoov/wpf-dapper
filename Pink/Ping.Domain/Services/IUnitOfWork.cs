using System;
using System.Collections.Generic;
using System.Text;

namespace Ping.Domain.Services
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
    }
}
