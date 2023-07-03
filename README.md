# Order Management GrpcServer
<br/>
<p align="center">
   <h1 align="center">gRPC Server</h1>
   <p align="center">
    An Awesome Example For gRPC Server
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
* [License](#license)
* [Authors](#authors)

## About The Project

gRPC is a cross-platform open source high performance remote procedure call framework. gRPC was initially created by Google, which used a single general-purpose RPC infrastructure called Stubby to connect the large number of microservices running within and across its data centers from about 2001.
This project includes the complete implementation of a grpc server in which three different services have been created to meet the business needs of the project, which is a shopping cart and order registration system. You can use this example to implement the examples you want.

This project includes the complete implementation of a grpc server in which three different services have been created to meet the business needs of the project (here is a shopping cart and order registration system).

The <b> Service </b> structure of this project is as follows:

<b> Customer service </b> (to perform CRUD operations on the customer entity) </br>
<b> Product service </b> (to perform CRUD operations on the product entity) </br>
<b> Order service </b> (to perform CRUD operations on the order entity and order items (shopping cart)) </br>

note that:
For more simplicity, the service and repository layers are placed in the same project and in the corresponding folders. Also, instead of connecting to the database, static lists are used for storage in the repository layer. You can use this example to implement the specific services you want, or by participating in the project, you can add more features to the project.

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
   git clone https://github.com/PayamDarabi/GrpcServer.git
   ```
2. Enter your application runtime url in `launchSettings.json`

   ```JSON
   "applicationUrl": "http://localhost:5213;https://localhost:7199",
   ```
3. Run the project
   
## Usage

This code shows how to create an ASP.NET Core gRPC Server. At the end, you'll have a gRPC service that responses to clients that need its functionalities.

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.
* If you have suggestions for adding or removing projects, feel free to [open an issue](https://github.com/PayamDarabi/GrpcServer/issues/new) to discuss it, or directly create a pull request after you edit the *README.md* file with necessary changes.
* Please make sure you check your spelling and grammar.
* Create individual PR for each suggestion.
  
### Creating A Pull Request

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License
Distributed under the MIT License. See LICENSE for more information.

## Authors

* **Payam Darabi** - *Software Developer* - [Payam Darabi](https://www.linkedin.com/in/payamdarabi/)
