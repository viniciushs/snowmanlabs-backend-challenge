USE [webapi--dev]
GO

BEGIN
	IF NOT EXISTS(SELECT 1 FROM Category C WHERE LOWER(C.Name) = 'park')
    BEGIN
        INSERT INTO Category(Uuid, CreatedOn, Active, Name) 
        VALUES(NEWID(), GETDATE(), 1, 'Park');
    END

    IF NOT EXISTS(SELECT 1 FROM Category C WHERE LOWER(C.Name) = 'museum')
    BEGIN
        INSERT INTO Category(Uuid, CreatedOn, Active, Name) 
        VALUES(NEWID(), GETDATE(), 1, 'Museum');
    END

    IF NOT EXISTS(SELECT 1 FROM Category C WHERE LOWER(C.Name) = 'theater')
    BEGIN
        INSERT INTO Category(Uuid, CreatedOn, Active, Name) 
        VALUES(NEWID(), GETDATE(), 1, 'Theater');
    END

    IF NOT EXISTS(SELECT 1 FROM Category C WHERE LOWER(C.Name) = 'monument')
    BEGIN
        INSERT INTO Category(Uuid, CreatedOn, Active, Name) 
        VALUES(NEWID(), GETDATE(), 1, 'Monument');
    END
END
