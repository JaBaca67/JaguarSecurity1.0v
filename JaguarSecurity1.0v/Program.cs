// =====================================================================
//   OPCIÓN 4: INFORMACIÓN DEL OPERADOR E HISTORIAL DE ACCESOS
// =====================================================================
void InformacionOperadorHistorialAccesos()
{
    Console.Clear();
    Console.Write("\x1b[3J"); // Limpia el rastro del scroll

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" ╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine(" ║                                     SESIÓN ACTUAL & HISTORIAL DE ACCESOS DE GUARDIAS                                      ║");
    Console.WriteLine(" ╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    int indiceActual = totalLogins - 1;
    Guardia operadorActual = guardias[indiceActual];

    string portonOperadorActual = string.IsNullOrEmpty(operadorActual.porton) ? "No definido" : operadorActual.porton;

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  >>> OPERADOR EN TURNO ACTUAL <<<");
    Console.ResetColor();
    Console.WriteLine($"  • Nombre Completo        : {operadorActual.nombre} {operadorActual.apellido}");
    Console.WriteLine($"  • Credencial (ID)        : {operadorActual.id}");
    Console.WriteLine($"  • Usuario de Red         : {operadorActual.nombreUsuario}");
    Console.WriteLine($"  • Portón Asignado        : {portonOperadorActual}");
    Console.WriteLine($"  • Hora de Entrada        : {operadorActual.horaInicio.ToString("dd/MM/yyyy - HH:mm:ss")}");
    Console.WriteLine($"  • Vehículos Registrados  : {i} vehículos en este turno");

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("\n ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  >>> HISTORIAL COMPLETO DE LOGINS DEL DÍA <<<");

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" ┌────┬───────────────────┬────────────┬──────────┬──────────┬────────────────┬──────────────────────┬──────────────────────┐");

    // Títulos
    Console.Write(" │"); Console.ResetColor(); Console.Write("Nº".PadRight(4));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Nombre del Guardia".PadRight(19));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Usuario".PadRight(12));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("ID Cred.".PadRight(10));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Vehículos".PadRight(10));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Portón".PadRight(16));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Hora de Entrada".PadRight(22));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│"); Console.ResetColor(); Console.Write("Hora de Salida".PadRight(22));
    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("│");

    Console.WriteLine(" ├────┼───────────────────┼────────────┼──────────┼──────────┼────────────────┼──────────────────────┼──────────────────────┤");

    for (int j = 0; j < totalLogins; j++)
    {
        Guardia g = guardias[j];
        string num = (j + 1).ToString();

        string nombreCompleto = $"{g.nombre} {g.apellido}";
        string nombreMostrar = nombreCompleto.Length > 19 ? nombreCompleto.Substring(0, 16) + "..." : nombreCompleto;

        string usuario = string.IsNullOrEmpty(g.nombreUsuario) ? "N/A" : g.nombreUsuario;
        usuario = usuario.Length > 12 ? usuario.Substring(0, 12) : usuario;

        string credencial = string.IsNullOrEmpty(g.id) ? "N/A" : g.id;

        // Variables corregidas y en orden
        string vehiculosTxt = (j == indiceActual) ? i.ToString() : g.vehiculosRegistrados.ToString();
        string portonMostrar = string.IsNullOrEmpty(g.porton) ? "N/A" : g.porton;
        portonMostrar = portonMostrar.Length > 16 ? portonMostrar.Substring(0, 16) : portonMostrar;

        string horaIn = g.horaInicio.ToString("dd/MM/yyyy - HH:mm:ss");
        string horaOut = g.horaSalida.HasValue ? g.horaSalida.Value.ToString("dd/MM/yyyy - HH:mm:ss") : "En curso...";

        // Imprimimos asegurando que el orden coincide exactamente con los títulos de arriba
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" │");
        Console.ResetColor(); Console.Write(num.PadRight(4));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(nombreMostrar.PadRight(19));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(usuario.PadRight(12));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(credencial.PadRight(10));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(vehiculosTxt.PadRight(10)); // <--- VEHÍCULOS VA AQUÍ
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(portonMostrar.PadRight(16)); // <--- PORTÓN VA AQUÍ
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(horaIn.PadRight(22));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(horaOut.PadRight(22));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("│");

        if (j < totalLogins - 1)
        {
            Console.WriteLine(" ├────┼───────────────────┼────────────┼──────────┼──────────┼────────────────┼──────────────────────┼──────────────────────┤");
        }
        else
        {
            Console.WriteLine(" └────┴───────────────────┴────────────┴──────────┴──────────┴────────────────┴──────────────────────┴──────────────────────┘");
        }
    }
}