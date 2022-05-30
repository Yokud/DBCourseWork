create table Shops
(
	ID serial primary key,
	Name text not null,
	Description text not null
);

create table SaleReceipts
(
	ID serial primary key,
	FIO text not null,
	ShopID integer not null,
	DateOfPurchase date not null,
	foreign key (ShopID) references Shops(ID) on delete cascade
);

create table Products
(
	ID serial primary key,
	Name text not null,
	ProductType text not null
);

create table Availability
(
	ID serial primary key,
	ShopID integer not null,
	ProductID integer not null,
	foreign key (ShopID) references Shops(ID) on delete cascade,
	foreign key (ProductID) references Products(ID) on delete cascade
);

create table SaleReceiptPositions
(
	ID serial primary key,
	AvailabilityID integer not null,
	SaleReceiptID integer not null,
	foreign key (AvailabilityID) references Availability(ID) on delete cascade,
	foreign key (SaleReceiptID) references SaleReceipts(ID) on delete cascade
);

create table CostStory
(
	ID serial primary key,
	Year integer not null,
	Month integer not null,
	Cost integer not null,
	AvailabilityID integer not null,
	foreign key (AvailabilityID) references Availability(ID) on delete cascade
);

create table Costs
(
	AvailabilityID integer not null,
	Cost integer not null,
	foreign key (AvailabilityID) references Availability(ID) on delete cascade
);

insert into Costs
	select T.AvailabilityID, Cost
	from CostStory join (select AvailabilityID, max(make_date(Year, Month, 1)) as CostDate
						from CostStory
						group by AvailabilityID) as T on T.AvailabilityID = CostStory.AvailabilityID and T.CostDate = make_date(CostStory.Year, CostStory.Month,1);



create function get_products_by_shopid(shop_id integer)
returns table(ProdID integer, ProdName text, ProdType text, Cost float)
begin
	select APC.ProductID, APC.Name, APC.ProductType, APC.Cost
	from (((select Availability.ID as AvailID, Availability.ShopID, Availability.ProductID from Availability where Availability.ShopID = shop_id) as A 
			join Products on A.ProductID = Products.ID) as AP join Costs on AP.AvailID = Costs.AvailabilityID) as APC;
end

create function get_coststory_by_shopid_prodid(shop_id integer, prod_id integer)
returns table(Year integer, Month integer, Cost integer)
as $$
	select CostStory.Year, CostStory.Month, CostStory.Cost
	from CostStory
	where AvailabilityID = (select ID
						   from Availability
						   where ShopID = shop_id and ProductID = prod_id);
$$ language sql;

create function get_salereceipts_by_shopid(shop_id integer)
returns table(SR_ID integer, FIO text, DateOfPurchase date, SummaryCost integer)
as $$
	select S.S_ID, max(FIO), max(DateOfPurchase), sum(Cost) as SummaryCost
	from ((select ID as S_ID, FIO, ShopID, DateOfPurchase from SaleReceipts where ShopID = shop_id) as SR join SaleReceiptPositions on SaleReceiptPositions.SaleReceiptID = SR.S_ID) as S join Costs on S.AvailabilityID = Costs.AvailabilityID
	group by S.S_ID;
$$ language sql;

create function get_content_from_salereceipt(sr_id integer)
returns table(ProdID integer, Name text, ProductType text, Cost integer)
as $$
	select SAP.ProductID as ProdID, Name, ProductType, Cost::integer
	from ((SaleReceiptPositions join Availability on SaleReceiptPositions.AvailabilityID = Availability.ID) as SA join Products on Products.ID = SA.ProductID) as SAP join Costs on SAP.AvailabilityID = Costs.AvailabilityID
	where SaleReceiptID = sr_id;
$$ language sql;


copy Products from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\Products.csv' delimiter ';' CSV HEADER;
copy Shops from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\Shops.csv' delimiter ';' CSV HEADER;
copy SaleReceipts from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\SaleReceipts.csv' delimiter ';' CSV HEADER;
copy Availability from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\Availability.csv' delimiter ';' CSV HEADER;
copy SaleReceiptPositions from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\SaleReceiptPositions.csv' delimiter ';' CSV HEADER;
copy CostStory from 'D:\Repos\GitHub\DBCourseWork\src\db\tables_data\CostStory.csv' delimiter ';' CSV HEADER;
