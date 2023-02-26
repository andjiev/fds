FROM mcr.microsoft.com/azure-sql-edge:latest
COPY . /

ENV SA_PASSWORD "MssqlPass123"
ENV SQLCMDPASSWORD "MssqlPass123"
ENV ACCEPT_EULA "Y"

RUN (mkdir -p /opt/mssql-tools/bin && cd /opt/mssql-tools/bin && wget https://github.com/microsoft/go-sqlcmd/releases/download/v0.8.0/sqlcmd-v0.8.0-linux-arm64.tar.bz2 \
    && bzip2 -d sqlcmd-v0.8.0-linux-arm64.tar.bz2 && tar -xvf sqlcmd-v0.8.0-linux-arm64.tar && chmod 755 sqlcmd)

# RUN chmod +x /db-init.sh
CMD /bin/bash ./entrypoint.sh