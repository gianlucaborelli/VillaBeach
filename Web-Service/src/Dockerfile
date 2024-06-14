FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore "./Api.Application/Application.csproj" --disable-parallel

# copy everything else and build app
RUN dotnet publish "./Api.Application/Application.csproj" -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Copy communication templates to the correct directory
COPY --from=build /source/Api.CrossCutting.Communication/Templates /Api.CrossCutting.Communication/Templates

EXPOSE 80

ENTRYPOINT ["dotnet", "Application.dll"]