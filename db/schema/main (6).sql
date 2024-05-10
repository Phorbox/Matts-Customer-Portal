SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;


CREATE TABLE `Clientele` (
  `Clienteleid` int UNSIGNED NOT NULL,
  `ClienteleName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RetentionLength` int UNSIGNED NOT NULL DEFAULT '90',
  `SlaDueDate` int UNSIGNED NOT NULL DEFAULT '48',
  `ParentId` int UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Clientele` (`Clienteleid`, `ClienteleName`, `RetentionLength`, `SlaDueDate`, `ParentId`) VALUES
(1, 'Corpus Christi', 120, 24, NULL),
(3, 'Southernpine', 10, 48, NULL),
(4, 'Texas Water', 90, 48, NULL),
(7, 'Linebarger', 90, 48, NULL),
(8, 'I66 Toll', 90, 48, 4),
(9, 'Parking Lot Tolls', 90, 48, 4);

CREATE TABLE `Input` (
  `Inputid` int UNSIGNED NOT NULL,
  `Filename` varchar(255) NOT NULL,
  `Jobid` int UNSIGNED NOT NULL,
  `StoragePriority` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'Cool',
  `InputPDF` longblob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Input` (`Inputid`, `Filename`, `Jobid`, `StoragePriority`, `InputPDF`) VALUES
(1, 'testdel.csv', 1, 'Cool', 0x322c322c312c322c50656e64696e6720417070726f76616c2c4e554c4c2c323032342d30352d30372032303a35353a31300d0a332c312c322c332c50726f63657373696e672c4e554c4c2c323032342d30352d30372032303a35353a31300d0a342c322c322c342c50656e64696e6720417070726f76616c2c4e554c4c2c323032342d30352d30372032303a35353a31300d0a352c312c332c352c50726f63657373696e672c4e554c4c2c323032342d30352d30372032303a35353a3130),
(2, 'test2.csv', 55, 'Cool', 0x322c322c312c322c50656e64696e6720417070726f76616c2c4e554c4c2c323032342d30352d30372032303a35353a31300d0a332c312c322c332c50726f63657373696e672c4e554c4c2c323032342d30352d30372032303a35353a31300d0a342c322c322c342c50656e64696e6720417070726f76616c2c4e554c4c2c323032342d30352d30372032303a35353a31300d0a352c312c332c352c50726f63657373696e672c4e554c4c2c323032342d30352d30372032303a35353a3130);

CREATE TABLE `Job` (
  `Jobid` int UNSIGNED NOT NULL,
  `Projectid` int UNSIGNED NOT NULL,
  `Clienteleid` int UNSIGNED NOT NULL,
  `Inputid` int UNSIGNED NOT NULL,
  `Status` varchar(128) NOT NULL DEFAULT 'received',
  `DateApproved` datetime DEFAULT NULL,
  `DueDate` datetime DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Job` (`Jobid`, `Projectid`, `Clienteleid`, `Inputid`, `Status`, `DateApproved`, `DueDate`, `DateCreated`) VALUES
(1, 1, 1, 1, 'received', NULL, '2024-05-07 20:55:10', '2024-05-06 01:56:01'),
(55, 2, 1, 2, 'received', NULL, NULL, '2024-05-06 01:59:35'),
(56, 2, 1, 100, 'received', NULL, NULL, '2024-05-06 02:01:21');

CREATE TABLE `Project` (
  `Projectid` int UNSIGNED NOT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Clienteleid` int UNSIGNED NOT NULL,
  `SlaOveride` int UNSIGNED DEFAULT NULL,
  `Approval` varchar(64) NOT NULL DEFAULT 'Approval'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Project` (`Projectid`, `ProjectName`, `Clienteleid`, `SlaOveride`, `Approval`) VALUES
(1, 'statements', 1, NULL, 'Approval'),
(2, 'dq', 1, NULL, 'Approval'),
(3, 'statements', 3, NULL, 'Approval');


ALTER TABLE `Clientele`
  ADD PRIMARY KEY (`Clienteleid`),
  ADD UNIQUE KEY `Name` (`ClienteleName`),
  ADD KEY `ParentId` (`ParentId`);

ALTER TABLE `Input`
  ADD PRIMARY KEY (`Inputid`),
  ADD KEY `Jobid` (`Jobid`),
  ADD KEY `Filename` (`Filename`),
  ADD KEY `Storage Priority` (`StoragePriority`);

ALTER TABLE `Job`
  ADD PRIMARY KEY (`Jobid`),
  ADD KEY `Status` (`Status`),
  ADD KEY `Projectid` (`Projectid`),
  ADD KEY `Clienteleid` (`Clienteleid`),
  ADD KEY `Inputid` (`Inputid`),
  ADD KEY `DateCreated` (`DateCreated`);

ALTER TABLE `Project`
  ADD PRIMARY KEY (`Projectid`),
  ADD KEY `Clienteleid` (`Clienteleid`),
  ADD KEY `Name` (`ProjectName`) USING BTREE;


ALTER TABLE `Clientele`
  MODIFY `Clienteleid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

ALTER TABLE `Input`
  MODIFY `Inputid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

ALTER TABLE `Job`
  MODIFY `Jobid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;

ALTER TABLE `Project`
  MODIFY `Projectid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
