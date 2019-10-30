FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Signing.System.Tcc.MVC/Signing.System.Tcc.MVC.csproj", "Signing.System.Tcc.MVC/"]
RUN dotnet restore "Signing.System.Tcc.MVC/Signing.System.Tcc.MVC.csproj"
COPY . .
WORKDIR "/src/Signing.System.Tcc.MVC"
RUN dotnet build "Signing.System.Tcc.MVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Signing.System.Tcc.MVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Signing.System.Tcc.MVC.dll"]