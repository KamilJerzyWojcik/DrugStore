CREATE DATABASE DrugStoreDataBase;
--

--Medicines
CREATE TABLE Medicines(
	MedicineID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(256) Unique NOT NULL,
	Manufacturer varchar(256) NOT NULL,
	Price decimal(18,2),
	Amount int,
	WithPrescription bit NOT NULL
);
--

--Prescriptions
CREATE TABLE Prescriptions(
	PrescriptionID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CustomerName varchar(256) NOT NULL,
	PESEL varchar(11) NOT NULL,
	PrescriptionNumber varchar(256) NOT NULL
);
--

--Orders
CREATE TABLE Orders(
	OrderID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Date DateTime,
	Amount int
);
ALTER TABLE Orders
Add MedicineID int;
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Medicines FOREIGN KEY (MedicineID)
REFERENCES Medicines (MedicineID)
ON UPDATE CASCADE
ON DELETE CASCADE;
ALTER TABLE Orders
Add PrescriptionID int;
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Prescriptions FOREIGN KEY (PrescriptionID)
REFERENCES Prescriptions (PrescriptionID)
ON UPDATE CASCADE
ON DELETE CASCADE;
--