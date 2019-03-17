FROM microsoft/dotnet:2.1-sdk AS builder
WORKDIR /src
COPY ["./TryAspNetCore.Api.csproj", "."]
RUN dotnet restore "TryAspNetCore.Api.csproj"
COPY . .

RUN dotnet publish "TryAspNetCore.Api.csproj" -c Release -o /app --no-restore

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
COPY --from=builder /app .

EXPOSE 80
CMD ["dotnet", "TryAspNetCore.Api.dll"]
