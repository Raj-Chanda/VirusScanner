using Microsoft.AspNetCore.Mvc;
using nClam;
using VirusScanner.Models;

namespace VirusScanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VirusScanController : ControllerBase
{
    [HttpPost("/Scan")]
    public async Task<IActionResult> ScanAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Content("File not selected");
        }

        var ms = new MemoryStream();
        file.OpenReadStream().CopyTo(ms);
        byte[] fileBytes = ms.ToArray();

        var result = string.Empty;

        try
        {
            var clam = new ClamClient("localhost", 3310);
            var scanResult = await clam.SendAndScanFileAsync(fileBytes);

            switch (scanResult.Result)
            {
                case ClamScanResults.Clean:
                    result = $"The file is clean! ScanResult: {scanResult.RawResult}";
                    break;
                case ClamScanResults.VirusDetected:
                    result = $"Virus Found! Virus name: {scanResult.InfectedFiles.FirstOrDefault().VirusName}";
                    break;
                case ClamScanResults.Error:
                    result = $"An error occured while scanning the file! ScanResult: {scanResult.RawResult}";
                    break;
                case ClamScanResults.Unknown:
                    result = $"Unknown scan result while scanning the file! ScanResult: {scanResult.RawResult}";
                    break;
                default:
                    result = "Scan not performed";
                    break;
            }
        }
        catch (Exception ex)
        {
            result = $"ClamAV Scan Exception: {ex.ToString()}";
        }

        return Ok(result);
    }

    [HttpPost("/ScanBinaryData")]
    public async Task<IActionResult> ScanBinaryDataAsync([FromBody] ScanBinaryData data)
    {
        // Viris signature => X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*
        // save the file as text file to test for virus

        //binaryData = "WDVPIVAlQEFQWzRcUFpYNTQoUF4pN0NDKTd9JEVJQ0FSLVNUQU5EQVJELUFOVElWSVJVUy1URVNULUZJTEUhJEgrSCo=";

        var fileBytes = Convert.FromBase64String(data.BinaryData);

        var result = string.Empty;

        try
        {
            var clam = new ClamClient("localhost", 3310);
            var scanResult = await clam.SendAndScanFileAsync(fileBytes);

            switch (scanResult.Result)
            {
                case ClamScanResults.Clean:
                    result = $"The file is clean! ScanResult: {scanResult.RawResult}";
                    break;
                case ClamScanResults.VirusDetected:
                    result = $"Virus Found! Virus name: {scanResult.InfectedFiles.FirstOrDefault().VirusName}";
                    break;
                case ClamScanResults.Error:
                    result = $"An error occured while scanning the file! ScanResult: {scanResult.RawResult}";
                    break;
                case ClamScanResults.Unknown:
                    result = $"Unknown scan result while scanning the file! ScanResult: {scanResult.RawResult}";
                    break;
                default:
                    result = "Scan not performed";
                    break;
            }
        }
        catch (Exception ex)
        {
            result = $"ClamAV Scan Exception: {ex.ToString()}";
        }

        return Ok(result);
    }
}
