use Project

--Admin Procedures

/*1-Admin SignUp*/
Create Procedure Admin_SignUp 
@email varchar(20) ,@Password varchar(10) ,@First_Name varchar(50),@Last_Name varchar(50),@output int output
as 
begin
	if(@email is NULL or @Password is NULL or @First_Name is NULL or @Last_Name is NULL)
	begin
		print 'Values Not Be NULL'
		set @output=0
	end
	else
	if exists(select *
	from Admin_Profile
	where Admin_Profile.email=@email
	)
	begin
	print'Duplicate Email Error'
	set @output=2
	end
	else
	begin
	--Sign-UP only takes name , email and password rest of profile is updated in Update Profile
		insert into Admin_Profile values(@email,@Password,@First_Name,@Last_Name,'Male','00000000000','000000000000000','No','1/1/2001','No')
		set @output=1
	end
end

/*2-Admin Login_In*/
create procedure ADMIN_LOGIN
@email varchar(20),@Password varchar(10),@output int output
as
begin
	if exists(select * from admin_profile where email=@email and [password]=@password)
	begin
		select * from admin_profile
		set @output=1
	end
	else
	begin
		set @output=0
	end
end

/*3-Update Admin Profile*/
create procedure UPDATE_ADMIN_Profile
@email varchar(20),@newemail varchar(20),@newpassword varchar(10),@fname varchar(50),@lname varchar(50),@gender varchar(8),@phone char(11),@cnic char(13),@qual varchar(25),@DOB date,@address varchar(500),
@output int output
as
begin
	if exists(select * from admin_profile where email=@email)
	begin
		update admin_profile
		set email=@newemail,[password]=@newpassword,First_Name=@fname,Last_Name=@lname,Gender=@gender,Phone=@phone,CNIC=@cnic,Qualfication=@qual,DOB=@DOB,[Address]=@address
		where email=@email
		set @output=1
	end
	else
	begin
		print('Email or Password is Incorrect')
		set @output=0
	end
end

--4 Get Admin Profile
create Procedure Get_profile
@email varchar(20)
as
begin
select * from Admin_Profile where Admin_Profile.email=@email
end


/*6 Add New Category*/
create procedure ADD_Category
@Name varchar(50),@Descipt varchar(200),@output int output
as
begin
if exists(select *
from Category
where Category.Name=@Name)
begin
set @output=0
end
else
begin
	insert into Category
	values(@Name,@Descipt)
	set @output=1
	end
end

/*7 Update New Category*/
create procedure Update_Category
@CategoryID int, @Name varchar(50),@Descipt varchar(200),@output int output
as
begin
if exists(select *
from Category
	where Category.CategoryID=@CategoryID)
begin
	set @output=1
	update Category
	set Name=@Name,[Description]=@Descipt
	where Category.CategoryID=@CategoryID
end
else
begin
	set @output=0
end
end



/*8 Delete A Category*/
create procedure Delete_Category
@CategoryID int, @output int output
as
begin
if exists(select *
from Category
	where Category.CategoryID=@CategoryID)
begin
	set @output=1
	delete from Category
	where Category.CategoryID=@CategoryID
end
else
begin
	set @output=0
end
end

/*9 Get A Category*/
create procedure Get_Category
@CategoryID int
as
begin
(select *
from Category where Category.CategoryID=@CategoryID)
end

/*10 Add New Payment Method*/
create procedure Add_New_Payment_Method 
 @Name varchar(100),@Description varchar(300),@output int output
as 
begin
	if( @Name is NULL or @Description is null)
	begin
		print'Values Not be NULL'
		set @output=0
	end
	else if exists(select *
	from Payment_Methods
	where Name=@Name)
	begin
		print'Duplicate Name Error'
		set @output=2
	end
	else 
	begin
	set @output=1
		insert into Payment_Methods values(@Name,@Description)
	end
end


/*11 Update Payment Method*/
create procedure Update_Payment_Method 
@Method_id int, @Name varchar(100),@Description varchar(300),@output int output
as 
begin
	 if exists(select *
	from Payment_Methods
	where Payment_Methods.P_id=@Method_id)
	begin
		set @output=1
		Update Payment_Methods
		set Payment_Methods.Name=@Name,Payment_Methods.Description=@Description
		where Payment_Methods.P_id=@Method_id
	end
	else 
	begin
	set @output=0
		print'No record Found'
	end
end


