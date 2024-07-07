<?php   
    $host   = "mysql:host=127.0.0.1;dbname=test";
	$user   = "root";
	$pass   = "";
	$option = array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8");
	try{
		$con = new PDO($host,$user, $pass, $option);
		$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}catch(PDOException $e){ echo $e; }
?>