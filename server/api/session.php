<?php 
	include_once("db.php");

	if (isset($_POST["username"]) && !empty($_POST["username"]) && 
		isset($_POST["start_time"]) && !empty($_POST["start_time"]) &&
        isset($_POST["end_time"]) && !empty($_POST["end_time"]) &&
        isset($_POST["result"]) && !empty($_POST["result"])){

		InsertSession($_POST["username"], $_POST["start_time"], $_POST["end_time"], $_POST["result"]);
	}


	function InsertSession($username, $startTime, $endTime, $result) {
		GLOBAL $con;
	
		// Input validation
		if (empty($username) || empty($startTime) || empty($endTime) || empty($result)) {
			echo "SERVER: error, invalid input";
			exit();
		}
	
		// Check if username exists
		$sql = "SELECT ID FROM User WHERE Username=?";
		$st = $con->prepare($sql);
		$st->execute(array($username));
		$all = $st->fetchAll();
		if (count($all) == 0) {
			echo "SERVER: error, username not found";
			exit();
		}
	
		$userId = $all[0]['ID'];
	
		// Insert new session
		$sql = "INSERT INTO Session (UserID, StartTime, EndTime, Result) VALUES (?, ?, ?, ?)";
		$st = $con->prepare($sql);
		$st->execute(array($userId, $startTime, $endTime, $result));
	
		if ($st->rowCount() == 1) {
			$sessionId = $con->lastInsertId();
			echo "SERVER: Session insertion successful, SessionID: " . $sessionId;
			return $sessionId;
		} else {
			echo "SERVER: error, session insertion failed";
		}
	}
	
	// Example usage
	//insertSession('hung', '2024-07-06 10:00:00', '2024-07-06 11:00:00', 'Success');
?>