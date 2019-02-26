FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/TodoList.Web/TodoList.Web.csproj", "src/TodoList.Web/"]
RUN dotnet restore "src/TodoList.Web/TodoList.Web.csproj"
COPY . .
WORKDIR "/src/src/TodoList.Web"
RUN dotnet build "TodoList.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TodoList.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TodoList.Web.dll"]