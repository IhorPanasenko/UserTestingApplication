#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestApplicationServer/TestApplicationServer.csproj", "TestApplicationServer/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["DAL/DAL.csproj", "DAL/"]
RUN dotnet restore "TestApplicationServer/TestApplicationServer.csproj"
COPY . .
WORKDIR "/src/TestApplicationServer"
RUN dotnet build "TestApplicationServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApplicationServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApplicationServer.dll"]