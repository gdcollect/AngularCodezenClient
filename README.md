# AngularCodezenClient
The angularCodeGenerator generates the Angular code, WebApi  Controllers and SPS based on the given sql server database schema. It creates the CRUD operations with
•	Stored procedures for DB
•	DataAccessLayer, Models, Controllers for WebApi
•	Services, Components, Models, Modules, Routes, Html Files, CSS files, etc for Angular
Prerequisites
•	Database with tables each table having one Primary Key.
•	Visual Studio Code or equivalent tool for building the Angular code.
•	Visual Studio for compiling the WebApi project.
** Every table should have a primary key if not set it manually in the code generator project.
How to use : 
1.	Click on File->New and provide the connection string of the database which needs to be created.
2.	It Lists the Tables and columns in the tree structure.
3.	Every Table and column has many properties which will appear on the property window with the hints.
4.	Change the properties as required.
5.	Set the Download file Path by File->Download Folder Path.
6.	Click on Process->Generate Code.
7.	It will Download a zip file to the set path which contains the WebApi code, Angular Code, SPS and Readme file.
8.	Follow the steps provided in the readme file.

**The code generation is done at the server and the code will be downloaded. This project is for taking the necessary data and sending I to the server to generate the code.

