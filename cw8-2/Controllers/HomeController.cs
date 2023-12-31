﻿using cw8_2.DTOs;
using cw8_2.Entities;
using cw8_2.services;
using Microsoft.AspNetCore.Mvc;

namespace cw8_2.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        EmptyCurrentUser _currentUser;
        public static string _productFilePath = Path.Combine(Directory.GetCurrentDirectory(), "productsFilePath.txt");

        public static string _personsPath = Path.Combine(Directory.GetCurrentDirectory(), "personsFilePath.txt");

        public static string _currentUserPath = Path.Combine(Directory.GetCurrentDirectory(), "currentUserPath.txt");

        UserService _userService = new UserService(_personsPath, _productFilePath, _currentUserPath);

        public HomeController(EmptyCurrentUser emptyCurrent)
        {
            _currentUser=emptyCurrent;
            _currentUser.Empty();
        }

  
        [HttpPost]
        [Route("Register")]
        public IActionResult register(RegisterDTO register)
        {
            Person person = new Person()
            {
                FullName=register.FullName,
                Age=register.Age,
                Password=register.Password,
                Id=Guid.NewGuid().ToString(),
                IsDeleted=false,
                Role=new Role() {RoleId=1,Title="client"}   
            };
            _userService.Register(person);
            _userService.UpdateCurrentUser(person);
            return Ok(person);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO login)
        {
            Person person = _userService.Login(login.FullName,login.Password);
            _userService.UpdateCurrentUser(person);
            return Ok(person);
        }

        


    }
}
