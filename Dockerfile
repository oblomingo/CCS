FROM mcr.microsoft.com/dotnet/sdk:3.1-bullseye-arm32v7 AS builder
WORKDIR /source

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

COPY . /source

WORKDIR /source/CCS.Repository
RUN dotnet restore

WORKDIR /source/CSS.GPIO
RUN dotnet restore

WORKDIR /source/CCS.Web
RUN dotnet restore

WORKDIR /source/CCS.Web
RUN dotnet publish "./CCS.Web.csproj" --output "../dist" --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:3.1-bullseye-slim-arm32v7 as runtime
WORKDIR /source
COPY --from=builder /source/dist .
EXPOSE 80
ENTRYPOINT ["dotnet", "CCS.Web.dll"]