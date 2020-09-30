# Continiuous Integration Pipeline

Varje gång vi pushar till master byggs en ny image i Azure.  Vår bygg konfiguration är satt till release och det kanske skulle medfört några fördelar att istället ha den satt till "Debug" eftersom det skulle inkluderat lite debug-filer i exe-filen.

Stackoverflow: https://stackoverflow.com/a/933744	"Debug vs. Release"

Eftersom att vi var osäkra på ifall detta skulle vara användbart valde vi att köra med "Release" . Vi valde att använda några variabler för att få koden att se lite renare ut. 

```yaml
trigger:
- master

pool:
  vmImage: '$(vmImage)'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'
  vmImage: 'windows-latest'
  tag: '$(Build.BuildId)'
```

Vi valde att använda Stages för att optimisera pipelinen. Det finns t.ex. ingen mening att köra enhetstester ifall imagen inte kan skapas. Piplinen uppdelades därför i följande stages Build -> Test -> Push som alla är beroende utav att föregående steg fungerar.

I build-steget installeras först NuGet-paketen och sedan byggs vår utifrån den plattform och konfiguration som vi angett ovan.

*Notera: Vi fick det inte att fungera med linux-image hela vägen igenom, vilket är varför den är satt till "windows-latest" här.*

```Yaml
stages:
- stage: Build
  jobs:
  - job: Build
    steps:
    - task: NuGetToolInstaller@1
    - task: NuGetCommand@2
      inputs:
        restoreSolution: '$(solution)'
    - task: VSBuild@1
      inputs:
        solution: '$(solution)'
        msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:DesktopBuildPackageLocation="$(build.artifactStagingDirectory)\WebApp.zip" /p:DeployIisAppPath="Default Web Site"'
        platform: '$(buildPlatform)'
        configuration: '$(buildConfiguration)'
```

Sedan körs våra tester (som egentligen bara är för att visa konceptet, då vi bara har ett test). Eftersom att Testerna också ligger i ett eget projekt i vår solution ville vi se till att detta fungerade och även var ett eget steg.

```yaml
- stage: Test
  dependsOn: Build
  condition: succeeded()
  jobs:
  - job: Test
    steps:
    - task: VSTest@2
      inputs:
        platform: '$(buildPlatform)'
        configuration: '$(buildConfiguration)'
```

Om allt fungerat som det ska skapar vi nu vår docker-image för våra två projekt, vilket i sin tu är uppdelat i två delar; en del för backend och del för frontend. De använder också två olika docker-filer som vi placerat en nivå upp i vår projektmapp eftersom att vi haft en del problem med att vi inte kunde hitta dem. När våra docker-images är byggda pushas de upp till två olika container registrys så att vi kan fortsätta arbeta separat på front-/backend utan att den ena/andra slutar fungera.

```yaml

- stage: Push
  dependsOn: Test
  condition: succeeded()
  pool:
   -vmImage: 'ubuntu-latest'
  jobs:
  - job: Push
    steps:
    - task: Docker@2
      inputs:
        containerRegistry: 'spacepark-api-dev-connnection'
        repository: 'spacepark-api-dev-01'
        command: 'buildAndPush'
        Dockerfile: '**/Dockerfile'
    - task: Docker@2
      inputs:
        containerRegistry: 'spacepark-webapp-dev-connnection'
        repository: 'spacepark-webapp-dev-connection'
        command: 'buildAndPush'
        Dockerfile: '**/DockerfileFrontend'
```

Hur vi kom fram till vår lösning var egentligen inte svårare an att vi försökte hitta en bra resurs som vi kunde följa. Hela "CI -> CD/CD -> Prod" flödet insirerades mycket av följande: 

![Blue-Green Deployment with Azure DevOps and App Services](https://www.edmondek.com/images/blue_green_azure_devops_app_service.png)

Referens: https://www.edmondek.com/Blue-Green-Deployment-Azure-DevOps-App-Services/#:~:text=Use%20Azure%20DevOps%20to%20enable,Deployment%20to%20Azure%20App%20Service.&amp;text=The%20Build%20Pipeline%20includes%20jobs,publish%20artifacts%20to%20Azure%20Artifacts

