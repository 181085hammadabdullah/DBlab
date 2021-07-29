create database Project
use Project

CREATE TABLE Admin_Profile
(
	Admin_Id int identity(1,1) NOT NULL primary key,
	email varchar(20) not NULL,
	Password varchar(10) not NULL,
	First_Name varchar(50) NOT NULL,
	Last_Name varchar(50) NOT NULL,
	Gender varchar(8) NOT NULL check(Gender in('Male','Female') or Gender in('male','female')),
	Phone char(11) NOT NULL,
	CNIC char(15) NOT NULL,
	Qualfication varchar(25) NOT NULL,
	DOB date NOT NULL,
	[Address] varchar(500) NOT NULL,
);

CREATE TABLE Seller
( 
	SellerID int identity(1,1) NOT NULL primary key,
	[Password] varchar(20) not NULL,
    [Name] varchar(50) NOT NULL,
	Gender varchar(8) NOT NULL check(Gender in('Male','Female') or Gender in('male','female')),
	[Address] varchar(500) NOT NULL,
	City varchar(20) NOT NULL,
	ContactNo char(11) NOT NULL,
	DOB date NOT NULL,
	Email varchar(25) NOT NULL,
	SignUpDate DATETIME default CURRENT_TIMESTAMP NOT NULL,
);

CREATE TABLE Customer
(
	CustomerID int identity(1,1) NOT NULL primary key,
	FIrst_Name varchar(50) NOT NULL,
	Last_Name varchar(50) NOT NULL,
	Phone varchar(15) NOT NULL,
	Gender varchar(8) NOT NULL,
	Email varchar(50) NOT NULL,
	[Password] varchar(20) NOT NULL,
	Zipcode char(5),
	[Address] varchar(100),
	City varchar(50),
	Last_Login DATETIME default CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Category
(
	CategoryID int identity(1,1) NOT NULL primary key,
	[Name] varchar(50) NOT NULL,
	[Description] varchar(200) NOT NULL
);

CREATE TABLE Product
(
	ProductID int identity(1,1) NOT NULL,
	Product_Name varchar(100) NOT NULL,
	Unit_price int NOT NULL check(Unit_price>0),
	Stock int not null check(Stock>0),
	City varchar(20) not null,
	Date_Time_of_Entry DATETIME default CURRENT_TIMESTAMP NOT NULL,
	Manufacturer varchar(100) not null,
	Model char(4) not null check(Model>='1980' and Model <='2020'),
	Released_Date Date not null,
	CategoryID int NOT NULL,
	SellerID int not NULL,
	Primary Key(ProductID),
	foreign key (CategoryID) references Category (CategoryID) on delete cascade on update Cascade,
	foreign key (SellerID) references Seller (SellerID) on delete cascade on update cascade
)

create table Payment_Methods
(
	P_id int identity(1,1) not NULL Primary Key,
	[Name] varchar(100) NOT NULL,
	[Description] varchar(300) NOT NULL,
)

CREATE TABLE Invoice
( 
	InvoiceNo int identity(1,1) not NULL Primary Key,
	CustomerID int NOT NULL,
	Date_Time  DATETIME default CURRENT_TIMESTAMP NOT NULL,
	[status] varchar(20),
	Payment_Method_id int NOT NULL
	foreign key (CustomerID) references Customer (CustomerID) on delete cascade on update Cascade,
	foreign key (Payment_Method_id) references Payment_Methods (P_id) on delete cascade on update Cascade
);
CREATE TABLE InvoiceItems
( 
	InvoiceNo int NOT NULL,
	ProductID int NOT NULL,
	Quantity int NOT NULL check(Quantity>0),
	PRIMARY KEY(InvoiceNo,ProductID),
	foreign key (InvoiceNo) references Invoice (InvoiceNo) on delete cascade on update Cascade,
	foreign key (ProductID) references Product (ProductID) on delete cascade on update Cascade
);

Create table Feedback
(
	CustomerID int NOT NULL,
	invoice_no int NOT NULL,
	Feed_back varchar(500) NOT NULL,
	Date_Time DATETIME default CURRENT_TIMESTAMP NOT NULL,
	Primary Key(CustomerID,invoice_no),
	foreign key (CustomerID) references Customer (CustomerID) on delete cascade on  update Cascade,
	foreign key (invoice_no) references Invoice (InvoiceNo) on delete no action on update no action
)

create table Blocked_Accounts
(
	Seller_id int NOT NULL,
	Date_time DATETIME default CURRENT_TIMESTAMP NOT NULL,
	[Name] varchar(50) NOT NULL,
	Email varchar(25) NOT NULL,
	Gender varchar(8) NOT NULL check(Gender in('Male','Female') or Gender in('male','female')),
	Primary Key(Seller_id),
	
)

create table [Notification]
(
	ID int identity(1,1) not NULL Primary Key,
	Title varchar(100) NOT NULL,
	[Description] varchar(500) NOT NULL,
	Time_Date DATETIME default CURRENT_TIMESTAMP NOT NULL,
)

Create Table Add_To_Cart
(
	Customer_Id  int NOT NULL,
	Product_Id int NOT NULL,
	Quantity int NOT NULL default 1,
	Primary Key(Customer_Id,Product_Id),
	foreign key (Customer_Id) references Customer (CustomerID) on delete no action on update Cascade,
	foreign key (Product_Id) references Product(ProductId) on delete no action on update Cascade
)

