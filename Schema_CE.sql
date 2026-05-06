CREATE TABLE Customers
(
    CustomerId INT IDENTITY PRIMARY KEY,
    Name VARBINARY(MAX),
    Mobile VARBINARY(MAX),
    Email VARBINARY(MAX),
    ProofType VARBINARY(MAX),
    ProofNumber VARBINARY(MAX),
    Address VARBINARY(MAX)
);
GO

CREATE PROCEDURE usp_EnrollCustomer
    @Name NVARCHAR(200),
    @Mobile NVARCHAR(50),
    @Email NVARCHAR(200),
    @ProofType NVARCHAR(50),
    @ProofNumber NVARCHAR(100),
    @Address NVARCHAR(500),
    @CustomerId INT OUTPUT
AS
BEGIN
    -- TODO: move passphrase to config
    DECLARE @pass NVARCHAR(50) = 'myPass123';

    INSERT INTO Customers
    (
        Name, Mobile, Email, ProofType, ProofNumber, Address
    )
    VALUES
    (
        ENCRYPTBYPASSPHRASE(@pass, @Name),
        ENCRYPTBYPASSPHRASE(@pass, @Mobile),
        ENCRYPTBYPASSPHRASE(@pass, @Email),
        ENCRYPTBYPASSPHRASE(@pass, @ProofType),
        ENCRYPTBYPASSPHRASE(@pass, @ProofNumber),
        ENCRYPTBYPASSPHRASE(@pass, @Address)
    );

    SET @CustomerId = SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE usp_GetCustomer
    @CustomerId INT
AS
BEGIN
    DECLARE @pass NVARCHAR(50) = 'myPass123';

    SELECT
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, Name)) AS Name,
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, Mobile)) AS Mobile,
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, Email)) AS Email,
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, ProofType)) AS ProofType,
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, ProofNumber)) AS ProofNumber,
        CONVERT(NVARCHAR(MAX), DECRYPTBYPASSPHRASE(@pass, Address)) AS Address
    FROM Customers
    WHERE CustomerId = @CustomerId;
END
GO