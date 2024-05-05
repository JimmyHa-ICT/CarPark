-- Table structure for table `users`
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
);



-- Dumping data for table `users`
INSERT INTO `users` (`username`, `password`) VALUES
( 'johndoe', sha1('johndoe') ),
( 'johnsmith', sha1('johnsmith') );
COMMIT;

DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
);

INSERT INTO roles (name) VALUES ('user'), ('admin');

DROP TABLE IF EXISTS `user_roles`;
CREATE TABLE IF NOT EXISTS `user_roles` (
  id INT(11) NOT NULL AUTO_INCREMENT,
  user_id int(11),
  role_id int(11),
 CONSTRAINT pk_user  FOREIGN KEY (user_id) REFERENCES users(`id`),
 CONSTRAINT pk_role  FOREIGN KEY (role_id) REFERENCES roles(`id`)
);

INSERT INTO `user_roles` (user_id, role_id) VALUES(1, 2), (2, 2);