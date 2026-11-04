# Documento de requisitos: VidaPet

**Nome:** Fernando Leite Rodrigues

**Disciplina:** Projeto de Bloco: Desenvolvimento Back-End [26E4_5] · Instituto Infnet

**Atividade:** TP1 · Requisitos e casos de uso

## 1. O sistema

**Sistema de Agendamento da Clínica Veterinária VidaPet.** Organiza o cadastro de animais e
veterinários e a agenda de consultas de uma clínica pequena que atende cães e gatos, no lugar do
caderno da recepção, que gera horários duplicados e consultas esquecidas.

**Dentro do escopo:** cadastrar animais (nome, espécie, idade, nome e telefone do tutor) e
veterinários (nome e CRMV); agendar consultas de 30 minutos, de segunda a sexta, das 8h às 18h,
sem conflito de veterinário e nunca em data passada; cancelar consulta agendada; consultar a
agenda do dia de um veterinário; registrar a consulta como realizada, com observações, só pelo
veterinário da consulta.

**Fora do escopo, até o professor pedir:** lembretes de consulta; editar e excluir animais e
veterinários (P01); feriados, férias de veterinário, encaixe, lista de espera, pagamento e
prontuário; tutor como entidade própria (PR04). A justificativa está em `justificativa-escopo.md`.

## 2. Regras de negócio (RN)

Vêm do enunciado e existem mesmo sem software.

| ID | Regra |
|---|---|
| RN01 | Toda consulta dura 30 minutos. |
| RN02 | Consultas só são marcadas de segunda a sexta, das 8h às 18h. |
| RN03 | Um veterinário não pode ter duas consultas agendadas no mesmo horário. |
| RN04 | Não se agenda consulta em data que já passou. |
| RN05 | Uma consulta só pode ser cancelada enquanto estiver agendada. |
| RN06 | Só uma consulta agendada pode ser marcada como realizada. |
| RN07 | Só o veterinário da consulta pode marcá-la como realizada. |
| RN08 | A clínica atende apenas cães e gatos. |

Decorrência de RN05 e RN06: a consulta nasce **agendada** e segue para **realizada** ou
**cancelada**; os dois estados são finais.

## 3. Requisitos funcionais (RF)

| ID | O sistema deve… | Origem |
|---|---|---|
| RF01 | cadastrar um animal com nome, espécie, idade, nome do tutor e telefone do tutor. | enunciado |
| RF02 | recusar o cadastro de animal cuja espécie não seja cão ou gato. | RN08 |
| RF03 | cadastrar um veterinário com nome e CRMV. | enunciado |
| RF04 | agendar uma consulta para um animal com um veterinário em uma data e um horário. | enunciado |
| RF05 | recusar agendamento fora de segunda a sexta ou fora da janela das 8h às 18h, considerada a duração de 30 minutos. | RN01 · RN02 · PR01 |
| RF06 | recusar agendamento quando o veterinário já tiver consulta agendada no mesmo horário. | RN03 · PR03 |
| RF07 | recusar agendamento em data e horário já passados. | RN04 · PR02 |
| RF08 | cancelar uma consulta agendada, mudando a situação para cancelada. | enunciado · RN05 |
| RF09 | recusar o cancelamento de consulta realizada ou já cancelada. | RN05 |
| RF10 | listar as consultas de um veterinário em um dia, com animal, horário e situação. | enunciado · derivado (a recepcionista precisa dela para agendar e cancelar) |
| RF11 | registrar uma consulta agendada como realizada, com as observações do atendimento. | enunciado · RN06 |
| RF12 | recusar o registro como realizada de consulta que não esteja agendada. | RN06 |
| RF13 | recusar o registro como realizada feito por veterinário diferente do veterinário da consulta. | RN07 |

## 4. Requisitos não funcionais (RNF)

