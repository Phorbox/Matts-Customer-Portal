class ImageDeployment {
    [string]$ImageName
    [string]$ImageSrc
    [string]$ImageVersion


    ImageDeployment() { $this.Init(@{}) }
    ImageDeployment([hashtable]$Properties) { $this.Init($Properties) }
    ImageDeployment([string]$ImageName, [string]$ImageSrc, [string]$ImageVersion) { 
        $this.Init(@{
                ImageName    = $ImageName
                ImageSrc     = $ImageSrc
                ImageVersion = $ImageVersion
            }) 
    }

    [void] Init([hashtable]$Properties) {
        foreach ($Property in $Properties.Keys) {
            $this.$Property = $Properties.$Property
        }
    }

    [void] build([string]$ImageName, [string]$ImageSrc, [string]$ImageVersion) {
        docker build -t phorbox/${ImageName}:$ImageVersion ./$ImageSrc/
    }
    [void] build() {
        $this.build($this.ImageName, $this.ImageSrc, $this.ImageVersion)
    }
    
    [void] buildLatest() {
        $this.build($this.ImageName, $this.ImageSrc, "latest")
    }

    [void] push([string]$ImageName, [string]$ImageVersion) {
        docker push phorbox/${ImageName}:$ImageVersion
    }
    [void] push() {
        $this.push($this.ImageName, $this.ImageVersion)
    }
    [void] pushLatest() {
        $this.push($this.ImageName, "latest")
    }

    [void] fullDeployment() {
        $this.build()
        $this.buildLatest()
        $this.push()
        $this.pushLatest()
    }
}

$version = 4
$front = [ImageDeployment]::new("portal-demo-front", "backend", $version)
$api = [ImageDeployment]::new("portal-demo-api", "backend", $version)
$proxy = [ImageDeployment]::new("portal-demo-proxy", "proxy", $version)

$deployments = @($front, $api)

foreach ($currentItemName in $deployments) {
    echo "Deploying $($currentItemName.ImageName)"
    $currentItemName.fullDeployment()
}