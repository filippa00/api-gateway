# Use Keycloak base image
FROM jboss/keycloak:latest
COPY /keycloak-config/standalone.xml /opt/jboss/keycloak/standalone/configuration/config-file

# Expose Keycloak ports
EXPOSE 8080
EXPOSE 8443