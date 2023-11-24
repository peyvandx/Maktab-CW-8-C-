using cw8_2.Context;
using cw8_2.Context.Interfaces;
using cw8_2.Entities;
using cw8_2.Exceptions;
using System.Threading;
using System.Timers;

namespace cw8_2.services
{
    public class AdminService
    {
        string _productsFilePath;
        string _personsFilePath;
        IGenericRepository<Product> _productRepository;
        IGenericRepository<Person> _personRepository;
        List<Product> _products;
        List<Person> _persons;


        public AdminService(string productFilePath, string personsFilePath)
        {
            _productsFilePath = productFilePath;
            _personsFilePath = personsFilePath;

            _productRepository = new GenericRepository<Product>(_productsFilePath);
            _personRepository = new GenericRepository<Person>(_personsFilePath);

            _products = _productRepository.GetAll();
            _persons = _personRepository.GetAll();
        }

        //System.Timers.Timer _timer;
        public void CreateProduct(Product product, int time)
        {

            if (_products.Any(x => x.Name == product.Name))
            {
                var savedProduct = _products.FirstOrDefault(x => x.Name == product.Name);

                if (savedProduct.IsDeleted)
                {
                    savedProduct.IsDeleted = false;
                    _productRepository.SaveChanges();
                }
            }
            else
            {
                _products.Add(product);
               DoublePrice(product, time);
            }

        }

        public void DoublePrice(Product product, int time)
        {
            Thread DoublePriceThread = new Thread(() =>
            {
                DoublePriceTimerrCallBack(product,time);
            });
            DoublePriceThread.Start();

        }
        
        public void DoublePriceTimerrCallBack(Product product ,int time) 
        {
            Thread.Sleep(time);
            var newProduct = _productRepository.GetByID(product.Id);
            newProduct.Price = product.Price * 2;

            _productRepository.SaveChanges();
        }

        public List<Person> GetPeople() => _personRepository.GetAll();


        public List<Person> GetpeopleWhoBuy()
        {
            return _persons.Where(x => x.orders.Any(x => x.IsPaid)).ToList();
        }


        public Product UpdateProduct(Product product)
        {
            if (_products.Any(x => x.Name == product.Name))
            {
                var savedProduct = _products.FirstOrDefault(x => x.Name == product.Name);

                if (savedProduct.IsDeleted)
                {
                    savedProduct.IsDeleted = false;
                    _productRepository.SaveChanges();
                    return savedProduct;
                }
                throw new AlreadyTaken("product name ");
            }
            else
            {
                //var newProduct = _products.Where(p => p.Id == product.Id).Select(x=> x=product).FirstOrDefault();
                var newProduct = _products.FirstOrDefault(x => x.Id == product.Id);

                newProduct.Id = product.Id;
                newProduct.Name = product.Name;
                newProduct.IsDeleted = product.IsDeleted;
                newProduct.Price = product.Price;
                newProduct.OffPrese = product.OffPrese;

                _productRepository.SaveChanges();
                //return _productRepository.Update(product);
                return newProduct;
            }

        }

        public bool DeleteProduct(string id) =>_productRepository.Delete(id);



    }
}
