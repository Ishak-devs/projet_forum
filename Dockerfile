# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers
COPY . .  
RUN dotnet publish -c Release -o out

# Étape 2 : runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exposer le port (ex: 5000)
EXPOSE 5000

ENTRYPOINT ["dotnet", "Forum.dll"]
