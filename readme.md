# Softplan Services

Challenge to be a Softplayer.

## Como executar?

1. Inicie o eureka
```bash
docker-compose up -d service_discovery
```

2. Após o eureka ficar **pronto** (verifique se abriu a interface administrativa em localhost:8671), execute o comando abaixo:

```bash
docker-compose up -d apigateway taxa juros
```

**É importante esperar o ```service_discovery``` ficar 'pronto' antes de subir os outros serviços, pois eles dependem do mesmo.**
