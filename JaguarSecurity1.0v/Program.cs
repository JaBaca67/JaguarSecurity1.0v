// =====================================================================
//   OPCIÓN 5: MÓDULO DE AUDITORÍA (REVISIÓN DE SESIONES ANTERIORES)
// =====================================================================
void ModuloAuditoria()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================================================");
        Console.WriteLine("      MÓDULO DE AUDITORÍA: CONTROL DE SESIONES ANTERIORES UAM      ");
        Console.WriteLine("====================================================================");
        Console.ResetColor();

        // 1. Escanear el directorio del programa para buscar archivos .csv
        string rutaActual = Directory.GetCurrentDirectory();
        string[] archivosGuardados = Directory.GetFiles(rutaActual, "*.csv");

        // Validación en caso de que no existan reportes guardados todavía
        if (archivosGuardados.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n >> No se han encontrado registros de sesiones anteriores (.CSV) en el sistema.");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
            break; // Sale de la auditoría y regresa al menú principal
        }

        Console.WriteLine("\nSeleccione el número del archivo de registro que desea auditar:\n");

        // 2. Listar de forma numérica dinámica todos los archivos encontrados (Guarda_Fecha_ID.csv)
        for (int idx = 0; idx < archivosGuardados.Length; idx++)
        {
            // Path.GetFileNameWithoutExtension quita la ruta y el ".csv" para que se vea limpio
            string nombreLimpio = Path.GetFileNameWithoutExtension(archivosGuardados[idx]);
            Console.WriteLine($" [{idx + 1}] {nombreLimpio}");
        }

        // Creamos una opción automática extra al final de la lista para salir ordenadamente
        int opcionSalir = archivosGuardados.Length + 1;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($" [{opcionSalir}] Volver al Menú Principal");
        Console.ResetColor();
        Console.WriteLine("────────────────────────────────────────────────────────────────────");

        // 3. Solicitar la opción al usuario
        int seleccion;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($" >> Digite una opción (1-{opcionSalir}): ");
        Console.ResetColor();
        string entrada = Console.ReadLine()!.Trim();

        // 4. VALIDACIONES ESTRICTAS DE ENTRADA
        // Comprueba que sea un número entero y que esté estrictamente dentro del rango de la lista
        if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= opcionSalir)
        {
            // Si el guarda elige el último número, rompe el bucle de auditoría y vuelve al menú
            if (seleccion == opcionSalir)
            {
                break;
            }

            // 5. Lectura y despliegue del contenido del archivo seleccionado
            string rutaArchivoElegido = archivosGuardados[seleccion - 1];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=====================================================================================================");
            Console.WriteLine($" VISUALIZANDO HISTORIAL DE AUDITORÍA: {Path.GetFileName(rutaArchivoElegido).ToUpper()}");
            Console.WriteLine("=====================================================================================================\n");
            Console.ResetColor();

            // Lee todas las líneas de texto almacenadas en ese CSV anterior
            string[] filasHistorial = File.ReadAllLines(rutaArchivoElegido);

            if (filasHistorial.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" >> El archivo seleccionado está vacío o no contiene datos tabulados.");
                Console.ResetColor();
            }
            else
            {
                // Imprime línea por línea el contenido original de la sesión auditada
                foreach (string fila in filasHistorial)
                {
                    Console.WriteLine(fila);
                }
            }

            Console.WriteLine("\n=====================================================================================================");
            Console.WriteLine("Presione cualquier tecla para regresar al listado numérico de auditoría...");
            Console.ReadKey();
            // El ciclo while continuará y volverá a pintar la lista de archivos por si desea auditar otro.
        }
        else
        {
            // Si digita letras, espacios vacíos o números fuera del rango
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n >> Error: Selección Inválida. Por favor digite un número correcto de la lista.");
            Console.ResetColor();
            Console.WriteLine(" Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}