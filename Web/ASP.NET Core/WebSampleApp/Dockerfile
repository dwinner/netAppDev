#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /appdocker 
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebSampleApp.csproj", "."]
RUN dotnet restore "./WebSampleApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebSampleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSampleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSampleApp.dll"]