# Wallet Application

## Instruções para Executar a Aplicação

### Passo 1: Subir os Containers com Docker Compose

1. Certifique-se de que você tem o Docker e o Docker Compose instalados em sua máquina.
2. Navegue até o diretório raiz do projeto onde o arquivo `docker-compose.yml` está localizado.
3. Execute o seguinte comando para subir os containers:
   docker-compose up

### Passo 2: Utilizar o Arquivo .http para Testar a API

1. Após os containers estarem em execução, utilize o arquivo `client.http` que está na pasta `API` para testar a API.
2. Abra o arquivo `client.http` localizado em `api/client.http` com um editor de texto ou uma ferramenta que suporte requisições HTTP, como o VS Code com a extensão "REST Client".
3. Execute a requisição que está marcada como `### update balance`:
   ### update balance
   POST http://localhost:8080/transactions HTTP/1.1
   Content-Type: application/json

   {
       "account_id_from": "f8df753c-3b58-43aa-8016-12aaa4f1ea3e",
       "account_id_to": "0216ea38-524f-4e85-8743-d484a8f7538e",
       "amount": 1
   }

4. Após executar a requisição de atualização de saldo, verifique os resultados utilizando as requisições `### get balance by account id` para as contas criadas via seed:
   ### get balance by account id
   GET http://localhost:3003/balances/0216ea38-524f-4e85-8743-d484a8f7538e HTTP/1.1

   ### get balance by account id
   GET http://localhost:3003/balances/f8df753c-3b58-43aa-8016-12aaa4f1ea3e HTTP/1.1

### Observações

- Certifique-se de que as portas `8080` e `3003` estão disponíveis em sua máquina.
