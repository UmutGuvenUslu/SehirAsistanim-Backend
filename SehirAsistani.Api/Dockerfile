


# Bu aşama, hizmet projesini oluşturmak için kullanılır
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SehirAsistani.Api/SehirAsistani.Api.csproj", "SehirAsistani.Api/"]
COPY ["SehirAsistanim.Domain/SehirAsistanim.Domain.csproj", "SehirAsistanim.Domain/"]
COPY ["SehirAsistanim.Infrastructure/SehirAsistanim.Infrastructure.csproj", "SehirAsistanim.Infrastructure/"]
COPY ["SehirAsistanim.Persistence/SehirAsistanim.Persistence.csproj", "SehirAsistanim.Persistence/"]
RUN dotnet restore "./SehirAsistani.Api/SehirAsistani.Api.csproj"
COPY . .
WORKDIR "/src/SehirAsistani.Api"
RUN dotnet build "./SehirAsistani.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Bu aşama, son aşamaya kopyalanacak hizmet projesini yayımlamak için kullanılır
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SehirAsistani.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Bu aşama üretimde veya VS'den normal modda çalıştırıldığında kullanılır (Hata Ayıklama yapılandırması kullanılmazken varsayılan olarak ayarlıdır)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SehirAsistani.Api.dll"]