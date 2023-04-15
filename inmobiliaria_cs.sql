-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-04-2023 a las 23:37:57
-- Versión del servidor: 10.4.25-MariaDB
-- Versión de PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria_cs`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL,
  `fechaInicio` datetime NOT NULL,
  `fechaFinalizacion` datetime NOT NULL,
  `id_precio` int(11) NOT NULL,
  `id_inmueble` int(11) NOT NULL,
  `id_inquilino` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`id_contrato`, `fechaInicio`, `fechaFinalizacion`, `id_precio`, `id_inmueble`, `id_inquilino`) VALUES
(1, '2023-04-14 00:00:00', '2023-04-28 00:00:00', 1, 5, 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `precio` double NOT NULL,
  `cantAmbientes` int(11) NOT NULL,
  `latitud` int(11) NOT NULL,
  `longitud` int(11) NOT NULL,
  `id_propietario` int(11) NOT NULL,
  `id_tipo` int(11) NOT NULL,
  `id_uso` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id_inmueble`, `direccion`, `precio`, `cantAmbientes`, `latitud`, `longitud`, `id_propietario`, `id_tipo`, `id_uso`) VALUES
(1, 'San Martin 777', 450000, 3, -33, -62, 3, 1, 1),
(2, 'Santa Fe 599', 12999, 3, 116, 206, 1, 3, 1),
(4, 'San Martin 888', 675888, 2, 31, -35, 3, 2, 2),
(5, 'Decandencia 899', 123999, 2, 12456664, 12457574, 5, 2, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `Id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(10) NOT NULL,
  `domicilio` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`Id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono`) VALUES
(2, 'Diego', 'Orellano', '12345678', 'Rivadavia 560', '0987348756'),
(3, 'Marcelo', 'Bustos', '98457683', 'Sarmiento 540', '8934758693'),
(4, 'Mariano', 'Martines', '48375968', 'Belgrano 450', '03498673849'),
(5, 'Pepe', 'Patriada', '38947568', 'Amsterdam 596', '908737468'),
(7, 'Horacio', 'Quiroga', '6178235', 'Quinteros 849', '23478789097'),
(8, 'Jeremy', 'Renner', '12348576', 'Fararrow 890', '0983289404'),
(9, 'Carlos', 'Cornejo', '99865273', 'Noruega 349', '976388920');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL,
  `nroPago` int(11) NOT NULL,
  `importe` double NOT NULL,
  `fecha` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`id_pago`, `nroPago`, `importe`, `fecha`) VALUES
(1, 1, 34999, '2023-04-14 23:07:57');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(10) NOT NULL,
  `domicilio` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `id_inmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono`, `id_inmueble`) VALUES
(1, 'Mauricio', 'Ferrieres', '12345678', 'Volcan 540', '23498775', 2),
(2, 'Edder', 'Santibañez', '23456789', 'San Luis 234', '29859335', 3),
(3, 'Matias', 'Diaz', '87654321', 'V Mercedes 5660', '3456775', 7),
(4, 'Diego', 'Orellano', '98765432', 'Veloisa 2884', '89982372', 9),
(5, 'Osvaldo', 'Cabiado', '16306789', 'Amsterdam 596', '0987346453', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipos`
--

CREATE TABLE `tipos` (
  `id_tipo` int(11) NOT NULL,
  `descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipos`
--

INSERT INTO `tipos` (`id_tipo`, `descripcion`) VALUES
(1, 'Casa'),
(2, 'Departamento'),
(3, 'Deposito'),
(4, 'Local');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usos`
--

CREATE TABLE `usos` (
  `id_uso` int(11) NOT NULL,
  `descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usos`
--

INSERT INTO `usos` (`id_uso`, `descripcion`) VALUES
(1, 'Comercial'),
(2, 'Residencial');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id_contrato`),
  ADD KEY `id_precio` (`id_precio`,`id_inmueble`,`id_inquilino`),
  ADD KEY `id_inmueble` (`id_inmueble`),
  ADD KEY `id_inquilino` (`id_inquilino`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id_inmueble`),
  ADD KEY `id_propietario` (`id_propietario`,`id_tipo`,`id_uso`),
  ADD KEY `id_uso` (`id_uso`),
  ADD KEY `id_tipo` (`id_tipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id_pago`),
  ADD KEY `importe` (`importe`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `tipos`
--
ALTER TABLE `tipos`
  ADD PRIMARY KEY (`id_tipo`);

--
-- Indices de la tabla `usos`
--
ALTER TABLE `usos`
  ADD PRIMARY KEY (`id_uso`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `tipos`
--
ALTER TABLE `tipos`
  MODIFY `id_tipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usos`
--
ALTER TABLE `usos`
  MODIFY `id_uso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id_inmueble`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`Id`),
  ADD CONSTRAINT `contrato_ibfk_4` FOREIGN KEY (`id_precio`) REFERENCES `pago` (`id_pago`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`id_uso`) REFERENCES `usos` (`id_uso`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`id_tipo`) REFERENCES `tipos` (`id_tipo`),
  ADD CONSTRAINT `inmueble_ibfk_3` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
