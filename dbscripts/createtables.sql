CREATE TABLE messages (id int GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY, Text varchar(128) NOT NULL, CreatedAt timestamp, Number int);