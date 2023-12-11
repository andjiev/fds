FROM node:18-alpine as node
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base

COPY --from=node /usr/lib /usr/lib
COPY --from=node /usr/local/lib /usr/local/lib
COPY --from=node /usr/local/include /usr/local/include
COPY --from=node /usr/local/bin /usr/local/bin

WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

COPY ["server/src/Services/FDS.Update.Service/FDS.Update.Service.csproj", "server/src/Services/FDS.Update.Service/"]
COPY ["server/src/Core/FDS.Common/FDS.Common.csproj", "server/src/Core/FDS.Common/"]
COPY ["server/src/Services/FDS.Update.Domain/FDS.Update.Domain.csproj", "server/src/Services/FDS.Update.Domain/"]
COPY ["server/src/Services/FDS.Update.Repository/FDS.Update.Repository.csproj", "server/src/Services/FDS.Update.Repository/"]
# COPY ["package.json", "server/src/Services/FDS.Update.Service/"]
RUN dotnet restore "server/src/Services/FDS.Update.Service/FDS.Update.Service.csproj"
COPY . .
WORKDIR "/server/src/Services/FDS.Update.Service"
RUN dotnet build "FDS.Update.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FDS.Update.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FDS.Update.Service.dll"]
