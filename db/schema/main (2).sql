-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: db:3306
-- Generation Time: May 06, 2024 at 05:56 AM
-- Server version: 8.4.0
-- PHP Version: 8.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `main`
--

-- --------------------------------------------------------

--
-- Table structure for table `Workorder`
--

CREATE TABLE `Workorder` (
  `Workorderid` int UNSIGNED NOT NULL,
  `Jobid` int UNSIGNED NOT NULL,
  `Clientid` int UNSIGNED NOT NULL,
  `Inputid` int UNSIGNED NOT NULL,
  `Status` varchar(128) NOT NULL DEFAULT 'received',
  `DateApproved` datetime DEFAULT NULL,
  `DueDate` datetime DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Workorder`
--

INSERT INTO `Workorder` (`Workorderid`, `Jobid`, `Clientid`, `Inputid`, `Status`, `DateApproved`, `DueDate`, `DateCreated`) VALUES
(1, 1, 1, 1, 'received', NULL, '2024-05-07 20:55:10', '2024-05-06 01:56:01'),
(55, 2, 1, 55, 'received', NULL, NULL, '2024-05-06 01:59:35'),
(56, 2, 1, 100, 'received', NULL, NULL, '2024-05-06 02:01:21');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Workorder`
--
ALTER TABLE `Workorder`
  ADD PRIMARY KEY (`Workorderid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Workorder`
--
ALTER TABLE `Workorder`
  MODIFY `Workorderid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
