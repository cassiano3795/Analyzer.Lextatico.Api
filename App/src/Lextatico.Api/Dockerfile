FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000;https://+:5001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app && chown -R appuser /root
# USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["src/Lextatico.Api/Lextatico.Api.csproj", "src/Lextatico.Api/"]
RUN dotnet restore "src/Lextatico.Api/Lextatico.Api.csproj"
COPY . .
WORKDIR "/src/src/Lextatico.Api"
RUN dotnet build "Lextatico.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lextatico.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lextatico.Api.dll"]
