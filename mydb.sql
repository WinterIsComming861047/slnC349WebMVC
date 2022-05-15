-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: c349
-- ------------------------------------------------------
-- Server version	8.0.25

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
-- Table structure for table `c349member`
--

DROP TABLE IF EXISTS `c349member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `c349member` (
  `id` int NOT NULL AUTO_INCREMENT,
  `EmployeeID` varchar(6) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Company` varchar(45) NOT NULL,
  `Tel` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Turn` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`),
  UNIQUE KEY `employeeNumber_UNIQUE` (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=90 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `c349member`
--

LOCK TABLES `c349member` WRITE;
/*!40000 ALTER TABLE `c349member` DISABLE KEYS */;
INSERT INTO `c349member` VALUES (1,'153155','謝明夏','中鋼',NULL,NULL,'04'),(2,'214585','賴丙鈞','中鋼','5731',NULL,'A'),(3,'197459','黃喆洺','中鋼',NULL,NULL,'B'),(4,'143800','呂昆龍','中鋼',NULL,NULL,'C'),(5,'110593','江金地','中鋼',NULL,NULL,'D'),(6,'127951','許誠鑜','中鋼','5714',NULL,'A'),(7,'206300','朱紀維','中鋼',NULL,NULL,'B'),(8,'110379','張清裕 ','中鋼',NULL,NULL,'C'),(9,'127167','蘇天生 ','中鋼',NULL,NULL,'D'),(10,'101790','陳永樹 ','中鋼',NULL,NULL,'22'),(11,'058461','薛同明','中鋼',NULL,NULL,'22'),(12,'136465','吳慶義','中鋼','5719',NULL,'A'),(13,'110395','沈一郎','中鋼',NULL,NULL,'B'),(14,'137554','張森裕','中鋼',NULL,NULL,'C'),(15,'116863','周品宏 ','中鋼',NULL,NULL,'D'),(16,'164301','莊琇雄 ','中鋼',NULL,NULL,'04'),(17,'000000','無','無',NULL,NULL,'00'),(49,'7B5979','吳羿均','鋼堡',NULL,NULL,'A'),(50,'7A7548','蔡昀達','鋼堡',NULL,NULL,'A'),(51,'7A9100','陳琦方','鋼堡',NULL,NULL,'A'),(52,'7A1990','林晉偉','得亨',NULL,NULL,'A'),(56,'794219','劉凱文','鋼堡',NULL,NULL,'B'),(57,'797703','林榆博','鋼堡',NULL,NULL,'B'),(58,'799119','梁正育','鋼堡',NULL,NULL,'B'),(59,'798861','江永福','得亨',NULL,NULL,'B'),(63,'784505','陳仁政','鋼堡',NULL,NULL,'C'),(64,'7A4431','嚴明正','鋼堡',NULL,NULL,'C'),(65,'795059','陳韋廷','鋼堡',NULL,NULL,'C'),(66,'785662','吳台平','得亨',NULL,NULL,'C'),(70,'7A3435','許秉鄴','鋼堡',NULL,NULL,'D'),(71,'798995','傅永鈞','鋼堡',NULL,NULL,'D'),(72,'798643','謝松穎','鋼堡',NULL,NULL,'D'),(73,'7B0508','朱建收','得亨',NULL,NULL,'D'),(75,'7A4424','陳永祥','鋼堡',NULL,NULL,NULL),(76,'7C2730','莊凱淳','鋼堡',NULL,NULL,NULL),(77,'759688','李倚菘','得亨',NULL,NULL,NULL),(78,'798862','林武','得亨',NULL,NULL,NULL),(79,'7B4072','蔡宜蓁','得亨',NULL,NULL,NULL),(80,'7A2266','謝道登','得亨',NULL,NULL,NULL),(81,'7A8355','陳信宏','得亨',NULL,NULL,NULL),(82,'785308','于振洋','得亨',NULL,NULL,NULL),(85,'747828','羅介成','富泰',NULL,NULL,NULL),(86,'729871','李鴻騰','富泰',NULL,NULL,NULL),(87,'767278','趙中丕','富泰',NULL,NULL,NULL),(88,'7A3703','呂玉獎','富泰',NULL,NULL,NULL);
/*!40000 ALTER TABLE `c349member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cover`
--

DROP TABLE IF EXISTS `cover`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cover` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` date NOT NULL,
  `timeA` varchar(45) DEFAULT NULL,
  `timeB` varchar(45) DEFAULT NULL,
  `ask4leave` varchar(45) DEFAULT NULL,
  `coverA` varchar(45) DEFAULT NULL,
  `coverB` varchar(45) DEFAULT NULL,
  `comment` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cover`
--

LOCK TABLES `cover` WRITE;
/*!40000 ALTER TABLE `cover` DISABLE KEYS */;
INSERT INTO `cover` VALUES (1,'2021-10-22','07-11',NULL,'蘇天生 ','無',NULL,'dfds'),(2,'2021-10-22','15-16',NULL,'無','無',NULL,NULL),(3,'2021-10-28','15-19',NULL,'無','無',NULL,NULL),(14,'2021-09-28',NULL,NULL,NULL,NULL,NULL,NULL),(15,'2021-11-26','15-19','19-23','朱紀維','蘇天生 ','許誠鑜','上課(取消)'),(16,'2021-11-29','15-19','19-23','朱紀維','張清裕 ','蘇天生 ','上課(取消)'),(17,'2021-10-01',NULL,NULL,NULL,NULL,NULL,NULL),(18,'2021-11-30','15-23',NULL,'朱紀維','許誠鑜',NULL,'上課(取消)'),(19,'2021-12-04','07-15',NULL,'朱紀維','蘇天生 ',NULL,'休假'),(20,'2021-09-27',NULL,NULL,NULL,NULL,NULL,NULL),(21,'2021-12-05','07-11','11-15','朱紀維','張清裕 ','許誠鑜','請假'),(22,'2021-12-06','07-11',NULL,'朱紀維','張清裕 ',NULL,'請假'),(23,'2021-09-28',NULL,NULL,NULL,NULL,NULL,NULL),(24,'2021-11-05','07-09',NULL,'周品宏 ','無',NULL,'請假2小時'),(25,'2021-11-10','07-11',NULL,'張清裕 ','蘇天生 ',NULL,'急救回訓'),(26,'2021-11-10','07-11',NULL,'呂昆龍','江金地',NULL,'急救回訓'),(27,'2021-11-12','07-11',NULL,'朱紀維','蘇天生 ',NULL,'天車回訓'),(28,'2021-11-21','07-11','11-15','許誠鑜','朱紀維','張清裕 ','請假'),(29,'2021-12-11','07-11','11-15','賴丙鈞','黃喆洺','呂昆龍','喪假'),(30,'2021-12-11','07-11','11-15','許誠鑜','薛同明','張清裕','請假'),(31,'2021-12-16','07-11','11-15','賴丙鈞','江金地','黃喆洺','喪假'),(32,'2021-12-17','07-11','11-15','賴丙鈞','江金地','江金地','喪假'),(33,'2021-12-31',NULL,NULL,NULL,NULL,NULL,NULL),(34,'2021-12-06',NULL,NULL,NULL,NULL,NULL,NULL),(35,'2021-12-16','15-16',NULL,'朱紀維','蘇天生 ',NULL,'討論尾牙'),(36,'2021-12-23','07-15',NULL,'朱紀維',NULL,NULL,NULL),(37,'2021-12-24','07-15',NULL,'朱紀維',NULL,NULL,NULL),(38,'2022-01-04',NULL,NULL,NULL,NULL,NULL,NULL),(39,'2022-01-19',NULL,NULL,NULL,NULL,NULL,NULL),(40,'2022-01-19',NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `cover` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historical_mail`
--

DROP TABLE IF EXISTS `historical_mail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historical_mail` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `SenderID` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `SenderName` varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `SendDate` date DEFAULT NULL,
  `Subject` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Link` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=big5;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historical_mail`
--

LOCK TABLES `historical_mail` WRITE;
/*!40000 ALTER TABLE `historical_mail` DISABLE KEYS */;
INSERT INTO `historical_mail` VALUES (1,'197459','黃洺','2020-03-24','109年度外僱廠內油壓吊車共同作業協議組織會議（C349回覆）','<td>寄件人 :  197459 黃�梤y 2020.03.24 11:22:12 AM</br>收件人 :  中鋼165936  ;   </br>副本 :  中鋼153155謝明夏  ; 中鋼110593江金地  ; 中鋼143800呂昆龍  ; 中鋼195347柯宗佑  ; 中鋼214585賴丙鈞  ;   </br>密件副本 :    </br>附件 :  C349危害告知_外人-001-01.doc </br>本文 :  </br><FONT face=微軟正黑體>全成好&nbsp;&nbsp;&nbsp; 收信平安：<BR><BR>敬附C349最新版危害告知如附件。<BR><BR><BR>C349&nbsp;&nbsp;&nbsp; 黃?洺 敬上 #5731</FONT></br></td></tr>','D:\\214585\\HistoryMail\\20200324-112212-109年度外僱廠內油壓吊車共同作業協議組織會議（C349回覆）\\本文.html'),(2,'153155','謝明夏','2020-03-24','內外銷出貨流程','<td>寄件人 :  153155 謝明夏 2020.03.24 12:34:48 AM</br>收件人 :  中鋼214585賴丙鈞  ;   </br>副本 :    </br>密件副本 :    </br>附件 :  內外銷出貨相關流程與作業.pptx </br>本文 :  </br></br></td></tr>','D:\\214585\\HistoryMail\\20200324-123448-內外銷出貨流程\\本文.html'),(3,'195347','柯宗佑','2020-03-24','http：��dept.csc.com.tw�MIS�C34�C34Web�C34X.aspx','<td>寄件人 :  195347 柯宗佑 2020.03.24 3:11:08 PM</br>收件人 :  中鋼214585  ;   </br>副本 :    </br>密件副本 :    </br>附件 :  </br>本文 :  </br></br></td></tr>','D:\\214585\\HistoryMail\\20200324-151108-http：／／dept.csc.com.tw／MIS／C34／C34Web／C34X.aspx\\本文.html');
/*!40000 ALTER TABLE `historical_mail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stock`
--

DROP TABLE IF EXISTS `stock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` datetime DEFAULT NULL,
  `weight` int DEFAULT NULL,
  `count` int DEFAULT NULL,
  `warning` int DEFAULT '46500',
  `hard` int DEFAULT '56000',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock`
--

LOCK TABLES `stock` WRITE;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turncalendar`
--

DROP TABLE IF EXISTS `turncalendar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turncalendar` (
  `id` int NOT NULL,
  `EmployeeID` varchar(6) DEFAULT NULL,
  `Day1` varchar(45) DEFAULT NULL,
  `Day2` varchar(45) DEFAULT NULL,
  `Day3` varchar(45) DEFAULT NULL,
  `Day4` varchar(45) DEFAULT NULL,
  `Day5` varchar(45) DEFAULT NULL,
  `Day6` varchar(45) DEFAULT NULL,
  `Day7` varchar(45) DEFAULT NULL,
  `Day8` varchar(45) DEFAULT NULL,
  `Day9` varchar(45) DEFAULT NULL,
  `Day10` varchar(45) DEFAULT NULL,
  `Day11` varchar(45) DEFAULT NULL,
  `Day12` varchar(45) DEFAULT NULL,
  `Day13` varchar(45) DEFAULT NULL,
  `Day14` varchar(45) DEFAULT NULL,
  `Day15` varchar(45) DEFAULT NULL,
  `Day16` varchar(45) DEFAULT NULL,
  `Day17` varchar(45) DEFAULT NULL,
  `Day18` varchar(45) DEFAULT NULL,
  `Day19` varchar(45) DEFAULT NULL,
  `Day20` varchar(45) DEFAULT NULL,
  `Day21` varchar(45) DEFAULT NULL,
  `Day22` varchar(45) DEFAULT NULL,
  `Day24` varchar(45) DEFAULT NULL,
  `Day25` varchar(45) DEFAULT NULL,
  `Day26` varchar(45) DEFAULT NULL,
  `Day27` varchar(45) DEFAULT NULL,
  `Day28` varchar(45) DEFAULT NULL,
  `Day29` varchar(45) DEFAULT NULL,
  `Day30` varchar(45) DEFAULT NULL,
  `Day31` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idnew_table_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turncalendar`
--

LOCK TABLES `turncalendar` WRITE;
/*!40000 ALTER TABLE `turncalendar` DISABLE KEYS */;
/*!40000 ALTER TABLE `turncalendar` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vaccination`
--

DROP TABLE IF EXISTS `vaccination`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vaccination` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `first` varchar(45) DEFAULT NULL,
  `second` varchar(45) DEFAULT NULL,
  `third` varchar(45) DEFAULT NULL,
  `plan` varchar(45) DEFAULT NULL,
  `company` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idVaccination_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vaccination`
--

LOCK TABLES `vaccination` WRITE;
/*!40000 ALTER TABLE `vaccination` DISABLE KEYS */;
INSERT INTO `vaccination` VALUES (1,'謝明夏','10/29','12/3','3/10',NULL,'中鋼'),(2,'莊琇雄','2/7','3/8',NULL,NULL,'中鋼'),(3,'呂昆龍','7/26','10/06','1/14','','中鋼'),(4,'張森裕','8/13','11/19','111/02/26',NULL,'中鋼'),(5,'張清裕','8/23','9/30','3/15',NULL,'中鋼'),(6,'江金地','7/26','10/07',NULL,NULL,'中鋼'),(7,'蘇天生','7/30','10/16','4/22',NULL,'中鋼'),(8,'周品宏','7/26','10/16','01/13','','中鋼'),(9,'黃喆洺','12/1','4/21',NULL,NULL,'中鋼'),(10,'朱紀維','9/12','12/6','3/24',NULL,'中鋼'),(11,'沈一郎','8/13','11/24','2/19',NULL,'中鋼'),(12,'賴丙鈞','9/9','01/11','4/26',NULL,'中鋼'),(13,'許誠鑜','7/27','10/18','已接種',NULL,'中鋼'),(14,'吳慶義','8/14','11/10','02/10','','中鋼'),(15,'陳永樹','7/30','10/21',NULL,'','中鋼'),(16,'薛同明','7/26','10/19','1/12',NULL,'中鋼'),(17,'陳琦方','7/24','11/02',NULL,NULL,'鋼堡'),(18,'羅介成','7/26','已施打','3/9',NULL,'富泰'),(19,'劉凱文','8/5','11/10','3/29',NULL,'鋼堡'),(20,'朱建收','7/26','9/17','111/03/02',NULL,'得亨'),(21,'梁正育','9/6','11/11',NULL,NULL,'鋼堡'),(23,' 林榆博','9/11','10/14',NULL,NULL,'鋼堡'),(24,' 嚴明正','9/10','11/20','111/03/01',NULL,'鋼堡'),(25,' 許秉鄴','8/26','9/30','1/20','','鋼堡'),(26,'蔡宜蓁','10/16','11/19','3/9',NULL,'得亨'),(27,'江永福','9/20','11/20','3/9',NULL,'得亨'),(28,' 謝道登','9/28','11/17','111/03/02',NULL,'得亨'),(29,' 陳信宏','9/11','10/22','111/03/07 BNT',NULL,'得亨'),(30,' 林武','10/22','',NULL,'反應不良，不可施打第二劑','得亨'),(31,'李倚菘','10/13','11/19','111/02/24',NULL,'得亨'),(32,'黃志傑','11/13','11/19',NULL,NULL,'鋼堡'),(33,'林晉瑋','10/16','11/25',NULL,NULL,'得亨'),(36,'陳永祥','9/11','10/23',NULL,NULL,'鋼堡'),(37,' 陳仁政','9/7','11/29','3/3',NULL,'鋼堡'),(38,' 蔡昀達','9/22','11/22',NULL,NULL,'鋼堡'),(39,'呂玉獎','已接種','已接種','2/20','','富泰'),(40,'傅永鈞','9/11','11/29','111/02/28',NULL,'鋼堡'),(42,' 李鴻騰','已接種','已接種',NULL,NULL,'富泰'),(43,' 趙中丕','已接種','已接種',NULL,NULL,'富泰'),(45,'林意貞','已接種','已接種','2/16','','富泰'),(47,'莊志鴻','已接踵','已接踵','2/24',NULL,'中鋼'),(48,' 謝松穎',NULL,NULL,NULL,NULL,'鋼堡'),(49,'劉治言','110/10/22','110/11/26','111/3/4',NULL,'中鋼'),(50,' ',NULL,NULL,NULL,NULL,NULL),(51,' ',NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `vaccination` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `work_order`
--

DROP TABLE IF EXISTS `work_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `work_order` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `keyInDate` date DEFAULT NULL,
  `keyInMan` varchar(100) DEFAULT NULL,
  `work_orderNum` varchar(8) DEFAULT NULL,
  `content` varchar(100) DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `work_order`
--

LOCK TABLES `work_order` WRITE;
/*!40000 ALTER TABLE `work_order` DISABLE KEYS */;
INSERT INTO `work_order` VALUES (1,'2021-06-11','我我我4','C349A123','eqweqweq','qweqweqw'),(2,'1990-01-01','2','3','4','5'),(3,'1990-01-01','2','3','4','5'),(4,'1990-01-01','2','3','4','5'),(5,'1990-01-01','2','3','4','5'),(6,'1991-01-02','6','58','h','ds');
/*!40000 ALTER TABLE `work_order` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-14 21:15:23
