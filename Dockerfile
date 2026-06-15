# Etapa 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar el .csproj y restaurar dependencias
COPY *.csproj .
RUN dotnet restore

# Copiar todo el código y compilar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copiar solo lo compilado de la etapa anterior
COPY --from=build /app/publish .

# Puerto que expone el microservicio
EXPOSE 5160

# Comando para arrancar
ENTRYPOINT ["dotnet", "AuthService.dll"]