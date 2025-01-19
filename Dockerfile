# Use the official .NET 6.0 runtime image for the final build
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET 6.0 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Way2bill/Way2bill.csproj", "Way2bill/"]
RUN dotnet restore "Way2bill/Way2bill.csproj"
COPY . .
WORKDIR "/src/Way2bill"
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Way2bill.dll"]
