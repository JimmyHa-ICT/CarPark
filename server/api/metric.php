<?php
include_once("db.php");
if (isset($_POST["session_id"])  && 
isset($_POST["time_taken"])  &&
isset($_POST["collision"]) ){
    
		insertPerformanceMetric($_POST["session_id"], $_POST["time_taken"], $_POST["collision"]);
	}


function insertPerformanceMetric($sessionId, $timeTaken, $collision) {
    GLOBAL $con;

    // Input validation
    if ($sessionId === null || $timeTaken === null || $collision === null) {
        echo "SERVER: error, invalid input";
        exit();
    }

    // Check if sessionId exists
    $sql = "SELECT ID FROM Session WHERE ID=?";
    $st = $con->prepare($sql);
    $st->execute(array($sessionId));
    $all = $st->fetchAll();
    if (count($all) == 0) {
        echo "SERVER: error, session not found";
        exit();
    }

    // Insert new performance metric
    $sql = "INSERT INTO PerformanceMetrics (SessionID, TimeTaken, Collision) VALUES (?, ?, ?)";
    $st = $con->prepare($sql);
    $st->execute(array($sessionId, $timeTaken, $collision));

    if ($st->rowCount() == 1) {
        echo "SERVER: Performance metric insertion successful";
    } else {
        echo "SERVER: error, performance metric insertion failed";
    }
}
?>
