# Tech Challenge - Fase 2 - FIAP Cloud Games (FCG)

# Vídeo de apresentação
Link [Google Drive](https://drive.google.com/file/d/1obK1rZlVQMg1Ae3IBzCLjT1LOipDIqRj/view?usp=sharing).

# API
> Utilizamos na fase 2 a mesma API apresentada na Fase 1. 

A imagem Docker foi armazenada no repositório Dockerhub rodando em Linux na distribuição Ubuntu e banco de dados SQL 2022 e monitoramento integrado com New Relic.

# PipeLines
A aplicação será atualiza pela Azure Cloud.

## CI
```
pr:
  branches:
    include:
      - '*'

pool:
  vmImage: "windows-latest"
 
variables:
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'
  solution: 'FCG_Docker.FCG.sln'
  project: 'FCG/FCG.csproj'
  testProject: 'TEST-FCG/TEST-FCG.csproj'

steps:
- task: UseDotNet@2
  displayName: 'Instala o .NET SDK'
  inputs:
    packageType: 'sdk'
    version: '8.0.x'
    installationPath: $(Agent.ToolsDirectory)/dotnet

- task: NuGetToolInstaller@1
  displayName: 'Instala o NuGet'

- task: NuGetCommand@2
  displayName: 'Restaura os pacotes do NuGet'
  inputs:
    command: 'restore'
    restoreSolution: '$(solution)'
    feedsToUse: 'select'

- task: DotNetCoreCLI@2
  displayName: 'Executa os testes'
  inputs:
    command: 'test'
    projects: '$(testProject)'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Build do projeto'
  inputs:
    command: 'build'
    arguments: '--configuration $(buildConfiguration)'
    projects: '$(project)'

- task: DotNetCoreCLI@2
  displayName: 'Publica o projeto'
  inputs:
    command: 'publish'
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)/$(buildConfiguration)'
    projects: '$(project)'
    publishWebProjects: false
    zipAfterPublish: true

- publish: '$(Build.ArtifactStagingDirectory)'
  artifact: drop
```

## CD
```
trigger:
  - main

pool:
  vmImage: "windows-latest"
 
variables:
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'
  dotNetSdkVersion: '8.0.x'
  solution: 'FCG_Docker.FCG.sln'
  project: 'FCG/FCG.csproj'
  testProject: 'TEST-FCG/TEST-FCG.csproj'

steps:
- task: UseDotNet@2
  displayName: 'Instala o .NET SDK'
  inputs:
    packageType: 'sdk'
    version: '$(dotNetSdkVersion)'
    installationPath: $(Agent.ToolsDirectory)/dotnet

- task: NuGetToolInstaller@1
  displayName: 'Instala o NuGet'

- task: NuGetCommand@2
  displayName: 'Restaura os pacotes do NuGet'
  inputs:
    command: 'restore'
    restoreSolution: '$(solution)'
    feedsToUse: 'select'

- task: DotNetCoreCLI@2
  displayName: 'Executa os testes'
  inputs:
    command: 'test'
    projects: '$(testProject)'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Build do projeto'
  inputs:
    command: 'build'
    arguments: '--configuration $(buildConfiguration)'
    projects: '$(project)'

- task: DotNetCoreCLI@2
  displayName: 'Publica o projeto'
  inputs:
    command: 'publish'
    publishWebProjects: false
    projects: '$(project)'
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: false

- task: PublishBuildArtifacts@1
  displayName: 'Publica os artefatos do build'
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: 'FCGTeste'
    publishLocation: 'Container'

- publish: '$(Build.ArtifactStagingDirectory)'
  artifact: drop

- download: current
  artifact: drop

- script:
    echo O deploy em produção foi executado com sucesso!
  displayName: 'Deploy em produção'
```

# Dockerfile
## Estrutura de estágios (Multi-stage Build)
O Dockerfile utiliza uma abordagem de build em múltiplos estágios. O primeiro estágio, chamado build, é responsável por compilar e publicar a aplicação. O segundo estágio, chamado final, é usado para rodar a aplicação em ambiente de produção com apenas os arquivos essenciais.

## Imagem base do build
No estágio build, é utilizada a imagem mcr.microsoft.com/dotnet/sdk:8.0, que contém o SDK do .NET 8. Essa imagem oferece todas as ferramentas necessárias para compilar e publicar a aplicação.

## Restauração e publicação da aplicação
Primeiramente, o arquivo .csproj é copiado e as dependências são restauradas automaticamente. Em seguida, todo o código-fonte é copiado e a aplicação é publicada no modo Release, com os artefatos finais sendo salvos em /app/publish.

## Imagem base de runtime
O estágio final utiliza a imagem mcr.microsoft.com/dotnet/aspnet:8.0, que contém apenas os componentes necessários para executar aplicações ASP.NET em produção, sem o SDK completo.

## Instalação do New Relic Agent
Durante o estágio final, o Dockerfile instala o agente do New Relic para monitoramento da aplicação. Isso envolve a adição do repositório da New Relic, a instalação do agente via apt, e a configuração das variáveis.

## Configuração de ambiente para o New Relic
São definidas diversas variáveis de ambiente que ativam o profiler do .NET, apontam para a biblioteca do New Relic, e fornecem a chave de licença e o nome da aplicação para o monitoramento.

## Transferência dos artefatos compilados
Os arquivos publicados no estágio de build são copiados para o diretório de trabalho /app no contêiner final, permitindo que a aplicação possa ser executada sem as ferramentas de desenvolvimento.

## Exposição na porta 80
A aplicação expõe a porta 80, que é a padrão para aplicações web, permitindo que o tráfego HTTP externo seja direcionado ao contêiner.

## Definição do ponto de entrada
A instrução ENTRYPOINT define que, ao iniciar o contêiner, será executado o comando dotnet FCG.dll, iniciando assim a aplicação .NET.
```
# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o csproj e restaura dependências
COPY *.csproj ./

# Copia todo o restante e publica em modo Release
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Instala o agente
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Habilita o agente
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=acc9c2c99d1fa184923b990ed2b609c2FFFFNRAL \
NEW_RELIC_APP_NAME="dotnet-newrelic"

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Porta exposta
EXPOSE 80

# Executa a aplicação
ENTRYPOINT ["dotnet", "FCG.dll"]
```

# Docker Compose

O arquivo docker-compose.yml define dois serviços: fcg-api, uma aplicação ASP.NET Core, e sqlserver, um banco SQL Server 2022.

A API é construída a partir do Dockerfile local (build: .), expõe a porta 80 internamente e a mapeia para 8080 no host. As variáveis de ambiente incluem ASPNETCORE_ENVIRONMENT=Development e a ConnectionString apontando para o banco.

O SQL Server roda com a imagem oficial da Microsoft, usando a porta padrão 1433, e define SA_PASSWORD e ACCEPT_EULA como exigido. Um volume nomeado (sqlvolume) garante persistência dos dados.

Ambos os serviços compartilham a mesma rede Docker (fcg-network), permitindo comunicação via hostname (sqlserver).

A instrução depends_on garante que o banco seja iniciado antes da API, embora não aguarde a disponibilidade completa do serviço.

Esse Compose é ideal para desenvolvimento local, oferecendo um ambiente isolado, persistente e replicável.
```
version: '3.9'

services:
  fcg-api:
    build: .
    ports:
      - "8080:80"
    depends_on:
      - sqlserver
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__connectionString=Server=sqlserver;Database=FCG;User Id=sa;Password=qwe123!@#;TrustServerCertificate=True
    networks:
      - fcg-network

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    ports:
      - "1433:1433"
    environment:
      SA_PASSWORD: "qwe123!@#A"
      ACCEPT_EULA: "Y"
    volumes:
      - sqlvolume:/var/opt/mssql
    networks:
      - fcg-network

networks:
  fcg-network:

volumes:
  sqlvolume:
```
