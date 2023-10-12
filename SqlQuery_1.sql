/* #Tenemos 3 pasos

Create test user
Create initial database
populate initial data

*/

USE CarsManager;



CREATE TABLE 'Cars' (
	'Id' INT(10) NOT NULL AUTO_INCREMENT,
	'Maker' VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'utf8mb4_0900_ai_ci',
	'Trim' VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'utf8mb4_0900_ai_ci',
	'Year' INT(10) NOT NULL,
	'Model' VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'utf8mb4_0900_ai_ci',
	'Driver' INT(10) NOT NULL,
	'Mechanic' VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'utf8mb4_0900_ai_ci',
	'LastMaintenance' DATE NOT NULL DEFAULT (curdate()),
	'LastOdometer' FLOAT(15,1) NOT NULL DEFAULT '0',
	PRIMARY KEY ('Id') USING BTREE
)
COLLATE='utf8mb4_0900_ai_ci'
ENGINE=InnoDB
AUTO_INCREMENT=2
;

