#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Delivery/Loyalty-API/Src/API/Loyalty-API/Loyalty-API.csproj","Delivery/Loyalty-API/Src/API/Loyalty-API/"]
RUN dotnet restore "Delivery/Loyalty-API/Src/API/Loyalty-API/Loyalty-API.csproj"
COPY . .
WORKDIR "/src/Delivery/Loyalty-API/Src/API/Loyalty-API"
RUN dotnet build "Loyalty-API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Loyalty-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/publish
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loyalty-API.dll"]