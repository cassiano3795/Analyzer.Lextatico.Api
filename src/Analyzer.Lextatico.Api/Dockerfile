FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 9004
EXPOSE 9005

ENV ASPNETCORE_URLS=http://+:9004;https://+:9005

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app && chown -R appuser /root
# USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["src/Analyzer.Lextatico.Api/Analyzer.Lextatico.Api.csproj", "src/Analyzer.Lextatico.Api/"]
RUN dotnet restore "src/Analyzer.Lextatico.Api/Analyzer.Lextatico.Api.csproj"
COPY . .
WORKDIR "/src/src/Analyzer.Lextatico.Api"
RUN dotnet build "Analyzer.Lextatico.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Analyzer.Lextatico.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Analyzer.Lextatico.Api.dll"]
