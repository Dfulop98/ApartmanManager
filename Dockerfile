FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY ApartmanManagerApi/*.csproj ./ApartmanManagerApi/
COPY DataAccessLayer/*.csproj ./DataAccessLayer/
COPY DataModelLayer/*.csproj ./DataModelLayer/
COPY ServiceLayer/*.csproj ./ServiceLayer/
COPY Tests/*.csproj ./Tests/

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime-env
WORKDIR /app
EXPOSE 80

COPY --from=build-env /app/out ./

ENTRYPOINT ["dotnet", "ApartmanManagerApi.dll"]