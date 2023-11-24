using cw8_2.Context;
using cw8_2.Context.Interfaces;
using cw8_2.Entities;
using cw8_2.Exceptions;

namespace cw8_2.services
{
    public class EmptyCurrentUser
    {
        Person person = new Person()
        {
            FullName = "",
            Id = "0",
            Password = "",
            IsDeleted = false,
            orders = null,
            Role = new Role()
            {
                RoleId = 1,
                Title = "User",
            }
        };

        public static string _productFilePath = Path.Combine(Directory.GetCurrentDirectory(), "productsFilePath.txt");
        //public static string _logPath = Path.Combine(Directory.GetCurrentDirectory(), "Log.csv");
        public static string _personsPath = Path.Combine(Directory.GetCurrentDirectory(), "personsFilePath.txt");
        public static string _currentUsersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "currentUserFilePath.txt");

        UserService _userService = new UserService(_productFilePath, _personsPath, _currentUsersFilePath);

        public void Empty()
        {
            _userService.UpdateCurrentUser(person);
        }

    }


    public class UserService
    {
        string _productsFilePath;
        string _personsFilePath;
        string _currentUserPath;

        IGenericRepository<Product> _productRepository;
        IGenericRepository<Person> _personRepository;
        IGenericRepository<Person> _currentUserRepository;

        List<Product> _products;
        List<Person> _persons;
        List<Person> _currentUsers;

        public UserService(string personsFilePath, string productFilePath, string currentUserPath)
        {
            _productsFilePath = productFilePath;
            _personsFilePath = personsFilePath;
            _currentUserPath = currentUserPath;
            _productRepository = new GenericRepository<Product>(_productsFilePath);
            _personRepository = new GenericRepository<Person>(_personsFilePath);
            _currentUserRepository = new GenericRepository<Person>(_currentUserPath);

            _products = _productRepository.GetAll();
            _persons = _personRepository.GetAll();

            
        }


        public Person Login(string fullName, string password)
        {
            return _persons.FirstOrDefault(x => x.FullName == fullName && x.Password == password);
        }


        public void Register(Person person)
        {


            if (_persons.Any(x => x.FullName == person.FullName))
            {
                throw new AlreadyTaken("this user name ");
            }

            else
            {
                _persons.Add(person);
                _personRepository.SaveChanges();
            }

            
        }


        public void AddOrder(Order order , string personID) 
        {
            var newPerson = _persons.FirstOrDefault(x => x.Id == personID);

            newPerson.orders.Add(order);

            _personRepository.SaveChanges();
        }


        public bool IsAvailable(string personID)
        {
            return _persons.Any(x => x.Id == personID);
        }

        public List<Product> SearchProduct(string productName)
        {
            return _products.Where(x=>x.Name.Contains(productName)).ToList();
        }

        public bool UpdateCurrentUser(Person person)
        {
            try
            {
                _currentUsers = _currentUserRepository.GetAll();
                _currentUsers.Remove(_currentUsers.FirstOrDefault());
                _currentUsers.Add(person);
                _currentUserRepository.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        //public void Register(User person)
        //{
        //    if (!DataBase.users.Any(x => x.name == person.name))
        //    {
        //        DataBase.users.Add(person);
        //        Console.WriteLine("\nregister successful...");
        //    }

        //    else
        //    {
        //        Console.WriteLine("\nyou already have account");
        //    }
        //}

    }
}
