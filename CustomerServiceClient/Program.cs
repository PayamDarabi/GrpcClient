using Grpc.Net.Client;
using CustomerServiceClient;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Customer.CustomerClient customerClient;
        Product.ProductClient productClient;
        Order.OrderClient orderClient;
        int enteredNumber = 0;
        bool isValid = false;

        CreateGrpcClients();
        await MainMenu();

        async Task MainMenu()
        {
            enteredNumber = 0;

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Enter which section you want to operate from this list:");
            Console.WriteLine("[1] Customer Menu");
            Console.WriteLine("[2] Product Menu");
            Console.WriteLine("[3] Order Menu");
            Console.WriteLine("[4] Exit");

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
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Welcome to Customer area.. please enter a number: ");
                    Console.WriteLine("[1] Add Customer");
                    Console.WriteLine("[2] Update Customer");
                    Console.WriteLine("[3] Delete Customer");
                    Console.WriteLine("[4] Get Customer");
                    Console.WriteLine("[5] GetAll Customers");
                    Console.WriteLine("[6] Back To MainMenu");

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
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Welcome to Product area.. please enter a number: ");
                    Console.WriteLine("[1] Add Product");
                    Console.WriteLine("[2] Update Product");
                    Console.WriteLine("[3] Delete Product");
                    Console.WriteLine("[4] Get Product");
                    Console.WriteLine("[5] GetAll Products");
                    Console.WriteLine("[6] Back To MainMenu");

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
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("Welcome to Order area.. please enter a number: ");
                    Console.WriteLine("[1] Add Order");
                    Console.WriteLine("[2] Update Order");
                    Console.WriteLine("[3] Delete Order");
                    Console.WriteLine("[4] Get Order");
                    Console.WriteLine("[5] GetAll Order");
                    Console.WriteLine("[6] Back To MainMenu");

                    isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
                    switch (enteredNumber)
                    {
                        case 1:
                            await AddOrder();
                            break;
                        case 2:
                            //    await UpdateOrder();
                            break;

                        case 3:
                            //    await DeleteOrder();
                            break;

                        case 4:
                            //    await GetOrder();
                            break;

                        case 5:
                            //     await GetOrders();
                            break;

                        case 6:
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
            Console.WriteLine("Please enter required information: ");
            Console.WriteLine("CustomerId: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("CustomerName: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("CustomerAge: ");
            int customerAge = int.Parse(Console.ReadLine());

            var result = await customerClient.AddAsync(new CustomerRequest { Id = customerId, Age = customerAge, Name = customerName });
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
            Console.WriteLine("Your selected operation is update customer.. please enter Id of customer you want to be updated:");
            int customerId = int.Parse(Console.ReadLine());
            var customerReply = await customerClient.GetAsync(new GetCustomerRequest { Id = customerId });

            Console.WriteLine("Enter your new infromation for customer with id:{0}", customerReply.Customer.Id);
            Console.WriteLine("CustomerName: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("CustomerAge: ");
            int customerAge = int.Parse(Console.ReadLine());
            var result = await customerClient.UpdateAsync(new CustomerRequest { Id = customerReply.Customer.Id, Age = customerAge, Name = customerName });
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
        async Task DeleteCustomer()
        {
            Console.WriteLine("Your selected operation is delete customer.. please enter Id of customer you want to be deleted:");
            int customerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your new infromation for customer with id:{0}", customerId);
            var result = await customerClient.DeleteAsync(new DeleteCustomerRequest { Id = customerId });
            if (result.IsSuccess)
            {
                Console.WriteLine("Customer with id {0} is deleted successfully...", customerId);
            }
            else
            {
                Console.WriteLine("delete operation is not successful. Error:", result.Message);
            }
            await PrintSubMenu(1);
        }
        async Task GetCustomer()
        {
            Console.WriteLine("Your selected operation is get customer.. please enter Id of customer you want:");

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
            Console.WriteLine("Your selected operation is get all customers..");
            var result = await customerClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty { });
            if (result.Customers != null && result.Customers.Count > 0)
            {
                Console.WriteLine("Customers Count: {0}", result.Customers.Count);
            }
            await PrintSubMenu(1);

        }

        async Task AddProduct()
        {
            Console.WriteLine("Please enter required information: ");
            Console.WriteLine("ProductId: ");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("ProductName: ");
            string productName = Console.ReadLine();

            Console.WriteLine("ProductPrice: ");
            double productPrice = int.Parse(Console.ReadLine());

            var result = await productClient.AddAsync(new ProductRequest { Id = productId, Price = productPrice, Name = productName });
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
            Console.WriteLine("Your selected operation is update Product.. please enter Id of Product you want to be updated:");
            int productId = int.Parse(Console.ReadLine());
            var productReply = await productClient.GetAsync(new GetProductRequest { Id = productId });

            Console.WriteLine("Enter your new infromation for Product with id:{0}", productId);
            Console.WriteLine("ProductName: ");
            string productName = Console.ReadLine();

            Console.WriteLine("ProductPrice: ");
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
            await PrintSubMenu(2);
        }
        async Task DeleteProduct()
        {
            Console.WriteLine("Your selected operation is delete Product.. please enter Id of Product you want to be deleted:");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your new infromation for Product with id:{0}", productId);
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
            Console.WriteLine("Your selected operation is get Product.. please enter Id of Product you want:");

            int productId = int.Parse(Console.ReadLine());
            var result = await productClient.GetAsync(new GetProductRequest { Id = productId });

            if (result.IsSuccess)
            {
                Console.WriteLine($"ProductName is {result.Product.Name} and Price is {result.Product.Price}");
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
                Console.WriteLine("Customers Count: {0}", result.Products.Count);
            }
            await PrintSubMenu(2);
        }

        async Task AddOrder()
        {
            Console.WriteLine("Please enter required information: ");
            Console.WriteLine("Please enter OrderId: ");
            int orderId = int.Parse(Console.ReadLine());

            var customers = await customerClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty());

            Console.WriteLine("available Customers: ");
            foreach (var customer in customers.Customers)
            {
                Console.WriteLine($"CustomerId: {customer.Id} CustomerName: {customer.Name}");
            }

            Console.WriteLine("Please eneter CustomerId: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Add your OrderItem: ");
            var products = await productClient.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty());

            Console.WriteLine("available Products: ");
            foreach (var product in products.Products)
            {
                Console.WriteLine($"ProductId: {product.Id} ProductName: {product.Name}");
            }
            Console.WriteLine("Please eneter ProductId: ");
            int productId = int.Parse(Console.ReadLine());


            List<OrderItemRequest> orderItemRequests = new List<OrderItemRequest>();
            OrderItemRequest orderItemRequest = new OrderItemRequest
            {
                Id = orderId + 120,
                OrderId = orderId,
                ProductId = productId
            };
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
            await PrintSubMenu(3);
        }
    }
}