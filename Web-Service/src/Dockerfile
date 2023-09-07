# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore "./Api.Application/Application.csproj" --disable-parallel
RUN dotnet restore "./Api.CrossCutting/CrossCutting.csproj" --disable-parallel
RUN dotnet restore "./Api.Data/Data.csproj" --disable-parallel
RUN dotnet restore "./Api.Domain/Domain.csproj" --disable-parallel
RUN dotnet restore "./Api.Service/Service.csproj" --disable-parallel

# copy everything else and build app
RUN dotnet publish "./Api.Application/Application.csproj" -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Application.dll