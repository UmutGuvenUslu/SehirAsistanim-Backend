# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Proje dosyalarını kopyala
COPY ["SehirAsistani.Api/SehirAsistani.Api.csproj", "SehirAsistani.Api/"]
COPY ["SehirAsistanim.Domain/SehirAsistanim.Domain.csproj", "SehirAsistanim.Domain/"]
COPY ["SehirAsistanim.Infrastructure/SehirAsistanim.Infrastructure.csproj", "SehirAsistanim.Infrastructure/"]
COPY ["SehirAsistanim.Persistence/SehirAsistanim.Persistence.csproj", "SehirAsistanim.Persistence/"]

# Bağımlılıkları yükle
RUN dotnet restore "SehirAsistani.Api/SehirAsistani.Api.csproj"

# Tüm kaynakları kopyala
COPY . .

# Build işlemi
WORKDIR "/src/SehirAsistani.Api"
RUN dotnet build "SehirAsistani.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish aşaması
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SehirAsistani.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final aşama (çalışma aşaması)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Publish edilen dosyaları kopyala
COPY --from=publish /app/publish .

# kotusoz.txt dosyasını da kopyala (middleware ile aynı klasör)
COPY SehirAsistani.Api/Middlewares/kotusoz.txt ./Middlewares/kotusoz.txt

ENTRYPOINT ["dotnet", "SehirAsistani.Api.dll"]
