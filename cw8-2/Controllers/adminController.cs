using cw8_2.Entities;
using cw8_2.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cw8_2.DTOs;

namespace cw8_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {

        public static string _productFilePath = Path.Combine(Directory.GetCurrentDirectory(), "productsFilePath.txt");

        public static string _personsPath = Path.Combine(Directory.GetCurrentDirectory(), "personsFilePath.txt");

        UserService _userService = new UserService(_personsPath, _productFilePath);

        AdminService _adminService = new AdminService(_productFilePath, _personsPath);

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(ProductDTO productDTO)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Price = productDTO.Price,
                Name = productDTO.Name,
                OffPrese = productDTO.OffPrese

            };
            _adminService.CreateProduct(product,2000);
            return Ok(product);
        }


        [HttpPost]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            _adminService.UpdateProduct(product);
            return Ok(product);
        }




    }
}
