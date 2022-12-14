<p align="center">
  <img alt="Repository size" src="https://img.shields.io/github/repo-size/VarRgas/VaccineC.BackEnd">

  <a href="https://github.com/VarRgas/VaccineC.BackEnd/commits/main">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/VarRgas/VaccineC.BackEnd">
  </a>
    
   <img alt="License" src="https://img.shields.io/badge/license-MIT-7159c1">
   <a href="https://github.com/VarRgas/VaccineC.BackEnd/stargazers">
 
  </a>
</p>

<h1 align="center">
    <img alt="VaccineC" title="VaccineC" src="VaccineC/VaccineC/Resources/Images/login-bg-pic.png" />
</h1>

<h2 align="center"> 
	VaccineC - Software de Gestão para Clínicas de Vacinação (BACKEND)
</h2>

<p align="center">
 <a href="#-sobre-o-projeto">Sobre</a> •
 <a href="#-funcionalidades">Funcionalidades</a> •
 <a href="#-diagrama-er">Diadrama ER</a> • 
 <a href="#-tecnologias">Tecnologias</a> • 
 <a href="#-autores">Autores</a> • 
 <a href="#user-content--licença">Licença</a>
</p>


## 💻 Sobre o projeto

O Projeto VaccineC surgiu com o propósito de auxiliar clínicas privadas de imunização no gerenciamento de vendas de vacinas, bem como o agendamento e a realização de aplicações.

Foi desenvolvido como trabalho acadêmico, sendo requisito para aprovação e conclusão do curso de Análise e Desenvolvimento de Sistemas da [UNIFTEC - Centro Universitário](https://www.ftec.com.br/), sob orientação do Professor [Thiarlei Machado Macedo](https://www.linkedin.com/in/thiarlei/).

---

## ⚙️ Funcionalidades

- [x] <b>Cadastros:</b>
  - [x] Pessoas
  - [x] Empresas
  - [x] Usuários
  - [x] Produtos
  <br>
  
- [x] <b>Orçamentos:</b>
  - [x] Previsão de venda de produtos e/ou esquemas vacinais
  <br>
 
- [x] <b>Agendamentos:</b>
  - [x] Agendamentos de vacinas e/ou outros para aplicação domiciliar ou na clínica
  - [x] Sugestão de próximas doses
  - [x] Envio de lembretes via SMS
  <br>

- [x] <b>Aplicações:</b>
  - [x] Realização de aplicação de vacinas e/ou outros produtos
  - [x] Comunicação com Mock API, simulando o [SI-PNI](http://pni.datasus.gov.br/) do Ministério da Saúde.
  - [x] Histórico de aplicações
  - [x] Situação das Integrações
  <br>

- [x] <b>Estoque:</b>
  - [x] Movimentação de Estoque - Entrada, Saída e Descarte
  - [x] Gerenciamento de Lotes
  - [x] Baixa automática a cada aplicação realizada
  <br>
---

## 💾 Diagrama ER

O Diagrama Entidade Relacionamento do Projeto VaccineC foi construído através de engenharia reversa após a criação das tabelas pelo [DBeaver](https://dbeaver.io/about/):
<br>

<p align="center">
  <img alt="Diagrama ER" title="Diagrama ER" src="VaccineC/VaccineC/Resources/Images/vaccinec-er-diagram.png" width="600px" height="800px">  
</p>

---

## 🛠 Tecnologias

As seguintes tecnologias foram utilizadas na construção do projeto:

#### **FRONTEND**

-   **[Angular](https://angular.io/)**
-   **[Typescript](https://www.typescriptlang.org/)**
-   **[Javascript](https://www.javascript.com/)**
-   **HTML5**
-   **CSS3**

#### **BACKEND**

-   **[C# .NET](https://dotnet.microsoft.com/pt-br/)**
-   **[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)**
-   **[Pattern CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)**

#### **BANCO DE DADOS**

-   **[SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-2022)**

#### **UTILITÁRIOS**

-   Protótipos: **[Moqups](https://app.moqups.com/)**
-   APIs: **[SMSDev](https://www.smsdev.com.br/)**, **[ViaCEP](https://viacep.com.br/)**, **[MockAPI](https://mockapi.io/)**
-   Editores: **[Visual Studio Code](https://code.visualstudio.com/)**, **[Visual Studio Community 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)**
-   SGBDs: **[DBeaver](https://dbeaver.io/about/)**, **[SQL Server Management Studio](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**
-   Teste de API: **[Postman](https://www.postman.com/)**
-   Frameworks de Design de Interface: **[Bootstrap](https://getbootstrap.com/), [Angular Material](https://material.angular.io/)**
-   Ícones: **[Font Awesome](https://fontawesome.com/icons)**, **[Material Design Icons](https://fonts.google.com/icons)**

> Veja a estrutura utilizada no Banco de Dados: [VaccineC DDL BD](https://github.com/VarRgas/VaccineC.BackEnd/blob/main/VaccineC/VaccineC/Resources/Images/bd-estrutura.txt)

---

## 🦸 Autores

<a href="https://github.com/amanda-maschio">
 <img style="border-radius: 50%;" src="https://avatars3.githubusercontent.com/u/65790874?v=4" width="100px;" alt="Amanda Maschio" title="Amanda Maschio"/>
</a>
<p>Amanda Maschio</p>

[![Linkedin Badge](https://img.shields.io/badge/-Amanda-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/amanda-maschio-272783186/)](https://www.linkedin.com/in/amanda-maschio-272783186/) 

<br>

<a href="https://github.com/VarRgas">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/89429606?v=4" width="100px;" alt="Guilherme Scariot Vargas" title="Guilherme Scariot Vargas"/>
</a>
<p>Guilherme Scariot Vargas</p>

[![Linkedin Badge](https://img.shields.io/badge/-Guilherme-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-scariot-vargas-0b9baa163/)](https://www.linkedin.com/in/guilherme-scariot-vargas-0b9baa163/) 

---

## 📝 Licença

Este projeto está sob licença [MIT](LICENSE).
