#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleFormsService.API/SimpleFormsService.API.csproj", "SimpleFormsService.API/"]
RUN dotnet restore "SimpleFormsService.API/SimpleFormsService.API.csproj"
COPY . .
WORKDIR "/src/SimpleFormsService.API"
RUN dotnet build "SimpleFormsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleFormsService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleFormsService.API.dll"]
