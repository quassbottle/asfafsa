FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microservices.Languages/Microservices.Languages.csproj", "Microservices.Languages/"]
RUN dotnet restore "Microservices.Languages/Microservices.Languages.csproj"
COPY . .
WORKDIR "/src/Microservices.Languages"
RUN dotnet build "Microservices.Languages.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Languages.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Languages.dll"]
