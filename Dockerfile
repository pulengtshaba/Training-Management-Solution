# ==========================================
# Stage 1: Build
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file first
# This allows Docker to cache the restore layer
COPY ["TrainingManagement.Api/TrainingManagement.Api.csproj", "TrainingManagement.Api/"]

# Restore dependencies
RUN dotnet restore "TrainingManagement.Api/TrainingManagement.Api.csproj"

# Copy the remaining source code
COPY . .

# Build the application
WORKDIR "/src/TrainingManagement.Api"

RUN dotnet build "TrainingManagement.Api.csproj" \
    --configuration Release \
    --no-restore \
    --output /app/build


# ==========================================
# Stage 2: Publish
# ==========================================
FROM build AS publish

RUN dotnet publish "TrainingManagement.Api.csproj" \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false


# ==========================================
# Stage 3: Runtime
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# ASP.NET Core will listen on port 8080
ENV ASPNETCORE_HTTP_PORTS=8080

# Copy published application
COPY --from=publish /app/publish .

# Document the port used by the container
EXPOSE 8080

# Start the API
ENTRYPOINT ["dotnet", "TrainingManagement.Api.dll"]