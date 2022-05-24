FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DMT.WebApi/DMT.WebApi.csproj", "DMT.WebApi/"]
COPY ["DMT.DataAccess/DMT.DataAccess.csproj", "DMT.DataAccess/"]
RUN dotnet restore "DMT.WebApi/DMT.WebApi.csproj"
COPY . .
WORKDIR "/src/DMT.WebApi"
RUN dotnet build "DMT.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DMT.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DMT.WebApi.dll"]
