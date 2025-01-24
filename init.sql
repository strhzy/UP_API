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
	(N'Менеджер'),
	(N'Механик');
	
	-- Вставка данных в таблицу EmployeeAccounts
	INSERT INTO EmployeeAccounts (Login, Password, Telephone, Email, RoleId) VALUES
    (N'admin', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'79991234567', N'admin@example.com', 1), -- Пароль: admin123
    (N'manager', N'1c9c424f40b3c486bfa9f5d934b2a5f1a5a4d3a7f8b9c0d1e2f3a4b5c6d7e8f', N'79997654321', N'manager@example.com', 2), -- Пароль: manager123
    (N'mechanic', N'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', N'79991112233', N'mechanic@example.com', 3), -- Пароль: mechanic123
    (N'user4', N'6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', N'79992223344', N'user4@example.com', 2), -- Пароль: password4
    (N'user5', N'd4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35', N'79993334455', N'user5@example.com', 3), -- Пароль: password5
    (N'user6', N'4e07408562bedb8b60ce05c1decfe3ad16b72230967de01f640b7e4729b49fce', N'79994445566', N'user6@example.com', 2), -- Пароль: password6
    (N'user7', N'4b227777d4dd1fc61c6f884f48641d02b4d121d3fd328cb08b5531fcacdabf8a', N'79995556677', N'user7@example.com', 3), -- Пароль: password7
    (N'user8', N'ef2d127de37b942baad06145e54b0c619a1f22327b2ebbcfbec78f5564afe39d', N'79996667788', N'user8@example.com', 2), -- Пароль: password8
    (N'user9', N'e7f6c011776e8db7cd330b54174fd76f7d0216b612387a5ffcfb81e6f0919683', N'79997778899', N'user9@example.com', 3), -- Пароль: password9
    (N'user10', N'4a44dc15364204a80fe80e9039455cc1608281820fe2b24f1e5233ade6af1dd5', N'79998889900', N'user10@example.com', 2); -- Пароль: password10
	
	-- Вставка данных в таблицу Position
	INSERT INTO Position (PositionName) VALUES
	(N'Главный механик'),
	(N'Старший механик'),
	(N'Младший механик'),
	(N'Механик-стажер'),
	(N'Специалист по диагностике'),
	(N'Мастер цеха'),
	(N'Техник'),
	(N'Инженер'),
	(N'Консультант'),
	(N'Администратор сервиса');
	
	-- Вставка данных в таблицу Qualification
	INSERT INTO Qualification (QualificationName) VALUES
	(N'Мастер'),
	(N'Специалист'),
	(N'Новичок');
	
	-- Вставка данных в таблицу ServiceStations
	INSERT INTO ServiceStations (Address, TelephoneNumber, Email, QuantityWorkPlaces) VALUES
	(N'ул. Главная, 123', N'12345678901', N'station1@example.com', 5),
	(N'ул. Вязов, 456', N'09876543210', N'station2@example.com', 3),
	(N'ул. Лесная, 789', N'11223344556', N'station3@example.com', 4),
	(N'ул. Садовая, 101', N'22334455667', N'station4@example.com', 6),
	(N'ул. Парковая, 202', N'33445566778', N'station5@example.com', 7),
	(N'ул. Речная, 303', N'44556677889', N'station6@example.com', 8),
	(N'ул. Горная, 404', N'55667788990', N'station7@example.com', 9),
	(N'ул. Солнечная, 505', N'66778899001', N'station8@example.com', 10),
	(N'ул. Лунная, 606', N'77889900112', N'station9@example.com', 11),
	(N'ул. Звездная, 707', N'88990011223', N'station10@example.com', 12);
	
	-- Вставка данных в таблицу Employee
	INSERT INTO Employee (Surname, EmployeeName, Patronymic, PositionId, QualificationId, ServiceStationId, EmployeeAccountID) VALUES
	(N'Иванов', N'Иван', N'Иванович', 1, 1, 1, 1),
	(N'Петров', N'Петр', N'Петрович', 2, 2, 1, 2),
	(N'Сидоров', N'Сидор', N'Сидорович', 3, 3, 2, 3),
	(N'Кузнецов', N'Алексей', N'Алексеевич', 4, 1, 2, 4),
	(N'Смирнов', N'Дмитрий', N'Дмитриевич', 5, 2, 3, 5),
	(N'Васильев', N'Василий', N'Васильевич', 6, 3, 3, 6),
	(N'Михайлов', N'Михаил', N'Михайлович', 7, 1, 4, 7),
	(N'Алексеев', N'Алексей', N'Алексеевич', 8, 2, 4, 8),
	(N'Андреев', N'Андрей', N'Андреевич', 9, 3, 5, 9),
	(N'Антонов', N'Антон', N'Антонович', 10, 1, 5, 10);
	
	-- Вставка данных в таблицу Clients
	INSERT INTO Clients (Surname, ClientName, Patronymic, TelephoneNumber, CarBrand, CarModel, GovNumber, LastVisitDate) VALUES
	(N'Смирнов', N'Алексей', N'Игоревич', N'11111111111', N'Toyota', N'Corolla', N'А123БВ', N'2023-10-01'),
	(N'Кузнецова', N'Елена', N'Александровна', N'22222222222', N'Honda', N'Civic', N'В456ДЕ', N'2023-09-15'),
	(N'Иванов', N'Иван', N'Иванович', N'33333333333', N'Ford', N'Focus', N'С789ФГ', N'2023-08-20'),
	(N'Петров', N'Петр', N'Петрович', N'44444444444', N'Chevrolet', N'Cruze', N'Д012ЖЗ', N'2023-07-25'),
	(N'Сидоров', N'Сидор', N'Сидорович', N'55555555555', N'Nissan', N'Altima', N'Е345ИК', N'2023-06-30'),
	(N'Алексеев', N'Алексей', N'Алексеевич', N'66666666666', N'Hyundai', N'Elantra', N'Ф678ЛМ', N'2023-05-05'),
	(N'Андреев', N'Андрей', N'Андреевич', N'77777777777', N'Kia', N'Rio', N'Х901НО', N'2023-04-10'),
	(N'Антонов', N'Антон', N'Антонович', N'88888888888', N'Volkswagen', N'Golf', N'Ц234ПР', N'2023-03-15'),
	(N'Борисов', N'Борис', N'Борисович', N'99999999999', N'Skoda', N'Octavia', N'Ч567СТ', N'2023-02-20'),
	(N'Владимиров', N'Владимир', N'Владимирович', N'10101010101', N'Renault', N'Logan', N'Ш890УФ', N'2023-01-25');
	
	-- Вставка данных в таблицу Orders
	INSERT INTO Orders (ClientId, DateReference, RepairDate, StatusName) VALUES
	(1, N'2023-10-05', N'2023-10-10', N'Завершено'),
	(2, N'2023-10-06', NULL, N'В процессе'),
	(3, N'2023-10-07', N'2023-10-12', N'Завершено'),
	(4, N'2023-10-08', NULL, N'В процессе'),
	(5, N'2023-10-09', N'2023-10-14', N'Завершено'),
	(6, N'2023-10-10', NULL, N'В процессе'),
	(7, N'2023-10-11', N'2023-10-16', N'Завершено'),
	(8, N'2023-10-12', NULL, N'В процессе'),
	(9, N'2023-10-13', N'2023-10-18', N'Завершено'),
	(10, N'2023-10-14', NULL, N'В процессе');
	
	-- Вставка данных в таблицу Operations
	INSERT INTO Operations (OperationName, Price) VALUES
	(N'Замена масла', 50.00),
	(N'Ремонт тормозов', 120.00),
	(N'Замена фильтра', 30.00),
	(N'Замена свечей', 40.00),
	(N'Замена ремня ГРМ', 200.00),
	(N'Замена аккумулятора', 80.00),
	(N'Замена шин', 60.00),
	(N'Балансировка колес', 50.00),
	(N'Замена стекла', 150.00),
	(N'Покраска кузова', 300.00);
	
	-- Вставка данных в таблицу SpareParts
	INSERT INTO SpareParts (PartName, Price) VALUES
	(N'Масляный фильтр', 10.00),
	(N'Тормозные колодки', 45.00),
	(N'Воздушный фильтр', 15.00),
	(N'Свечи зажигания', 20.00),
	(N'Ремень ГРМ', 50.00),
	(N'Аккумулятор', 100.00),
	(N'Шины', 80.00),
	(N'Стекло лобовое', 120.00),
	(N'Краска', 200.00),
	(N'Детали двигателя', 300.00);
	
	-- Вставка данных в таблицу OrderSpareParts
	INSERT INTO OrderSpareParts (SparePartId, OrderId) VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4),
	(5, 5),
	(6, 6),
	(7, 7),
	(8, 8),
	(9, 9),
	(10, 10);
END;



