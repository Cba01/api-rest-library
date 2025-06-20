# ----------------------
# 1) Build stage
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo el csproj y hace restore (para cachear paquetes)
COPY WebApiLibrary.csproj ./
RUN dotnet restore WebApiLibrary.csproj

# Copia el resto del código
COPY . ./

# Publica sólo el proyecto, no la solución
RUN dotnet publish WebApiLibrary.csproj \
    -c Release \
    -o /app/publish

# ----------------------
# 2) Runtime stage
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApiLibrary.dll"]
