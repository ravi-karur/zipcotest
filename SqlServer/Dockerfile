FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

ENV ACCEPT_EULA = "Y"
ENV SA_PASSWORD = "Jko3va1D9821jhsvGD"
ENV MSQL_PID = Express
ENV PATH=${PATH}:/opt/mssql/bin

RUN mkdir -p /var/opt/mssql/data
RUN chmod -R g=u /var/opt/mssql /etc/passwd

WORKDIR /var/opt/mssql/data

COPY ./SqlServer .



RUN chmod 767 /var/opt/mssql/data/entrypoint.sh

RUN ls -la
#COPY CreateCustomerDB.sql /var/opt/mssql/data


#ENTRYPOINT [ "/bin/bash", "/var/opt/mssql/data/entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]

# RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
#     && /opt/mssql-tools/bin/sqlcmd -S localhost -i /var/opt/mssql/data/CreateCustomerDB.sql \
#     && pkill sqlservr

# RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
#     && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Jko3va-D9821jhsvGD' -i /var/opt/mssql/data/CreateCustomerDB.sql \
#     && pkill sqlservr



