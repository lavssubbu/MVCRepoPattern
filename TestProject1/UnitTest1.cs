using Moq;
using MVCRepoPatternDemo.Repository;
using MVCRepoPatternDemo.Models;
using MVCRepoPatternDemo.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange - creating the instance
            var mockrepo = new Mock<IProduct>();
            mockrepo.Setup(repo => repo.GetAll())
                .Returns(TestGetAllProducts());

            var mockrepo2 = new Mock<ICategory>();

            var controller = new ProductsController(mockrepo.Object,mockrepo2.Object);
            //Act
            var result = controller.Index();
            //Assert
              var viewres =  Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<ProductCl>>(viewres.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private IEnumerable<ProductCl> TestGetAllProducts()
        {
            return new List<ProductCl>()
            {
                new ProductCl(){ProId=11,ProName="Laptop",Price=50000},
                 new ProductCl(){ProId=12,ProName="Mac",Price=90000}

            };
        }
    }
}