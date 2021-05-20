using Dapper;
using Microsoft.Extensions.Configuration;
using Ping.Domain.Entities;
using Ping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;
        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> Add(Product entity)
        {
            var listOfProducts = await GetAll();
            if (listOfProducts.AsList().Any(p => p.Name == entity.Name))
            {
                throw new Exception("Name should be unique");
            }

            var sql = "Insert into Product (Name) VALUES (@Name)";
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "Delete from Product where Id = @Id";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new Product { Id = id });

                return result;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var sql = "select * from product";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql);

                return result;
            }
        }

        public  async Task<Product> GetById(int id)
        { 
            var sql = "Select * FROM Product where Id = @Id";
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await  connection.QueryFirstOrDefaultAsync<Product>(sql, new Product { Id = id });

                return result;
            }
        }

        public async Task<int> Update(int id, Product entity)
        {
            var listOfProducts = await GetAll();
            if (listOfProducts.AsList().Any(p => p.Name == entity.Name))
            {
                throw new Exception("Name should be unique");
            }

            var sql = "UPDATE Product SET Name = @Name WHERE Id = @Id";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
