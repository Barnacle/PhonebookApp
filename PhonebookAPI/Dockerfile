#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 44335

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PhonebookAPI/PhonebookAPI.csproj", "PhonebookAPI/"]
RUN dotnet restore "PhonebookAPI/PhonebookAPI.csproj"
COPY . .
WORKDIR "/src/PhonebookAPI"
RUN dotnet build "PhonebookAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhonebookAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhonebookAPI.dll"]