FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Epidemic.API.Listing/Epidemic.API.Listing.csproj", "Epidemic.API.Listing/"]
RUN dotnet restore "Epidemic.API.Listing/Epidemic.API.Listing.csproj"
COPY . .
WORKDIR "/src/Epidemic.API.Listing"
RUN dotnet build "Epidemic.API.Listing.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Epidemic.API.Listing.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Epidemic.API.Listing.dll"]