# e-Cicle 

![Alt Text](https://media.giphy.com/media/i3otJcGC9pxzXdGb5Z/giphy.gif)

### Plataforma de compartilhamento de projetos usando componentes eletrônicos retirados do e-waste

Foram utilizados no desenvolvimento:
- Asp Net Core 2.0 https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.4
- npm(node 6.5.9) https://nodejs.org/download/release/v6.9.5/node-v6.9.5-x64.msi ou maior
- Javascript
- React Redux
- SQLExpress e SQL Server Management Studio https://www.microsoft.com/en-us/download/confirmation.aspx?id=42299

#### Como rodar a aplicação localmente (sem usar o docker)

- instalar as dependencias listadas acima (com link)

##### Para o backend

- instalar as dependencias 
- startar o bd

- no cmd, a partir da pasta backend\src\EletronicPartsCatalog <br />
``` dotnet build ```

- iniciar a aplicação <br />
``` dotnet run ```

*-> Porta -> 5000*

*-> Swagger -> 5000/swagger*

##### Para o frontend

- instalar as dependencias <br />

- no cmd, a partir da pasta frontend <br />
``` npm install ```

- iniciar a aplicação <br />
``` npm start ```

*-> Porta -> 4100*


