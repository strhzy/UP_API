IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'UP')
BEGIN
	CREATE DATABASE UP;
	

	USE UP;
	

	-- Создание таблиц
	CREATE TABLE Roles (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    RoleName NVARCHAR(50) NOT NULL
	);
	
	CREATE TABLE EmployeeAccounts (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    Login NVARCHAR(50) NOT NULL UNIQUE,
	    Password NVARCHAR(64) NOT NULL,
	    Telephone NVARCHAR(11) NOT NULL,
	    Email NVARCHAR(50) NOT NULL,
	    RoleId BIGINT,
	    FOREIGN KEY (RoleId) REFERENCES Roles(ID)
	);
	
	CREATE TABLE Position (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    PositionName NVARCHAR(50) NOT NULL
	);
	
	CREATE TABLE Qualification (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    QualificationName NVARCHAR(50) NOT NULL
	);
	
	CREATE TABLE ServiceStations (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    Address NVARCHAR(30) NOT NULL,
	    TelephoneNumber NVARCHAR(11) NOT NULL,
	    Email NVARCHAR(50) NOT NULL,
	    QuantityWorkPlaces BIGINT NOT NULL
	);
	
	CREATE TABLE Employee (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    Surname NVARCHAR(50) NOT NULL,
	    EmployeeName NVARCHAR(50) NOT NULL,
	    Patronymic NVARCHAR(50),
	    PositionId BIGINT,
	    QualificationId BIGINT,
	    ServiceStationId BIGINT,
	    EmployeeAccountID BIGINT,
	    FOREIGN KEY (PositionId) REFERENCES Position(ID),
	    FOREIGN KEY (QualificationId) REFERENCES Qualification(ID),
	    FOREIGN KEY (ServiceStationId) REFERENCES ServiceStations(ID),
	    FOREIGN KEY (EmployeeAccountID) REFERENCES EmployeeAccounts(ID)
	);
	
	CREATE TABLE Clients (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    Surname NVARCHAR(50) NOT NULL,
	    ClientName NVARCHAR(50) NOT NULL,
	    Patronymic NVARCHAR(50),
	    TelephoneNumber NVARCHAR(11) NOT NULL,
	    CarBrand NVARCHAR(50) NOT NULL,
	    CarModel NVARCHAR(50) NOT NULL,
	    GovNumber NVARCHAR(20) NOT NULL,
	    LastVisitDate DATETIME
	);
	
	CREATE TABLE Orders (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    ClientId BIGINT,
	    DateReference DATETIME NOT NULL,
	    RepairDate DATETIME,
	    StatusName NVARCHAR(20),
	    FOREIGN KEY (ClientId) REFERENCES Clients(ID)
	);
	
	CREATE TABLE Operations (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    OperationName NVARCHAR(50) NOT NULL,
	    Price DECIMAL(10, 2) NOT NULL
	);
	
	CREATE TABLE SpareParts (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    PartName NVARCHAR(50) NOT NULL,
	    Price DECIMAL(10, 2) NOT NULL
	);
	
	CREATE TABLE OrderSpareParts (
	    ID BIGINT PRIMARY KEY IDENTITY(1,1),
	    SparePartId BIGINT,
	    OrderId BIGINT,
	    FOREIGN KEY (SparePartId) REFERENCES SpareParts(ID),
	    FOREIGN KEY (OrderId) REFERENCES Orders(ID)
	);
	
	-- Вставка данных в таблицу Roles
	INSERT INTO Roles (RoleName) VALUES
	(N'Администратор'),
	(N'Мастер-консультант'),
	(N'Бухгалтер'),
	(N'Менеджер СТО'),
	(N'Автослесарь');
	
	-- Вставка данных в таблицу Position
	INSERT INTO Position (PositionName) VALUES
	(N'Администратор'),
	(N'Мастер-консультант'),
	(N'Бухгалтер'),
	(N'Менеджер СТО'),
	(N'Автослесарь');
	
	-- Вставка данных в таблицу Qualification
	INSERT INTO Qualification (QualificationName) VALUES
	(N'Мастер'),
	(N'Специалист'),
	(N'Новичок');
	
	-- Вставка данных в таблицу ServiceStations
	INSERT INTO ServiceStations (Address, TelephoneNumber, Email, QuantityWorkPlaces) VALUES
	(N'ул. Ленина, 10', '79101234567', 'station1@example.com', 5),
	(N'ул. Пушкина, 25', '79107654321', 'station2@example.com', 3),
	(N'ул. Гагарина, 15', '79109876543', 'station3@example.com', 4),
	(N'ул. Мира, 30', '79101112233', 'station4@example.com', 6),
	(N'ул. Советская, 5', '79104445566', 'station5@example.com', 2);
	
	-- Вставка данных в таблицу EmployeeAccounts
	-- Пароль дублируется в комментарии
	INSERT INTO EmployeeAccounts (Login, Password, Telephone, Email, RoleId) VALUES
	(N'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', '79100000001', 'admin@example.com', 1), -- Пароль: admin123
	(N'master', 'e7bc2f973afb8dfaf00fadfb19596741108be08ab4a107c6a799c429b684c64a', '79100000002', 'master@example.com', 2), -- Пароль: master123
	(N'accountant', '4d393ec34c3c6a875b95e66df5e6d6fc09efc33d66f12e3e98afca347d6b7638', '79100000003', 'accountant@example.com', 3), -- Пароль: accountant123
	(N'manager', '866485796cfa8d7c0cf7111640205b83076433547577511d81f8030ae99ecea5', '79100000004', 'manager@example.com', 4), -- Пароль: manager123
	(N'mechanic', 'ec23dea4ae16e63726aebb67afa6ea8d3f972533f43c76eae6c13dc228c8c316', '79100000005', 'mechanic@example.com', 5); -- Пароль: mechanic123
	
	-- Вставка данных в таблицу Employee
	INSERT INTO Employee (Surname, EmployeeName, Patronymic, PositionId, QualificationId, ServiceStationId, EmployeeAccountID) VALUES
	(N'Иванов', N'Иван', N'Иванович', 1, 1, 1, 1),
	(N'Петров', N'Петр', N'Петрович', 2, 2, 2, 2),
	(N'Сидоров', N'Сидор', N'Сидорович', 3, 3, 3, 3),
	(N'Кузнецов', N'Алексей', N'Николаевич', 4, 1, 4, 4),
	(N'Смирнов', N'Дмитрий', N'Александрович', 5, 2, 5, 5);
	
	-- Вставка данных в таблицу Clients
	INSERT INTO Clients (Surname, ClientName, Patronymic, TelephoneNumber, CarBrand, CarModel, GovNumber, LastVisitDate) VALUES
	(N'Васильев', N'Василий', N'Васильевич', '79101111111', N'Toyota', N'Camry', N'А123ВС77', '2023-10-01'),
	(N'Николаев', N'Николай', N'Николаевич', '79102222222', N'Honda', N'Civic', N'В456ОР78', '2023-09-15'),
	(N'Алексеев', N'Алексей', N'Алексеевич', '79103333333', N'Ford', N'Focus', N'С789ТУ79', '2023-08-20'),
	(N'Дмитриев', N'Дмитрий', N'Дмитриевич', '79104444444', N'BMW', N'X5', N'Е012КХ80', '2023-07-10'),
	(N'Сергеев', N'Сергей', N'Сергеевич', '79105555555', N'Audi', N'A4', N'М345НВ81', '2023-06-05');
	
	-- Вставка данных в таблицу Orders
	INSERT INTO Orders (ClientId, DateReference, RepairDate, StatusName) VALUES
	(1, '2023-10-01', '2023-10-05', N'Завершен'),
	(2, '2023-09-15', '2023-09-20', N'Завершен'),
	(3, '2023-08-20', '2023-08-25', N'Завершен'),
	(4, '2023-07-10', '2023-07-15', N'Завершен'),
	(5, '2023-06-05', '2023-06-10', N'Завершен');
	
	-- Вставка данных в таблицу Operations
	INSERT INTO Operations (OperationName, Price) VALUES
	(N'Замена масла', 2000.00),
	(N'Замена тормозных колодок', 5000.00),
	(N'Диагностика двигателя', 3000.00),
	(N'Замена аккумулятора', 4000.00),
	(N'Шиномонтаж', 1500.00);
	
	-- Вставка данных в таблицу SpareParts
	INSERT INTO SpareParts (PartName, Price) VALUES
	(N'Масло моторное', 3000.00),
	(N'Тормозные колодки', 7000.00),
	(N'Аккумулятор', 10000.00),
	(N'Фильтр воздушный', 1500.00),
	(N'Шины', 12000.00);
	
	-- Вставка данных в таблицу OrderSpareParts
	INSERT INTO OrderSpareParts (SparePartId, OrderId) VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4),
	(5, 5);
	
END;