| ID | Categoria | Requisito | Origem |
|---|---|---|---|
| RNF01 | tecnologia | Desenvolvido em C# com ASP.NET Core (.NET 10), trilha mantida até a Apresentação Final. | enunciado · escolha do aluno |
| RNF02 | segurança | O perfil recepcionista executa RF01–RF10; o perfil veterinário executa RF10–RF13; em RF11 o usuário autenticado deve ser o veterinário da consulta. | RN07 · vale a partir da etapa de autenticação |
| RNF03 | segurança | RN01–RN07 são validadas no servidor, nunca apenas no cliente. | vale a partir da etapa de API |
| RNF04 | confiabilidade | O agendamento é atômico: ou a consulta é gravada sem conflito, ou nada muda; dois agendamentos simultâneos para o mesmo veterinário e horário nunca gravam ambos. | RN03 · problema dos horários duplicados |
| RNF05 | desempenho | A agenda do dia de um veterinário responde em até 2 s. | premissa, a calibrar com o professor |
| RNF06 | usabilidade | Agendar uma consulta exige no máximo 3 passos a partir da agenda do dia. | premissa, a calibrar com o professor |
| RNF07 | privacidade | Nome e telefone de tutor usados em desenvolvimento, teste e demonstração são fictícios. | LGPD |

## 5. Premissas assumidas

Decisões tomadas onde o enunciado não responde; valem até o professor confirmar ou corrigir.

| ID | Premissa |
|---|---|
| PR01 | Horários começam em múltiplos de 30 min (8:00, 8:30 … 17:30); o último início é 17:30, para a consulta terminar às 18h. |
| PR02 | "Data que já passou" compara data e hora: hoje às 9h, depois das 9h, é passado. |
| PR03 | Com horários em múltiplos de 30 min, "mesmo horário" (RN03) é o mesmo início; não há sobreposição parcial a tratar. |
| PR04 | Tutor fica como atributos de Animal, como no enunciado; não vira entidade. |
| PR05 | Feriados não são tratados; só o calendário de segunda a sexta. |

### Pontos em aberto com o professor

| ID | Pergunta |
|---|---|
| P01 | Editar e excluir animal e veterinário estão no escopo? O enunciado só diz "cadastra". |
| P02 | O mesmo animal pode ter duas consultas no mesmo horário com veterinários diferentes? RN03 só fala do veterinário. |
| P03 | "Observações" é preenchido só na realização ou também no agendamento? |
| P04 | Como o veterinário cadastrado se liga ao usuário que entra no sistema? |
| P06 | O programa standalone do TP1 já deve tocar o domínio ou basta exercitar tipos, operadores e controle de fluxo? |
| P07 | O vídeo apresenta os requisitos e casos de uso, o programa, ou os dois? |

## 6. Atores e casos de uso

Diagrama: `casos-de-uso.svg` (fonte em `casos-de-uso.puml`).

| Ator | Papel |
|---|---|
| Recepcionista | cadastra animais e veterinários, agenda e cancela consultas |
| Veterinário | consulta sua agenda do dia e registra a consulta como realizada |

| Código | Caso de uso | Ator principal | Requisitos |
|---|---|---|---|
| UC01 | Cadastrar animal | Recepcionista | RF01 · RF02 |
| UC02 | Cadastrar veterinário | Recepcionista | RF03 |
| UC03 | Agendar consulta | Recepcionista | RF04 · RF05 · RF06 · RF07 |
| UC04 | Cancelar consulta | Recepcionista | RF08 · RF09 |
| UC05 | Consultar agenda do dia | Veterinário · Recepcionista | RF10 |
| UC06 | Registrar consulta realizada | Veterinário | RF11 · RF12 · RF13 |
| UC07 | Verificar disponibilidade | — (incluído por UC03) | RF05 · RF06 · RF07 |

**UC03 «include» UC07.** Todo agendamento verifica janela (RN01, RN02), data passada (RN04) e
conflito de veterinário (RN03) antes de gravar. Não há «extend» nem generalização de atores: o
enunciado não traz comportamento opcional, e recepcionista e veterinário só compartilham UC05.
O login fica fora do diagrama, como pré-condição "ator identificado" nas especificações.

