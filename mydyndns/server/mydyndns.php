<?php

$validation_key = "d644c0a2d3125ff010e0e81c9de362ed";
$filename = "/etc/dnsmasq.d/dynamic.hosts";

if (!isset ($_REQUEST['key']) || $_REQUEST['key'] != $validation_key) {
	die ("invalid key!");
}

$entries = array ();
$file = fopen ($filename, "rb") or die ("can't open file");

while (!feof ($file)) {
        $line = fgets ($file);

        if (strpos($line, ' ' ) !== false) {
	        list ($ip, $hostname) = explode (' ', $line, 2);
	        
	        if (filter_var ($ip, FILTER_VALIDATE_IP)) {
	        	$entries[trim ($hostname)] = $ip;
	        }
        }
}

fclose ($file);

if ($_SERVER['REQUEST_METHOD'] == 'POST' && isset($_REQUEST['domain']) && $_POST['domain'] != "") {
	$entries[trim($_POST['domain'])] = $_SERVER['REMOTE_ADDR'];
	
	$file = fopen($filename, 'w') or die("can't open file");
	
	fwrite($file, "#" . $filename . ": Dynamic Host Database\n\n");
	while (list ($hostname, $ip) = each ($entries)) {
		fwrite ($file, "$ip $hostname\n");
	}
	
	fclose ($file);
}

foreach ($entries as $hostname=>$ip) {
    echo "$ip $hostname\n";
}

?>
