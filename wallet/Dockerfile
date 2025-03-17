FROM golang:latest as build

WORKDIR /build
COPY . .
RUN go build -o app ./cmd/walletcore/...
 
FROM alpine:latest as run

RUN apk add libc6-compat
WORKDIR /app
COPY --from=build /build/app .
EXPOSE 8080
ENTRYPOINT ["/app/app"]
