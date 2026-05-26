FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# only the .csproj files are copied before restore. Source code changes don't touch that layer,
# so the restore cache is reused. Only when you add/remove a NuGet package (which changes a .csproj) does
# restore re-run.

COPY ["WIFIService.Api/WIFIService.Api.csproj", "WIFIService.Api/"]
COPY ["WIFIService.Application/WIFIService.Application.csproj", "WIFIService.Application/"]
COPY ["WIFIService.Domain/WIFIService.Domain.csproj", "WIFIService.Domain/"]
COPY ["WIFIService.Contracts/WIFIService.Contracts.csproj", "WIFIService.Contracts/"]
COPY ["WIFIService.Infrastructure.External/WIFIService.Infrastructure.External.csproj", "WIFIService.Infrastructure.External/"]
RUN dotnet restore "WIFIService.Api/WIFIService.Api.csproj"

COPY . .
RUN dotnet publish "WIFIService.Api/WIFIService.Api.csproj" -c Release -o /app/publish --no-restore

#  This starts a fresh, clean image that only has the ASP.NET runtime — no SDK, no build tools, no source
#  code. The build stage compiled everything, now you just copy the published output into this lightweight image.
#  Smaller, more secure, nothing unnecessary shipped to production.
#  By having a second FROM, that's what starts a new stage and discards the previous one.
#  everything between two FROMS are getting discarded when the next FROM is encountered. So the final image only has the runtime and the published app, not the SDK or source code, but it used the build stage to compile the app and get the published output. This is a common pattern in Dockerfiles for .NET apps, often called "multi-stage builds". It allows you to keep your final image small and secure while still having all the tools you need to build your app in the earlier stage.
#  so the final image only contains everything after the last FROM 
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

# It documents that the container listens on port 8080. It doesn't actually open the port — it's just
#  metadata. The application inside the container still needs to be configured to listen on that port, and when you run the container, you can map that port to a port on the host machine. But it's a good practice to include EXPOSE in your Dockerfile to indicate which ports your application uses. In this case, it suggests that the application will be listening for HTTP requests on port 8080.
EXPOSE 8080
COPY --from=build /app/publish .

# Inside the WORKDIR the ENTRYPOINT is executed — so /app. That's why it can just reference WIFIService.Api.dll directly without a full
#  path, because that file was copied into /app.
ENTRYPOINT ["dotnet", "WIFIService.Api.dll"]
