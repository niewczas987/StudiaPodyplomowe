GO
CREATE TABLE [dbo].[Employees]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Name] NVARCHAR(50),
[Surname] NVARCHAR(50),
[Age] BIGINT,
[Position] BIGINT,
FOREIGN KEY (Position) REFERENCES EmployeePosition(Id),
[Department] BIGINT,
FOREIGN KEY (Department) REFERENCES EmployeeDepartment(Id),
[Salary] DECIMAL,
[City] NVARCHAR(50),
[Address] NVARCHAR(50)
)

GO
CREATE TABLE [dbo].[EmployeePosition]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[PositionName] NVARCHAR(50)
)

GO
CREATE TABLE [dbo].[EmployeeDepartment]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[DepartmentName] NVARCHAR(50)
)

GO
CREATE TABLE [dbo].[EmployeeGrade]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[IDEmployee] BIGINT,
FOREIGN KEY (IDEmployee) REFERENCES Employees(Id),
[Value] BIGINT,
[TimeStamp] DATETIME
)

GO
CREATE TABLE [dbo].[EmployeeRise]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[IDEmployee] BIGINT,
FOREIGN KEY (IDEmployee) REFERENCES Employees(Id),
[Value] BIGINT,
[TimeStamp] DATETIME
)

GO
CREATE TABLE [dbo].[Item]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[ItemName] NVARCHAR(50),
[ItemPrice] DECIMAL,
[ItemCategory] BIGINT,
FOREIGN KEY (ItemCategory) REFERENCES ItemCategory(Id),
[ItemType] BIGINT,
FOREIGN KEY (ItemType) REFERENCES ItemType(Id)
)

GO
CREATE TABLE [dbo].[ItemCategory]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[CategoryName] NVARCHAR(50)
)

GO
CREATE TABLE [dbo].[ItemType]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[TypeName] NVARCHAR(50)
)

GO
CREATE TABLE [dbo].[Transactions]
(
[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[TimeStamp] DATETIME,
[IdItem] BIGINT,
FOREIGN KEY (IdItem) REFERENCES Item(Id),
[UnitPrice] DECIMAL,
[Amount] BIGINT,
[TotalPrice] DECIMAL
)