-- phpMyAdmin SQL Dump
-- version 4.8.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 27, 2020 at 11:20 PM
-- Server version: 10.1.32-MariaDB
-- PHP Version: 5.6.36

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `canteenpos`
--

-- --------------------------------------------------------

--
-- Table structure for table `inventory`
--

CREATE TABLE `inventory` (
  `Product ID` int(10) NOT NULL,
  `Product Name` varchar(30) NOT NULL,
  `Description` varchar(50) NOT NULL,
  `Quantity` int(10) NOT NULL,
  `Date added` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inventory`
--

INSERT INTO `inventory` (`Product ID`, `Product Name`, `Description`, `Quantity`, `Date added`) VALUES
(2001, 'Beans', '500g Packets', 103, '2020-07-20'),
(2002, 'Rice', '25kg Bag', 12, '2020-07-01'),
(2003, 'Royco ', '500g Satchets', 54, '2020-07-02'),
(2004, 'Mealie Meal', '50kg Bag', 6, '2020-07-23'),
(2005, 'Eggs', 'Crate', 30, '2020-07-01'),
(1234567891, 'Cooking Oil', '2l Bottles', 47, '2020-07-27');

-- --------------------------------------------------------

--
-- Table structure for table `meal_management`
--

CREATE TABLE `meal_management` (
  `id` int(10) NOT NULL,
  `Meal` varchar(50) NOT NULL,
  `Price` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `meal_management`
--

INSERT INTO `meal_management` (`id`, `Meal`, `Price`) VALUES
(1001, 'Rice Beef', 57),
(1002, 'Rice Chicken', 62),
(1003, 'Sadza Chicken', 57),
(1004, 'Sadza Beef', 52),
(1005, 'Sadza Beans', 46),
(1006, 'Rice Beans', 55),
(1007, 'Sadza Vegetables', 36),
(1012, '330ml Coke', 40);

-- --------------------------------------------------------

--
-- Table structure for table `sales_products`
--

CREATE TABLE `sales_products` (
  `id` int(10) NOT NULL,
  `Meal` varchar(50) NOT NULL,
  `quantity` int(10) NOT NULL,
  `price` int(10) NOT NULL,
  `total` int(10) NOT NULL,
  `date` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `sales_report`
--

CREATE TABLE `sales_report` (
  `id` int(10) NOT NULL,
  `Meal` varchar(50) NOT NULL,
  `quantity_sold` int(100) NOT NULL,
  `total_sales` int(10) NOT NULL,
  `date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sales_report`
--

INSERT INTO `sales_report` (`id`, `Meal`, `quantity_sold`, `total_sales`, `date`) VALUES
(1001, 'Rice Beef', 23, 575, '2020-07-05'),
(1002, 'Rice Chicken', 34, 986, '2020-07-05'),
(1003, 'Sadza Chicken', 784, 19600, '2020-07-05'),
(1004, 'Sadza Beef', 23, 460, '2020-07-05'),
(1006, 'Rice Beans', 232, 3248, '2020-07-05'),
(1008, 'Rice Soup', 100, 600, '2020-07-05'),
(1001, 'Rice Beef', 23, 575, '2020-07-20'),
(1002, 'Rice Chicken', 123, 3567, '2020-07-20'),
(1003, 'Sadza Chicken', 234, 5850, '2020-07-20'),
(1004, 'Sadza Beef', 526, 10520, '2020-07-20'),
(1006, 'Rice Beans', 1623, 48690, '2020-07-20'),
(1008, 'Rice Soup', 798, 23940, '2020-07-20'),
(1001, 'Rice Beef', 35, 1199, '2020-07-23'),
(1002, 'Rice Chicken', 12, 348, '2020-07-23'),
(1003, 'Sadza Chicken', 35, 875, '2020-07-23'),
(1004, 'Sadza Beef', 567, 11340, '2020-07-23'),
(1007, 'Sadza Vegetables', 56, 3136, '2020-07-23'),
(1001, 'Rice Beef', 113, 5876, '2020-07-25'),
(1002, 'Rice Chicken', 213, 6177, '2020-07-25'),
(1003, 'Sadza Chicken', 34, 850, '2020-07-25'),
(1004, 'Sadza Beef', 79, 1580, '2020-07-25'),
(1005, 'Sadza Beans', 87, 4002, '2020-07-25'),
(1006, 'Rice Beans', 47, 1410, '2020-07-25'),
(1007, 'Sadza Vegetables', 13, 728, '2020-07-25'),
(1008, 'Rice Soup', 120, 3600, '2020-07-25'),
(1001, 'Rice Beef', 301, 17157, '2020-07-27'),
(1002, 'Rice Chicken', 127, 7874, '2020-07-27'),
(1003, 'Sadza Chicken', 137, 7809, '2020-07-27'),
(1004, 'Sadza Beef', 68, 3536, '2020-07-27'),
(1005, 'Sadza Beans', 148, 6808, '2020-07-27'),
(1006, 'Rice Beans', 113, 5198, '2020-07-27'),
(1007, 'Sadza Vegetables', 104, 3744, '2020-07-27'),
(1008, 'Rice Soup', 34, 1224, '2020-07-27'),
(1001, 'Rice Beef', 34, 1938, '2020-07-27'),
(1006, 'Rice Beans', 56, 2576, '2020-07-27'),
(1001, 'Rice Beef', 34, 1938, '2020-07-27'),
(1002, 'Rice Chicken', 0, 0, '2020-07-27'),
(1003, 'Sadza Chicken', 0, 0, '2020-07-27'),
(1004, 'Sadza Beef', 34, 1768, '2020-07-27'),
(1005, 'Sadza Beans', 0, 0, '2020-07-27'),
(1006, 'Rice Beans', 0, 0, '2020-07-27'),
(1007, 'Sadza Vegetables', 0, 0, '2020-07-27'),
(1008, 'Rice Soup', 0, 0, '2020-07-27'),
(1012, '330ml Coke', 0, 0, '2020-07-27');

-- --------------------------------------------------------

--
-- Table structure for table `used_inventory`
--

CREATE TABLE `used_inventory` (
  `id` int(10) NOT NULL,
  `Product Name` varchar(30) NOT NULL,
  `Description` varchar(50) NOT NULL,
  `Quantity` int(10) NOT NULL,
  `Date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `used_inventory`
--

INSERT INTO `used_inventory` (`id`, `Product Name`, `Description`, `Quantity`, `Date`) VALUES
(2001, 'Beans', '500g Satchets', 12, '2020-07-07'),
(2001, 'Beans', '500g Satchets', 15, '2020-07-08'),
(2003, 'Royco', '500g Satchets', 2, '2020-06-23'),
(2003, 'Royco ', '500g Satchets', 10, '2020-07-23'),
(2001, 'Beans', '500g Packets', 20, '2020-07-23'),
(1234567890, 'Magarine', '500g Stock', 100, '2020-07-25'),
(1234567891, 'Cooking Oil', '2l Bottles', 3, '2020-07-27');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Employee ID` int(10) NOT NULL,
  `Fullname` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Username` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Employee ID`, `Fullname`, `Email`, `Username`, `Password`) VALUES
(1000, 'Denzel Chiuseni', 'h190672r@hit.ac.zw', 'h190672r', 'dchiuseni'),
(1003, 'Shabour Banda', 'h190420y@hit.ac.zw', 'h190420y', 'sbanda'),
(1004, 'Miriam Chanda', 'h190558q@hit.ac.zw', 'h190558q', 'mchanda'),
(1005, 'Leanah Chavhunduka', 'h190698g@hit.ac.zw', 'h190698g', 'lchavhunduka'),
(1006, 'Marshal Makuti', 'h190397c@hit.ac.zw', 'h190397c', 'mmakuti'),
(1007, 'Clayton Sibanda', 'h190275a@hit.ac.zw', 'h190275a', 'csibanda'),
(1010, 'Tafadzwa Govere', 'h190677e@hit.ac.zw', 'h190677e', 'tgovere'),
(1011, 'Tendai Dzvatsva', 'h190385@hit.ac.zw', 'h190385t', 'tdzvatsva'),
(1012, 'Mutsawashe Nemhara', 'h190519z@hit.ac.zw', 'h190519z', 'mnemhara'),
(1013, 'Tanaka	Mwenga', 'h170428e@hit.ac.zw', 'h170428e', 'tmwenga'),
(1014, 'Clive Munyoro', 'h180210n@hit.ac.zw', 'h180210n', 'cmunyoro'),
(1015, 'Michael Meuka', 'h190386p@hit.ac.zw', 'h190386p', 'mmeuka');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `inventory`
--
ALTER TABLE `inventory`
  ADD PRIMARY KEY (`Product ID`);

--
-- Indexes for table `meal_management`
--
ALTER TABLE `meal_management`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Employee ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `inventory`
--
ALTER TABLE `inventory`
  MODIFY `Product ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1234567892;

--
-- AUTO_INCREMENT for table `meal_management`
--
ALTER TABLE `meal_management`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1014;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Employee ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1016;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
