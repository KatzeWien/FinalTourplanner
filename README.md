First it is necessary to add an api key for openstreetmap in usersecrets.
Also it is necessary to build the structure of the database on you own firstly with following format:
CREATE TABLE IF NOT EXISTS tour (
name VARCHAR(100) PRIMARY KEY,
description VARCHAR(250),
fromInput VARCHAR(250),
toInput VARCHAR(250),
transportType VARCHAR(250),
distance INT,
estimatedTime INTERVAL
);

CREATE TABLE IF NOT EXISTS tourLog (
id SERIAL PRIMARY KEY,
nameOfTour VARCHAR(100) REFERENCES tour(name) ON UPDATE CASCADE ON DELETE CASCADE,
dateInput TIMESTAMP,
comment VARCHAR(250),
difficulty VARCHAR(250),
distance INT,
totalTime INTERVAL,
rating VARCHAR(250)
);
