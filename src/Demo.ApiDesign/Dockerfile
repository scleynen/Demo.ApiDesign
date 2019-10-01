FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.ApiDesign/Demo.ApiDesign.csproj", "Demo.ApiDesign/"]
RUN dotnet restore "Demo.ApiDesign/Demo.ApiDesign.csproj"
COPY . .
WORKDIR "/src/Demo.ApiDesign"
RUN dotnet build "Demo.ApiDesign.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.ApiDesign.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.ApiDesign.dll"]