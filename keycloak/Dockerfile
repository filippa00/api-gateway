# Use Keycloak base image
FROM jboss/keycloak:latest
COPY standalone.xml /opt/jboss/keycloak/standalone/configuration/config-file
COPY add-user-keycloak.sh /opt/jboss/keycloak/bin/

# Expose Keycloak ports
EXPOSE 8080
EXPOSE 8443