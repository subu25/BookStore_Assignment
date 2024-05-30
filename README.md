# BookStore

Here's a step-by-step guide to set up and run the RESTful API project locally 
using .NET Core:
Install Prerequisites:
Install the .NET Core SDK for your operating system if you haven't already. 
Download and install all necessary softwares and packages mentioned in the Prerequisite section of the document.
Clone the Repository:
Clone the repository containing the RESTful API project to your local machine 
using Git.
Navigate to the Project Directory:
Open a terminal or command prompt and navigate to the directory where you 
cloned the repository.
Set Up the Database:
Ensure you have a SQL Server instance installed and running locally.
Open the appsettings.json file in the project and update the connection string to 
point to your local SQL Server instance.
Run the Entity Framework Core migrations to create the database schema and 
seed initial data
16
Build the Project:
Build the project on your local machine.
Run the Project
Verify API Operation:
Once the project is running, open a web browser or a tool like Postman and 
navigate to https://localhost:<port>/api/tasks to verify that the API is up and 
running. You should see a list of tasks if everything is set up correctly.
Test Endpoints:
Use an API testing tool like Postman or curl to test each endpoint (create, retrieve, 
update, delete) with sample requests and verify that they are functioning as 
expected.
You now have the RESTful API project set up and running locally on your 
machine
