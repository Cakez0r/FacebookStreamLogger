-- ----------------------------
-- Table structure for `entries`
-- ----------------------------
DROP TABLE IF EXISTS `entries`;
CREATE TABLE `entries` (
  `PostID` varchar(64) NOT NULL,
  `ActorID` bigint(20) NOT NULL,
  `Message` varchar(2048) NOT NULL,
  `CreatedTime` datetime NOT NULL,
  PRIMARY KEY (`PostID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
