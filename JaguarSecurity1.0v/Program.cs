void MostrarRegistros()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=====================================================================================================");
    Console.WriteLine("                              REGISTROS DE LA SESIÓN ACTUAL");
    Console.WriteLine("=====================================================================================================\n");
    Console.ResetColor();

    // Validacion de registros
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay vehículos registrados en esta sesión actualmente.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("  Nota: Los textos muy largos se muestran resumidos para mantener la estabilidad visual de la tabla.");
    Console.WriteLine("        El registro completo se almacena de forma íntegra en el sistema.\n");
    Console.ResetColor();
    // Encabezado de la tabla estructurada (Ancho total optimizado)
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔════╦══════════════╦══════════════════╦════════════════════════╦════════════════╦══════════════════════╦═══════════════════════════╗");
    Console.WriteLine("║ Nº ║ Placa        ║ Tipo             ║ Conductor              ║ Cédula         ║ Destino              ║ Detalles                  ║");
    Console.WriteLine("╠════╬══════════════╬══════════════════╬════════════════════════╬════════════════╬══════════════════════╬═══════════════════════════╣");
    Console.ResetColor();

    for (int r = 0; r < i; r++)
    {
        // Alinear y limitar el texto para cada columna usando los nuevos anchos seguros
        string num = (r + 1).ToString().PadRight(2);

        string placa = vehiculos[r].placa.Length > 12 ? vehiculos[r].placa.Substring(0, 12) : vehiculos[r].placa.PadRight(12);
        string tipo = vehiculos[r].tipo.Length > 16 ? vehiculos[r].tipo.Substring(0, 16) : vehiculos[r].tipo.PadRight(16);
        string cond = vehiculos[r].conductor.Length > 22 ? vehiculos[r].conductor.Substring(0, 22) : vehiculos[r].conductor.PadRight(22);
        string ced = vehiculos[r].cedula.Length > 14 ? vehiculos[r].cedula.Substring(0, 14) : vehiculos[r].cedula.PadRight(14);
        string dest = vehiculos[r].destino.Length > 20 ? vehiculos[r].destino.Substring(0, 20) : vehiculos[r].destino.PadRight(20);

        // NUEVA COLUMNA(Faltaba por añadirse: Detalles (Límite visual de 25 caracteres)
        string det = vehiculos[r].detalles.Length > 25 ? vehiculos[r].detalles.Substring(0, 25) : vehiculos[r].detalles.PadRight(25);

        // Imprimir la fila perfectamente alineada
        Console.WriteLine($"║ {num} ║ {placa} ║ {tipo} ║ {cond} ║ {ced} ║ {dest} ║ {det} ║");
    }

    // Pie de la tabla
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╚════╩══════════════╩══════════════════╩════════════════════════╩════════════════╩══════════════════════╩═══════════════════════════╝");
    // Mostrar el total de vehículos registrados
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nTotal de vehículos registrados: {i}");
    Console.ResetColor();
}
