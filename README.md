.Net
    create new WEB API project
        dotnet new webapi -n CommandsService
        
Docker
    get version
        docker --version
    build an image (-t target image name)
        docker build -t andreybogdashyts/learn-platformservice .
    run a container. -p is important to make app available outside container
        docker run -p 8080:80 -d andreybogdashyts/learn-platformservice
    get all containers
        docker ps
    stop container
        docker stop {container ID}
    push image
        docker push andreybogdashyts/learn-platformservice

Kuberbetes
    create deployment
        kubectl apply -f platforms-depl.yaml
    get deployements
        kubectl get deployments
    get pods
        kubectl get pods
        kubectl get pods --namespace=ingress-nginx
    delete deployment
        kubectl delete deployment platform-depl
    get services
        kubectl get services
    refresh deployment
        kubectl rollout restart deployment .\platforms-depl.yaml
    create secret
        kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
    use specific configuration
        kubectl config use-context docker-desktop
    get details of pod
        kubectl describe pod {pod_name}
    nginx
        kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.2.0/deploy/static/provider/cloud/deploy.yaml
