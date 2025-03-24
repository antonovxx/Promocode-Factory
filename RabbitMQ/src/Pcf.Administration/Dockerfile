# Prepare the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and project files, then restore dependencies
COPY *.sln .
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done
RUN dotnet restore

# Copy the remaining files
COPY . ./aspnetapp

# Build and publish WebHost
FROM build AS webhost-build
WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out/webhost Pcf.Administration.WebHost/Pcf.Administration.WebHost.csproj

# Build and publish RmqHost
FROM build AS rmqhost-build
WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out/rmqhost Pcf.Administration.RmqHost/Pcf.Administration.RmqHost.csproj

# WebHost runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS webhost-runtime
WORKDIR /app
COPY --from=webhost-build /app/aspnetapp/out/webhost ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "Pcf.Administration.WebHost.dll"]

# RmqHost runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS rmqhost-runtime
WORKDIR /app
COPY --from=rmqhost-build /app/aspnetapp/out/rmqhost ./
ENTRYPOINT ["dotnet", "Pcf.Administration.RmqHost.dll"]