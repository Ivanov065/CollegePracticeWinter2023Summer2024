
-- create table Roles (
-- 	id int primary key generated always as identity,
-- 	role_name varchar(20) unique not null
-- );

-- create table Users (
-- 	id int primary key generated always as identity,
-- 	U_role int references Roles(id) on delete cascade
-- );


-- create table Passwords (
-- 	id int primary key generated always as identity,
-- 	login varchar(40) unique not null,
-- 	P_password varchar(40) not null,
-- 	User_id int not null references Users(id) on delete cascade
-- );


-- create table Client (
-- 	id int primary key references Users(id) on delete cascade,
-- 	C_name varchar(30) not null,
-- 	Surname varchar(30) not null,
-- 	Middlename varchar(40),
-- 	Adres varchar(100) not null,
-- 	Phone varchar(12) not null,
-- 	Date_b date not null
-- );

-- create table Services (
-- 	id int primary key generated always as identity,
-- 	s_name varchar(50) not null
-- );

-- create table Trainings (
-- 	id int primary key generated always as identity,
-- 	t_name varchar(50) not null
-- );


-- create table Season_ticket (
-- 	id int primary key generated always as identity,
-- 	training_code int not null references Trainings(id) on delete cascade,
-- 	service_code int not null references Services(id) on delete cascade,
-- 	price float not null check(price > 0.0),
-- 	Visiting_amount int not null check(Visiting_amount > 0),
-- 	Days_active int not null check(Days_active > 0)
-- );


-- create table Season_tickets_active (
-- 	id int primary key generated always as identity,
-- 	Season_ticket_code int not null references Season_ticket(id) on delete cascade,
-- 	client_code int not null references Client(id) on delete cascade,
-- 	date_start date not null,
-- 	date_end date not null,
-- 	last_visitings int not null check(last_visitings >= 0)
-- );


-- create table Employees (
-- 	id int primary key references Users(id) on delete cascade,
-- 	E_role int not null references Roles(id) on delete cascade,
-- 	C_name varchar(30) not null,
-- 	Surname varchar(30) not null,
-- 	Middlename varchar(40),
-- 	Adres varchar(100) not null,
-- 	Phone varchar(12) not null,
-- 	Date_b date not null,
-- 	Salary int not null check(Salary > 0)
-- );


-- create table Schedule (
-- 	id int primary key generated always as identity,
-- 	Date_conduction date not null,
-- 	Time_start time not null,
-- 	Time_end time not null check(Time_start < Time_end),
-- 	Emp_code int not null references Employees(id) on delete cascade,
-- 	Service_code int not null references Services(id) on delete cascade,
-- 	Training_code int not null references Trainings(id) on delete cascade
-- );

-- create table Visit_control (
-- 	id int primary key generated always as identity,
-- 	schedule_code int not null references Schedule(id) on delete cascade,
-- 	client_card_code int not null references Season_tickets_active(id) on delete cascade
-- )

-- create table Main_Log (
-- 	id int primary key generated always as identity,
-- 	log_text Text not null
-- )



