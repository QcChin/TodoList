# TodoList
---
## Introduction
### This is a sample by asp.net core with unit testing、integration testing and function testing.

## Build
```
git clone git@github.com:QcChin/TodoList.git
```

### `PowerShell`
```
dotnet restore
```
### `CLI`
```
dotnet restore
```

if you change the model of DbContext or init the database, you can run this command.
you should make you direction is `TodoList.Repository` .

### `PowerShell`
```
Update-Databse -Context TodoContext -OutputDir Data\Migrations
```

### `CLI`
```
dotnet ef database update --context TodoContext --output-dir Data\Migrations
```

## Test
```
dotnet test
```

## Publish
```
docker-compose build
docker-compose up
```
