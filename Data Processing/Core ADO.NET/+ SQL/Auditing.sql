USE Northwind
GO

/*******************************************
 * Процедура для вставки новой записи в таблицу категорий
 *******************************************/
CREATE PROCEDURE CategoryInsert(
    @CategoryName     NVARCHAR(15),
    @Description      NTEXT,
    @CategoryID       INTEGER OUTPUT
)
AS
	SET NOCOUNT OFF;
	
	INSERT INTO Categories
	  (
	    CategoryName,
	    DESCRIPTION
	  )
	VALUES
	  (
	    @CategoryName,
	    @Description
	  );
	
	SELECT @CategoryID = @@IDENTITY ;
GO

/*******************************************
 * Таблица аудита категорий
 *******************************************/
CREATE TABLE CategoryAudit
(
	AuditID        INT NOT NULL IDENTITY(1, 1),
	CategoryID     INT NOT NULL,
	OldName        NVARCHAR(15) NULL,
	NEWNAME        NVARCHAR(15) NOT NULL
)
GO

/*******************************************
 * Добавление первичного ключа для таблицы аудита
 *******************************************/
ALTER TABLE CategoryAudit
  ADD CONSTRAINT PK_CategoryAudit 
    PRIMARY KEY(AuditID)
GO

/*******************************************
 * Добавление внешнего ключа для таблицы аудита
 *******************************************/
ALTER TABLE CategoryAudit
  ADD CONSTRAINT FK_CategoryAudit_Category
    FOREIGN KEY(CategoryID)
      REFERENCES Categories(CategoryID)
GO

/*******************************************
 * Триггеры для таблицы аудита
 *******************************************/
CREATE TRIGGER CategoryInsertTrigger
ON Categories
   AFTER INSERT
AS
	INSERT INTO CategoryAudit
	  (
	    CategoryID,
	    NEWNAME
	  )
	SELECT CategoryID,
	       CategoryName
	FROM   INSERTED ;
GO

CREATE TRIGGER CategoryUpdateTrigger
ON Categories
   AFTER UPDATE
AS
	INSERT INTO CategoryAudit
	  (
	    CategoryID,
	    OldName,
	    NEWNAME
	  )
	SELECT old.CategoryID,
	       old.CategoryName,
	       new.CategoryName
	FROM   DELETED     AS old,
	       Categories  AS new
	WHERE  old.CategoryID = new.CategoryID ;
GO