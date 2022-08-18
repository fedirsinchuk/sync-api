FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["./Sync.API"]
