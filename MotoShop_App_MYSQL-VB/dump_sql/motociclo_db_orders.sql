CREATE DATABASE  IF NOT EXISTS `motociclo_db` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci */;
USE `motociclo_db`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: motociclo_db
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `date_ordered` datetime NOT NULL,
  `est_delivery` date NOT NULL,
  `order_status` enum('pending','processing','shipped','delivered','cancelled','returned') NOT NULL,
  `payment_status` enum('pending','paid','refunded','failed') NOT NULL,
  `payment_option` enum('cash','ATM') NOT NULL,
  `user_id` int(11) NOT NULL,
  `voucher_code` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`order_id`),
  KEY `fk_orders_accounts1_idx` (`user_id`),
  KEY `fk_orders_vouchers1_idx` (`voucher_code`),
  CONSTRAINT `fk_orders_accounts1` FOREIGN KEY (`user_id`) REFERENCES `accounts` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_orders_vouchers1` FOREIGN KEY (`voucher_code`) REFERENCES `vouchers` (`voucher_code`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (6,'2025-08-04 08:54:57','2025-08-09','cancelled','pending','cash',10,NULL),(7,'2025-08-04 08:55:12','2025-08-09','delivered','paid','cash',10,NULL),(8,'2025-08-04 08:58:09','2025-08-09','delivered','paid','cash',3,'SUMMR'),(9,'2025-08-04 14:34:34','2025-08-09','pending','pending','ATM',3,NULL);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-04 17:12:12
