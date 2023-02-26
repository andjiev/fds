#wait for the SQL Server to come up
# sleep 30s

#run the setup script to create the DB and the schema in the DB
# /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "MssqlPass123" -d master -i db-init.sql
# /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -d master -i db-init.sql

/opt/mssql/bin/sqlservr & sleep 20 && \
     /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -d master \
     -Q "create database testdb;" && \
     /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -d master -i db-init.sql
