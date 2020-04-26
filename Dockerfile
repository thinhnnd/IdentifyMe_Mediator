FROM streetcred/dotnet-indy:1.14.2 AS base
WORKDIR /app
EXPOSE 5000

FROM streetcred/dotnet-indy:1.14.2 AS build
WORKDIR /src

COPY ["IdentifyMe_Mediator.csproj", "IdentifyMe_Mediator/"]
RUN dotnet restore "IdentifyMe_Mediator/IdentifyMe_Mediator.csproj"
    COPY . ./IdentifyMe_Mediator

WORKDIR "/src/IdentifyMe_Mediator"
RUN dotnet build "IdentifyMe_Mediator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentifyMe_Mediator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "IdentifyMe_Mediator.dll"]