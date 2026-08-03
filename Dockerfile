FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY Mona-Logistics-LTD.Data/Mona-Logistics-LTD.Data.csproj Mona-Logistics-LTD.Data/
COPY Mona-Logistics-LTD.Data.Common/Mona-Logistics-LTD.Data.Common.csproj Mona-Logistics-LTD.Data.Common/
COPY Mona-Logistics-LTD.Data.Models/Mona-Logistics-LTD.Data.Models.csproj Mona-Logistics-LTD.Data.Models/
COPY Mona-Logistics-LTD.Services.Common/Mona-Logistics-LTD.Services.Common.csproj Mona-Logistics-LTD.Services.Common/
COPY Mona-Logistics-LTD.Services/Mona-Logistics-LTD.Services.csproj Mona-Logistics-LTD.Services/
COPY Mona-Logistics-LTD.Web.Infrastructure/Mona-Logistics-LTD.Web.Infrastructure.csproj Mona-Logistics-LTD.Web.Infrastructure/
COPY Mona-Logistics-LTD.Web/Mona-Logistics-LTD.Web.csproj Mona-Logistics-LTD.Web/

RUN dotnet restore "Mona-Logistics-LTD.Web/Mona-Logistics-LTD.Web.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "Mona-Logistics-LTD.Web/Mona-Logistics-LTD.Web.csproj" \
    -c Release \
    -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Mona-Logistics-LTD.Web.dll"]