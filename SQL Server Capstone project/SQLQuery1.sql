CREATE TABLE Users (
UserId VARCHAR(50) CONSTRAINT pk_UserId PRIMARY KEY,
UserName VARCHAR(50) NOT NULL,
Password VARCHAR(50) NOT NULL,
Age INT NOT NULL,
Gender CHAR(1) CONSTRAINT chk_Gender CHECK(Gender IN ('M','F')),
EmailId VARCHAR(50) CONSTRAINT unq_EmailId UNIQUE,
PhoneNumber NUMERIC(10) NOT NULL
);

INSERT INTO Users VALUES ('mary_potter', 'Mary Potter', 'Mary@123', 25, 'F', 'mary_p@gmail.com' , 9786543211);
INSERT INTO Users VALUES ('jack_sparrow', 'Jack Sparrow', 'Spar78!jack', 28, 'M', 'jack_spa@yahoo.com' , 7865432102);

CREATE TABLE TheatreDetails (
TheatreId INT IDENTITY(1,1) CONSTRAINT pk_TheatreId PRIMARY KEY,
TheatreName VARCHAR(50) NOT NULL,
Location VARCHAR(50) NOT NULL
);

INSERT INTO TheatreDetails VALUES ('PVR', 'Pune');
INSERT INTO TheatreDetails VALUES ('Inox', 'Delhi');


CREATE TABLE ShowDetails (
ShowId INT CONSTRAINT pk_ShowId PRIMARY KEY IDENTITY(1001,1),
TheatreId INT CONSTRAINT fk_TheatreId FOREIGN KEY REFERENCES TheatreDetails(TheatreId),
ShowDate DATE NOT NULL,
ShowTime TIME NOT NULL,
MovieName VARCHAR(50) NOT NULL,
TicketCost DECIMAL(6,2) NOT NULL,
TicketsAvailable INT NOT NULL
);

INSERT INTO ShowDetails VALUES (2, '28-MAY-2018', '14:30', 'Avengers', 250.00, 100);
INSERT INTO ShowDetails VALUES (2, '30-MAY-2018', '17:30', 'Hit Man', 200.00, 150);

CREATE TABLE BookingDetails (
BookingId VARCHAR(5) PRIMARY KEY CONSTRAINT chk_BookingId CHECK(BookingId LIKE 'B%'),
UserId VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Users(UserId),
ShowId INT NOT NULL FOREIGN KEY REFERENCES ShowDetails(ShowId),
NoOfTickets INT NOT NULL,
TotalAmt DECIMAL(6,2) NOT NULL
);

INSERT INTO BookingDetails VALUES ('B1001', 'jack_sparrow', 1001, 2, 500.00);
INSERT INTO BookingDetails VALUES ('B1002', 'mary_potter', 1002, 5, 1000.00);

CREATE PROCEDURE usp_BookTheTicket 
(
	@UserId VARCHAR(50),
	@ShowId INT,
	@NoOfTickets INT
)
AS
BEGIN
	DECLARE @TicketsAvailable INT, @LastBookingId VARCHAR(5), @numstrBookingId VARCHAR(4), @numBookingId VARCHAR(4), @TotalAmt DECIMAL(6,2), @TicketCost DECIMAL(6,2)
	BEGIN TRY
		IF NOT EXISTS (SELECT UserId FROM Users WHERE UserId=@UserId)
		BEGIN
			RETURN -1
		END
		IF NOT EXISTS (SELECT ShowId FROM ShowDetails WHERE ShowId=@ShowId)
		BEGIN
			RETURN -2
		END
		IF 	(@NoOfTickets < 0)
		BEGIN
			RETURN -3
		END
		
		SELECT @TicketsAvailable = TicketsAvailable FROM ShowDetails WHERE ShowId = @ShowId
		IF (@TicketsAvailable > @NoOfTickets)
		BEGIN
			SET @LastBookingId = (SELECT TOP 1 BookingId FROM BookingDetails ORDER BY BookingId DESC)
			SET @numstrBookingId = (SELECT RIGHT(@LastBookingId,4))
			SET @numBookingId = (SELECT CONVERT(INT, @numstrBookingId)) + 1;
			SET @TicketCost = (SELECT TicketCost FROM ShowDetails WHERE ShowId = @ShowId)
			SET @TotalAmt = (@TicketCost * @NoOfTickets)
			UPDATE ShowDetails SET TicketsAvailable = TicketsAvailable - @NoOfTickets WHERE ShowId = @ShowId
			INSERT INTO BookingDetails VALUES ('B'+@numBookingId, @UserId, @ShowId, @NoOfTickets, @TotalAmt)
		END
		ELSE
		BEGIN
			RETURN -4
		END
	END TRY
	BEGIN CATCH
		RETURN -99
	END CATCH
END

DECLARE @ReturnValue INT
EXEC @ReturnValue = usp_BookTheTicket 'jack_sparrow', 1002 , 7
SELECT @ReturnValue

CREATE FUNCTION ufn_GetMovieShowtimes 
(
	@MovieName VARCHAR(50),
	@Location VARCHAR(50)
)
RETURNS TABLE
AS
RETURN
	(SELECT MovieName, ShowDate, ShowTime, t.TheatreName, TicketCost FROM ShowDetails s INNER JOIN TheatreDetails t ON s.TheatreId = t.TheatreId WHERE MovieName = @MovieName AND Location = @Location)

SELECT * FROM ufn_GetMovieShowtimes ('Avengers','Delhi')

CREATE FUNCTION ufn_BookedDetails 
(
	@BookingId VARCHAR(5)
)
RETURNS TABLE
AS
RETURN 
	(SELECT BD.BookingId, USR.UserName, SD.MovieName, TD.TheatreName, SD.ShowDate, SD.ShowTime, BD.NoOfTickets, BD.TotalAmt FROM BookingDetails BD INNER JOIN Users USR ON BD.UserId = USR.UserId INNER JOIN ShowDetails SD ON SD.ShowId = BD.ShowId INNER JOIN TheatreDetails TD ON TD.TheatreId = SD.TheatreId WHERE BD.BookingId = @BookingId)

SELECT * FROM ufn_BookedDetails ('B1002')