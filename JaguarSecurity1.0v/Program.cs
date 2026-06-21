void MostrarRegistros()
{
    while (true)
    {
        Console.Clear();
        Console.Write("\x1b[3J"); // Limpia el rastro del scroll de la consola

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                               BITÁCORA DE CONTROL DE ACCESO VEHICULAR                                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        if (i == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  [i] La bitácora de la sesión actual se encuentra vacía. No hay vehículos registrados.");
            Console.ResetColor();
            Console.WriteLine("\n  Presione cualquier tecla para regresar al Menú Principal...");
            Console.ReadKey();
            break; // Regresa al menú principal
        }

        // Orden y formato de la tabla optimizados para mejor legibilidad
        string formatoTabla = " │ {0,-3} │ {1,-8} │ {2,-13} │ {3,-10} │ {4,-18} │ {5,-14} │ {6,-12} │ {7,-25} │";

        Console.ForegroundColor = ConsoleColor.Cyan;
        // Bordes superiores ajustados a los nuevos tamaños
        Console.WriteLine(" ┌─────┬──────────┬───────────────┬────────────┬────────────────────┬────────────────┬──────────────┬───────────────────────────┐");
        Console.WriteLine(string.Format(formatoTabla, "Nº", "Hora", "Tipo Vehículo", "Placa", "Nombre Conductor", "Cédula", "Destino", "Detalles/Observaciones"));
        Console.WriteLine(" ├─────┼──────────┼───────────────┼────────────┼────────────────────┼────────────────┼──────────────┼───────────────────────────┤");
        Console.ResetColor();
        for (int v = 0; v < i; v++)
        {
            string num = (v + 1).ToString();
            string hora = vehiculos[v].horaIngreso.ToString("HH:mm:ss"); // <--- Ahora se calcula de segundo
            string tipo = vehiculos[v].tipo.Length > 16 ? vehiculos[v].tipo.Substring(0, 13) + "..." : vehiculos[v].tipo;
            string placa = vehiculos[v].placa.Length > 10 ? vehiculos[v].placa.Substring(0, 10) : vehiculos[v].placa;
            string cond = vehiculos[v].conductor.Length > 22 ? vehiculos[v].conductor.Substring(0, 19) + "..." : vehiculos[v].conductor;
            string ced = vehiculos[v].cedula;
            string dest = vehiculos[v].destino.Length > 15 ? vehiculos[v].destino.Substring(0, 12) + "..." : vehiculos[v].destino;
            // NUEVO: Detalles con límite amplio de 25 caracteres
            string det = vehiculos[v].detalles.Length > 25 ? vehiculos[v].detalles.Substring(0, 22) + "..." : vehiculos[v].detalles;

            // Se mandan las variables en el orden exacto del formato
            Console.WriteLine(string.Format(formatoTabla, num, hora, tipo, placa, cond, ced, dest, det));
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" └─────┴──────────┴───────────────┴────────────┴────────────────────┴────────────────┴──────────────┴───────────────────────────┘");
        Console.ResetColor();

        // --- BARRA INTERACTIVA DE OPCIONES ---

        Console.WriteLine("\n  [ENTER] Volver al Menú   │   [E] Eliminar Registro   │   [M] Modificar Registro");
        Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Seleccione una acción: ");
        Console.ResetColor();

        string accion = Console.ReadLine()!.Trim().ToUpper();

        if (accion == "" || accion == "ENTER")
        {
            break;
        }
        else if (accion == "E")
        {
            SubProcesoEliminar();
        }
        else if (accion == "M")
        {
            SubProcesoModificar();
        }
    }
}