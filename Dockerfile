# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos csproj y restauramos dependencias
COPY *.sln ./
COPY MiniCoreMultiCliente.MiniCore.API/*.csproj ./MiniCoreMultiCliente.MiniCore.API/
COPY MiniCoreMultiCliente.MiniCore.Application/*.csproj ./MiniCoreMultiCliente.MiniCore.Application/
COPY MiniCoreMultiCliente.MiniCore.Domain/*.csproj ./MiniCoreMultiCliente.MiniCore.Domain/
COPY MiniCoreMultiCliente.MiniCore.Infrastructure/*.csproj ./MiniCoreMultiCliente.MiniCore.Infrastructure/
RUN dotnet restore

# Copiamos el resto del código
COPY . .

# Publicamos la app
RUN dotnet publish MiniCoreMultiCliente.MiniCore.API/MiniCoreMultiCliente.MiniCore.API.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto 
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MiniCoreMultiCliente.MiniCore.API.dll"]
