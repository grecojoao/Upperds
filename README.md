# Pomar de um produtor de frutas

### Este é o Back-End de uma api Rest para o gerenciamento de um pomar de frutas.

### Quais são os métodos disponíveis? E como posso utilizá-los? 
Você pode acessar a [documentação técnica](https://upperds-api.azurewebsites.net/swagger/index.html) ou mandar um help-me em: junior.greco@hotmail.com

---

Para realizar as requisições é necessário ter um token de acesso. Você pode obtê-lo ao se cadastrar em: https://upperds-api.azurewebsites.net/v1/Login/Create, utilizando usuário e senha com privilégios de administrador, fornecidos previamente.
Cada token possui tempo de expiração de 1 hora, sendo necessário logar novamente no sistema:
https://upperds-api.azurewebsites.net/v1/Login

Você pode cadastrar Espécies, Grupos de Árvores, Árvores e Colheitas. Sendo elas de Árvores ou Grupos.
Para cadastrar uma árvore é necessário antes cadastrar **espécie *e* grupo**.

São permitidas também exclusões de colheitas e grupos sem afetar árvores, mas ao deletar uma *espécie* você também estará excluindo as **árvores** pertencentes a ela.

---

No desenvolvimento foram utilizados containers docker para o SQL Server em máquinas Linux.

Api e banco de dados estão rodando na plataforma Azure, também em Linux.

Página principal para requisições: https://upperds-api.azurewebsites.net/