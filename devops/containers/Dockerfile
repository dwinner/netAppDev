FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /containers

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /containers
COPY --from=build-env /containers/out .
ENTRYPOINT ["dotnet", "AboutMyEnvironment.dll"]
