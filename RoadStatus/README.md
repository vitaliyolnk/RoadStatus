# Road Status API Client
A TfL WebApi client to check status of a road managed by TfL and print the following information:
1. Road display name
2. Road status
3. Status description

## Implementation Framework
.Net Core 2

## To build
Download/clone the code, use Visual Studio 2017 to open RoadStatus solution file and **Rebuild** the solution.

## To run

1. Open *appsettings.json* file under **RoadStatus** project and update the value of "appId" and "appKey" fields with your values of appId and appKey respectively  
2. Build the solution
3. Publish **RoadStatus** project (by selecting **Publish...** from context menu of the project and choosing *Folder* as a publishing target profile). By default, Publish output directory ("Target Location") is **bin\Release\PublishOutput** folder in the project directory but can be configured.
4. Open PowerShell and change working directory (run **cd** command) to the published directory selected in step 3.
5. Type **dotnet RoadStatus.dll** followed by space and the name of the road which status is required to check. I.e. "**dotnet RoadStatus.dll A2**"
6. To check the exit code, type *echo $LASTEXITCODE* or just *$LASTEXITCODE* in PowerShell after the program has executed.

## Unit tests

The application was developed using TDD, MSTest and NSubstitutes.
An attempt with BDD was made but abandoned due to .Net Core 2 and SpecFlow incompatibility.

### To run unit tests
1. Build/Rebuild the solution
2. Update *appsettings.json* of the Test project **RoadStatus.Test** by entering required "appId" and "appKey" fields.
3. Run unit test from Test Explorer
