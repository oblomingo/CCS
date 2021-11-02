FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /source

RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs

COPY . /source

WORKDIR /source/CCS.Repository
RUN dotnet restore -r linux-arm

WORKDIR /source/CSS.GPIO
RUN dotnet restore -r linux-arm

WORKDIR /source/CCS.WebApp
RUN dotnet restore -r linux-arm

WORKDIR /source/CCS.WebApp
RUN dotnet publish "./CCS.WebApp.csproj" --output "../dist" --configuration Release -r linux-arm --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 as runtime
WORKDIR /source
COPY --from=builder /source/dist .
EXPOSE 80
ENTRYPOINT ["dotnet", "CCS.WebApp.dll"]