/*12 Delete Payment Method*/
create procedure Delete_Payment_Method 
@Method_id int,@output int output
as 
begin
	 if exists(select *
	from Payment_Methods
	where Payment_Methods.P_id=@Method_id)
	begin
		set @output=1
		delete from Payment_Methods
		where Payment_Methods.P_id=@Method_id
	end
	else 
	begin
	set @output=0
		print'No record Found'
	end
end


/*13 Get A Payment_Method*/
create procedure Get_Payment_Method
@p_id int
as
begin
(select *
from Payment_Methods where Payment_Methods.P_id=@p_id)
end


/*14 Add New Notification*/
create procedure Add_New_Notification 
 @Title varchar(100),@Description varchar(300),@output int output
as 
begin
	if(@Title is NULL or @Description is null)
	begin
		print'Values Not be NULL'
		set @output=0
	end
	else 
	begin
		insert into [Notification] values(@Title,@Description,CURRENT_TIMESTAMP)
		set @output=1
	end
end


/*15 Block A Seller*/
create Procedure Block_A_Seller 
@Seller_id int,@output int output
as 
begin
		if exists (select *from Seller where Seller.SellerID=@Seller_id)
		begin
		declare @Name varchar(50)
		declare @Email varchar(50)
		declare @Gender varchar(8)
		select @Name=Seller.Name,@Email=Seller.Email,@Gender=Seller.Gender
		from Seller
		where Seller.SellerID=@Seller_id
		insert into Blocked_Accounts values(@Seller_id,current_timestamp,@Name,@Email,@Gender)
			
		end

		else
		begin
			print'Invalid seller Id'
			set @output=0
		end
		begin
		if exists (select *from Seller where Seller.SellerID=@Seller_id)
		begin
		delete from Seller
		where Seller.SellerID=@Seller_id
		set @output=1
		end
		end
		
end

declare @output1 int
exec Block_A_Seller @Seller_id=18 ,@output=@output1  output

/*16 Show All Customers*/
create Procedure Show_All_Customers
as 
begin
	select *
	from Customer
end


/*17 Show All Non Blocked Sellers*/
create procedure NON_BLOCKED_Sellers
as
begin
	select * from seller
end

/*18 Show All Blocked Sellers*/
create procedure Blocked_Sellers
as
begin
	select *
	from Blocked_Accounts 
end


/*19 Show All feedbacks*/
create procedure show_all_feedbacks
as
begin
	select Customer.CustomerID As CustomerID,invoice_no,Email,Date_Time,Feed_back,FIrst_Name,Last_Name,Phone,Address
	from Feedback join Customer on Feedback.CustomerID=Customer.CustomerID
end


--20-Show All Categories
create Procedure Show_All_Categories
as 
begin
select *
from Category
end

--21-Show All Payments
create Procedure Show_All_Payments
as 
begin
select *
from Payment_Methods
end

--22-Unblock A Seller
create Procedure UnBlock_A_Seller 
@Seller_id int,@output int output
as 
begin
		if exists (select * from Blocked_Accounts where Blocked_Accounts.Seller_id=@Seller_id)
		begin
		declare @Name varchar(50)
		declare @Email varchar(50)
		declare @Gender varchar(8)
		select @Name=Blocked_Accounts.Name,@Email=Blocked_Accounts.Email,@Gender=Blocked_Accounts.Gender
		from Blocked_Accounts
		where Blocked_Accounts.Seller_id=@Seller_id
		insert into Seller values('123456',@Name,@Gender,'No','No','00000000000','2001/4/5',@Email,CURRENT_TIMESTAMP)
			
		end

		else
		begin
			print'Invalid seller Id'
			set @output=0
		end
		begin
		if exists (select *from Blocked_Accounts where Blocked_Accounts.Seller_id=@Seller_id)
		begin
		delete from Blocked_Accounts
		where Blocked_Accounts.Seller_id=@Seller_id
		set @output=1
		end
		end
		
end
--Customer Procedures

--1-Customer login
create procedure Customer_Login
@email varchar(50),@password varchar(20),@output int output
as 
begin
	if (@email is null or @password is  null)
    begin
		print 'Values Should Not be NULL'
		set @output=0
    end
	else 
	begin 
		if exists(select * from customer where Email=@email and [Password]=@password)
		begin
			set @output=1
			update customer
			set Last_Login=CURRENT_TIMESTAMP
			where email=@email
		end
		else
		begin
			print'User not found with these details'
			set @output=2
		end
	end
