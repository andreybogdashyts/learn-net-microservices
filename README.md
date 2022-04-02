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
        kubects get pods
    delete deployment
        kubectl delete deployment platform-depl
    get services
        kubectl get services