void MostrarRegistros()
{

    // Validacion de registros
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay vehículos registrados en esta sesión actualmente.");
        Console.ResetColor();
        return;
    }

    for (int r = 0; r < i; r++)
    {
        // Alinear y limitar el texto para cada columna
        string num = (r + 1).ToString().PadRight(2);


        string placa = vehiculos[r].placa;
        string tipo = vehiculos[r].tipo;
        string cond = vehiculos[r].conductor;
        string ced = vehiculos[r].cedula;
        string dest = vehiculos[r].destino;

        // Imprimir la fila con formato
        Console.WriteLine($"║ {num} ║ {placa} ║ {tipo} ║ {cond} ║ {ced} ║ {dest} ║");
    }

    // Encabezado de la tabla
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔════╦══════════════╦══════════════════╦══════════════════════╦════════════════╦══════════════════╗");
    Console.WriteLine("║ Nº ║ Placa        ║ Tipo             ║ Conductor            ║ Cédula         ║ Destino          ║");
    Console.WriteLine("╠════╬══════════════╬══════════════════╬══════════════════════╬════════════════╬══════════════════╣");
    Console.ResetColor();

    Console.WriteLine($"║ {num} ║ {placa} ║ {tipo} ║ {cond} ║ {ced} ║ {dest} ║");

    // Pie de la tabla
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╚════╩══════════════╩══════════════════╩══════════════════════╩════════════════╩══════════════════╝");

    // Mostrar el total de vehículos registrados
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n Total de vehículos registrados: {i}");
    Console.ResetColor();


}

