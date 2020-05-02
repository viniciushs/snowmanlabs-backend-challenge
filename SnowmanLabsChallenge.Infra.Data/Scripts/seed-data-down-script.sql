BEGIN
    DELETE FROM Category WHERE LOWER(C.Name) = 'park';
    DELETE FROM Category WHERE LOWER(C.Name) = 'museum';
    DELETE FROM Category WHERE LOWER(C.Name) = 'theater';
    DELETE FROM Category WHERE LOWER(C.Name) = 'monument';
END
