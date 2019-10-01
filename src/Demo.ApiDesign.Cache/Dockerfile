FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.ApiDesign.Cache/Demo.ApiDesign.Cache.csproj", "Demo.ApiDesign.Cache/"]
RUN dotnet restore "Demo.ApiDesign.Cache/Demo.ApiDesign.Cache.csproj"
COPY . .
WORKDIR "/src/Demo.ApiDesign.Cache"
RUN dotnet build "Demo.ApiDesign.Cache.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.ApiDesign.Cache.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.ApiDesign.Cache.dll"]
