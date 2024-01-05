# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR ..\api-gateway

# Copy csproj and restore as distinct layers
COPY api-gateway/*.csproj ./api-gateway/
COPY *.sln ./
#RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish api-gateway -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /api-gateway
COPY --from=build-env /api-gateway/out .
ENTRYPOINT ["dotnet", "api-gateway.dll"]

