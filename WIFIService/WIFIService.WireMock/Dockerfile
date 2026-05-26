FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WIFIService.WireMock/WIFIService.WireMock.csproj", "WIFIService.WireMock/"]
RUN dotnet restore "WIFIService.WireMock/WIFIService.WireMock.csproj"

COPY . .
RUN dotnet publish "WIFIService.WireMock/WIFIService.WireMock.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
EXPOSE 9001
EXPOSE 9002
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WIFIService.WireMock.dll"]
