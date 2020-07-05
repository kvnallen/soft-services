#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5030
ENV ASPNETCORE_URLS=http://+5030

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CalculadoraJuros/Taxa.csproj", "CalculadoraJuros/"]
RUN dotnet restore "CalculadoraJuros/Taxa.csproj"
COPY . .
WORKDIR "/src/CalculadoraJuros"
RUN dotnet build "Taxa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taxa.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taxa.dll"]