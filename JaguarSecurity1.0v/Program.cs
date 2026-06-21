// =====================================================================
//   OPCIÓN 4: INFORMACIÓN DEL OPERADOR E HISTORIAL DE ACCESOS
// =====================================================================
void InformacionOperadorHistorialAccesos()
{
    Console.Clear();
    Console.Write("\x1b[3J"); // Limpia el rastro del scroll

    // 1. ENCABEZADO ORDENADO Y ALINEADO (Bordes celestes)
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" ╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine(" ║                                       SESIÓN ACTUAL & HISTORIAL DE ACCESOS DE GUARDIAS                                      ║");
    Console.WriteLine(" ╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    // 2. VALIDACIÓN: Verificar si hay registros en el día
    if (totalLogins == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  [i] El historial de accesos se encuentra vacío en este momento.");
        Console.WriteLine("      Aún no se ha registrado ningún turno en el sistema el día de hoy.");
        Console.ResetColor();
        Console.WriteLine("\n  Presione cualquier tecla para regresar al Menú Principal...");
        Console.ReadKey();
        return; // Rompemos la función y volvemos al menú general
    }

    // 3. DATOS DEL OPERADOR EN TIEMPO REAL (ARRIBA)
    int indiceActual = totalLogins - 1;
    Guardia operadorActual = guardias[indiceActual];

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  >>> OPERADOR EN TURNO ACTUAL <<<");
    Console.ResetColor();
    Console.WriteLine($"  • Nombre Completo        : {operadorActual.nombre} {operadorActual.apellido}");
    Console.WriteLine($"  • Credencial (ID)        : {operadorActual.id}");
    Console.WriteLine($"  • Usuario de Red         : {operadorActual.nombreUsuario}");
    Console.WriteLine($"  • Hora de Entrada        : {operadorActual.horaInicio.ToString("dd/MM/yyyy - HH:mm:ss")}");
    Console.WriteLine($"  • Vehículos Registrados  : {i} vehículos en este turno"); // Usa la 'i' en tiempo real

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("\n ───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
    Console.ResetColor();

    // 4. TABLA DEL HISTORIAL COMPLETO DEL DÍA (ABAJO)
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  >>> HISTORIAL COMPLETO DE LOGINS DEL DÍA <<<");

    // Imprimimos el borde superior de la tabla en Celeste
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" ┌────┬────────────────────┬───────────────┬──────────────┬──────────────────────┬──────────────────────┬──────────────────────┐");

    // Imprimimos los títulos alternando Celeste (Bordes) y Normal (Texto)
    Console.Write(" │"); Console.ResetColor(); Console.Write("Nº".PadRight(4));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Nombre del Guardia".PadRight(20));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Usuario de Red".PadRight(15));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("ID Credencial".PadRight(14));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Vehículos Registrados".PadRight(22));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Hora de Entrada".PadRight(22));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Hora de salida".PadRight(22));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("│");

    // Divisor de títulos en Celeste
    Console.WriteLine(" ├────┼────────────────────┼───────────────┼──────────────┼──────────────────────┼──────────────────────┼──────────────────────┤");

    // Recorremos el arreglo de guardias
    for (int j = 0; j < totalLogins; j++)
    {
        Guardia g = guardias[j];
        string num = (j + 1).ToString();

        string nombreCompleto = $"{g.nombre} {g.apellido}";
        string nombreMostrar = nombreCompleto.Length > 20 ? nombreCompleto.Substring(0, 17) + "..." : nombreCompleto;

        string usuario = g.nombreUsuario.Length > 15 ? g.nombreUsuario.Substring(0, 15) : g.nombreUsuario;
        string credencial = g.id;

        string vehiculosTxt = (j == indiceActual) ? i.ToString() : g.vehiculosRegistrados.ToString();
        string horaIn = g.horaInicio.ToString("dd/MM/yyyy - HH:mm:ss");
        string horaOut = g.horaSalida.HasValue ? g.horaSalida.Value.ToString("dd/MM/yyyy - HH:mm:ss") : "En curso...";

        // Imprimimos cada fila alternando Celeste (Bordes) y Normal (Texto de los datos)
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" │");
        Console.ResetColor(); Console.Write(num.PadRight(4));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(nombreMostrar.PadRight(20));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(usuario.PadRight(15));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(credencial.PadRight(14));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(vehiculosTxt.PadRight(22));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(horaIn.PadRight(22));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(horaOut.PadRight(22));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("│");
    }

    // Borde inferior en Celeste
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" └────┴────────────────────┴───────────────┴──────────────┴──────────────────────┴──────────────────────┴──────────────────────┘");
    Console.ResetColor();

    Console.WriteLine("\n  Presione cualquier tecla para regresar al Menú Principal...");
    Console.ReadKey();
}