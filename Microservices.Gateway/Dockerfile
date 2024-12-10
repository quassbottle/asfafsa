FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microservices.Gateway/Microservices.Gateway.csproj", "Microservices.Gateway/"]
RUN dotnet restore "Microservices.Gateway/Microservices.Gateway.csproj"
COPY . .
WORKDIR "/src/Microservices.Gateway"
RUN dotnet build "Microservices.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Gateway.dll"]