end
--2-customer sign up
create procedure Customer_sign_up
@FName varchar(50),@LName varchar(50),@Phone varchar(50),@Gender varchar(8),@email varchar(50),@Password varchar(20) ,@output int output
as 
begin
	if(@FName is NULL or @LName is NULL or @Phone is NULL or @gender is NULL or @email is NULL or @Password is NULL)
	begin
		print 'Values cannot be NULL'
		set @output=0
	end
	else if exists(select *from Customer where Customer.email=@email)
	begin
		print'Duplicate Email Error'
		set @output=0
	end
	else
	begin
		insert into Customer(FIrst_Name,Last_Name,Phone,Gender,Email,[Password],Zipcode,[Address],City) 
		values
		(@FName,@LName,@Phone,@Gender,@email,@Password,NULL,NULL,NULL)
		set @output=1
	end
end

--3-update customer profile
create procedure Update_Customer_Profile 
@FName varchar(50),@LName varchar(50),@email varchar(50),@password varchar(20),@address varchar(100),@city varchar(50),@zipCode char(5),
@output int output
as 
begin
	if (@FName is null or @LName is NULL or @email is null or @password is  null or 
	@address is  null or @city is NULL or @zipCode is NULL)
    begin
		print 'Values Should Not Be NULL'
		set @output=0
    end
	else 
	begin 
		if exists (select * from Customer where Email=@Email)
		begin 
			update Customer 
			set FIrst_Name=@FName,Last_Name=@LName,Email=@email,[Password]=@password,[Address]=@address
			,City=@city,ZipCode=@zipCode
			where Email=@email
			set @output=1
		end
		else
		begin
			print 'Customer could not be found'
			set @output=0;
		end
	end
end

--4-update qunatity
create procedure Update_Quantity @p_id int,@quan int,@email varchar(50)
as
begin
	declare @custid int,@q int
	SELECT @custid =Customer.CustomerID FROM Customer
	where email=@email
	update Add_To_Cart
	set Quantity=@quan where Customer_Id=@custid and Product_Id=@p_id

end
--5-show all customers ----------------------------------------------------------------
create procedure show_all_customers
as
begin
	select First_Name,Last_Name,Phone,Gender,Email,[Password]
	from Customer
end
--6-show all products
create procedure show_all_products
as
begin
	select* from product
end

--7-filter_search
create procedure Filter_Products @categ varchar(100),@upper int ,@lower int,@p_name varchar(50),@p_location varchar(50),@output int output
as
begin
	if @categ is NULL or @upper is NULL or @lower is NULL or @p_name is NULL or @p_location is null
	begin
		set @output=0
	end
	else
	begin
		if exists(	select * from Product join category on Product.CategoryID = category.CategoryID 
					where [Name]=@categ and Unit_price >=@lower 
					and Unit_price <=@upper and Product_Name=@p_name and City =@p_location)
		begin
			select * from Product join category on Product.CategoryID = category.CategoryID 
					where [Name]=@categ and Unit_price >=@lower 
					and Unit_price <=@upper and Product_Name=@p_name and City =@p_location
			set @output=1
		end
		else
		begin
			set @output=2
		end
	end
end
--8-get cart items
create procedure get_cart_items @email varchar(50)
as
begin
	declare @custid int
	SELECT @custid =Customer.CustomerID FROM Customer
	where email=@email
	select * from dbo.add_to_Cart join Product on add_to_cart.Product_id=Product.ProductID and add_to_cart.Customer_id=@custid join Category on Category.CategoryID=Product.CategoryID
end
--9-get invoice customer
create procedure get_invoice_customer @email varchar(50)
as
begin
	select * from customer where Email=@email
end
--10-get invoice information
create procedure get_invoice_information @email varchar(50)
as
begin
	declare @custid int
	SELECT @custid=Customer.CustomerID FROM Customer
	WHERE email=@email
	select invoiceNo from invoice
	where CustomerID=@custid
end
--11-get Notifications
create Procedure get_Notifications
as
begin
	select Title,[Description],Convert(varchar(10),Time_Date,103) as Date_Time from [Notification]
end
--12-insert cart product
create procedure insert_cart_product @id int ,@email varchar(50),@output int output
as
begin
		declare @custid int
		SELECT @custid=Customer.CustomerID FROM Customer
		WHERE email=@email
	if exists (select * from Add_To_Cart where Product_Id=@id and Customer_Id=@custid)
	begin
		set @output=0
	end
	else
	begin
		insert into Add_To_Cart(Customer_Id,Product_Id)
		values
		(@custid,@id)
		set @output=1
	end
