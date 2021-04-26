using Ping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ping.Api.Models
{
    public class ProductRequest
    {
        public string Name { get; set; }

        public Product ToProductEntity(int? id = null)
              => new Product
              {
                  Id = id ?? 0,
                  Name = Name,
              };
    }
}
