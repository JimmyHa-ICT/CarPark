-- Create the database
CREATE DATABASE IF NOT EXISTS your_database_name;
USE your_database_name;

-- Create the User table
CREATE TABLE IF NOT EXISTS User (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'User', 'Guest') NOT NULL
);

-- Create the Session table
CREATE TABLE IF NOT EXISTS Session (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Result ENUM('Success', 'Failure', 'Pending') NOT NULL,
    FOREIGN KEY (UserID) REFERENCES User(ID)
);

-- Create the PerformanceMetrics table
CREATE TABLE IF NOT EXISTS PerformanceMetrics (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    SessionID INT NOT NULL,
    TimeTaken INT NOT NULL,
    Collision INT NOT NULL,
    FOREIGN KEY (SessionID) REFERENCES Session(ID)
);

-- Insert users into the User table
INSERT INTO User (Username, Password, Role) VALUES 
('hung', SHA1('123456'), 'Admin'),
('johndoe', SHA1('123456'), 'User'),
('johnsmith', SHA1('123456'), 'User');

-- Create Collisions table
CREATE TABLE Collisions ( 
    collisionID INT AUTO_INCREMENT PRIMARY KEY, 
    sessionID INT NOT NULL, 
    typeOfCollision INT NOT NULL, 
    collideLatitude DOUBLE NOT NULL, 
    collideLongitude DOUBLE NOT NULL, 
    mapID INT NOT NULL,
    FOREIGN KEY (sessionID) REFERENCES session(ID) 
);
