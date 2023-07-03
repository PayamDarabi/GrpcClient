using Grpc.Net.Client;
using OrderManagementGrpcClient;
using System.Globalization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Customer.CustomerClient customerClient;
        Product.ProductClient productClient;
        Order.OrderClient orderClient;

        int enteredNumber = 0;
        bool isValid = false;
        ChangeConsoleStyle();

        CreateGrpcClients();
        await MainMenu();

        async Task MainMenu()
        {
            enteredNumber = 0;

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Customer Menu");
            Console.WriteLine("2) Product Menu");
            Console.WriteLine("3) Order Menu");
            Console.WriteLine("4) Exit");

            Console.Write("\r\nSelect an option: ");

            isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
            if (isValid)
            {
                switch (enteredNumber)
                {
                    case 1:
                        await PrintSubMenu(1);
                        break;

                    case 2:
                        await PrintSubMenu(2);
                        break;

                    case 3:
                        await PrintSubMenu(3);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("you have entered an invalid number... please enter a correct one");
                await MainMenu();
            }
        }
        async Task PrintSubMenu(int menu)
        {
            switch (menu)
            {
                case 1:
                    Console.WriteLine("\r\nChoose an option from Customer Menu:");

                    Console.WriteLine("[1] Add Customer");
                    Console.WriteLine("[2] Update Customer");
                    Console.WriteLine("[3] Delete Customer");
                    Console.WriteLine("[4] Get Customer");
                    Console.WriteLine("[5] GetAll Customers");
                    Console.WriteLine("[6] Back To MainMenu");

                    Console.Write("\r\nSelect an option: ");

                    isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
                    switch (enteredNumber)
                    {
                        case 1:
                            await AddCustomer();
                            break;
                        case 2:
                            await UpdateCustomer();
                            break;

                        case 3:
                            await DeleteCustomer();
                            break;

                        case 4:
                            await GetCustomer();
                            break;

                        case 5:
                            await GetCustomers();
                            break;

                        case 6:
                            await MainMenu();
                            break;

                        default:
                            break;
                    }

                    break;
                case 2:
                    Console.WriteLine("\r\nChoose an option from Product Menu:");

                    Console.WriteLine("Welcome to Product area.. please enter a number: ");
                    Console.WriteLine("[1] Add Product");
                    Console.WriteLine("[2] Update Product");
                    Console.WriteLine("[3] Delete Product");
                    Console.WriteLine("[4] Get Product");
                    Console.WriteLine("[5] GetAll Products");
                    Console.WriteLine("[6] Back To MainMenu");

                    Console.Write("\r\nSelect an option: ");

                    isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
                    switch (enteredNumber)
                    {
                        case 1:
                            await AddProduct();
                            break;
                        case 2:
                            await UpdateProduct();
                            break;

                        case 3:
                            await DeleteProduct();
                            break;

                        case 4:
                            await GetProduct();
                            break;

                        case 5:
                            await GetProducts();
                            break;

                        case 6:
                            await MainMenu();
                            break;

                        default:
                            break;
                    }

                    break;
                case 3:
                    Console.WriteLine("\r\nChoose an option from Order Menu:");

                    Console.WriteLine("Welcome to Order area.. please enter a number: ");
                    Console.WriteLine("[1] Add Order");
                    Console.WriteLine("[2] Delete Order");
                    Console.WriteLine("[3] Get Order");
                    Console.WriteLine("[4] GetAll Order");
                    Console.WriteLine("[5] Back To MainMenu");

                    Console.Write("\r\nSelect an option: ");

                    isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
                    switch (enteredNumber)
                    {
                        case 1:
                            await AddOrder();
                            break;

                        case 2:
                            await DeleteOrder();
                            break;

                        case 3:
                            await GetOrder();
                            break;

                        case 4:
                            await GetOrders();
                            break;

                        case 5:
                            await MainMenu();
                            break;

                        default:
                            break;
                    }
                    break;
            }
        }

        void CreateGrpcClients()
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7199");
            customerClient = new Customer.CustomerClient(channel);
            productClient = new Product.ProductClient(channel);
            orderClient = new Order.OrderClient(channel);
        }

        async Task AddCustomer()
        {
            Console.WriteLine("\r\nEnter Customer information: ");
            Console.Write("Customer Id: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Customer Name: ");
            string customerName = Console.ReadLine();

            Console.Write("Customer Age: ");
            int customerAge = int.Parse(Console.ReadLine());

            var result = await customerClient.AddAsync(new CustomerRequest
            {
                Id = customerId,
                Age = customerAge,
                Name = customerName
            });

            if (result.IsSuccess)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            await PrintSubMenu(1);
        }
        async Task UpdateCustomer()
        {
            Console.Write("\r\nEnter Id of Customer you want to update:");
            int customerId = int.Parse(Console.ReadLine());
            var customerReply = await customerClient.GetAsync(new GetCustomerRequest { Id = customerId });
            if (customerReply.IsSuccess)
            {
                Console.WriteLine($"\r\nEnter infromation for Customer with Id: {customerReply.Customer.Id}");
                Console.Write("CustomerName: ");
                string customerName = Console.ReadLine();

                Console.Write("CustomerAge: ");
                int customerAge = int.Parse(Console.ReadLine());
                var result = await customerClient.UpdateAsync(new CustomerRequest
                {
                    Id = customerReply.Customer.Id,
                    Age = customerAge,
                    Name = customerName
                });

                if (result.IsSuccess)
                {
                    Console.WriteLine(result.Message);
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
            else
            {
                Console.WriteLine(customerReply.Message);
            }
            await PrintSubMenu(1);
        }
        async Task DeleteCustomer()
        {
            Console.Write("\r\nEnter Id of Customer you want to Delete:");
            int customerId = int.Parse(Console.ReadLine());
            var result = await customerClient.DeleteAsync(new DeleteCustomerRequest { Id = customerId });
            if (result.IsSuccess)
            {
                Console.WriteLine($"Customer with Id: {customerId} is deleted successfully...");
            }
            else
            {
                Console.WriteLine("Delete Customer was not successful. Error:", result.Message);
            }
            await PrintSubMenu(1);
        }
        async Task GetCustomer()
        {
            Console.Write("\r\nEnter Id of Customer you want:");

            int customerId = int.Parse(Console.ReadLine());
            var result = await customerClient.GetAsync(new GetCustomerRequest { Id = customerId });

            if (result.IsSuccess)
            {
                Console.WriteLine($"CustomerName is {result.Customer.Name} and CustomerAge is {result.Customer.Age}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            await PrintSubMenu(1);

        }
        async Task GetCustomers()
        {
            var result = await customerClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty { });
            if (result.Customers != null && result.Customers.Count > 0)
            {
                var customers = result.Customers;
                Console.WriteLine($"Customers Count: {customers.Count}");

                foreach (var customer in customers)
                {
                    Console.WriteLine($"CustomerName is {customer.Name} and CustomerAge is {customer.Age}");
                }
            }
            await PrintSubMenu(1);
        }

        async Task AddProduct()
        {
            Console.WriteLine("\r\nEnter Product information: ");

            Console.Write("Product Id: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Product Price: ");
            double productPrice = int.Parse(Console.ReadLine());

            var result = await productClient.AddAsync(new ProductRequest
            {
                Id = productId,
                Price = productPrice,
                Name = productName
            });

            if (result.IsSuccess)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            await PrintSubMenu(2);
        }
        async Task UpdateProduct()
        {
            Console.WriteLine("\r\nEnter Id of Product you want to update:");
            int productId = int.Parse(Console.ReadLine());
            var productReply = await productClient.GetAsync(new GetProductRequest { Id = productId });
            if (productReply.IsSuccess)
            {
                Console.WriteLine($"Enter infromation for Product with id: {productId}");
                Console.Write("Product Name: ");
                string productName = Console.ReadLine();

                Console.Write("Product Price: ");
                double productPrice = double.Parse(Console.ReadLine());

                var result = await productClient.UpdateAsync(new ProductRequest { Id = productReply.Product.Id, Price = productPrice, Name = productName });
                if (result.IsSuccess)
                {
                    Console.WriteLine(result.Message);
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
            else
            {
                Console.WriteLine(productReply.Message);
            }
            await PrintSubMenu(2);
        }
        async Task DeleteProduct()
        {
            Console.WriteLine("\r\nEnter Id of Product you want to delete:");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter your new infromation for Product with Id: {productId}");
            var result = await productClient.DeleteAsync(new DeleteProductRequest { Id = productId });
            if (result.IsSuccess)
            {
                Console.WriteLine("Product with id {0} is deleted successfully...", productId);
            }
            else
            {
                Console.WriteLine("delete operation is not successful. Error:", result.Message);
            }
            await PrintSubMenu(2);
        }
        async Task GetProduct()
        {
            Console.WriteLine("\r\nEnter Id of Product to get:");

            int productId = int.Parse(Console.ReadLine());
            var result = await productClient.GetAsync(new GetProductRequest { Id = productId });

            if (result.IsSuccess)
            {
                Console.WriteLine($"Product Name is {result.Product.Name} and Price is {result.Product.Price}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            await PrintSubMenu(2);
        }
        async Task GetProducts()
        {
            Console.WriteLine("You selected GetAll Products..");
            var result = await productClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty { });
            if (result.Products != null && result.Products.Count > 0)
            {
                var products = result.Products;
                Console.WriteLine($"Products Count: {products.Count}");

                foreach (var product in products)
                {
                    Console.WriteLine($"Product Name is {product.Name} and Product Price is {product.Price}");
                }
            }
            await PrintSubMenu(2);
        }

        async Task AddOrder()
        {
            Console.WriteLine("Please enter required information: ");
            Console.Write("Please enter OrderId: ");
            int orderId = int.Parse(Console.ReadLine());

            var customers = await customerClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty());

            Console.WriteLine("available Customers: ");
            foreach (var customer in customers.Customers)
            {
                Console.WriteLine($"CustomerId: {customer.Id} CustomerName: {customer.Name}");
            }

            Console.Write("Please eneter CustomerId: ");
            int customerId = int.Parse(Console.ReadLine());

            List<OrderItemRequest> orderItemRequests = await AddOrderItems(orderId);

            await AddOrderRequest(orderId, customerId, orderItemRequests);
            await PrintSubMenu(3);

            async Task<List<OrderItemRequest>> AddOrderItems(int orderId)
            {
                Console.WriteLine("Add your OrderItem: ");
                var products = await productClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty());

                Console.WriteLine("available Products: ");
                foreach (var product in products.Products)
                {
                    Console.WriteLine($"ProductId: {product.Id} ProductName: {product.Name}");
                }
                Console.Write("Please eneter ProductId: ");
                int productId = int.Parse(Console.ReadLine());


                List<OrderItemRequest> orderItemRequests = new List<OrderItemRequest>();
                OrderItemRequest orderItemRequest = new OrderItemRequest
                {
                    Id = orderId + 120,
                    OrderId = orderId,
                    ProductId = productId
                };
                orderItemRequests.Add(orderItemRequest);
                Console.Write("Do you want to add another order item (y/n)?");
                var response = Console.ReadLine();
                if (response.ToLower() == "y")
                {
                    orderItemRequests.AddRange(await AddOrderItems(orderId));
                }
                return orderItemRequests;
            }

            async Task AddOrderRequest(int orderId, int customerId, List<OrderItemRequest> orderItemRequests)
            {
                var request = new OrderRequest
                {
                    CustomerId = customerId,
                    Id = orderId,
                };
                request.OrderItems.AddRange(orderItemRequests);
                var result = await orderClient.AddAsync(request);

                if (result.IsSuccess)
                {
                    Console.WriteLine(result.Message);
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
        }
        async Task DeleteOrder()
        {
            Console.Write("\r\nEnter Id of Order you want to delete: ");
            int orderId = int.Parse(Console.ReadLine());
            var result = await orderClient.DeleteAsync(new DeleteOrderRequest { Id = orderId });
            if (result.IsSuccess)
            {
                Console.WriteLine($"Order with Id: {orderId} is deleted successfully...");
            }
            else
            {
                Console.WriteLine("Delete operation is not successful. Error:", result.Message);
            }
            await PrintSubMenu(3);
        }
        async Task GetOrder()
        {
            Console.Write("\r\nEnter Id of Order to get: ");

            int orderId = int.Parse(Console.ReadLine());
            var result = await orderClient.GetAsync(new GetOrderRequest { Id = orderId });

            if (result.IsSuccess)
            {
                PersianCalendar pc = new PersianCalendar();
                var date = result.Order.CreateDate.ToDateTime();
                var persianDate = pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date) + " " + pc.GetHour(date) + ":" + pc.GetMinute(date) + ":" + pc.GetSecond(date);

                Console.WriteLine($"Order CreateDate is {persianDate} and CustomerId is {result.Order.CustomerId}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            await PrintSubMenu(3);

        }
        async Task GetOrders()
        {
            PersianCalendar pc = new PersianCalendar();
            Console.WriteLine("GetAll Orders..");
            var result = await orderClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty { });
            if (result.Orders != null && result.Orders.Count > 0)
            {
                Console.WriteLine("Orders with details:\n");
                foreach (var order in result.Orders)
                {
                    var date = order.CreateDate.ToDateTime();
                    var persianDate = pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date) + " " + pc.GetHour(date) + ":" + pc.GetMinute(date) + ":" + pc.GetSecond(date);
                    var customer = await customerClient.GetAsync(new GetCustomerRequest
                    {
                        Id = order.CustomerId
                    });
                    Console.WriteLine($"OrderId: {order.Id} CustomerName: {customer.Customer.Name} PersianDate: {persianDate}");
                    Console.WriteLine($"with items:");
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = await productClient.GetAsync(new GetProductRequest
                        {
                            Id = orderItem.ProductId,
                        });
                        Console.WriteLine($"ProductName: {product.Product.Name} with ProductPrice: {product.Product.Price}");
                    }
                }
            }
            await PrintSubMenu(3);
        }

    }
    
    private static void ChangeConsoleStyle()
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine();
        Console.WriteLine("***************************************");
        Console.WriteLine();
        Console.WriteLine("  " + "Welcome to Order Management System");
        Console.WriteLine();
        Console.WriteLine("***************************************");
        Console.WriteLine();
    }
}