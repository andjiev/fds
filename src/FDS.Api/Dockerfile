#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/FDS.Api/FDS.Api.csproj", "src/FDS.Api/"]
COPY ["src/Core/FDS.Common/FDS.Common.csproj", "src/Core/FDS.Common/"]
COPY ["src/Services/FDS.Package.Service/FDS.Package.Service.csproj", "src/Services/FDS.Package.Service/"]
COPY ["src/Services/FDS.Package.Domain/FDS.Package.Domain.csproj", "src/Services/FDS.Package.Domain/"]
COPY ["src/Services/FDS.Package.Repository/FDS.Package.Repository.csproj", "src/Services/FDS.Package.Repository/"]
RUN dotnet restore "src/FDS.Api/FDS.Api.csproj"
COPY . .
WORKDIR "/src/src/FDS.Api"
RUN dotnet build "FDS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FDS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FDS.Api.dll"]