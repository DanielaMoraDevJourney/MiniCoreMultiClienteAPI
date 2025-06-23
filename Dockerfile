# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# Restauramos dependencias
RUN dotnet restore MiniCoreMultiCliente.csproj

# Publicamos el proyecto
RUN dotnet publish MiniCoreMultiCliente.csproj -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "MiniCoreMultiCliente.dll"]
