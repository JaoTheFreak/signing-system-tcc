FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Signing.System.Tcc.MVC/Signing.System.Tcc.MVC.csproj", "Signing.System.Tcc.MVC/"]
COPY ["Signing.System.Tcc.DependencyConfiguration/Signing.System.Tcc.DependencyConfiguration.csproj", "Signing.System.Tcc.DependencyConfiguration/"]
COPY ["Signing.System.Tcc.Data/Signing.System.Tcc.Data.csproj", "Signing.System.Tcc.Data/"]
COPY ["Signing.System.Tcc.Domain/Signing.System.Tcc.Domain.csproj", "Signing.System.Tcc.Domain/"]
COPY ["Signing.System.Tcc.Helpers/Signing.System.Tcc.Helpers.csproj", "Signing.System.Tcc.Helpers/"]
COPY ["Signing.System.Tcc.Application/Signing.System.Tcc.Application.csproj", "Signing.System.Tcc.Application/"]
COPY ["Signing.System.Tcc.Ethereum/Signing.System.Tcc.Ethereum.csproj", "Signing.System.Tcc.Ethereum/"]
RUN dotnet restore "Signing.System.Tcc.MVC/Signing.System.Tcc.MVC.csproj"
COPY . .
WORKDIR "/src/Signing.System.Tcc.MVC"
RUN dotnet build "Signing.System.Tcc.MVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Signing.System.Tcc.MVC.csproj" -c Release -o /app

FROM base AS final
ENV CONN_STRING="Server=ec2-23-21-106-241.compute-1.amazonaws.com; Port=5432; Database=dc9ftta10rhsgs;User Id=oxdqmwplvvqacq; Password=0274b7acbfbbbeef63cb7ec267fd5450a77f5a8f23e18eacb29862bda19b0cdc; sslmode=Require; Trust Server Certificate=true;"
ENV INFURA_PROJECT="https://ropsten.infura.io/v3/c04cfa171c2140fa8cb57c4287825d51"
ENV ACCOUNT_ADDRESS="0x3aceBa8696918A3AF245e059049685De52300ad5"
ENV CONTRACT_ADDRESS="0x7f7276D18888562cF42Ef2D1D609209911F87fD6"
ENV BUCKET_NAME="signing_system_tcc"
ENV COINBASE_URL_API="https://api.coinbase.com/v2/"
ENV GOOGLE_APPLICATION_CREDENTIALS="./google-app-credentials.json"
ENV WALLET_PRIVATE_KEY="0xD1042D34B0D0E04E5585D84657CC3CA738E7B735794D68AF66FF2AF132164B3B"

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=publish /app .
COPY Signing.System.Tcc.MVC/wwwroot wwwroot
ENTRYPOINT ["dotnet", "Signing.System.Tcc.MVC.dll"]
