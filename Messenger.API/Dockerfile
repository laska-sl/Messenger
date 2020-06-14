FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Messenger.API/Messenger.API.csproj", "Messenger.API/"]
COPY ["Messenger.Data/Messenger.Data.csproj", "Messenger.Data/"]
RUN dotnet restore "Messenger.API/Messenger.API.csproj"
COPY . .
WORKDIR "/src/Messenger.API"
RUN dotnet build "Messenger.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messenger.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messenger.API.dll"]
