void BuscarPorPlaca()
{
    Console.Clear();
    Console.Write("\x1b[3J");

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║             CONSULTA DE ANTECEDENTES POR PLACA VEHICULAR            ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  [i] No existen registros en la sesión actual.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("\n  >> Ingrese la placa a consultar: ");
    Console.ResetColor();

    string placaBuscada = Console.ReadLine()!.Trim().ToUpper();

    bool encontrado = false;

    for (int j = 0; j < i; j++)
    {
        if (vehiculos[j].placa != null &&
            vehiculos[j].placa.ToUpper() == placaBuscada)
        {
            encontrado = true;

            Guardia operadorActual = guardias[totalLogins - 1];

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     REGISTRO VEHICULAR ENCONTRADO                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine($"  ├─ Placa                : {vehiculos[j].placa}");
            Console.WriteLine($"  ├─ Conductor            : {vehiculos[j].conductor}");
            Console.WriteLine($"  ├─ Cédula               : {vehiculos[j].cedula}");
            Console.WriteLine($"  ├─ Destino              : {vehiculos[j].destino}");
            Console.WriteLine($"  ├─ Detalles             : {vehiculos[j].detalles}");
            Console.WriteLine($"  ├─ Hora de Ingreso      : {vehiculos[j].horaIngreso:dd/MM/yyyy - HH:mm:ss}");
            Console.WriteLine($"  └─ Registrado por       : {operadorActual.nombre} {operadorActual.apellido}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n──────────────────────────────────────────────────────────────────────");
            Console.WriteLine(" Estado del registro: ACTIVO EN BITÁCORA ACTUAL");
            Console.ResetColor();

            break;
        }
    }

    if (!encontrado)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n  [!] No se encontró ningún registro asociado a esa placa.");
        Console.ResetColor();
    }
}