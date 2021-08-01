FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CurrencyToWordsConverter.API/CurrencyToWordsConverter.API.csproj", "CurrencyToWordsConverter.API/"]
RUN dotnet restore "CurrencyToWordsConverter.API/CurrencyToWordsConverter.API.csproj"
COPY . .
WORKDIR "/src/CurrencyToWordsConverter.API"
RUN dotnet build "CurrencyToWordsConverter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyToWordsConverter.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyToWordsConverter.API.dll"]
