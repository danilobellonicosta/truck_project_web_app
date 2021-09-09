# Truk Project - Instruções de execução do aplicativo:

## Prerequisites:
- NET 5 Runtime
- NPM
- NodeJS
- SQL Server
- .NET EF (no CMD, type "dotnet tool install --global dotnet-ef")


## Execução da API .NET Core


No diretório do projeto \TruckProject, abra o CLI (Command Line) ou Windows Power Shell e execute:
- dotnet build

Em seguida no diretório do projeto \TruckProject\TruckProject.API, abra o CLI (Command Line) ou Windows Power Shell e execute:
- dotnet ef database update
- dotnet run


## Execução do React App:

No diretório do projeto \web\app, abra o cmd e execute o comando:
#### npm i

Em seguida inicie a compilação dos scripts com o comando:

#### npm start

Após os arquivos serem compilados o navegador irá abrir o app em (http://localhost:3000)
