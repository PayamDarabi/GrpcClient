# Order Management GrpcClient
<br/>
<p align="center">
   <h1 align="center">gRPC Client</h1>
   <p align="center">
    This Is Example For gRPC Client With .Net Console Application
    <br/>
    <br/>
  </p>
  <p align="center">  
    <img  style="center" src=https://github.com/PayamDarabi/GrpcServer/assets/8627007/7c0d7308-6dbf-43f8-9f0a-61692e59cffc/>
  </p>
</p>



## Table Of Contents

* [About the Project](#about-the-project)
* [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Contributing](#contributing)
* [Authors](#authors)

## About The Project

![Capture](https://github.com/PayamDarabi/GrpcClient/assets/8627007/96ef910a-c449-47c8-8873-4c9a6d387f43)

gRPC is a cross-platform open source high performance remote procedure call framework. gRPC was initially created by Google, which used a single general-purpose RPC infrastructure called Stubby to connect the large number of microservices running within and across its data centers from about 2001.
This project includes the complete implementation of a grpc server in which three different services have been created to meet the business needs of the project, which is a shopping cart and order registration system. You can use this example to implement the examples you want.

This project includes the complete implementation of a grpc client with console application in C# that uses  three different services have been created to meet the business needs of the project (here is a shopping cart and order registration system).

<b> Customer Menu </b> (to perform CRUD operations on the customer entity) </br>
<b> Product Menu </b> (to perform CRUD operations on the product entity) </br>
<b> Order Menu </b> (to perform CRUD operations on the order entity and order items (shopping cart)) </br>

## Built With

Technologies that used in this project

* [.NetCore](https://dotnet.microsoft.com/en-us/download)
* [gRPC](https://grpc.io/)

## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

Visual Studio 2022 with the ASP.NET and web development workload.

### Installation

1. Clone the repo

```sh
git clone https://github.com/PayamDarabi/GrpcClient.git
```
2. Add .proto files from gRPC server
Create a Protos folder in the gRPC client project.
Copy the Protos\.proto files from the Order Management Grpc Server to the Protos folder in the Order Management Grpc Client project.

Update the namespace inside the .proto files to the project's namespace:
 ```JSON
 option csharp_namespace = "OrderManagementGrpcClient";
 ```
Edit the GrpcGreeterClient.csproj project file:

Right-click the project and select Edit Project File.
Add an item group with a <Protobuf> element that refers to the greet.proto file:

```XML

<ItemGroup>
	<Protobuf Include="Protos\customer.proto" GrpcServices="Client" />
	<Protobuf Include="Protos\order.proto" GrpcServices="Client" />
	<Protobuf Include="Protos\orderItem.proto" GrpcServices="Client" />
	<Protobuf Include="Protos\product.proto" GrpcServices="Client" />
</ItemGroup>
```
3. Update the gRPC client Program.cs file with the following code.

```C#
// The port number must match the port of the Order Management gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7199");
```
In the preceding code, replace the localhost port number 7199 with the HTTPS port number specified in Properties/launchSettings.json within the Order Management Grpc Server project.
Program.cs contains the entry point and logic for the gRPC client.

4. Run the project

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.
* If you have suggestions for adding or removing projects, feel free to [open an issue](https://github.com/PayamDarabi/GrpcClient/issues/new) to discuss it, or directly create a pull request after you edit the *README.md* file with necessary changes.
* Please make sure you check your spelling and grammar.
* Create individual PR for each suggestion.
  
### Creating A Pull Request

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Authors

* **Payam Darabi** - *Software Developer* - [Payam Darabi](https://www.linkedin.com/in/payamdarabi/)
