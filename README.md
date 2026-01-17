# 🚀 Integração C# com Microsoft Dataverse

Este projeto é uma **aplicação Console em C#** desenvolvida como parte de um laboratório prático, com o objetivo de demonstrar a **integração com o Microsoft Dataverse** utilizando autenticação via **Client Credentials**.

O projeto aborda desde a conexão com o Dataverse até a criação de registros em massa, comparando desempenho entre abordagens diferentes.

---

## 🧩 Funcionalidades

- Conexão com o Microsoft Dataverse via C#
- Criação de registros do tipo **Account (Conta)**
- Importação de dados a partir de arquivo **CSV**
- Criação de múltiplos registros
- Comparação de desempenho entre:
  - Criação individual de registros
  - Execução em lote (Multiple Request)

---

## 🛠️ Tecnologias Utilizadas

- C#
- .NET
- Microsoft Power Platform
- Microsoft Dataverse
- Visual Studio
- Git & GitHub

---

## ⚙️ Configuração do Ambiente

⚠️ **Por segurança, as credenciais não estão versionadas neste repositório.**

Para executar o projeto localmente, é necessário configurar as seguintes **variáveis de ambiente** no sistema:

| Variável | Descrição |
|--------|-----------|
| `DV_URL` | URL do ambiente Dataverse |
| `DV_TENANT_ID` | ID do Tenant |
| `DV_CLIENT_ID` | Client ID do App Registration |
| `DV_CLIENT_SECRET` | Client Secret do App Registration |

Após configurar as variáveis, reinicie o Visual Studio antes de executar o projeto.

---

## 📂 Estrutura do Projeto
PrimeiroConsole/
├── PrimeiroConsole.slnx
├── PrimeiroConsole/
│   ├── Program.cs
│   ├── Connection.cs
│   ├── Models/
│   ├── Services/
│   └── Utils/
├── .gitignore
└── README.md


*(A estrutura pode variar conforme a evolução do projeto)*

---

## 📈 Resultados

Durante os testes, foi possível observar uma **diferença significativa de tempo de execução** entre a criação individual de registros e a criação em lote, evidenciando a importância do uso de operações em massa para grandes volumes de dados.

---

## 🔐 Segurança

- Nenhuma credencial sensível é armazenada no código
- Dados de autenticação são gerenciados via variáveis de ambiente
- Boas práticas de segurança aplicadas para publicação em repositório público

---

## 👩‍💻 Autora

**Rafaela Campos**  
Estudante de Análise e Desenvolvimento de Sistemas  
Apaixonada por programação e Power Platform 💙

---

## 📌 Observação

Este projeto tem fins educacionais e foi desenvolvido como parte de um laboratório de aprendizado sobre integração com o Dataverse.



