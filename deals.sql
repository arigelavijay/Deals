
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/03/2016 13:16:30
-- Generated from EDMX file: D:\Vijay Samples\users\users\Models\dbEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[deals2].[fk_address_stateId]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[addresses] DROP CONSTRAINT [fk_address_stateId];
GO
IF OBJECT_ID(N'[deals2].[fk_address_userid]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[addresses] DROP CONSTRAINT [fk_address_userid];
GO
IF OBJECT_ID(N'[deals2].[fk_categoryconfig_category]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[categoryconfig] DROP CONSTRAINT [fk_categoryconfig_category];
GO
IF OBJECT_ID(N'[deals2].[fk_deal_banners]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[deal] DROP CONSTRAINT [fk_deal_banners];
GO
IF OBJECT_ID(N'[deals2].[fk_deal_category]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[deal] DROP CONSTRAINT [fk_deal_category];
GO
IF OBJECT_ID(N'[deals2].[fk_deal_locations]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[deal] DROP CONSTRAINT [fk_deal_locations];
GO
IF OBJECT_ID(N'[deals2].[fk_deal_vendors]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[deal] DROP CONSTRAINT [fk_deal_vendors];
GO
IF OBJECT_ID(N'[deals2].[FK_fkey_banner_id]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[usercarts] DROP CONSTRAINT [FK_fkey_banner_id];
GO
IF OBJECT_ID(N'[deals2].[FK_fkey_deal_id]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[usercarts] DROP CONSTRAINT [FK_fkey_deal_id];
GO
IF OBJECT_ID(N'[deals2].[FK_fkey_location_id]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[usercarts] DROP CONSTRAINT [FK_fkey_location_id];
GO
IF OBJECT_ID(N'[deals2].[FK_fkey_vendor_id]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[usercarts] DROP CONSTRAINT [FK_fkey_vendor_id];
GO
IF OBJECT_ID(N'[deals2].[fk_orderItems_orders]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[orderitems] DROP CONSTRAINT [fk_orderItems_orders];
GO
IF OBJECT_ID(N'[deals2].[fk_orders_shipping]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[orders] DROP CONSTRAINT [fk_orders_shipping];
GO
IF OBJECT_ID(N'[deals2].[fk_orders_users]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[orders] DROP CONSTRAINT [fk_orders_users];
GO
IF OBJECT_ID(N'[deals2].[fk_shippingadd_userid]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[shippingaddress] DROP CONSTRAINT [fk_shippingadd_userid];
GO
IF OBJECT_ID(N'[deals2].[fk_userHistory_deal]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userhistory] DROP CONSTRAINT [fk_userHistory_deal];
GO
IF OBJECT_ID(N'[deals2].[fk_userHistory_users]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userhistory] DROP CONSTRAINT [fk_userHistory_users];
GO
IF OBJECT_ID(N'[deals2].[fk_userRating_deals]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userrating] DROP CONSTRAINT [fk_userRating_deals];
GO
IF OBJECT_ID(N'[deals2].[fk_userRating_users]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userrating] DROP CONSTRAINT [fk_userRating_users];
GO
IF OBJECT_ID(N'[deals2].[fk_userReviews_deals]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userreviews] DROP CONSTRAINT [fk_userReviews_deals];
GO
IF OBJECT_ID(N'[deals2].[fk_userReviews_users]', 'F') IS NOT NULL
    ALTER TABLE [deals2].[userreviews] DROP CONSTRAINT [fk_userReviews_users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[deals2].[addresses]', 'U') IS NOT NULL
    DROP TABLE [deals2].[addresses];
GO
IF OBJECT_ID(N'[deals2].[authentication_token]', 'U') IS NOT NULL
    DROP TABLE [deals2].[authentication_token];
GO
IF OBJECT_ID(N'[deals2].[banners]', 'U') IS NOT NULL
    DROP TABLE [deals2].[banners];
GO
IF OBJECT_ID(N'[deals2].[category]', 'U') IS NOT NULL
    DROP TABLE [deals2].[category];
GO
IF OBJECT_ID(N'[deals2].[categoryconfig]', 'U') IS NOT NULL
    DROP TABLE [deals2].[categoryconfig];
GO
IF OBJECT_ID(N'[deals2].[deal]', 'U') IS NOT NULL
    DROP TABLE [deals2].[deal];
GO
IF OBJECT_ID(N'[deals2].[guestid]', 'U') IS NOT NULL
    DROP TABLE [deals2].[guestid];
GO
IF OBJECT_ID(N'[deals2].[locations]', 'U') IS NOT NULL
    DROP TABLE [deals2].[locations];
GO
IF OBJECT_ID(N'[deals2].[orderitems]', 'U') IS NOT NULL
    DROP TABLE [deals2].[orderitems];
GO
IF OBJECT_ID(N'[deals2].[orders]', 'U') IS NOT NULL
    DROP TABLE [deals2].[orders];
GO
IF OBJECT_ID(N'[deals2].[parentlocation]', 'U') IS NOT NULL
    DROP TABLE [deals2].[parentlocation];
GO
IF OBJECT_ID(N'[deals2].[promocodes]', 'U') IS NOT NULL
    DROP TABLE [deals2].[promocodes];
GO
IF OBJECT_ID(N'[deals2].[shippingaddress]', 'U') IS NOT NULL
    DROP TABLE [deals2].[shippingaddress];
GO
IF OBJECT_ID(N'[deals2].[states]', 'U') IS NOT NULL
    DROP TABLE [deals2].[states];
GO
IF OBJECT_ID(N'[deals2].[usercarts]', 'U') IS NOT NULL
    DROP TABLE [deals2].[usercarts];
GO
IF OBJECT_ID(N'[deals2].[userhistory]', 'U') IS NOT NULL
    DROP TABLE [deals2].[userhistory];
GO
IF OBJECT_ID(N'[deals2].[userrating]', 'U') IS NOT NULL
    DROP TABLE [deals2].[userrating];
GO
IF OBJECT_ID(N'[deals2].[userreviews]', 'U') IS NOT NULL
    DROP TABLE [deals2].[userreviews];
GO
IF OBJECT_ID(N'[deals2].[users]', 'U') IS NOT NULL
    DROP TABLE [deals2].[users];
GO
IF OBJECT_ID(N'[deals2].[vendors]', 'U') IS NOT NULL
    DROP TABLE [deals2].[vendors];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'addresses'
CREATE TABLE [dbo].[addresses] (
    [id] int  NOT NULL,
    [userId] int  NOT NULL,
    [name] varchar(45)  NOT NULL,
    [pincode] int  NOT NULL,
    [address1] varchar(250)  NOT NULL,
    [landmark] varchar(100)  NOT NULL,
    [city] varchar(45)  NOT NULL,
    [state] int  NOT NULL,
    [phone] varchar(10)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isActive] bool  NOT NULL,
    [isDefault] bool  NOT NULL
);
GO

-- Creating table 'authentication_token'
CREATE TABLE [dbo].[authentication_token] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userid] int  NOT NULL,
    [token] varchar(45)  NOT NULL,
    [created] datetime  NOT NULL,
    [expiretime] datetime  NOT NULL
);
GO

-- Creating table 'banners'
CREATE TABLE [dbo].[banners] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [description] varchar(200)  NULL,
    [createdDate] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'categories'
CREATE TABLE [dbo].[categories] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(45)  NULL,
    [description] varchar(300)  NULL,
    [createdOn] datetime  NULL,
    [isActive] bool  NULL
);
GO

-- Creating table 'deals'
CREATE TABLE [dbo].[deals] (
    [dealId] int IDENTITY(1,1) NOT NULL,
    [vendorId] int  NOT NULL,
    [locationId] int  NOT NULL,
    [bannerId] int  NOT NULL,
    [name] varchar(45)  NOT NULL,
    [categoryId] int  NULL,
    [description] varchar(200)  NOT NULL,
    [startsOn] datetime  NOT NULL,
    [endsOn] datetime  NOT NULL,
    [count] int  NOT NULL,
    [image] varchar(45)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isActive] bool  NOT NULL,
    [sold] int  NULL,
    [unitPrice] float  NOT NULL,
    [discount] float  NOT NULL,
    [sellingPrice] float  NOT NULL,
    [hasShipping] bool  NULL
);
GO

-- Creating table 'guestids'
CREATE TABLE [dbo].[guestids] (
    [id] int IDENTITY(1,1) NOT NULL,
    [guest_id] int  NOT NULL
);
GO

-- Creating table 'locations'
CREATE TABLE [dbo].[locations] (
    [locationId] int IDENTITY(1,1) NOT NULL,
    [locationName] varchar(75)  NOT NULL,
    [createddate] datetime  NOT NULL,
    [isActive] bool  NOT NULL,
    [parentId] int  NOT NULL
);
GO

-- Creating table 'orderitems'
CREATE TABLE [dbo].[orderitems] (
    [id] int IDENTITY(1,1) NOT NULL,
    [orderId] int  NOT NULL,
    [dealId] int  NOT NULL,
    [quantity] int  NOT NULL,
    [unitPrice] float  NOT NULL,
    [discount] float  NOT NULL,
    [sellingPrice] float  NOT NULL,
    [subTotal] float  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'orders'
CREATE TABLE [dbo].[orders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [orderId] varchar(45)  NOT NULL,
    [userId] int  NULL,
    [orderStatus] int  NOT NULL,
    [subTotal] float  NOT NULL,
    [promoCode] varchar(45)  NULL,
    [promoDiscount] float  NULL,
    [grandTotal] float  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isActive] bool  NOT NULL,
    [shippingId] int  NOT NULL
);
GO

-- Creating table 'promocodes'
CREATE TABLE [dbo].[promocodes] (
    [promoId] int IDENTITY(1,1) NOT NULL,
    [promoCode1] varchar(10)  NOT NULL,
    [promoValue] int  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [expiresOn] datetime  NOT NULL,
    [isPercentage] bool  NOT NULL,
    [isActive] bool  NOT NULL,
    [categoryId] int  NOT NULL
);
GO

-- Creating table 'shippingaddresses'
CREATE TABLE [dbo].[shippingaddresses] (
    [id] int  NOT NULL,
    [userId] int  NOT NULL,
    [name] varchar(45)  NOT NULL,
    [pincode] int  NOT NULL,
    [address] varchar(250)  NOT NULL,
    [landmark] varchar(100)  NOT NULL,
    [city] varchar(45)  NOT NULL,
    [state] int  NOT NULL,
    [phone] varchar(15)  NOT NULL,
    [createdOn] datetime  NOT NULL
);
GO

-- Creating table 'states'
CREATE TABLE [dbo].[states] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(45)  NOT NULL,
    [code] varchar(45)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isactive] bool  NOT NULL
);
GO

-- Creating table 'usercarts'
CREATE TABLE [dbo].[usercarts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [locationId] int  NOT NULL,
    [bannerId] int  NOT NULL,
    [vendorId] int  NOT NULL,
    [quantity] int  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [dateModified] datetime  NOT NULL,
    [isActive] bool  NOT NULL,
    [dealId] int  NOT NULL,
    [isguest] bool  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [firstName] varchar(45)  NOT NULL,
    [lastName] varchar(45)  NOT NULL,
    [userName] varchar(45)  NOT NULL,
    [email] varchar(45)  NOT NULL,
    [password] varchar(45)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'vendors'
CREATE TABLE [dbo].[vendors] (
    [id] int IDENTITY(1,1) NOT NULL,
    [firstname] varchar(45)  NOT NULL,
    [lastname] varchar(45)  NULL,
    [vendorname] varchar(45)  NOT NULL,
    [email] varchar(45)  NOT NULL,
    [password] varchar(45)  NOT NULL,
    [createddate] datetime  NOT NULL,
    [isactive] bool  NOT NULL
);
GO

-- Creating table 'categoryconfigs'
CREATE TABLE [dbo].[categoryconfigs] (
    [id] int IDENTITY(1,1) NOT NULL,
    [categoryId] int  NOT NULL,
    [sequence] int  NOT NULL,
    [displayItems] int  NOT NULL,
    [isMore] bool  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'parentlocations'
CREATE TABLE [dbo].[parentlocations] (
    [id] int IDENTITY(1,1) NOT NULL,
    [parentname] varchar(100)  NOT NULL,
    [createdDate] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'userhistories'
CREATE TABLE [dbo].[userhistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [dealId] int  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'userratings'
CREATE TABLE [dbo].[userratings] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [dealId] int  NOT NULL,
    [rating] int  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- Creating table 'userreviews'
CREATE TABLE [dbo].[userreviews] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [dealId] int  NOT NULL,
    [title] varchar(50)  NOT NULL,
    [review] varchar(300)  NOT NULL,
    [rating] int  NOT NULL,
    [name] varchar(50)  NOT NULL,
    [dateCreated] datetime  NOT NULL,
    [isActive] bool  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'addresses'
ALTER TABLE [dbo].[addresses]
ADD CONSTRAINT [PK_addresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'authentication_token'
ALTER TABLE [dbo].[authentication_token]
ADD CONSTRAINT [PK_authentication_token]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'banners'
ALTER TABLE [dbo].[banners]
ADD CONSTRAINT [PK_banners]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [PK_categories]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [dealId] in table 'deals'
ALTER TABLE [dbo].[deals]
ADD CONSTRAINT [PK_deals]
    PRIMARY KEY CLUSTERED ([dealId] ASC);
GO

-- Creating primary key on [id] in table 'guestids'
ALTER TABLE [dbo].[guestids]
ADD CONSTRAINT [PK_guestids]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [locationId] in table 'locations'
ALTER TABLE [dbo].[locations]
ADD CONSTRAINT [PK_locations]
    PRIMARY KEY CLUSTERED ([locationId] ASC);
GO

-- Creating primary key on [id] in table 'orderitems'
ALTER TABLE [dbo].[orderitems]
ADD CONSTRAINT [PK_orderitems]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [PK_orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [promoId] in table 'promocodes'
ALTER TABLE [dbo].[promocodes]
ADD CONSTRAINT [PK_promocodes]
    PRIMARY KEY CLUSTERED ([promoId] ASC);
GO

-- Creating primary key on [id] in table 'shippingaddresses'
ALTER TABLE [dbo].[shippingaddresses]
ADD CONSTRAINT [PK_shippingaddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'states'
ALTER TABLE [dbo].[states]
ADD CONSTRAINT [PK_states]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'usercarts'
ALTER TABLE [dbo].[usercarts]
ADD CONSTRAINT [PK_usercarts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'vendors'
ALTER TABLE [dbo].[vendors]
ADD CONSTRAINT [PK_vendors]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'categoryconfigs'
ALTER TABLE [dbo].[categoryconfigs]
ADD CONSTRAINT [PK_categoryconfigs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'parentlocations'
ALTER TABLE [dbo].[parentlocations]
ADD CONSTRAINT [PK_parentlocations]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'userhistories'
ALTER TABLE [dbo].[userhistories]
ADD CONSTRAINT [PK_userhistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'userratings'
ALTER TABLE [dbo].[userratings]
ADD CONSTRAINT [PK_userratings]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'userreviews'
ALTER TABLE [dbo].[userreviews]
ADD CONSTRAINT [PK_userreviews]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [state] in table 'addresses'
ALTER TABLE [dbo].[addresses]
ADD CONSTRAINT [fk_address_stateId]
    FOREIGN KEY ([state])
    REFERENCES [dbo].[states]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_address_stateId'
CREATE INDEX [IX_fk_address_stateId]
ON [dbo].[addresses]
    ([state]);
GO

-- Creating foreign key on [userId] in table 'addresses'
ALTER TABLE [dbo].[addresses]
ADD CONSTRAINT [fk_address_userid]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_address_userid'
CREATE INDEX [IX_fk_address_userid]
ON [dbo].[addresses]
    ([userId]);
GO

-- Creating foreign key on [bannerId] in table 'deals'
ALTER TABLE [dbo].[deals]
ADD CONSTRAINT [fk_deal_banners]
    FOREIGN KEY ([bannerId])
    REFERENCES [dbo].[banners]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_deal_banners'
CREATE INDEX [IX_fk_deal_banners]
ON [dbo].[deals]
    ([bannerId]);
GO

-- Creating foreign key on [bannerId] in table 'usercarts'
ALTER TABLE [dbo].[usercarts]
ADD CONSTRAINT [FK_fkey_banner_id]
    FOREIGN KEY ([bannerId])
    REFERENCES [dbo].[banners]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_fkey_banner_id'
CREATE INDEX [IX_FK_fkey_banner_id]
ON [dbo].[usercarts]
    ([bannerId]);
GO

-- Creating foreign key on [categoryId] in table 'deals'
ALTER TABLE [dbo].[deals]
ADD CONSTRAINT [fk_deal_category]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[categories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_deal_category'
CREATE INDEX [IX_fk_deal_category]
ON [dbo].[deals]
    ([categoryId]);
GO

-- Creating foreign key on [locationId] in table 'deals'
ALTER TABLE [dbo].[deals]
ADD CONSTRAINT [fk_deal_locations]
    FOREIGN KEY ([locationId])
    REFERENCES [dbo].[locations]
        ([locationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_deal_locations'
CREATE INDEX [IX_fk_deal_locations]
ON [dbo].[deals]
    ([locationId]);
GO

-- Creating foreign key on [vendorId] in table 'deals'
ALTER TABLE [dbo].[deals]
ADD CONSTRAINT [fk_deal_vendors]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_deal_vendors'
CREATE INDEX [IX_fk_deal_vendors]
ON [dbo].[deals]
    ([vendorId]);
GO

-- Creating foreign key on [dealId] in table 'usercarts'
ALTER TABLE [dbo].[usercarts]
ADD CONSTRAINT [FK_fkey_deal_id]
    FOREIGN KEY ([dealId])
    REFERENCES [dbo].[deals]
        ([dealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_fkey_deal_id'
CREATE INDEX [IX_FK_fkey_deal_id]
ON [dbo].[usercarts]
    ([dealId]);
GO

-- Creating foreign key on [locationId] in table 'usercarts'
ALTER TABLE [dbo].[usercarts]
ADD CONSTRAINT [FK_fkey_location_id]
    FOREIGN KEY ([locationId])
    REFERENCES [dbo].[locations]
        ([locationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_fkey_location_id'
CREATE INDEX [IX_FK_fkey_location_id]
ON [dbo].[usercarts]
    ([locationId]);
GO

-- Creating foreign key on [orderId] in table 'orderitems'
ALTER TABLE [dbo].[orderitems]
ADD CONSTRAINT [fk_orderItems_orders]
    FOREIGN KEY ([orderId])
    REFERENCES [dbo].[orders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_orderItems_orders'
CREATE INDEX [IX_fk_orderItems_orders]
ON [dbo].[orderitems]
    ([orderId]);
GO

-- Creating foreign key on [userId] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [fk_orders_users]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_orders_users'
CREATE INDEX [IX_fk_orders_users]
ON [dbo].[orders]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'shippingaddresses'
ALTER TABLE [dbo].[shippingaddresses]
ADD CONSTRAINT [fk_shippingadd_userid]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_shippingadd_userid'
CREATE INDEX [IX_fk_shippingadd_userid]
ON [dbo].[shippingaddresses]
    ([userId]);
GO

-- Creating foreign key on [vendorId] in table 'usercarts'
ALTER TABLE [dbo].[usercarts]
ADD CONSTRAINT [FK_fkey_vendor_id]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_fkey_vendor_id'
CREATE INDEX [IX_FK_fkey_vendor_id]
ON [dbo].[usercarts]
    ([vendorId]);
GO

-- Creating foreign key on [shippingId] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [fk_orders_shipping]
    FOREIGN KEY ([shippingId])
    REFERENCES [dbo].[shippingaddresses]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_orders_shipping'
CREATE INDEX [IX_fk_orders_shipping]
ON [dbo].[orders]
    ([shippingId]);
GO

-- Creating foreign key on [categoryId] in table 'categoryconfigs'
ALTER TABLE [dbo].[categoryconfigs]
ADD CONSTRAINT [fk_categoryconfig_category]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[categories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_categoryconfig_category'
CREATE INDEX [IX_fk_categoryconfig_category]
ON [dbo].[categoryconfigs]
    ([categoryId]);
GO

-- Creating foreign key on [dealId] in table 'userhistories'
ALTER TABLE [dbo].[userhistories]
ADD CONSTRAINT [fk_userHistory_deal]
    FOREIGN KEY ([dealId])
    REFERENCES [dbo].[deals]
        ([dealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userHistory_deal'
CREATE INDEX [IX_fk_userHistory_deal]
ON [dbo].[userhistories]
    ([dealId]);
GO

-- Creating foreign key on [dealId] in table 'userratings'
ALTER TABLE [dbo].[userratings]
ADD CONSTRAINT [fk_userRating_deals]
    FOREIGN KEY ([dealId])
    REFERENCES [dbo].[deals]
        ([dealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userRating_deals'
CREATE INDEX [IX_fk_userRating_deals]
ON [dbo].[userratings]
    ([dealId]);
GO

-- Creating foreign key on [dealId] in table 'userreviews'
ALTER TABLE [dbo].[userreviews]
ADD CONSTRAINT [fk_userReviews_deals]
    FOREIGN KEY ([dealId])
    REFERENCES [dbo].[deals]
        ([dealId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userReviews_deals'
CREATE INDEX [IX_fk_userReviews_deals]
ON [dbo].[userreviews]
    ([dealId]);
GO

-- Creating foreign key on [userId] in table 'userhistories'
ALTER TABLE [dbo].[userhistories]
ADD CONSTRAINT [fk_userHistory_users]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userHistory_users'
CREATE INDEX [IX_fk_userHistory_users]
ON [dbo].[userhistories]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'userratings'
ALTER TABLE [dbo].[userratings]
ADD CONSTRAINT [fk_userRating_users]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userRating_users'
CREATE INDEX [IX_fk_userRating_users]
ON [dbo].[userratings]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'userreviews'
ALTER TABLE [dbo].[userreviews]
ADD CONSTRAINT [fk_userReviews_users]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_userReviews_users'
CREATE INDEX [IX_fk_userReviews_users]
ON [dbo].[userreviews]
    ([userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------