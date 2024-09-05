
$version = 3

docker build -t phorbox/portal-demo-front:$version ./backend/
docker build -t phorbox/portal-demo-api:$version ./backend/
docker build -t phorbox/portal-demo-proxy:$version ./proxy/

docker push phorbox/portal-demo-front:$version
docker push phorbox/portal-demo-api:$version
docker push phorbox/portal-demo-proxy:$version

$version = "latest"

docker build -t phorbox/portal-demo-front:$version ./backend/
docker build -t phorbox/portal-demo-api:$version ./backend/
docker build -t phorbox/portal-demo-proxy:$version ./proxy/

docker push phorbox/portal-demo-front:$version
docker push phorbox/portal-demo-api:$version
docker push phorbox/portal-demo-proxy:$version