# ORM - O que estamos fazendo errado?

Apresentação e exemplos de código criadas por *Marcelo Wildfaier* e adaptadas por *Fabrício Rissetto*.

# Como executar os exemplos

### 1. Restaure o [backup](https://github.com/fabriciorissetto/ORM-CommonMistakes/blob/master/BancoDeDadosExemplo_Backup.rar) da base de dados disiponível na raiz do repositório

OBS: O método de Seed do `Configuration.cs` está comentado pois demora muito para popular a base (aproximadamente 1h), porém é auto suficiente para popular a base. Então caso não deseje restaurar o backup: 

1. Descomente o trecho de código do Seed
2. Rode o comando `Update-Database` no Package Manager Console do NuGet
3. Vá fazer um chimarrão

### 2. Rode a aplicação Console e siga as opções do menu

