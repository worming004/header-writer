FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["header-writer.csproj", "./"]
RUN dotnet restore "header-writer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "header-writer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "header-writer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "header-writer.dll"]