end
--23-print invoice
create procedure print_invoice @inv_num int
as
begin
	select InvoiceItems.ProductID,Product_Name, [Description],Unit_price,Quantity,CONVERT(VARCHAR(10), Date_Time, 103) as Date_Time from InvoiceItems join Product on InvoiceItems.ProductID=Product.ProductID join Category on Category.CategoryID=Product.CategoryID join Invoice on Invoice.InvoiceNo=InvoiceItems.InvoiceNo where invoiceitems.InvoiceNo=@inv_num
end
--24-remove cart product
create procedure remove_cart_product @id int,@email varchar(50) 
as
begin
		declare @custid int
		SELECT @custid=Customer.CustomerID FROM Customer
		WHERE email=@email
		delete from Add_To_Cart where Product_Id=@id and Customer_Id=@custid
end
--25-search by category
create procedure Search_by_categ @categ varchar(50),@output int output
as
begin
	if exists(select * from product join category on Category.CategoryID=Product.CategoryID where [Name]=@categ)
	begin
		select * from product join category on Category.CategoryID=Product.CategoryID where [Name]=@categ
		set @output=1
	end
	else
	begin
		set @output=0
	end
end

/*26-add customer feedback*/
create procedure add_customer_feedback @email varchar(50),@feedback varchar(500),@inv_num int
as
begin
	declare @custid int
	SELECT @custid=Customer.CustomerID FROM Customer
	WHERE email=@email
	insert into Feedback values
	(@custid,@inv_num,@feedback,CURRENT_TIMESTAMP)
end
--not added yet
--27-add invoice entry
create procedure add_invoice_entry @email varchar(50),@Payment_Method_id int,@output int output
as
begin
	declare @custid int
	SELECT @custid=Customer.CustomerID FROM Customer
	WHERE email=@email
	if exists(select * from Add_To_Cart where Customer_Id=@custid)
	begin
		insert into invoice(CustomerID,Payment_Method_id)
		values
		(@custid,@Payment_Method_id)
		insert into InvoiceItems(InvoiceNo,ProductID,Quantity)
		select SCOPE_IDENTITY(), Product_Id,Quantity
		from Add_To_Cart
		where Customer_Id=@custid
		declare @quant int,@prod_id int
		select @quant=Quantity,@prod_id=Product_Id from Add_To_Cart where Customer_Id=@custid
		update Product
		set Stock=Stock-@quant where ProductID=@prod_id
		delete from Add_To_Cart
		where Customer_Id=@custid
		set @output=1
	end
	else
	begin
		set @output=0
	end
end

--28-current logged in user
Create Procedure Current_LoggedIN_User @email varchar(50)
as
begin
	SELECT * FROM Customer
	WHERE email=@email
end
--29--Get all invoices records to show
create procedure get_all_invoice_information 
as
begin
	select invoiceNo,Email 
	from invoice join Customer on Invoice.CustomerID=Customer.CustomerID
end

--30--Get Payment Method from invoice
create procedure get_payment_from_Invoice @invoiceno int
as begin
select Name
from Invoice join Payment_Methods on Invoice.Payment_Method_id=Payment_Methods.P_id
where Invoice.InvoiceNo=@invoiceno
end


------------------SELLER PROCEDURES------------------
drop procedure  Seller_SignUp

/*1. seller Sign_Up*/
create Procedure Seller_SignUp 
@name varchar(50),@email varchar(25), @password varchar(20) ,@gender varchar(8),@output int output
as 
begin
	if(@email is NULL or @password is NULL or  @name is NULL or @gender is NULL)
	begin
		print 'Values cannot be NULL'
		set @output=0
	end
	else
	if exists(select *
		from Seller
		where Seller.Email=@email
		)
	begin
		print'Duplicate Email Error'
		set @output=0
	end
	else if exists(select *
		from Blocked_Accounts
		where Blocked_Accounts.Email=@email
		)
	begin
		print'Duplicate Email Error'
		set @output=2
	end
	else
	begin
	--Sign-UP only takes name , email password and gender rest of profile is updated in Update Profile
		insert into Seller values(@Password,@Name,@gender,'No','No','00000000000','1/1/2001',@email,CURRENT_TIMESTAMP)
		set @output=1
	end
end

declare @my_output_param int
exec Seller_SignUp @name='Hassan Mahmood', @email='hassan@gmail.com',@password='abc123',@gender='Male',  @output = @my_output_param OUTPUT
select @my_output_param

drop procedure Seller_Login

/*2. Seller Login_In*/
create procedure Seller_Login
@email varchar(25),@password varchar(20),@output int output
as
begin

 if exists(select *
		from Blocked_Accounts
		where Blocked_Accounts.Email=@email
		)
		begin
		print'Duplicate Email Error'
		set @output=2
	end
	else if exists(select * from seller where email=@email and [password]=@password)
	begin
		print('Login Successful')
		set @output=1
	end
	
	
	else
	begin
		print('Email or Password is Incorrect')
		set @output=0
	end
