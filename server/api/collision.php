<?php 
	include_once("db.php");

	// Handle POST request
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        $sessionID = isset($_POST['sessionID']) ? $_POST['sessionID'] : null;
        $typeOfCollision = isset($_POST['typeOfCollision']) ? $_POST['typeOfCollision'] : null;
        $collideLatitude = isset($_POST['collideLatitude']) ? $_POST['collideLatitude'] : null;
        $collideLongitude = isset($_POST['collideLongitude']) ? $_POST['collideLongitude'] : null;
        $mapID = isset($_POST['mapID']) ? $_POST['mapID'] : null;

        insertCollision($sessionID, $typeOfCollision, $collideLatitude, $collideLongitude, $mapID);
    }


    function insertCollision($sessionID, $typeOfCollision, $collideLatitude, $collideLongitude, $mapID) {
        GLOBAL $con;
    
        // Input validation
        if ($sessionID === null || $typeOfCollision === null || $collideLatitude === null || $collideLongitude === null || $mapID === null) {
            echo "SERVER: error, invalid input ";
            echo $sessionID, " ", $typeOfCollision, " ", $collideLatitude, " ", $collideLongitude, " ", $mapID;
            exit();
        }
    
        // Check if sessionID exists
        $sql = "SELECT ID FROM Session WHERE ID=?";
        $st = $con->prepare($sql);
        $st->execute(array($sessionID));
        $all = $st->fetchAll();
        if (count($all) == 0) {
            echo "SERVER: error, session not found";
            exit();
        }
    
        // Insert new collision
        $sql = "INSERT INTO Collisions (sessionID, typeOfCollision, collideLatitude, collideLongitude, mapID) VALUES (?, ?, ?, ?, ?)";
        $st = $con->prepare($sql);
        $st->execute(array($sessionID, $typeOfCollision, $collideLatitude, $collideLongitude, $mapID));
    
        if ($st->rowCount() == 1) {
            echo "SERVER: Collision insertion successful";
        } else {
            echo "SERVER: error, collision insertion failed";
        }
    }
?>