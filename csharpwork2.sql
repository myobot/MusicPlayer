-- MySQL dump 10.13  Distrib 5.7.16, for Win64 (x86_64)
--
-- Host: localhost    Database: csharpwork
-- ------------------------------------------------------
-- Server version	5.7.16-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `user_name` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `image_num` int(11) NOT NULL,
  PRIMARY KEY (`user_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('123','123',19),('aa','123',14),('dxj','123',12),('dxj2','123',16),('lzh','123',15),('mama','1234',15),('sherlock','123',17),('sherlock2','123',19),('sherlock3','123',17),('sherlock4','123',18),('xuanmiao','xuanmiao',11);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_like_music_list`
--

DROP TABLE IF EXISTS `user_like_music_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_like_music_list` (
  `like_list_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) NOT NULL,
  `music_path` varchar(400) NOT NULL,
  PRIMARY KEY (`like_list_id`),
  KEY `user_name` (`user_name`),
  CONSTRAINT `user_like_music_list_ibfk_1` FOREIGN KEY (`user_name`) REFERENCES `user` (`user_name`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=118 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_like_music_list`
--

LOCK TABLES `user_like_music_list` WRITE;
/*!40000 ALTER TABLE `user_like_music_list` DISABLE KEYS */;
INSERT INTO `user_like_music_list` VALUES (40,'aa','C:\\Users\\Lenovo\\Music\\陈奕迅\\陈奕迅 - 红玫瑰.mp3'),(41,'aa','C:\\Users\\Lenovo\\Music\\北京室内男声合唱团\\北京室内男声合唱团 - 中国军魂(1).mp3'),(42,'sherlock4','C:\\Users\\Lenovo\\Music\\陈奕迅\\陈奕迅 - 红玫瑰.mp3'),(43,'sherlock4','C:\\Users\\Lenovo\\Music\\北京室内男声合唱团\\北京室内男声合唱团 - 中国军魂(1).mp3'),(44,'sherlock4','C:\\Users\\Lenovo\\Music\\Jam\\Jam - 七月上.mp3'),(45,'sherlock4','C:\\Users\\Lenovo\\Music\\Alan Walker\\Alan Walker - Faded (消逝).mp3'),(91,'mama','C:\\Users\\Lenovo\\Music\\李玉刚\\李玉刚 - 刚好遇见你.mp3'),(114,'dxj','C:\\Users\\Lenovo\\Music\\北京室内男声合唱团\\北京室内男声合唱团 - 中国军魂(1).mp3'),(115,'dxj','C:\\Users\\Lenovo\\Music\\Maroon 5\\Maroon 5 - One More Night.mp3'),(116,'123','C:\\Users\\Lenovo\\Music\\Maroon 5\\Maroon 5 - One More Night.mp3'),(117,'lzh','C:\\Users\\Lenovo\\Music\\林俊杰\\林俊杰 - 当你.mp3');
/*!40000 ALTER TABLE `user_like_music_list` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-06-24 17:15:00
