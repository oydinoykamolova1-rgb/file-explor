# Multi-stage Dockerfile for Nature File Explorer (.NET 10 API + Vue 3 Frontend)

# Step 1: Build Vue 3 Frontend
FROM node:20-alpine AS frontend-build
WORKDIR /src/frontend
COPY FileExploler.FrontEnd/package*.json ./
RUN npm install
COPY FileExploler.FrontEnd/ ./
RUN npm run build

# Step 2: Build .NET 10 Backend
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS backend-build
WORKDIR /src
COPY FileExploler.BackEnd/ ./FileExploler.BackEnd/
# Copy built Vue static files into backend wwwroot
COPY --from=frontend-build /src/frontend/dist ./FileExploler.BackEnd/FileExploler.Api/wwwroot/

WORKDIR /src/FileExploler.BackEnd/FileExploler.Api
RUN dotnet publish FileExploler.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Step 3: Final Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=backend-build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FileExploler.Api.dll"]
