using cw8_2.Context;
using cw8_2.Context.Interfaces;
using cw8_2.Entities;
using cw8_2.Exceptions;

namespace cw8_2.services
{
    public class UserService
    {
        string _productsFilePath;
        string _personsFilePath;
        IGenericRepository<Product> _productRepository;
        IGenericRepository<Person> _personRepository;
        List<Product> _products;
        List<Person> _persons;

        public UserService(string personsFilePath, string productFilePath)
        {
            _productsFilePath = productFilePath;
            _personsFilePath = personsFilePath;

            _productRepository = new GenericRepository<Product>(_productsFilePath);
            _personRepository = new GenericRepository<Person>(_personsFilePath);

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
