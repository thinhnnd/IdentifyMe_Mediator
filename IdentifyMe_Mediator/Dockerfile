#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["IdentifyMe_Mediator/IdentifyMe_Mediator.csproj", "IdentifyMe_Mediator/"]
RUN dotnet restore "IdentifyMe_Mediator/IdentifyMe_Mediator.csproj"
COPY . .
WORKDIR "/src/IdentifyMe_Mediator"
RUN dotnet build "IdentifyMe_Mediator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentifyMe_Mediator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentifyMe_Mediator.dll"]