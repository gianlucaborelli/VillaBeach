
# https://hub.docker.com/_m/icrosoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Changed the port from 5000 to 80 as the error states that the Dockerfile must expose a TCP port
# It is common to expose port 80 for web applications
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

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
