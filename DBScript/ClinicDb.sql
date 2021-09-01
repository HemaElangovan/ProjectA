create database Clinic_Management_System

use Clinic_Management_System

create table tbl_StaffLogin(
UserName varchar(30)NOT NULL, FirstName varchar(30)NOT NULL, LastName varchar(30)NOT NULL, StaffPassword varchar(20)NOT NULL)

--/^[a-zA-Z0-9]*$/ - Validation of username in regular expression.

insert into tbl_StaffLogin values ('Priya1001','Priya','Baskaran','PRI001')
insert into tbl_StaffLogin values ('Joey1002','Joey','Smith','JOE002')
insert into tbl_StaffLogin values ('Rachel1003','Rachel','Jones','RAC003')
insert into tbl_StaffLogin values ('Monica1004','Monica','Anniston','MON004')

select * from tbl_StaffLogin

create table tbl_Doctor(
Doctor_ID int identity(2,2), FirstName varchar(50), LastName varchar(50),Sex varchar(15),
Specialization varchar(50), VisitingHours Time
constraint PK_Did primary key(Doctor_ID))

select * from tbl_Doctor

select Doctor_ID, FirstName, LastName, Sex, Specialization, CAST(CONVERT(TIME(6),VisitingHours) AS VARCHAR(5)) as Available_From
from tbl_Doctor

Insert into tbl_Doctor values ('Dr.Umayal','Rajan','Female','Pediatrics','01:00')
Insert into tbl_Doctor values ('Dr.Anbu','Santhanam','Male','General','02:00')
Insert into tbl_Doctor values ('Dr.Soundarya','Karthick','Female','Pediatrics','12:00')
Insert into tbl_Doctor values ('Dr.Balaji','Venkatesan','Male','Internal Medicine','02:00')
Insert into tbl_Doctor values ('Dr.Hari','Govind','Male','Orthopedics','03:00')

delete tbl_Doctor where Doctor_ID=4
select * from tbl_Doctor

drop table tbl_Doctor

create table tbl_Patient(
Patient_ID int identity(1,1), FirstName varchar(50), LastName varchar(50), Sex varchar(15),
Age int, Date_Of_Birth Date
constraint PK_Pid primary key(Patient_ID))

drop table tbl_Patient

--Date_format(Date_Of_Birth,'%D, %M, %Y')- Use in where condition

insert tbl_Patient values ('Swarna','Baskar','Female',25,'1995-08-16')
insert tbl_Patient values ('Karthick','Baskar','Female',25,'1995-08-16')
insert tbl_Patient values ('Swarna','Baskar','Female',25,'1995-08-16')

select * from tbl_Patient

select Patient_ID, FirstName, LastName, Sex, Age, Format(Date_Of_Birth,'dd/MM/yyyy') as DOB
from tbl_Patient

update tbl_Patient set FirstName='Kaaviya',LastName='Naresh',Sex='Female',Age=26,Date_Of_Birth='1995-12-08' where Patient_ID=4

create table tbl_Schedule(
Appointment_ID int identity(1,1), Patient_Id int, Specialization varchar(50), DoctorName varchar(50),
VisitDate Date, AppointmentTime Time
constraint PK_Aid primary key(Appointment_ID),
constraint FK_Pid foreign key(Patient_Id) references tbl_Patient(Patient_ID))

select * from tbl_Schedule

insert into tbl_Schedule values(1,'General','Dr.Anbu','10/09/21','02:00:00')
insert into tbl_Schedule values(4,'Pediatrics','Dr.Umayal','11/09/21','12:00')

select Appointment_ID, tp.Patient_Id, ts.Specialization, td.FirstName as DoctorName,Format(VisitDate,'dd/MM/yyyy')as Visit_Date,
CAST(CONVERT(TIME(6),AppointmentTime) AS VARCHAR(5)) as Appointment_Time
from tbl_Schedule ts 
join tbl_doctor td 
on ts.Specialization = td.Specialization
join tbl_Patient tp
on tp.Patient_ID = ts.Patient_Id

select * from tbl_Patient

select * from tbl_Doctor

select * from tbl_Schedule

sp_help 'tbl_Schedule'

drop table tbl_Schedule

delete tbl_Patient where Patient_ID=7


select * from tbl_schedule

delete tbl_Schedule where Patient_Id=1

SELECT CAST(CONVERT(TIME(6),AppointmentTime) AS VARCHAR(5)) from tbl_Schedule

sp_help'tbl_Schedule'

create table tbl_Cancel(
Patient_id int, VisitDate Date, Appointment varchar(100))

select ts.Appointment_ID,tc.Patient_id, tc.VisitDate, Appointment from tbl_Cancel  tc
join tbl_Schedule ts
on(ts.Patient_Id = tc.Patient_id)






