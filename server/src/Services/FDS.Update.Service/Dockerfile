FROM alpine:3.17.2
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base

# ENV NODE_PACKAGE_URL  https://unofficial-builds.nodejs.org/download/release/v18.16.0/node-v18.16.0-linux-x64-musl.tar.gz

ENV NODE_PACKAGE_URL https://unofficial-builds.nodejs.org/download/release/v14.2.0/node-v14.2.0-linux-armv6l.tar.gz

RUN apk add libstdc++
WORKDIR /opt
RUN wget $NODE_PACKAGE_URL
RUN mkdir -p /opt/nodejs
RUN tar -zxvf *.tar.gz --directory /opt/nodejs --strip-components=1
RUN rm *.tar.gz
RUN ln -s /opt/nodejs/bin/node /usr/local/bin/node
RUN ln -s /opt/nodejs/bin/npm /usr/local/bin/npm

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

# COPY --from=node /usr/lib /usr/lib
# COPY --from=node /usr/local/lib /usr/local/lib
# COPY --from=node /usr/local/include /usr/local/include
# COPY --from=node /usr/local/bin /usr/local/bin

# Get NodeJS
# COPY --from=node /usr/local/bin /usr/local/bin
# # Get npm
# COPY --from=node /usr/local/lib/node_modules /usr/local/lib/node_modules

ENTRYPOINT ["dotnet", "FDS.Update.Service.dll"]
