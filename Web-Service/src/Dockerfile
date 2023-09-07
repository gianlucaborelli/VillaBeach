# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore "./Api.Application/Application.csproj" --disable-parallel

# copy everything else and build app
RUN dotnet publish "./Api.Application/Application.csproj" -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Application.dll"]