-- CREACION DE LA BASE DE DATOS: task_manager_db
CREATE DATABASE task_manager_db; 
-- Creación de Tabla Tasks:
CREATE TABLE IF NOT EXISTS Tasks(
Id INT auto_increment primary KEY,
Title varchar(100) not null,
Description varchar(500) null,
IsCompleted tinyint(1) not null default 0,
CreatedAt datetime not null default current_timestamp
);
