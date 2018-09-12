use School
go

create table Exams(
	Id int identity(1, 1) not null,
	Number nchar(10) not null,
	Info xml not null,
	constraint Pk_Exams primary key(Id)
)
go
CREATE PRIMARY XML INDEX idx_exams on Exams (Info)
GO