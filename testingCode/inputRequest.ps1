$ApiUrl = "http://localhost/api/input"
# Convert Input object to JSON
$inputJson = '{"Inputid":null,"Filename":"Weapons.pdf","Jobid":null,"StoragePriority":null,"InputUri":null}'

try {
    # Invoke the API
    $response = (Invoke-RestMethod -Uri $ApiUrl -Method Post -Body $inputJson -ContentType "application/json").Content
    Write-Host $response
} catch {
    $err = $_.Exception
    # $_.Exception.Response.GetResponseStream()
}
$err.Response

# $err | Get-Member -MemberType Property
