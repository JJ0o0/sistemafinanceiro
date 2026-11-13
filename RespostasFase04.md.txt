# Fase 04 – Filosofando

**01. Por que o teste precisa falhar (Red) antes de escrever o código?**
Porque a falha prova que o teste realmente consegue detectar um erro. Um teste que nunca foi visto falhando pode estar passando por engano (assert errado, método nem sendo chamado). O Red também garante que o teste verifica um comportamento que ainda não existe, e que o código escrito depois é o que o faz passar.

**02. Como o contrato (interface) ajuda quando vários desenvolvedores trabalham juntos?**
A interface define o que o serviço faz (assinatura, tipos de entrada e saída) antes de existir a implementação. Quem escreve a Controller, quem escreve os testes e quem escreve o serviço podem trabalhar em paralelo, usando o mesmo contrato, sem esperar uns pelos outros. Também permite trocar a implementação (ou usar um mock) sem quebrar quem a consome.

**03. Vantagem de um projeto de teste separado?**
O código de teste não vai para produção: o assembly da aplicação fica menor e sem dependências de teste (xUnit etc.). Há separação clara de responsabilidades, e os testes podem ser executados isoladamente (`dotnet test`) e integrados facilmente a pipelines de CI/CD. Além disso, o projeto de teste só usa a API pública do projeto principal, o que simula o uso real.

**04. Dificuldade de testar juros compostos se o código estivesse direto na Controller?**
A Controller depende de infraestrutura do ASP.NET (HttpContext, ModelState, DbContext/banco, retorno `IActionResult`). Para testar apenas a conta seria preciso montar todo esse ambiente ou até subir o banco. A regra de negócio ficaria misturada com a camada de apresentação, e mudar de juros simples para compostos exigiria alterar a Controller e reescrever testes frágeis. Com um Service, a regra é uma classe simples que se testa com uma linha `new`.

**05. O que define um bom cenário de teste? Que casos de borda testar?**
Um bom teste é pequeno, focado em um único comportamento, determinístico, independente dos outros, com resultado esperado calculado de forma independente do código e com nome que descreve o cenário. Casos de borda: valor zero; taxa zero; valor negativo; taxa negativa; meses zero (juros compostos devem devolver o principal); meses negativos; um mês (composto = simples); taxas e prazos muito grandes (overflow); arredondamento (duas casas decimais) e valores muito pequenos (centavos).

**06. Por que `decimal` e não `double`/`float` em sistemas financeiros?**
`double` e `float` usam ponto flutuante binário, que não consegue representar exatamente muitos números decimais (por exemplo 0,1). Isso gera pequenos erros de arredondamento, como `0.1 + 0.2 != 0.3`, que se acumulam em milhões de operações. O `decimal` usa base 10 com 28-29 dígitos significativos, representando exatamente valores como R$ 0,10, o que é essencial quando "cada centavo conta".

**07. Como a necessidade de testabilidade influencia a organização do código?**
Obriga a separar responsabilidades: regras de negócio ficam em classes pequenas (Services), sem dependência de UI ou banco. Métodos ficam curtos, com entradas e saídas claras, preferindo funções sem efeitos colaterais. Dependências passam a ser injetadas por interfaces (baixo acoplamento, DI), o que também melhora legibilidade e manutenção.

**08. Um teste verde que fica vermelho após uma alteração é ruim?**
Não, é justamente o valor dos testes automatizados. Ele avisou imediatamente que a mudança quebrou um comportamento já garantido (regressão), antes de chegar ao cliente ou à produção, e aponta onde está o problema. Ruim seria não ter o teste e descobrir o erro depois, com dinheiro de verdade envolvido. O vermelho dá segurança para refatorar e evoluir o sistema.

**09. Vantagem da composição de objetos (ContaBancaria com um Cliente) sobre variáveis soltas?**
Os dados do cliente ficam centralizados em um único lugar. Se o cliente ganhar um novo campo (CPF, e-mail), basta alterar a classe `Cliente`, sem mexer em cada uso. Evita duplicação e inconsistência (nome diferente em cada conta), reflete o modelo do mundo real (uma conta *tem um* cliente e *pertence a* uma agência) e facilita reuso, validação e mapeamento para o banco (chaves estrangeiras/EF Core).

**10. "Testar não é encontrar erros, é garantir a confiança".**
Nenhum conjunto de testes prova que não há erros, mas ele fornece evidência de que as regras críticas se comportam como especificado. Em um sistema financeiro, um erro de arredondamento ou de fórmula significa dinheiro de pessoas calculado errado: prejuízo, processos e perda de confiança no banco. Os testes documentam as regras, protegem contra regressões e permitem entregar mudanças com segurança. O desenvolvedor é responsável por demonstrar, de forma repetível, que o sistema é confiável, e não apenas por "achar que funciona".
