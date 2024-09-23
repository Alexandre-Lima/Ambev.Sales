 # 💸 Protótipo de API de Vendas .NET

A concepção do protótipo de API de vendas .NET adotou os preceitos fundamentais da Clean Architecture, aliados à filosofia do Clean Code e SOLID, aplicando o Git Flow Workflow.
O cerne deste projeto reside na estreita representação de uma aplicação para gestão de vendas de forma simplificada.

## 📝 Instruções para compilar a solução

No visual studio, abra o arquivo "Ambev.Sales.sln", defina o "Ambev.Sales.Api" como projeto de inicialização, compile e execute;

## 🧪 Teste

O projeto inclui testes de unidade e testes de integração para o back-end (.NET).

## 📁 Estrutura do projeto


![soluction](https://github.com/user-attachments/assets/eb3cf642-da81-4727-8d04-9d65eb215d86)

🔹 <b>Ambev.Sales.Api</b><br/>
 Aplicação (WEB API) responsável por receber requisições e enviar respostas.

🔹 <b>Ambev.Sales.Domain</b><br/> 
Biblioteca com entidades do domínio, contratos para os Casos de Uso, e contratos para os Repositórios.

🔹 <b>Ambev.Sales.Domain.UseCases</b><br/>
Biblioteca que implementa as regras de negócio dos casos de uso definidos na camada de Domínio.

🔹 <b>Ambev.Sales.Repositories</b><br/>
Biblioteca que implementa o armazenamento de dados dos repositórios definidos na camada de Domínio.

🔹 <b>Ambev.Sales.Shared</b><br/>
Biblioteca auxiliar para compartilhamento de artefatos que não pertencem ao domínio;

🧪 <b>Ambev.Sales.IntegrationTest</b><br/>
Aplicação responsável por gerenciar os testes de integração dos recurso fornecidos pelo protótipo da API de vendas;

🧪 <b>Ambev.Sales.UnitTests</b><br/>
Aplicação responsável por gerenciar os testes de unidade dos casos de uso pertencentes ao domínio da aplicação;

## Recursos disponíveis

![swagger](https://github.com/user-attachments/assets/bc21f455-15af-496b-b4d0-9bd34a888910)

#### HealthCheck

 ✅ <b>Verificação de disponibilidade do protótipo da API de vendas</b><br/> 
  GET: /api/healthCheck/ping<br/>
  
**Sales**

 ✅ <b>Criar uma nova venda</b><br/> 
  POST: /api/sales<br/>

✅ <b>Alterar uma venda</b><br/>
  PUT: /api/sales/{saleId}<br/>
  
✅ <b>Cancelar/deletar uma venda</b><br/>
  DELETE: /api/sales/cancellation/{saleId}<br/>
  
✅ <b>Cancelar um item de uma venda</b><br/>
  PATCH: /api/sales/cancellation/{saleId}/item<br/> 

## 🤔 FAQ

#### O que preciso para rodar essa API?
R: Esta API foi construída utilizando o .NET 8, portanto, é necessário ter essa versão do framework instalada, juntamente com o Visual Studio 2022, em sua máquina.

#### Qual o canal de suporte?
R: Este projeto não dispõe de um canal de suporte direto.  Caso você enfrente algum problema, por favor, inicie uma discussão para tratar da questão. Se necessário, os administradores poderão converter a discussão em um problema oficial (issue) para ser abordado.

#### Qual é o fluxo de desenvolvimento?
R: Em nosso projeto, adotamos um modelo de gestão de código que compreende duas principais ramificações: 'master' e 'develop', aplicando o  Git Flow Workflow.

## ⚖️ Licença

A presente iniciativa é disponibilizada mediante a licença MIT License, reconhecida por conferir um leque amplo de prerrogativas, englobando a permissão para utilização, replicação, modificação e distribuição do código-fonte com notável flexibilidade.
