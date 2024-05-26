# NewsclipWebApp

This repository contains the work done in Visual Studio for the web application project. It includes a web app, web API, class libraries, and a relational database built with ASP.NET Core and SQL Server.

## Note

Please note that due to some file issues experienced during the development, the ReactJS part of this project is maintained in a separate repository. You can find it [HERE]([url](https://github.com/PierrevdMerwe/NewsclipWebAppReactJS)).

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

1. Clone both repositories.
2. Change your DefaultConnection string in the appsettings.json file to your localDB connection string
3. Make sure on your local db that you created a database inside `SQL Server`>`localdb...`>`Databases` called `NewsclipDB`, you can do this by using the SQL Server Object Explorer.
4. Run a new query to create a table just to initialize the table and call the table `dbo.Profile`. You can run the below sql query to get the same results:

`    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)  NULL,
    [LastName]    NVARCHAR (50)  NULL,
    [Email]       NVARCHAR (50)  NULL,
    [PhoneNumber] NVARCHAR (20)  NULL,
    [Links]       NVARCHAR (MAX) NULL,
    [Industry]    NVARCHAR (50)  NULL,
    [Skills]      NVARCHAR (MAX) NULL,
    [Experience]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);`

5. Run the NewsClipWebApp to start the API. You can run tests on Postman to see if its working properly.
6. Run the ReactJS project.

## Built With

* ASP.NET Core - The web framework used
* React.js - The frontend library used
* SQL Server - The database used
