/* #Tenemos 3 pasos

Create test user
Create initial database
populate initial data

*/

USE CarsManager;


CREATE TABLE `Cars` (
	`Id` INT(10) NOT NULL AUTO_INCREMENT,
	`Maker` VARCHAR(50) NOT NULL DEFAULT "",
	`Trim` VARCHAR(50) NOT NULL DEFAULT "",
	`Year` INT(10) NOT NULL DEFAULT 0,
	`Model` VARCHAR(50) NOT NULL DEFAULT "",
	`Driver` VARCHAR(50) NOT NULL DEFAULT "",
	`Mechanic` VARCHAR(50) NOT NULL DEFAULT "",
	`LastMaintenance` DATE NOT NULL DEFAULT (curdate()),
	`LastOdometer` FLOAT(15,1) NOT NULL DEFAULT 0,
	PRIMARY KEY (`Id`) USING BTREE
)
COLLATE=`utf8mb4_0900_ai_ci`
ENGINE=InnoDB
;

 


CREATE TABLE `Users` (
	`Id` INT(10) NOT NULL AUTO_INCREMENT,
	`UserName` VARCHAR(50) NOT NULL DEFAULT '' , 
	`UserKey` VARCHAR(50) NOT NULL DEFAULT '' COMMENT 'password' , 
	`ApiKey` VARCHAR(50) NOT NULL DEFAULT '' , 
	`RefreshToken` VARCHAR(50) NOT NULL DEFAULT '' , 
	`Creation` DATE NOT NULL DEFAULT (curdate()),
	`LastLogin` DATE NOT NULL DEFAULT (curdate()),
	`Token` VARCHAR(50) NOT NULL DEFAULT '' , 
	PRIMARY KEY (`Id`) USING BTREE
)
COLLATE='utf8mb4_0900_ai_ci'
ENGINE=InnoDB
;
