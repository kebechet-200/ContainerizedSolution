FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build


# Copy the .csproj into home/artifact folder.
WORKDIR /home/source-code
COPY *.csproj ./
RUN dotnet restore

# Copy the whole project files into there.
COPY . ./
RUN dotnet publish -c Release -o artifact


# Run the published output
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal

WORKDIR /app

# Copy the artifact into /app folder
COPY --from=build /home/source-code/artifact .

# Set the environment variables
ENV ASPNETCORE_HTTP_PORT=https://+:5001
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the ports
EXPOSE 5000
EXPOSE 5001

# Run point
ENTRYPOINT ["dotnet", "Containerized.Api.dll"]