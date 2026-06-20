// =====================================================================
//   OPCIÓN 6: CERRAR TURNO Y GENERAR REPORTES (.CSV)
// =====================================================================
bool CerrarTurnoYGenerarReporte()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                 CIERRE DE TURNO Y EXPORTACIÓN DE DATOS               ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    // Validacion inicial
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  [!] ALERTA: No se puede cerrar el turno ni generar el reporte.");
        Console.WriteLine("      Debe registrar al menos un (1) vehículo en la bitácora actual.");
        Console.ResetColor();

        //Evitamos que se cierre el turno cuando no hay registros
        return false;
    }

    //Confirmacion para el usuario
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  [?] ATENCIÓN: Está a punto de cerrar su turno actual.");
    Console.WriteLine("      Esto exportará la bitácora de vehículos y cerrará su sesión de usuario.");
    Console.Write("\n  ¿Está seguro que desea continuar? (S/N): ");
    Console.ResetColor();

    string confirmacion = Console.ReadLine()!.Trim().ToUpper();

    if (confirmacion != "S")
    {
        // Si digita cualquier cosa que no sea una S se cancela la operacion
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n  [i] Operación cancelada por el usuario.");
        Console.ResetColor();
        return false;
    }

    // Aca se captura la hora salida del guardia y calculamos el tiempo trabajado
    int indiceActual = totalLogins - 1;
    guardias[indiceActual].horaSalida = DateTime.Now;

    // Crear un variable de tipo TimeSpan para calcular el tiempo trabajado
    //Nota: TimeSpan es una estructura de C# que representa un intervalo de tiempo, en este caso lo usamos para calcular la duración del turno del guardia.
    Guardia operador = guardias[indiceActual];
    TimeSpan tiempoTrabajado = operador.horaSalida.Value - operador.horaInicio;

    // Aqui generamos el nombre del archivo de forma dinamica con el formato: nombre_apellido_fecha_id.csv
    string nombreLimpio = operador.nombre.Split(' ')[0].ToLower();
    string apellidoLimpio = operador.apellido.Split(' ')[0].ToLower();
    string idGuardia = operador.id;
    string fechaHoy = DateTime.Now.ToString("dd-MM-yyyy");

    string nombreArchivo = $"{nombreLimpio}_{apellidoLimpio}_{fechaHoy}_{idGuardia}.csv";

    try
    {
        List<string> lineasCsv = new List<string>();

        lineasCsv.Add("======================================================================================");
        lineasCsv.Add("                       REPORTE OFICIAL DE CONTROL DE ACCESO UAM                       ");
        lineasCsv.Add("                                 SISTEMA JAGUAR SECURITY                              ");
        lineasCsv.Add("======================================================================================");
        lineasCsv.Add("");

        //Datos del guarda
        lineasCsv.Add("--- I. DATOS DEL OPERADOR ---");
        lineasCsv.Add($"Nombre Completo:;{operador.nombre} {operador.apellido}");
        lineasCsv.Add($"Credencial (ID):;{operador.id}");
        lineasCsv.Add($"Usuario de Red:;{operador.nombreUsuario}");
        lineasCsv.Add($"Fecha y Hora de Entrada:;{operador.horaInicio.ToString("dd/MM/yyyy HH:mm:ss")}");
        lineasCsv.Add($"Fecha y Hora de Salida:;{operador.horaSalida.Value.ToString("dd/MM/yyyy HH:mm:ss")}");
        lineasCsv.Add($"Duración Total del Turno:;{tiempoTrabajado.Hours} Horas con {tiempoTrabajado.Minutes} Minutos");
        lineasCsv.Add("");

        // Bitacora de vehciulos registrados
        lineasCsv.Add("--- II. BITÁCORA DE VEHÍCULOS INGRESADOS ---");
        // Nueva columna
        lineasCsv.Add("Nº;Hora;Placa;Tipo de Vehículo;Conductor;Cédula;Destino;Detalles Adicionales");

        for (int r = 0; r < i; r++)
        {
            //Remplazar los ; por que se veian horribles
            string horaStr = vehiculos[r].horaIngreso.ToString("HH:mm:ss");
            string placa = vehiculos[r].placa.Replace(";", "-");
            string tipo = vehiculos[r].tipo.Replace(";", "-");
            string conductor = vehiculos[r].conductor.Replace(";", "-");
            string cedula = vehiculos[r].cedula.Replace(";", "-");
            string destino = vehiculos[r].destino.Replace(";", "-");
            string detalles = vehiculos[r].detalles.Replace(";", "-");

            // Se agrego la variable hora al csv
            lineasCsv.Add($"{r + 1};{horaStr};{placa};{tipo};{conductor};{cedula};{destino};{detalles}");
        }
        lineasCsv.Add("");

        // Cierre
        lineasCsv.Add("--- III. RESUMEN DE OPERACIONES ---");
        lineasCsv.Add($"Total de Vehículos Procesados:;{i}");
        lineasCsv.Add($"Estado del Turno:;CERRADO Y AUDITADO");
        lineasCsv.Add("======================================================================================");

        File.WriteAllLines(nombreArchivo, lineasCsv, System.Text.Encoding.UTF8);

        Console.WriteLine("\n──────────────────────────────────────────────────────────────────────");
        // Animacion Dinamica
        Console.Write("  Sincronizando datos locales");
        for (int p = 0; p < 6; p++)
        {
            Console.Write(".");
            Thread.Sleep(200);
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" [ OK ]");
        Console.ResetColor();

        // Animacion dinamica
        Console.Write("  Cerrando turno formalmente ");
        for (int p = 0; p < 6; p++)
        {
            Console.Write(".");
            Thread.Sleep(200);
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" [ OK ]");
        Console.ResetColor();

        // Animacion dinamica
        Console.Write("  Empaquetando archivo CSV   ");
        for (int p = 0; p < 6; p++)
        {
            Console.Write(".");
            Thread.Sleep(200);
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" [ OK ]");
        Console.ResetColor();
        Console.WriteLine("  El reporte oficial ha sido generado y tabulado correctamente:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  >> Directorio actual \\ {nombreArchivo}\n");
        Console.ResetColor();
        Console.WriteLine("\n──────────────────────────────────────────────────────────────────────");
        // Pausamos para que el usuario pueda leer que todo salio perfe
        Console.WriteLine("\n  Presione CUALQUIER TECLA para cerrar la sesión...");
        Console.ReadKey();

        // Animacion dinamica
        Console.Clear();
        Console.Write("\x1b[3J");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("CERRANDO SESIÓN Y ENCRIPTANDO DATOS");

        // Bucle para imprimir puntitos uno por uno
        for (int d = 0; d < 6; d++)
        {
            Console.Write(".");
            Thread.Sleep(350); // Pausa de 350 milisegundos entre cada punto
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[ SESIÓN FINALIZADA CON ÉXITO ]");
        Console.ResetColor();
        Thread.Sleep(1000);

        return true;//Devolver true para indicar que el turno se cerro

    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n  [!] ERROR CRÍTICO I/O: No se pudo generar el archivo. {ex.Message}");
        Console.ResetColor();

        // Si hay un error al escribir el archivo, evitamos cerrar el turno
        return false;
    }
}
//Nuevo struct para los guardas.
struct Guardia
{
    public string id;
    public string nombre;
    public string apellido;
    public string nombreUsuario;
    public DateTime horaInicio;
    public DateTime? horaSalida;
}
struct Vehiculo
{
    public string tipo;
    public string placa;
    public string conductor;
    public string cedula;
    public string destino;
    public string detalles;
    public DateTime horaIngreso; //Nueva variable(Se me habia olvidado) :D
}