CREATE DATABASE powermeter

CREATE TABLE users (
	id		int			NOT NULL PRIMARY KEY IDENTITY (1, 1),
	mail	varchar(50)	NOT NULL UNIQUE,
	price	decimal(5,2),
	currency varchar(3)
);

CREATE TABLE device (
	id		int			NOT NULL PRIMARY KEY IDENTITY (1, 1),
	name	varchar(50)	NOT NULL UNIQUE,
	id_user int			NOT NULL references users (id),
);

CREATE TABLE record (
	id		bigint		NOT NULL PRIMARY KEY IDENTITY (1, 1),
	id_dev	int			NOT NULL references device (id),
	stamp	datetime	NOT NULL ,
	voltage	decimal(5,2),
	current_l1 decimal(5,2),
	current_l2 decimal(5,2),
	current_l3 decimal(5,2),
);