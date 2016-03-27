USE ImageGallery
GO

IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.TABLES
       WHERE  TABLE_NAME = 'PictureDetails'
   )
    DROP TABLE PictureDetails
GO

IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.TABLES
       WHERE  TABLE_NAME = 'PictureGallery'
   )
    DROP TABLE PictureGallery
GO

IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.TABLES
       WHERE  TABLE_NAME = 'Account'
   )
    DROP TABLE Account
GO

CREATE TABLE Account
(
	AccountId     INT IDENTITY(1, 1) NOT NULL,
	UserName      VARCHAR(256) NOT NULL,
	CONSTRAINT Account_AccountId_PK PRIMARY KEY(AccountId),
	CONSTRAINT Account_UserName_UNIQUE UNIQUE(UserName)
)
GO

CREATE TABLE PictureGallery
(
	PictureId              INT IDENTITY(1, 1) NOT NULL,
	AccountId              INT NOT NULL,
	PictureDescription     VARCHAR(1024) NOT NULL DEFAULT '',
	Width                  INT NOT NULL,
	Height                 INT NOT NULL,
	PictureFileName        VARCHAR(256) NOT NULL,
	PictureData            VARBINARY(MAX) NOT NULL,
	PictureMimeType        VARCHAR(256) NOT NULL,
	CONSTRAINT PictuteGallery_PictureId_PK PRIMARY KEY(PictureId),
	CONSTRAINT PictureGalary_AccountId_Account_FK FOREIGN KEY(AccountId)
	REFERENCES Account(AccountId) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE PictureDetails
(
	PictureId                  INT NOT NULL,
	ShootingDate               DATETIME,
	CameraManufacturer         VARCHAR(256),
	CameraModel                VARCHAR(256),
	FocalLength                INT,
	ExposureTime               FLOAT(8),
	ShutterSpeedValue          FLOAT(8),
	FNumber                    INT,
	CompressedBitsPerPixel     VARCHAR(32),
	CONSTRAINT PictureDetails_PictureId_PK PRIMARY KEY(PictureId),
	CONSTRAINT PictureDetails_PictureId_PictureGallery_FK FOREIGN KEY(PictureId)
	REFERENCES PictureGallery(PictureId) ON UPDATE CASCADE ON DELETE CASCADE
)
GO