# 1. Giai đoạn Build: Sử dụng SDK 8.0 để biên dịch code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy các file project và restore thư viện
COPY *.sln .
COPY Netflix.API/*.csproj ./Netflix.API/
COPY Netflix.Data/*.csproj ./Netflix.Data/
COPY Netflix.Business/*.csproj ./Netflix.Business/
RUN dotnet restore

# Copy toàn bộ code và build
COPY . .
WORKDIR /app/Netflix.API
RUN dotnet publish -c Release -o /out

# 2. Giai đoạn Chạy: Sử dụng Runtime nhẹ hơn
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Mở cổng 80 (hoặc cổng Boss đã cấu hình)
EXPOSE 80
ENTRYPOINT ["dotnet", "Netflix.API.dll"]