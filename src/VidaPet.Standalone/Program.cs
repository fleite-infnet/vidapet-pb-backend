// ==========================================
// VidaPet.Standalone - TP1 Projeto de Bloco: Desenvolvimento Back-End [26E4_5] - Instituto Infnet
// Fernando Leite Rodrigues
// 03/11/2026
// ==========================================

bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("         SISTEMA VIDAPET - TP1          ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Verificar Horário de Atendimento (RN02/RN01)");
    Console.WriteLine("2. Verificar Espécie Permitida (RN08)");
    Console.WriteLine("3. Sair");
    Console.WriteLine("----------------------------------------");
    Console.Write("Escolha uma opção (1-3): ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            VerificarHorarioAtendimento();
            break;

        case "2":
            VerificarEspecieAnimal();
            break;

        case "3":
            executando = false;
            Console.WriteLine("\nEncerrando o programa. Até logo!");
            break;

        default:
            Console.WriteLine("\n[Falha] Opção inválida! Escolha um número entre 1 e 3.");
            Pausar();
            break;
    }
}

// ==========================================
// FUNÇÕES LOCAIS DE SUPORTE
// ==========================================

void VerificarHorarioAtendimento()
{
    Console.Clear();
    Console.WriteLine("--- Verificação de Horário (Seg-Sex, 8h-18h | Consultas de 30 min) ---");

    // Leitura do Dia da Semana (1 a 7)
    Console.Write("Informe o dia da semana (1-Segunda a 7-Domingo): ");
    if (!int.TryParse(Console.ReadLine(), out int diaSemana) || diaSemana < 1 || diaSemana > 7)
    {
        Console.WriteLine("\n[Falha] Dia da semana inválido. Digite um número entre 1 e 7.");
        Pausar();
        return;
    }

    // Leitura da Hora
    Console.Write("Informe a hora (0 a 23): ");
    if (!int.TryParse(Console.ReadLine(), out int hora) || hora < 0 || hora > 23)
    {
        Console.WriteLine("\n[Falha] Hora inválida. Digite um valor entre 0 e 23.");
        Pausar();
        return;
    }

    // Leitura do Minuto
    Console.Write("Informe o minuto (0 a 59): ");
    if (!int.TryParse(Console.ReadLine(), out int minuto) || minuto < 0 || minuto > 59)
    {
        Console.WriteLine("\n[Falha] Minuto inválido. Digite um valor entre 0 e 59.");
        Pausar();
        return;
    }

    // Minutos desde a meia-noite e limites de expediente
    int totalMinutos = (hora * 60) + minuto;
    int inicioExpedienteMinutos = 8 * 60;        // 08:00 (480 min)
    int fimExpedienteMinutos = (17 * 60) + 30;   // 17:30 (1050 min - última consulta)

    // Aplicação das Regras
    bool diaUtil = (diaSemana >= 1 && diaSemana <= 5);
    bool horarioComercial = (totalMinutos >= inicioExpedienteMinutos) && (totalMinutos <= fimExpedienteMinutos);
    bool minutoValido = (minuto % 30 == 0); // Múltiplos de 30 (0 ou 30)

    if (diaUtil && horarioComercial && minutoValido)
    {
        Console.WriteLine($"\n[Sucesso] Horário Permitido! Agendamento válido para {hora:D2}:{minuto:D2}.");
    }
    else
    {
        Console.WriteLine("\n[Falha] Horário Negado.");
        if (!diaUtil) Console.WriteLine(" - Motivo: Atendimento apenas de Segunda a Sexta.");
        if (!horarioComercial) Console.WriteLine(" - Motivo: Fora do expediente (8h às 18h, última consulta às 17h30).");
        if (!minutoValido) Console.WriteLine(" - Motivo: As consultas devem iniciar nos minutos 00 ou 30.");
    }

    Pausar();
}

void VerificarEspecieAnimal()
{
    Console.Clear();
    Console.WriteLine("--- Verificação de Espécie (RN08: Apenas Cães e Gatos) ---");
    Console.Write("Informe a espécie do animal: ");
    
    string? especie = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(especie))
    {
        Console.WriteLine("\n[Falha] Entrada vazia. Informe uma espécie válida.");
        Pausar();
        return;
    }

    string especieNormalizada = especie.Trim().ToLower();

    if (especieNormalizada == "cão" || especieNormalizada == "cao" || especieNormalizada == "cachorro" || especieNormalizada == "gato")
    {
        Console.WriteLine($"\n[Sucesso] Espécie '{especie}' aceita pela clínica!");
    }
    else
    {
        Console.WriteLine($"\n[Falha] A espécie '{especie}' não é atendida pela clínica (Apenas Cães e Gatos).");
    }

    Pausar();
}

void Pausar()
{
    Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
    Console.ReadKey();
}