# Set the base image to use for the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Spararting copying the .csproj and the remaining files has a great benefit
# (each instruction cause building a lyer to the image) this takes advantage of the caching
# mechanism of docker to optimize image re-building, as the .csproj is less likely to change. 
# if the dependincies didn't change Docker reuses the cached layer and skips the time-consuming 
#step of restoring dependencies. 

# Copy the project file(s) and restore dependencies 
COPY *.csproj ./
RUN dotnet restore

# Copy remaining code
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build the application
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish -c Release -o out --no-restore

# Build runtime image
# Set the base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
# Set the working directory inside the container
WORKDIR /app
# Copy the published output from the build stage
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]