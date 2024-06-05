Inventory Management Web Application 

  

  

Table of Contents 

  

- [Introduction] 

- [Features] 

- [Technology Stack] 

- [Getting Started]

- [Installation]

- [Contributing]

- [Contact]

  

Introduction 

  

The Inventory Management Web Application is a robust system designed to streamline the process of managing inventory. Built using .NET 8, this application provides a user-friendly interface and powerful backend for efficient inventory control. 

  

Features 

  

- User Authentication and Authorization 

- Product Management 

  

Technology Stack 

  

Frontend: 

 Angular 17 

  

Backend: 

  - .NET 8 

  - ASP.NET Core 

  - Entity Framework Core 

  

Database: 

  - SQL Server 

  

Tools: 

  - Visual Studio 2022 

  - Git 

 Getting Started 

  

To get a local copy up and running follow these simple steps. 

  

Prerequisites 

  

- .NET 8 SDK 

- SQL Server 

- Visual Studio 2022 or any preferred IDE 

 

Installation 

1. Clone the repository: 

    git clone https://github.com/your-username/inventory-management.git 

    cd inventory-management 

  

2. Setup the database: 

    - Create a new database in SQL Server. 

    - Update the connection string in `appsettings.json`. 

  

3. Restore dependencies 

    dotnet restore 

  

4. Apply migrations and update the database: 

    dotnet ef database update 

Or  

From package manager console  

Update-database 

 

5. Run the application: 

    dotnet run 

    

Usage 

  

- Open your browser and navigate to given localhost url by your terminal. 

- Register a new user or log in with existing credentials. 

	Default:  

username: admin, 

Password: pass123 

   
Additional Features:
It uses signalr package for realtime notification. 
Notification is sent through url https://localhost:7025/hub
Notification is sent in a format of 

```c#
public class HubMessageDTO
{

    public string Item { get; set; }
    public string StoreName { get; set; }
    public decimal Quantity { get; set; }
    public DateTime Date = DateTime.Now;
}
```
and in Frontend (angular) you have this syntax to receive data

```javascript
public startConnection() {
// Building the connection to the SignalR hub with automatic reconnection
this.hubConnection = new signalR.HubConnectionBuilder()
.withUrl('https://localhost:7025/hub')  // URL to the SignalR hub endpoint
.withAutomaticReconnect()  // Enables automatic reconnection in case of disconnection
.build();  // Completes the building of the HubConnection instance

// Starting the SignalR connection
this.hubConnection.start()
.then(() => console.log('Connection started'))  // Log success message on successful connection
.catch(err => console.log('Error while starting connection: ' + err));  // Log error message if the connection fails
}

// Method to add a listener for inventory updates from the server
public addInventoryUpdateListener(callback: (itemName: string, quantity: number) => void) {
// Setting up an event listener for 'ReceiveInventoryUpdate' event from the SignalR hub
this.hubConnection.on('GetLowStockNotification', (itemName: string, quantity: number) => {
callback(itemName, quantity);  // Invoke the callback function with the received data
});

```


The Keyword 'GetLowStockNotification' is used to get Notification when Item is running out
similarly there are two other keyword.

1. MileStoneSale
2. EmailNotification
3. GetLowStockNotification
   
Contributing 

Any contributions you make are greatly appreciated. 

  

1. Fork the Project. 

2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`). 

3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`). 

4. Push to the Branch (`git push origin feature/AmazingFeature`). 

5. Open a Pull Request. 

  

Contact 

Lekh Raj Awasthi 

lekhrajawasthi123@gmail.com  

 

Project Link:  

[https://github.com/LEKH-RAJ-AWASTHI/inventory-management]  
