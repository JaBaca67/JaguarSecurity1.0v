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
    Console.ResetColor();
    // Encabezado de la tabla estructurada (Ancho total optimizado)
    Console.ForegroundColor = ConsoleColor.Yellow;
    // Agregamos el espacio para "Hora" (8 caracteres)
    Console.WriteLine("╔════╦══════════╦══════════════╦══════════════════╦══════════════════════╦══════════════╦══════════════════╦════════════════════╗");
    Console.WriteLine("║ Nº ║ Hora     ║ Placa        ║ Tipo             ║ Conductor            ║ Cédula       ║ Destino          ║ Detalles           ║");
    Console.WriteLine("╠════╬══════════╬══════════════╬══════════════════╬══════════════════════╬══════════════╬══════════════════╬════════════════════╣");
    Console.ResetColor();

    for (int r = 0; r < i; r++)
    {
        string num = (r + 1).ToString().PadRight(2);

        // Formateamos la hora para que siempre ocupe exactamente 8 caracteres (ej: 14:30:00)
        string hora = vehiculos[r].horaIngreso.ToString("HH:mm:ss").PadRight(8);

        string placa = vehiculos[r].placa.Length > 12 ? vehiculos[r].placa.Substring(0, 12) : vehiculos[r].placa.PadRight(12);
        string tipo = vehiculos[r].tipo.Length > 16 ? vehiculos[r].tipo.Substring(0, 16) : vehiculos[r].tipo.PadRight(16);
        string cond = vehiculos[r].conductor.Length > 20 ? vehiculos[r].conductor.Substring(0, 20) : vehiculos[r].conductor.PadRight(20);
        string ced = vehiculos[r].cedula.Length > 12 ? vehiculos[r].cedula.Substring(0, 12) : vehiculos[r].cedula.PadRight(12);
        string dest = vehiculos[r].destino.Length > 16 ? vehiculos[r].destino.Substring(0, 16) : vehiculos[r].destino.PadRight(16);
        string det = vehiculos[r].detalles.Length > 18 ? vehiculos[r].detalles.Substring(0, 18) : vehiculos[r].detalles.PadRight(18);

        Console.WriteLine($"║ {num} ║ {hora} ║ {placa} ║ {tipo} ║ {cond} ║ {ced} ║ {dest} ║ {det} ║");
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╚════╩══════════╩══════════════╩══════════════════╩══════════════════════╩══════════════╩══════════════════╩════════════════════╝");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nTotal de vehículos registrados: {i}");
    Console.ResetColor();
}