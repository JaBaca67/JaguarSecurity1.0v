// =====================================================================
//   OPCIÓN 6: CERRAR TURNO Y GENERAR REPORTES (.CSV)
// =====================================================================
void CerrarTurnoYGenerarReporte()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("====================================================================");
    Console.WriteLine("          CERRAR TURNO Y GENERACIÓN DE REPORTE HISTÓRICO (.CSV)     ");
    Console.WriteLine("====================================================================");
    Console.ResetColor();

    // 1. VALIDACIÓN MADRE: Verificar obligatoriamente que exista al menos 1 vehículo registrado
    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n >> Error: No se puede cerrar el turno ni generar el archivo CSV.");
        Console.WriteLine("    Debe registrar al menos un (1) vehículo en la sesión actual antes de exportar.");
        Console.ResetColor();
        return; // Rompe la función y regresa al menú principal sin crear ningún archivo
    }

    // 2. GENERACIÓN DEL ID IDENTIFICATIVO ÚNICO DE 5 CARACTERES (Ejemplo: f5qr7)
    string caracteres = "abcdefghijklmnopqrstuvwxyz0123456789";
    Random rand = new Random();
    char[] idArray = new char[5];
    for (int j = 0; j < 5; j++)
    {
        idArray[j] = caracteres[rand.Next(caracteres.Length)];
    }
    string idIdentificativo = new string(idArray);

    // 3. FORMATEAR EL NOMBRE DEL ARCHIVO (Todo en minúsculas y reemplazando espacios por guiones bajos)
    string nombreLimpio = nombreGuardia.ToLower().Replace(" ", "_");
    string apellidoLimpio = apellidoGuardia.ToLower().Replace(" ", "_");
    string fechaHoy = DateTime.Now.ToString("dd-MM-yyyy");
    string nombreArchivo = $"{nombreLimpio}_{apellidoLimpio}_{fechaHoy}_{idIdentificativo}.csv";

    try
    {
        // Lista temporal que almacenará fila por fila la estructura interna del CSV
        List<string> lineasCsv = new List<string>();

        // ORDEN 1: Datos del guarda y a qué fecha y hora se logueó al sistema
        lineasCsv.Add("=== DATOS DE APERTURA DE TURNO ===");
        lineasCsv.Add($"Guardia de Turno:,{nombreGuardia} {apellidoGuardia}");
        // Como 'horaInicio' es local en tu Login, usamos el formato de fecha actual de la sesión
        lineasCsv.Add($"Fecha de Acceso al Sistema:,{DateTime.Now.ToString("dd/MM/yyyy")} - Turno Activo");
        lineasCsv.Add(""); // Línea en blanco para separación visual en Excel

        // ORDEN 2: Los vehículos registrados (Encabezados de columnas y registros de la matriz)
        lineasCsv.Add("=== VEHÍCULOS CAPTURADOS EN LA BITÁCORA ===");
        lineasCsv.Add("Nº,Placa,Tipo,Conductor,Cédula,Destino,Detalles");

        for (int r = 0; r < i; r++)
        {
            // Limpieza de seguridad: Se reemplazan las comas por punto y coma por si el usuario escribió comas,
            // evitando que se rompa o se desalinee la tabla al abrirla en Excel.
            string placa = vehiculos[r].placa.Replace(",", ";");
            string tipo = vehiculos[r].tipo.Replace(",", ";");
            string conductor = vehiculos[r].conductor.Replace(",", ";");
            string cedula = vehiculos[r].cedula.Replace(",", ";");
            string destino = vehiculos[r].destino.Replace(",", ";");
            string detalles = vehiculos[r].detalles.Replace(",", ";");

            // Añadir fila de vehículo indexado
            lineasCsv.Add($"{r + 1},{placa},{tipo},{conductor},{cedula},{destino},{detalles}");
        }
        lineasCsv.Add(""); // Línea en blanco para separación visual en Excel

        // ORDEN 3: A qué hora el guarda generó el archivo CSV
        lineasCsv.Add("=== CIERRE DE OPERACIONES ===");
        lineasCsv.Add($"Fecha y Hora Exacta de Exportación CSV:,{DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
        lineasCsv.Add($"Total de Registros Guardados:,{i} vehículos");

        // 4. GUARDAR EL ARCHIVO EN LA CARPETA LOCAL DEL PROYECTO
        File.WriteAllLines(nombreArchivo, lineasCsv, System.Text.Encoding.UTF8);

        // Mensaje de éxito decorativo al estilo Jaguar Security
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n ¡TURNO CERRADO Y REPORTE EXPORTADO CORRECTAMENTE!");
        Console.ResetColor();
        Console.WriteLine(" Se ha generado tu archivo de auditoría con el nombre:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($" >> {nombreArchivo}");
        Console.ResetColor();
        Console.WriteLine("\n Los datos de la sesión actual se han respaldado de forma segura.");

    }
    catch (Exception ex)
    {
        // Validación de protección contra errores de escritura en disco o archivos bloqueados
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n >> Error Crítico de escritura: No se pudo generar el archivo. {ex.Message}");
        Console.ResetColor();
    }
}