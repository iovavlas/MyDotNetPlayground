Entity Framework is an open-source ORM (Object/Relational Mapper) framework for .NET applications supported by Microsoft. 
It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored.

[Objects (Classes)] <------> [ORM (EF)] <-------> [Relational Data (DB)]



[DbContext (Gateway to our DB)] --- 1:N ---> [DbSet (Tables in our DB)]

We use LINQ to query the DbSets (Tables) and EF transforms our LINQ-Queries to SQL Queries for our DB. 

EF opens and closes the DB-Connection and does all the mapping for us automatically. 



In code first workflow, every time we modify our domain model by adding or modifying a class, we create a migration and then run it (sync) on the database.



Install EF using the following command: 
	PM> Install-Package EntityFramework -IncludePrerelease
Note: At least one class in our project must inherits from 'DbContext'. --> See Configuration.cs and CampContext.cs



The first time we want to use migrations, we need to enable them:
	PM> enable-migrations 
We should see then a new folder 'Migrations' in the Solution Explorer, where all our migrations will be stored.



After making a change to our model class, we should create a new migration:
	PM> add-migration {Name}
Then we should see a new migration inside the 'Migrations' folder. 



To run a migration, in order to sync with the DB:
	PM> update-database
Under 'App_Data' we should then see a hidden DB file. 
By opening it, we should then navigate to the DB, under Server Explorer.



We can override the default EF conventions for a column definition (e.g. a string is nullable by default) using Data Annotations in our Model (e.g. [Required]) and then running a migration.