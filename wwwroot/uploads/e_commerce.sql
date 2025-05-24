-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : ven. 11 avr. 2025 à 07:51
-- Version du serveur : 9.1.0
-- Version de PHP : 8.3.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `e_commerce`
--

-- --------------------------------------------------------

--
-- Structure de la table `adress`
--

DROP TABLE IF EXISTS `adress`;
CREATE TABLE IF NOT EXISTS `adress` (
  `adress_id` int NOT NULL AUTO_INCREMENT,
  `street_number` int NOT NULL,
  `street_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `postal_code` int NOT NULL,
  `country` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone` varchar(225) COLLATE utf8mb4_unicode_ci NOT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`adress_id`),
  KEY `fk_adress_user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `birthdays`
--

DROP TABLE IF EXISTS `birthdays`;
CREATE TABLE IF NOT EXISTS `birthdays` (
  `id_birthday` int NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL,
  `date_birthday` date NOT NULL,
  PRIMARY KEY (`id_birthday`),
  KEY `fk_birthdays_users` (`id_user`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `category_product`
--

DROP TABLE IF EXISTS `category_product`;
CREATE TABLE IF NOT EXISTS `category_product` (
  `category_id` int NOT NULL AUTO_INCREMENT,
  `nom_category` varchar(100) NOT NULL,
  `description` varchar(500) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `category_product`
--

INSERT INTO `category_product` (`category_id`, `nom_category`, `description`) VALUES
(1, 'Naruto', 'bien'),
(2, 'Blue Lock', 'Incroyable 10/10'),
(3, 'Demon Slayer', 'Je l\'aime'),
(4, 'One Piece', 'Mon GOAT'),
(5, 'RE ZEro:', 'Mon GOAT');

-- --------------------------------------------------------

--
-- Structure de la table `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `id_orders` int NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL,
  `order_item_id` int NOT NULL,
  PRIMARY KEY (`id_orders`),
  KEY `fk_user_orders_id` (`id_user`),
  KEY `fk_orders_item_orders_id` (`order_item_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `order_item`
--

DROP TABLE IF EXISTS `order_item`;
CREATE TABLE IF NOT EXISTS `order_item` (
  `orders_item_id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `statue` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `payment_method` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `orders_date` date NOT NULL,
  PRIMARY KEY (`orders_item_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `product`
--

DROP TABLE IF EXISTS `product`;
CREATE TABLE IF NOT EXISTS `product` (
  `product_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` varchar(500) COLLATE utf8mb4_unicode_ci NOT NULL,
  `price` decimal(4,2) NOT NULL,
  `stock_quantity` int NOT NULL,
  `image_url` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `category_id` int DEFAULT NULL,
  PRIMARY KEY (`product_id`),
  KEY `fk_product_categories` (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `product`
--

INSERT INTO `product` (`product_id`, `name`, `description`, `price`, `stock_quantity`, `image_url`, `category_id`) VALUES
(1, 'Blue Lock saga 1', '', 2.00, 30, '', 2),
(2, 'Demon Slayer saga 1', '', 15.00, 50, '', 3),
(3, 'Dragon Ball saga 1', '', 7.99, 100, '', 3),
(4, 'Naruto Saga 1', '', 5.98, 45, '', 1),
(5, 'RE ZEro ', '', 8.99, 200, '', 1);

-- --------------------------------------------------------

--
-- Structure de la table `shoppingcart`
--

DROP TABLE IF EXISTS `shoppingcart`;
CREATE TABLE IF NOT EXISTS `shoppingcart` (
  `id_shopping_cart` int NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL,
  `cart_item_id` int NOT NULL,
  PRIMARY KEY (`id_shopping_cart`),
  KEY `fk_shoppingcart` (`cart_item_id`),
  KEY `fk_shoppingcart_id` (`id_user`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `shoppingcart_items`
--

DROP TABLE IF EXISTS `shoppingcart_items`;
CREATE TABLE IF NOT EXISTS `shoppingcart_items` (
  `cart_item_id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `date_panier` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`cart_item_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `last_login` date NOT NULL,
  `username` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `statue` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`user_id`, `email`, `password`, `last_login`, `username`, `statue`) VALUES
(6, 'azerty@gmail.com', '$2y$10$0H497.pYm1Gy3GCM77lKyug27M6wZMZSmCZ7l2lUSwX4iZhAkMep6', '0000-00-00', 'Sami mtimet', 'en ligne'),
(7, 'azerty@gmail.com', '$2y$10$1VDHL9i7Wg1k2btyV49pVuHF1TtL0GEXNcAVX3LVdn2ij6Y3T4sQW', '0000-00-00', 'Sami mtimet', 'en ligne'),
(8, 'samimtimet08@gmail.com', '$2y$10$3tifusrfLA9T4Vnd0S06WOyOTtikKhf.6qlgDn6ULhep/FOpM9lo2', '0000-00-00', 'Sami mtimet', 'en ligne'),
(9, 'samimtimet89@gmail.com', '$2y$10$9304ecVwoOhksmtEFsqNE.ulKT3hyblXKOvdfaX.qWkdK5J/x6tDa', '0000-00-00', 'test1 mtimet', 'en ligne'),
(10, 'sami.mtimet4@gmail.com', '$2y$10$EFDywqiYynPsYverxWuI9u07AroXjvfNFY2UZA/UPbYn0paWgl9Pq', '0000-00-00', 'Sami mtimet', 'en ligne'),
(11, 'sami.mtimet4@gmail.com', '$2y$10$Su3N4BNZpihTSXJ6GA1ELeWKHU5GrgnQjZHrg.rFG/HlVDYhtQapC', '0000-00-00', 'Sami mtimet', 'en ligne'),
(12, 'charles@gmail.com', '$2y$10$K9j76T.BuXNL.E/iKXUxbOddHnFDugHkZJ1xd/1ATRErNRTVcgfc2', '0000-00-00', 'Sami mtimet', 'en ligne'),
(13, 'samimtimet08@gmail.com', '$2y$10$ZG/n.S56Rq.igYHwIgqp4.UVu0uhqtSyQfC/Z0NBBtrzdGTUI7w86', '0000-00-00', 'Sami mtimet', 'en ligne'),
(14, 'charles@gmail.com', '$2y$10$PhFpUng5sL452e6WxVYm4OqO/w4az/6L8JiEinm2rPiP5wXbBJjFG', '0000-00-00', 'Sami mtimet', 'en ligne'),
(15, 'jean080@gmail.com', '$2y$10$8z87.Kt4aNpo.Mq8O9n3OeACEJly0Q8p.s94hKCmhRzgeuVxJ9lgO', '0000-00-00', 'jean dupont', 'en ligne'),
(16, 'cassecroute@gmail.com', '$2y$10$OroewUxu7YvVnqNBIW2EQ.WRD9XEaUAlLBOC.NNPwjg94XHxF233G', '0000-00-00', 'micheal mtimet', 'en ligne');

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `adress`
--
ALTER TABLE `adress`
  ADD CONSTRAINT `fk_adress_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Contraintes pour la table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `fk_product_categories` FOREIGN KEY (`category_id`) REFERENCES `category_product` (`category_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
