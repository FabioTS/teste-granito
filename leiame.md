# Leia-me

Seguem algumas instruções sobre o projeto:

    CalculatorApi/  => Projeto da api de calculo
        CalculatorApi.Api/ => Projeto de início, WEBAPI, ASP NET Core
        CalculatorApi.Domain/ => Domínio da aplicação, padrão DDD
        CalculatorApi.Tests/ => Projeto com testes unitários, MSTest

    TaxApi/  => Projeto da api de taxas
        TaxApi.Api/ => Projeto de início, WEBAPI, ASP NET Core

    deploy/ => Arquivo de deploy
        docker/ => Dockerfiles
        kubernetes/ => Deployments K8s

## Como Executar

Entrar na pasta *.Api e executar "dotnet run".

É possível escolher a url do servidor usando a váriavel de ambiente "ASPNETCORE_URLS", exemplo: "ASPNETCORE_URLS": "http://+:5010"

Lembrar de ajustar a url da api de taxas em CalculatorApi.Api/appsettings.json

### Docker

É possível compilar/rodar com o docker usando os arquivos localizados em deploy/docker/

docker build -f deploy\docker\Dockerfile-TaxApi -t taxapi:local TaxApi/

docker run -p 5010:80 --name TaxApi taxapi:local

docker build -f deploy\docker\Dockerfile-CalculatorApi -t calculatorapi:local CalculatorApi/

docker run -p 5000:80 --name CalculatorApi calculatorapi:local

## Testes

Para rodar os testes, entrar na pasta *.Tests e executar "dotnet test"
