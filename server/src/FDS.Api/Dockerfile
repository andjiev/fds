FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

COPY ["server/src/FDS.Api/FDS.Api.csproj", "server/src/FDS.Api/"]
COPY ["server/src/Core/FDS.Common/FDS.Common.csproj", "server/src/Core/FDS.Common/"]
COPY ["server/src/Services/FDS.Package.Service/FDS.Package.Service.csproj", "server/src/Services/FDS.Package.Service/"]
COPY ["server/src/Services/FDS.Package.Domain/FDS.Package.Domain.csproj", "server/src/Services/FDS.Package.Domain/"]
COPY ["server/src/Services/FDS.Package.Repository/FDS.Package.Repository.csproj", "server/src/Services/FDS.Package.Repository/"]
RUN dotnet restore "server/src/FDS.Api/FDS.Api.csproj"
COPY . .
WORKDIR "/server/src/FDS.Api"
RUN dotnet build "FDS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FDS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FDS.Api.dll"]
