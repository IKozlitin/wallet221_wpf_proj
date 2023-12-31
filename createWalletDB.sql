﻿--Создание БД Кошелёк
create database WalletDB
go
--Используем БД Кошелёк
use WalletDB
go
--Создание таблицы Клиент
create table Client
(
	Id int identity (1,1) primary key,
	FirstName nvarchar(100) not null,
	SurName	nvarchar(100) not null,
	PhoneNumber bigint not null
)
go
--Добавление данных в таблицу Клиент
insert into Client values ('Ivan', 'Ivanov', 89001112233)
go
--Создание таблицы Карты
create table RublesCard
(
	Id int identity (1,1) primary key,
	ClientId int,
	CardName nvarchar(max) not null,
	CardBalance money not null
)
go
--Добавление данных в таблицу Карты
insert into RublesCard values (1, 'Рубли', 1000)
go
--Создание таблицы Вклады
create table RublesDeposit
(
	Id int identity (1,1) primary key,
	ClientId int,
	DepositName nvarchar(max) not null,
	DepositBalance money not null,
	DepositPercent float not null
)
go
--Добавление данных в таблицу Вклады
insert into RublesDeposit values (1, 'Рубли', 2000, 8.5)
go
--Создание таблицы Курсы валют
create table RateList
(
	Id int identity (1,1) primary key,
	CurrencyName nvarchar(25) not null,
	CurrencyRate float not null
)
go
--Добавление данных в таблицу Курсы валют
insert into RateList values ('USD', 100)
insert into RateList values ('EUR', 105)
insert into RateList values ('CNY', 15)
go
--Создание таблицы История операций
create table History
(
	Id int identity (1,1) primary key,
	ClientId int,
	Operation nvarchar(max),
	CreateAt datetime not null default getdate()
)
go