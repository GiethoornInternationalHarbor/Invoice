FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app

# Copy the project file
COPY *.sln ./
COPY InvoiceService.App/*.csproj ./InvoiceService.App/
COPY InvoiceService.Core/*.csproj ./InvoiceService.Core/
COPY InvoiceService.Infrastructure/*.csproj ./InvoiceService.Infrastructure/

# Restore the packages
RUN dotnet restore

# Copy everything else
COPY . ./
WORKDIR /app/InvoiceService.App

FROM build AS publish
# Build the release
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app

# Copy the output from the build env
COPY --from=publish /app/InvoiceService.App/out ./

ENTRYPOINT [ "dotnet", "InvoiceService.App.dll" ]