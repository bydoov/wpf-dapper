using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Ping.Domain.Entities;
using Ping.Domain.Infrastructure;
using Ping.Domain.Services;
using Ping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ping.Test
{
    public class Test
    {
        private IProductRepository productRepository = A.Fake<IProductRepository>();
        private IUnitOfWork unitOfWork = A.Fake<IUnitOfWork>();
        private ProductRepository repo = new ProductRepository(_con);
        private static IConfiguration _con = A.Fake<IConfiguration>();

        [Fact]
        public void Check_If_ListOfProducts_IsNotNull_ReturnTrue()
        {
            var fakeProducts = new List<Product>
           {
              new Product{Id = 2, Name="Apple"}
           };

            A.CallTo(() => unitOfWork.Products.GetAll()).Returns(fakeProducts);

            var products = repo.GetAll();

            Assert.NotNull(products);
        }

        [Fact]
        public void Check_By_Wrong_Id_ReturnFalse()
        {
            var prod = new Product { Id = 3, Name = "Carrot" };

            A.CallTo(() => productRepository.GetById(3)).Returns(prod);

            var products = repo.GetById(2);

            Assert.False(prod.Equals(products));
        }


        //[Fact]
        //public  void Check_If_ListOfProducts_IsNotNull_ReturnFalsy()
        //{
        //    var fakeProducts = new List<Product>
        //    {

        //    };

        //    A.CallTo(() => unitOfWork.Products.GetAll()).Returns(fakeProducts);

        //    var products =  repo.GetAll().Result;

        //    Assert.Null(products.Equals);
        //}



        //[Fact]
        //public void Check_If_ListOfProducts_IsNotNull_Return()
        //{

        //    var product = new Product { Id = 1, Name = "Apple" };

        //    A.CallTo(() => unitOfWork.Products.Add(product)).Returns(1);

        //    var a = productRepository.Add(product);


        //}
    }
}
