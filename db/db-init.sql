CREATE USER admin WITH PASSWORD 'admin' CREATEDB;
CREATE DATABASE fds
    WITH 
    OWNER = admin
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

\c fds

CREATE TABLE Package (
	id integer not null generated always as identity,
  name varchar (100) not null,
  currentVersion varchar (50) not null,
  latestVersion varchar (50) not null,
  score integer null,
  url varchar (200) not null,
  status integer default(1) not null,
  description varchar (1000) null,
  type integer default(1) not null,
  updatedOn timestamp null
);

CREATE TABLE Settings (
  id integer not null generated always as identity,
  state integer default(1) not null
);

INSERT INTO Settings (state) VALUES (1);
