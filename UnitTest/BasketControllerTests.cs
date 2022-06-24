using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using Xunit;

namespace UnitTest
{
    public class BasketControllerTests
    {

        [Fact]
        public void BasketResult()
        {
            // Arrange
            BasketController controller = new BasketController();

            // Act
            Task<IActionResult> result = controller.Basket() as Task<IActionResult>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }


    }
}
