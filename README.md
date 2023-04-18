# Teste Granito

**Você deverá criar duas APIs:**

1. Api de taxas, essa api deve ter um único endpoint que devolve a taxa de juros (1% ou 0,01)

2. Api de calculo, essa api deve ter dois endpoints:

    1. Endpoint "Calcula Juros":

        A primeira responde pelo path relativo "/calculajuros"

        Ela faz um cálculo em memória, de juros compostos, conforme abaixo: Valor Final = Valor Inicial * (1 + juros) ^ Tempo

        Valor inicial é um decimal recebido como parâmetro, o Juros é o valor retornado da api de taxas, o Tempo é um inteiro, que representa meses, também recebido como parâmetro ^ representa a operação de potência Resultado final deve ser truncado (sem arredondamento) em duas casas decimais

        Exemplo: /calculajuros?valorinicial=100&meses=5 Resultado esperado: 105,10

    2. Endpoint "Show me the code"

        Este responde pelo path relativo /showmethecode Deverá retornar a url de onde encontra-se o fonte no github

**Esperamos que a entrega tenha:**

* Código fonte em asp.net core no github (Mostrar conhecimento básico de git)
* Teste (Mostrar conhecimento de testes unitários)
* Utilização de Docker

Extras:

* O ideal é este código rodar em algum servidor seu, podemos combinar tempo que irá funcionar. Se não tiver avise o entrevistador.
* Teste de integração da API em linguagem de sua preferência (Damos importância para pirâmide de testes)
* Utilizar swagger
* Prazo e retorno
* Isso será combinado com quem você fez a entrevista. Você terá tempo para entender o cenário e nos retornar um prazo. Lembre-se: Prazo dado é prazo cumprido.

**Boa sorte!**