end

declare @my_output_param int
exec Seller_Login
@email='hassan@gamil.com',@password='abc123',@output = @my_output_param OUTPUT
select @my_output_param

/*3. Seller Update Profile*/
create procedure Update_Seller_Profile
@email varchar(25),@newemail varchar(25),@newpassword varchar(20),@name varchar(50),@gender varchar(8),@address varchar(500),@city varchar(20),@contactno char(11),@DOB date,
@output int output
as
begin
	if exists(select * from Seller where email=@email)
	begin
		update Seller
		set email=@newemail,[password]=@newpassword,[Name]=@name,Gender=@gender,[Address]=@address,city=@city,contactno=@contactno,DOB=@DOB
		where email=@email
		set @output=1
	end
	else
	begin
	set @output=0
		print('Email or Password is Incorrect')
	end
end

/*4. Delete a Seller*/
create procedure Delete_Seller
@Seller_ID int,@output int output
as
begin
	if exists(select *
		from Seller
		where SellerID=@Seller_ID)
	begin
		set @output=1
		delete from Seller
		where SellerID=@Seller_ID
	end
	else
	begin
		set @output=0
	end
end

--5 Get Seller Profile
create Procedure Get_Seller_profile
@email varchar(25)
as
begin
select * from Seller where Seller.email=@email
end

drop Procedure Add_Product

/*6. Add New Product*/
create procedure Add_Product
@p_name varchar(100),@unit_price int,@stock int,@city varchar(20),@Manufacturer_Name varchar(100),@model char(4),@released_date date,@category_id int,@seller_id int,@output int output
as
begin
	if @p_name is null or @unit_price is null or @stock is null or @city is null or @Manufacturer_Name is null or @model is null or @released_date is null or @category_id is null or @seller_id is null
	begin
		set @output=0;
	end
	else
	begin
		insert into Product
		values
		(@p_name,@unit_price,@stock,@city,CURRENT_TIMESTAMP,@Manufacturer_Name,@model,@released_date,@category_id,@seller_id)
		set @output=1;
	end
end

/*7. Update Product*/
create procedure Update_Product
@ProductID int,@p_name varchar(100),@unit_price int,@stock int,@city varchar(20),@Manufacturer_Name varchar(100),@model char(4),@category_id int,@output int output
as
begin
	if exists(select * from Product where ProductID=@ProductID)
	begin
		update Product
		set Product_Name=@p_name,Unit_price=@unit_price,Stock=@stock,City=@city,Manufacturer=@Manufacturer_Name,Model=@model,CategoryID=@category_id
		where ProductID=@ProductID
		set @output=1;
	end
	else
	begin
		set @output=0;
		print('Product ID does not exist')
	end
end

/*8. Get Product information*/
create procedure Get_Product_Information
@p_ID int,@output int output
as
begin
	if exists(select *
		from Product
		where ProductID=@p_ID)
	begin
		set @output=1
		select *
		from Product
		where ProductID=@p_ID
	end
	else
	begin
		set @output=0
	end
end


/*9. Delete a Product*/
create procedure Delete_Product
@p_ID int,@output int output
as
begin
	if exists(select *
		from Product
		where ProductID=@p_ID)
	begin
		set @output=1
		delete from Product
		where ProductID=@p_ID
	end
	else
	begin
		set @output=0
	end
end

/*10. Show all products of a Seller*/
create procedure Show_Seller_Products
@Sellerid int
as
begin
	select* from product
	where SellerID=@Sellerid
end



/*11. See Seller_Sold_Products details */
create Procedure See_Seller_Orders
@Sellerid int
as
begin
	select invoice.InvoiceNo, invoice.CustomerID, invoice.Date_Time, invoice.Payment_Method_id, invoiceitems.ProductID, Product.Product_Name, invoiceitems.Quantity
	from (Product join InvoiceItems on Product.ProductID = InvoiceItems.ProductID) join invoice on invoice.InvoiceNo = invoiceitems.InvoiceNo
	where Product.SellerID=@Sellerid
end

/*12. Current LoggedIN Seller*/
create Procedure GetSellerID @email varchar(25),@output int output
as
begin
	set @output = (SELECT SellerID FROM Seller
	WHERE email=@email)
end

/*13. Show All Notifications*/
create procedure show_all_Notifications
as
begin
	select *
	from [Notification]
end
