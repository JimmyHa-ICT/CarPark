<?php 
	include_once("db.php");

	if (isset($_POST["username"]) && !empty($_POST["username"]) && 
		isset($_POST["password"]) && !empty($_POST["password"])){

		Login($_POST["username"], $_POST["password"]);
	}

	function Login($username, $password)
	{
		GLOBAL $con;

		$sql = "SELECT id,username FROM users WHERE username=? AND password=?";
		$st=$con->prepare($sql);

		$st->execute(array($username, sha1($password)));//encrypt password
		$all=$st->fetchAll();
		if (count($all) == 1){
			echo "SERVER: ID#".$all[0]["id"]." - ".$all[0]["username"];
			exit();
		}

		//if username or password are empty strings
		echo "SERVER: error, invalid username or password";
		exit();
	}

	function Signup($username, $password) {
		GLOBAL $con;
	
		// Input validation
		if (empty($username) || empty($password)) {
			echo "SERVER: error, invalid username or password";
			exit();
		}
	
		// Check if username already exists
		$sql = "SELECT id FROM users WHERE username=?";
		$st = $con->prepare($sql);
		$st->execute(array($username));
		$all = $st->fetchAll();
		if (count($all) > 0) {
			echo "SERVER: error, username already exists";
			exit();
		}
	
		// Insert new user
		$sql = "INSERT INTO users (username, password) VALUES (?, ?)";
		$st = $con->prepare($sql);
		$encryptedPassword = sha1($password); // Encrypt password
		$st->execute(array($username, $encryptedPassword));
	
		if ($st->rowCount() == 1) {
			echo "SERVER: Signup successful";
		} else {
			echo "SERVER: error, signup failed";
		}
	}

	function DeleteAccount($username, $password) {
		GLOBAL $con;
	
		// Input validation
		if (empty($username) || empty($password)) {
			echo "SERVER: error, invalid username or password";
			exit();
		}
	
		// Check if the user exists and verify the password
		$sql = "SELECT id, password FROM users WHERE username=?";
		$st = $con->prepare($sql);
		$st->execute(array($username));
		$user = $st->fetch();
	
		if ($user && password_verify($password, $user['password'])) {
			// Delete the user
			$sql = "DELETE FROM users WHERE id=?";
			$st = $con->prepare($sql);
			$st->execute(array($user['id']));
	
			if ($st->rowCount() == 1) {
				echo "SERVER: Account deleted successfully";
			} else {
				echo "SERVER: error, account deletion failed";
			}
		} else {
			echo "SERVER: error, invalid username or password";
		}
	}

	function ChangePassword($username, $currentPassword, $newPassword) {
		GLOBAL $con;
	
		// Input validation
		if (empty($username) || empty($currentPassword) || empty($newPassword)) {
			echo "SERVER: error, invalid input";
			exit();
		}
	
		// Check if the user exists and verify the current password
		$sql = "SELECT id FROM users WHERE username=? AND password=?";
		$st = $con->prepare($sql);
		$st->execute(array($username, sha1($currentPassword))); // Encrypt current password with SHA-1
		$user = $st->fetch();
	
		if ($user) {
			// Encrypt the new password with SHA-1
			$encryptedNewPassword = sha1($newPassword);
	
			// Update the user's password
			$sql = "UPDATE users SET password=? WHERE id=?";
			$st = $con->prepare($sql);
			$st->execute(array($encryptedNewPassword, $user['id']));
	
			if ($st->rowCount() == 1) {
				echo "SERVER: Password changed successfully";
			} else {
				echo "SERVER: error, password change failed";
			}
		} else {
			echo "SERVER: error, invalid username or current password";
		}
	}
	

	//if username or password is null (not set)
	echo "SERVER: error, enter a valid username & password";

	//exit():  means end server connection (don't execute the rest)
?>