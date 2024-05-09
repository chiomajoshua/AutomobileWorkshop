#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Showroom/Showroom.Api/ShowroomService.Api.csproj", "Services/Showroom/Showroom.Api/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["Services/Showroom/Showroom.Data/ShowroomService.Data.csproj", "Services/Showroom/Showroom.Data/"]
COPY ["Services/Showroom/Showroom.Service/ShowroomService.Core.csproj", "Services/Showroom/Showroom.Service/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
RUN dotnet restore "./Services/Showroom/Showroom.Api/ShowroomService.Api.csproj"
COPY . .
WORKDIR "/src/Services/Showroom/Showroom.Api"
RUN dotnet build "./ShowroomService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShowroomService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShowroomService.Api.dll"]