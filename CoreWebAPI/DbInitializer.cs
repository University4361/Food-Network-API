using CoreWebAPI.EF;
using CoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Couriers.Any())
            {
                return;   // DB has been seeded
            }

            List<Courier> couriers = GetCouriersAndCustomers(5, 3, out List<Customer> customers, out List<Order> orders);

            List<Product> products = GetProducts(25);

            foreach (Courier courier in couriers)
            {
                context.Couriers.Add(courier);
            }

            foreach (Customer customer in customers)
            {
                context.Customers.Add(customer);
            }

            foreach (Product product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();

            List<ProductOrder> productOrders = GetProductOrders(products, orders, context, 100);

            foreach (ProductOrder productOrder in productOrders)
            {
                context.ProductOrders.Add(productOrder);
            }

            context.SaveChanges();
        }

        private static List<ProductOrder> GetProductOrders(List<Product> products, List<Order> orders, DataContext context, int objectsCount)
        {
            List<ProductOrder> productOrders = new List<ProductOrder>();

            Random random = new Random();

            for (int i = 0; i < objectsCount; i++)
            {
                Product curProd = products[random.Next(0, products.Count - 1)];
                Order curOrd = orders[random.Next(0, orders.Count - 1)];

                ProductOrder productOrder = productOrders.FirstOrDefault(po => po.OrderID == curOrd.ID && po.ProductID == curProd.ID);

                if (productOrder != null)
                {
                    productOrder.Quantity++;
                    curOrd.Price += curProd.Price;
                }
                else
                {
                    productOrder = productOrders.FirstOrDefault(po => po.OrderID == curOrd.ID);

                    if (productOrder == null)
                        curOrd.Price = curProd.Price;
                    else
                        curOrd.Price += curProd.Price;

                    productOrders.Add(new ProductOrder { OrderID = curOrd.ID, ProductID = curProd.ID });
                }

                context.Update(curOrd);
                context.SaveChanges();
            }

            return productOrders;
        }

        private static List<Product> GetProducts(int productsCount)
        {
            List<Product> products = new List<Product>();

            Random random = new Random();

            for (int i = 0; i < productsCount; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = $"Product description {i}",
                    Price = random.Next(150, 2000),
                    ImageUrl = "https://georges.net.au/wp-content/uploads/2016/06/qwewq.jpg",
                });
            }

            return products;
        }

        private static List<Courier> GetCouriersAndCustomers(int couriersCount, int ordersCount, out List<Customer> customers, out List<Order> orders)
        {
            List<Courier> couriers = new List<Courier>();
            customers = GetCustomers(couriersCount);
            orders = new List<Order>();

            Random random = new Random();

            for (int i = 0; i < couriersCount; i++)
            {
                List<Order> currentOrders = GetOrders(ordersCount, $"Courier {i}", customers[i]);

                couriers.Add(new Courier
                {
                    Name = $"Courier {i}",
                    BirthDate = new DateTime(random.Next(1990, 2000), random.Next(1, 12), random.Next(1, 30)),
                    Login = $"courier{i}@mail.ru",
                    Password = $"courier{i}",
                    PhotoUrl = "https://georges.net.au/wp-content/uploads/2016/06/qwewq.jpg",
                    Rate = random.Next(0, 5),
                    CourierToken = new CourierToken { Value = Guid.NewGuid().ToString(), DateOfExpire = DateTime.Now.AddDays(3) },
                    Orders = currentOrders,
                });

                orders.AddRange(currentOrders);
            }

            return couriers;
        }

        private static List<Order> GetOrders(int ordersCount, string courierName, Customer customer)
        {
            List<Order> orders = new List<Order> ();

            Random random = new Random();

            for (int i = 0; i < ordersCount; i++)
            {
                orders.Add(new Order
                {
                    Price = random.Next(0, 2000),
                    Comment = $"Comment for {i} order",
                    OrderStatus = Status.New,
                    DeliveryTime = DateTime.Now.AddHours(random.Next(2,15)),
                    Address = new Address { City = $"City {i} for {courierName}", Country = $"Country {i} for {courierName}", Street = $"Street {i} for {courierName}", HomeNumber = i },
                    Customer = customer
                });
            }

            return orders;
        }

        private static List<Customer> GetCustomers(int customersCount)
        {
            List<Customer> customers = new List<Customer>();

            Random random = new Random();

            for (int i = 0; i < customersCount; i++)
            {
                int k = random.Next(0, 9);

                customers.Add(new Customer
                {
                    Name = $"Customer {i}",
                    BirthDate = new DateTime(random.Next(1990, 2000), random.Next(1, 12), random.Next(1, 30)),
                    Login = $"customer{i}@mail.ru",
                    PhotoUrl = "https://georges.net.au/wp-content/uploads/2016/06/qwewq.jpg", 
                    PhoneNumber = $"+7953{k}5{k}4{k}5"
                });
            }

            return customers;
        }
    }
}
