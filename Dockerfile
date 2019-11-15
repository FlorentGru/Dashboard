FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DEV_dashboard_2019.csproj", ""]
RUN dotnet restore "./DEV_dashboard_2019.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DEV_dashboard_2019.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DEV_dashboard_2019.csproj" -c Release -o /app/publish

FROM base AS final
COPY . /app
WORKDIR /app
RUN ./entrypoint.sh
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DEV_dashboard_2019.dll"]