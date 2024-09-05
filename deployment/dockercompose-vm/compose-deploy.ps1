$resourcegroup = "myResourceGroupCLI"
$location = "centralus"
az group create `
    --name $resourcegroup `
    --location $location
az deployment group create `
    --resource-group $resourcegroup `
    --template-file ./vm/template.json `
    --parameters ./vm/parameters.json 