## 7. Especificações de casos de uso

Os três casos especificados são os que concentram regras de negócio. Os fluxos alternativos são
numerados pelo passo de onde desviam.

### UC03 – Agendar consulta

| Campo | Conteúdo |
|---|---|
| Ator principal | Recepcionista |
| Pré-condição | Recepcionista identificada; animal e veterinário cadastrados |
| Fluxo principal | 1. Recepcionista informa o animal.<br>2. Sistema mostra o animal e o tutor.<br>3. Recepcionista informa o veterinário e a data.<br>4. Sistema mostra a agenda do dia do veterinário (RF10).<br>5. Recepcionista escolhe o horário.<br>6. Sistema verifica a disponibilidade (UC07): janela de segunda a sexta, das 8h às 18h, em horários de 30 min (RN01, RN02, PR01); data e horário não passados (RN04, PR02); veterinário sem consulta agendada no horário (RN03, PR03).<br>7. Recepcionista confirma.<br>8. Sistema registra a consulta como agendada e mostra animal, veterinário, data e horário. |
| Fluxos alternativos | 2a. Animal não encontrado: sistema avisa e volta ao passo 1.<br>6a. Fora da janela (RF05): sistema mostra o motivo e volta ao passo 5.<br>6b. Data ou horário já passados (RF07): sistema mostra o motivo e volta ao passo 3.<br>6c. Veterinário já tem consulta agendada no horário (RF06): sistema mostra o conflito e volta ao passo 5.<br>7a. Recepcionista desiste: nada é registrado. |
| Pós-condição | Consulta registrada como agendada; o horário do veterinário fica ocupado. |
| Requisitos | RF04 · RF05 · RF06 · RF07 · RF10 · RN01–RN04 · RNF04 · PR01–PR03 |

### UC04 – Cancelar consulta

| Campo | Conteúdo |
|---|---|
| Ator principal | Recepcionista |
| Pré-condição | Recepcionista identificada; consulta registrada |
| Fluxo principal | 1. Recepcionista informa o veterinário e a data.<br>2. Sistema lista as consultas do dia, com animal, horário e situação (RF10).<br>3. Recepcionista escolhe a consulta.<br>4. Sistema verifica que a consulta está agendada (RN05).<br>5. Recepcionista confirma o cancelamento.<br>6. Sistema muda a situação para cancelada. |
| Fluxos alternativos | 2a. Nenhuma consulta no dia: sistema avisa e volta ao passo 1.<br>4a. Consulta realizada ou já cancelada (RF09): sistema recusa, mostra a situação atual e encerra.<br>5a. Recepcionista desiste: nada muda. |
| Pós-condição | Consulta cancelada (estado final). O horário volta a ficar livre, porque RN03 só conta consultas agendadas. |
| Requisitos | RF08 · RF09 · RF10 · RN05 · RN03 |

### UC06 – Registrar consulta realizada

| Campo | Conteúdo |
|---|---|
| Ator principal | Veterinário |
| Pré-condição | Veterinário identificado (P04); consulta registrada |
| Fluxo principal | 1. Veterinário pede sua agenda do dia.<br>2. Sistema lista as consultas dele no dia (RF10).<br>3. Veterinário escolhe a consulta atendida.<br>4. Sistema verifica que a consulta está agendada (RN06) e que é dele (RN07).<br>5. Veterinário informa as observações do atendimento.<br>6. Sistema muda a situação para realizada e grava as observações. |
| Fluxos alternativos | 4a. Consulta não está agendada (RF12): sistema recusa, mostra a situação atual e encerra.<br>4b. Consulta de outro veterinário (RF13): sistema recusa e encerra. A agenda do passo 2 só mostra as dele, mas a regra vale também para quem informar a consulta por outro caminho (RNF03).<br>5a. Veterinário desiste: nada muda. |
| Pós-condição | Consulta realizada (estado final), com as observações gravadas. |
| Requisitos | RF10 · RF11 · RF12 · RF13 · RN06 · RN07 · RNF02 · RNF03 |
