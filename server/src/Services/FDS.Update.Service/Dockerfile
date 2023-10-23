#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/FDS.Update.Service/FDS.Update.Service.csproj", "src/Services/FDS.Update.Service/"]
COPY ["src/Core/FDS.Common/FDS.Common.csproj", "src/Core/FDS.Common/"]
COPY ["src/Services/FDS.Update.Domain/FDS.Update.Domain.csproj", "src/Services/FDS.Update.Domain/"]
COPY ["src/Services/FDS.Update.DapperRepository/FDS.Update.DapperRepository.csproj", "src/Services/FDS.Update.DapperRepository/"]
RUN dotnet restore "src/Services/FDS.Update.Service/FDS.Update.Service.csproj"
COPY . .
WORKDIR "/src/src/Services/FDS.Update.Service"
RUN dotnet build "FDS.Update.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FDS.Update.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT Production

#ENTRYPOINT ["dotnet", "FDS.Update.Service.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FDS.Update.Service.dll