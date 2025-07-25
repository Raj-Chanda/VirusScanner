# VirusScanner
This is a sample application to demonstrating the actual implementation of ClamAV antivirus scan.


### Functionalities
* Scan files
* Scan binary data/ base64  string

### Endpoints
**[POST]** https://localhost:44388/api/VirusScan/Scan

This endpoint will scan files

### Endpoints
**[POST]** https://localhost:44388/api/VirusScan/ScanBinaryData

This endpoint will scan binary data/ base64 string

### How to setup ClamAV Antivirus
To run ClamAv antivirus instence execute the below docker command.

```
docker run --name "ClamAV" -d -p 3310:3310 clamav/clamav:stable
```


### Test Files
Test files are available in **Test Files** folder

### Sample Binary Data/Base64 string

```
WDVPIVAlQEFQWzRcUFpYNTQoUF4pN0NDKTd9JEVJQ0FSLVNUQU5EQVJELUFOVElWSVJVUy1URVNULUZJTEUhJEgrSCo=
```