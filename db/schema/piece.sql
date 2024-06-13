SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

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

INSERT INTO `Job` (`Jobid`, `Projectid`, `Clientid`, `Inputid`, `Status`, `DateApproved`, `DueDate`, `DateCreated`) VALUES
(1, 1, 1, 1, 'received', NULL, '2024-05-07 20:55:10', '2024-05-06 01:56:01'),
(55, 2, 1, 55, 'received', NULL, NULL, '2024-05-06 01:59:35'),
(56, 2, 1, 100, 'received', NULL, NULL, '2024-05-06 02:01:21');

ALTER TABLE `Job`
  ADD PRIMARY KEY (`Jobid`);

ALTER TABLE `Job`
  MODIFY `Jobid` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;
COMMIT;
