MatchOddsWebApi readme
======================

* * *

### 1.Description

MatchOddsWebApi is a Web Api using REST api architecture in .NET 7.0. It's main purpose is to manipulate data concerning Matches and their odds. This is achieved through communication with the database which contains tables Match and MatchOdds. We are using Swagger UI, in order to send requests to MatchOddsWebApi and perform actions of read,search,add,update and delete for matches and their odds.

### 2.Database

As entity framework is used in this project, adding the proper connection string in the section "ConnectionStrings: DefaultConnection" of appsettings.json file will automatically add the database given with the correct schema, based on the given entities. Alternatively,in the folder database.sql script can be used for the database generation.

### 3.Endpoints

The following endpoints are available:

#### For actions concerning matches:

GET /api/Match/GetAllMatches

GET /api/Match/GetMatchesbySport/

GET /api/Match/GetMatchesbyTeam/

GET /api/Match/GetMatchByDate/

GET /api/Match/GetMatch/

POST /api/Match/CreateMatch

PUT /api/Match/UpdateMatch

DELETE /api/Match/DeleteMatch/

#### For actions concerning matches:

GET /api/MatchOdds/GetAllMatchOdds

GET /api/MatchOdds/GetMatchOdd/

GET /api/MatchOdds/GetOddBySpecifier/

POST /api/MatchOdds/CreateMatchOdd

PUT /api/MatchOdds/UpdateMatchOdd

DELETE /api/MatchOdds/DeleteMatchOdd/

### 4.Maintenance

For the purposes of error control and maintenance Seriloger is added in C:\\logs\\MatchOddsApi.txt. The location for the path can be configured in appsettings.json

### 5.Launch Web Api

After application deployment or running docker container the Web Api will be running in [http://localhost/swagger/index.html](http://localhost/swagger/index.html)

In order to run the image in a docker container we can use the command: docker run -P -d eleor/matchoddswebapi:latest
