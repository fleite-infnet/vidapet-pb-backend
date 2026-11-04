**Nome:** Fernando Leite Rodrigues

**Disciplina:** Projeto de Bloco: Desenvolvimento Back-End [26E4_5]

**Atividade:** TP1 — Justificativa do Escopo e Premissas

O sistema VidaPet abrange casos de uso fundamentais como cadastrar, agendar, cancelar, consultar a agenda do dia e registrar consultas, mas optei por deixar de fora funcionalidades de edição e exclusão de cadastros porque o enunciado não especifica tais operações, limitando-se a orientar o cadastro e definindo o teto do projeto. Entre os problemas do caderno de recepção, focar em horários duplicados e conflitos de agenda resolve diretamente a integridade do agendamento, enquanto as consultas esquecidas ficaram de fora simplesmente porque o enunciado não prevê alertas ou lembretes, e não por limitações de persistência.

Para lidar com as lacunas do enunciado, como definir se a última consulta começa às 17h30 ou às 18h00, não adotei uma regra por conta própria; estabeleci uma premissa explícita para registrar e confirmar depois com o professor, derivando a partir dela a implementação do cálculo em minutos.

Para atender ao escopo do programa *standalone* — que exige apenas tipos primitivos, operadores e controle de fluxo, sem orientação a objetos —, optei por restringir as verificações do console exclusivamente a horário e espécie. Regras como a verificação de data passada (RN04) também ficaram de fora deste programa porque exigem comparar a data informada com a data atual do sistema, uma informação que o programa não possui por trabalhar unicamente com os valores informados pelo usuário na hora da digitação. Já a lista de espécies aceita quatro variações de texto ("cão", "cao", "cachorro" e "gato") por uma decisão pragmática alinhada ao escopo do TP1, enquanto o tratamento com `.ToLower()` garante que maiúsculas e minúsculas sejam indiferentes.

Em suma, este recorte procedural implementa as regras fundamentais usando variáveis primitivas, operadores e estruturas de controle, preparando o terreno para que as espécies se tornem escolhas controladas e as regras de negócio passem a integrar classes e objetos orientados a domínio nos próximos TPs.