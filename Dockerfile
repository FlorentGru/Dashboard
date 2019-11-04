FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["DEV_dashboard_2019/DEV_dashboard_2019.csproj", "DEV_dashboard_2019/"]
RUN dotnet restore "DEV_dashboard_2019/DEV_dashboard_2019.csproj"
COPY . .
WORKDIR "/src/DEV_dashboard_2019"
RUN dotnet build "DEV_dashboard_2019.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DEV_dashboard_2019.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DEV_dashboard_2019.dll"]