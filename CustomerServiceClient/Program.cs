using Grpc.Net.Client;
using CustomerServiceClient;

Customer.CustomerClient client;

CreateGrpcClient(out client);
await PrintMenu();


async Task PrintMenu()
{
    Console.WriteLine("Enter operation [number] from list:");
    Console.WriteLine("[1] Add");
    Console.WriteLine("[2] Update");
    Console.WriteLine("[3] Delete");
    Console.WriteLine("[4] Get");
    Console.WriteLine("[5] GetAll");
    Console.WriteLine("[6] Exit");

    int enteredNumber = 0;
    bool isValid = int.TryParse(Console.ReadLine(), out enteredNumber);
    if (isValid)
    {
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
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}

void CreateGrpcClient(out Customer.CustomerClient client)
{
    GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7199");
    client = new Customer.CustomerClient(channel);
}

async Task AddCustomer()
{
    Console.WriteLine("Your selected Operation is add new customer.. Please enter required information:");
    Console.WriteLine("CustomerId: ");
    int customerId = int.Parse(Console.ReadLine());

    Console.WriteLine("CustomerName: ");
    string customerName = Console.ReadLine();

    Console.WriteLine("CustomerAge: ");
    int customerAge = int.Parse(Console.ReadLine());

    var result = await client.AddAsync(new CustomerRequest { Id = customerId, Age = customerAge, Name = customerName });
    if (result.IsSuccess)
    {
        Console.WriteLine(result.Message);
    }
    else
    {
        Console.WriteLine(result.Message);
    }
    await PrintMenu();
}
async Task UpdateCustomer()
{
    Console.WriteLine("Your selected operation is update customer.. please enter Id of customer you want to be updated:");
    int customerId = int.Parse(Console.ReadLine());
    var customerReply = await client.GetAsync(new GetCustomerRequest { Id = customerId });

    Console.WriteLine("Enter your new infromation for customer with id:{0}", customerReply.Customer.Id);
    Console.WriteLine("CustomerName: ");
    string customerName = Console.ReadLine();

    Console.WriteLine("CustomerAge: ");
    int customerAge = int.Parse(Console.ReadLine());
    var result = await client.UpdateAsync(new CustomerRequest { Id = customerReply.Customer.Id, Age = customerAge, Name = customerName });
    if (result.IsSuccess)
    {
        Console.WriteLine(result.Message);
    }
    else
    {
        Console.WriteLine(result.Message);
    }
    await PrintMenu();
}
async Task DeleteCustomer()
{
    Console.WriteLine("Your selected operation is delete customer.. please enter Id of customer you want to be deleted:");
    int customerId = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter your new infromation for customer with id:{0}", customerId);
    var result = await client.DeleteAsync(new DeleteCustomerRequest { Id = customerId });
    if (result.IsSuccess)
    {
        Console.WriteLine("Customer with id {0} is deleted successfully...", customerId);
    }
    else
    {
        Console.WriteLine("delete operation is not successfull. Error:", result.Message);
    }
    await PrintMenu();
}
async Task GetCustomer()
{
    Console.WriteLine("Your selected operation is get customer.. please enter Id of customer you want:");

    int customerId = int.Parse(Console.ReadLine());
    var result = await client.GetAsync(new GetCustomerRequest { Id = customerId });

    if (result.IsSuccess)
    {
        Console.WriteLine($"CustomerName is {result.Customer.Name} and CustomerAge is {result.Customer.Age}");
    }
    else
    {
        Console.WriteLine(result.Message);
    }
    await PrintMenu();

}
async Task GetCustomers()
{
    Console.WriteLine("Your selected operation is get all customers..");
    var result = await client.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty { });
    if (result.Customers != null && result.Customers.Count > 0)
    {
        Console.WriteLine("Customers Count: {0}", result.Customers.Count);
    }
    await PrintMenu();

}