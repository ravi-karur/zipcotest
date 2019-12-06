#!/bin/bash
set -e

#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Jko3va-D9821jhsvGD -d master -i /var/opt/mssql/data/setup.sql

exec "$@"