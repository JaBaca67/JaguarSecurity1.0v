using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

Vehiculo[] vehiculos = new Vehiculo[1000];
Guardia[] historialGuardias = new Guardia[100]; // Capacidad para 100 turnos
string nombreGuardia = "";
string idGuardia = "";
int totalLogins = 0; // Contador de cuántos guardias han iniciado sesión
int i = 0;


void Main()
{
    IniciarSesionGuardia();
    int op;
    while (true)
    {


        do
        {
            // Llamamos a la función menu y guardamos el resultado en 'op'
            op = menu();

            // Evaluamos la opción seleccionada
            switch (op)
            {
                case 1:
                    Console.Clear();
                    AnimacionCargando("REGISTRAR INGRESO VEHICULAR SELECTIVO");
                    RegistroVehiculo();
                    break;
                case 2:
                    Console.Clear();
                    AnimacionCargando("EDITAR VEHICULOS REGISTRADOS");
                    EditarVehiculo();
                    break;
                case 3:
                    Console.Clear();
                    AnimacionCargando("BÚSQUEDA RÁPIDA DE VEHÍCULO");
                    BuscarVehiculo();
                    //Aquí se podría poner lo de eliminar un registro específico, 
                    break;
                case 4:
                    Console.Clear();
                    AnimacionCargando("MOSTRAR REGISTROS DE LA SESIÓN ACTUAL");
                    MostrarRegistros();

                    break;
                case 5:
                    Console.Clear();
                    AnimacionCargando("MÓDULO DE AUDITORÍA");
                    ModuloAuditoria();
                    break;
                case 6:
                    Console.Clear();
                    AnimacionCargando("CERRAR TURNO Y GENERAR REPORTES (.CSV)");
                    CerrarTurnoYGenerarReporte();
                    break;
                case 7:
                    Console.WriteLine("\nSaliendo del programa... ¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("\nOpción Inválida.");
                    break;
            }

            // Si no eligió salir, hacemos una pequeña pausa para ver el mensaje antes de volver a pintar el menú
            if (op != 6 && op != 7)
            {
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

        } while (op != 6 && op != 7);
        i = 0;

    }
}

Main();

void IniciarSesionGuardia()
{
    int entrada = 0;
    string nomEntrada = "";
    bool sesionActiva = false;

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==============================================");
    Console.WriteLine("        BIENVENIDO A JAGUARSECURITY v1.0      ");
    Console.WriteLine("==============================================");
    Console.ResetColor();
    Console.WriteLine("Para comenzar, por favor registre su acceso.\n");

    while (!sesionActiva)
    {
        Console.Write(">> Ingrese su Usuario: ");
        nombreGuardia = Console.ReadLine()!.Trim();

        if (nombreGuardia != "")
        {
            // === GENERADOR DE ID AUTOMÁTICO (4 Caracteres) ===
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] idArray = new char[4];
            for (int j = 0; j < 4; j++)
            {
                idArray[j] = caracteres[rand.Next(caracteres.Length)];
            }
            idGuardia = new string(idArray); // Asignamos el ID generado
            // =================================================

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Bienvenido, {nombreGuardia}! Su ID de operador asignado es: {idGuardia}");
            Console.ResetColor();

            // Selección de ubicación (Portón)
            do
            {
                Console.WriteLine("\n--- Selección de Entrada UAM ---");
                Console.WriteLine("1. Portón Principal");
                Console.WriteLine("2. Portón Sur");
                Console.WriteLine("3. Portón Norte");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\nSeleccione una entrada (1-3): ");
                Console.ResetColor();

                string entradaTexto = Console.ReadLine()!.Trim();
                if (!int.TryParse(entradaTexto, out entrada))
                {
                    entrada = 0;
                }
                switch (entrada)
                {
                    case 1: nomEntrada = "Portón Principal"; break;
                    case 2: nomEntrada = "Portón Sur"; break;
                    case 3: nomEntrada = "Portón Norte"; break;
                    default:
                        Console.WriteLine(">> Error: Opción inválida. Intente de nuevo.");
                        break;
                }

            } while (entrada < 1 || entrada > 3);

            // === GUARDAR EN EL STRUCT ===
            historialGuardias[totalLogins].nombreUsuario = nombreGuardia;
            historialGuardias[totalLogins].id = idGuardia;
            historialGuardias[totalLogins].horaInicio = DateTime.Now;

            totalLogins++; // Aumentamos el contador
            sesionActiva = true;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**********************************************");
            Console.WriteLine("          SESIÓN INICIADA CORRECTAMENTE       ");
            Console.WriteLine("**********************************************");
            Console.ResetColor();

            Console.WriteLine($"\n>>> ATENCIÓN: Usted es el usuario #{totalLogins} en loguearse hoy <<<\n");
            Console.WriteLine($"Operador  : {historialGuardias[totalLogins - 1].nombreUsuario} (ID: {historialGuardias[totalLogins - 1].id})");
            Console.WriteLine($"Ubicación : {nomEntrada}");
            Console.WriteLine($"Fecha y Hora: {historialGuardias[totalLogins - 1].horaInicio.ToString("dd/MM/yyyy - HH:mm:ss")}");
            Console.WriteLine("**********************************************");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>> Error: El usuario no puede estar vacío.\n");
            Console.ResetColor();
        }
    }
}


// ==========================================
// FUNCIÓN DEL MENÚ (CON VALIDACIÓN EN BUCLE)
// ==========================================
int menu()
{
    int opc;
    while (true)
    {
        Console.Clear();
        // Encabezado decorativo
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            SISTEMA DE SEGURIDAD INTEGRAL: JAGUAR V1.0            ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        // Saludo de bienvenida
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n      ¡Hola, oficial {nombreGuardia}! El sistema está listo para operar.\n");
        Console.ResetColor();

        // Opciones del menú
        Console.WriteLine("1. REGISTRAR INGRESO VEHICULAR SELECTIVO");
        Console.WriteLine("2. EDITAR VEHICULOS REGISTRADOS");
        Console.WriteLine("3. BÚSQUEDA RÁPIDA DE VEHÍCULO");
        Console.WriteLine("4. MOSTRAR REGISTROS DE LA SESIÓN ACTUAL");
        Console.WriteLine("5. MÓDULO DE AUDITORÍA (REVISIÓN DE SESIONES ANTERIORES)");
        Console.WriteLine("6. CERRAR TURNO Y GENERAR REPORTES (.CSV)");
        Console.WriteLine("7. SALIR DEL PROGRAMA");
        Console.WriteLine("\n────────────────────────────────────────────────────────────────────");
        Console.Write(" >>Digita tu opción (1-7): ");

        // Intenta convertir la entrada a número y valida que esté en el rango correcto
        if (int.TryParse(Console.ReadLine()!, out opc) && opc >= 1 && opc <= 7)
        {
            return opc; // Retorna la opción válida y rompe el bucle interno
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  >>Opción Inválida. Por favor, intente de nuevo.");
            Console.ResetColor();
            Console.WriteLine("  Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}

void AnimacionCargando(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"\n[Ejecutando] {mensaje} ");


    char[] spinner = { '|', '/', '-', '\\' };

    Console.ForegroundColor = ConsoleColor.Yellow;

    for (int i = 0; i < 15; i++)
    {

        Console.Write(spinner[i % 4]);


        System.Threading.Thread.Sleep(100);


        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("¡Listo!\n");
    Console.ResetColor();
    System.Threading.Thread.Sleep(400);
}

void RegistroVehiculo()
{
    string continuar = "S";

    while (continuar == "S")
    {
        int cantidad = 0;
        while (true)
        {
            Console.Write("¿Cuántos vehículos desea registrar en este momento?: ");
            if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Valor ingresado inválido. Por favor ingrese un número entero mayor a 0.");
            Console.ResetColor();
        }

        for (int contador = 0; contador < cantidad; contador++)
        {
            if (i >= 1000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay espacio disponible para almacenar más vehículos. Se alcanzó el límite de 1000 por hoja de registro.");
                Console.ResetColor();
                break;
            }

            Console.WriteLine($"\nRegistro de Vehículo #{i + 1}");

            vehiculos[i].tipo = SeleccionarTipo();
            vehiculos[i].placa = LeerPlaca();
            vehiculos[i].conductor = LeerConductor();
            vehiculos[i].cedula = LeerCedula();
            vehiculos[i].destino = LeerOpcional("Destino");
            vehiculos[i].detalles = LeerOpcional("Detalles del conductor o vehículo");

            i++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vehículo registrado exitosamente.");
            Console.ResetColor();
        }

        while (true)
        {
            Console.Write("¿Desea registrar más vehículos? (S/N): ");
            continuar = Console.ReadLine()!;
            if (continuar == "S" || continuar == "N")
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Carácter inválido, solo se aceptan las letras S (Si) o N (No).");
            Console.ResetColor();
        }
    }
}

string SeleccionarTipo()
{
    int op;
    while (true)
    {
        Console.WriteLine("Selecciona el tipo: 1. Vehículo liviano | 2. Motocicleta | 3. Otro");
        Console.Write("Digite su opción: ");
        if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= 3)
        {
            if (op == 1)
                return "Vehículo liviano";
            if (op == 2)
                return "Motocicleta";

            while (true)
            {
                Console.Write("Escriba el tipo de vehículo: ");
                string otro = Console.ReadLine()!;
                if (otro != "")
                    return otro;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El tipo de vehículo no puede quedar en blanco.");
                Console.ResetColor();
            }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Opción Inválida, por favor ingrese solo el número entero de las 3 opciones disponibles.");
        Console.ResetColor();
    }
}

string LeerPlaca()
{
    while (true)
    {
        Console.Write("\nPlaca del vehículo (Máx 15 caracteres), ej:M123456 o M 123456: ");
        string placa = Console.ReadLine()!;
        if (placa != "" && placa.Length <= 15)
            return placa;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Placa inválida, este campo no puede estar vacío o ser mayor a 15 caracteres).");
        Console.ResetColor();
    }
}

string LeerConductor()
{
    while (true)
    {
        Console.Write("Nombre del conductor: ");
        string nombre = Console.ReadLine()!;


        if (nombre == "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nombre inválido. Este campo no puede quedar en blanco.");
            Console.ResetColor();
            continue;
        }


        bool tieneNumeros = false;
        for (int c = 0; c < nombre.Length; c++)
        {
            if (nombre[c] == '0' || nombre[c] == '1' || nombre[c] == '2' ||
                nombre[c] == '3' || nombre[c] == '4' || nombre[c] == '5' ||
                nombre[c] == '6' || nombre[c] == '7' || nombre[c] == '8' || nombre[c] == '9')
            {
                tieneNumeros = true;
                break;
            }
        }


        if (tieneNumeros)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nombre inválido. Solo se aceptan letras, no se permite la adición de números.");
            Console.ResetColor();
        }
        else
        {
            return nombre;
        }
    }
}

string LeerCedula()
{
    while (true)
    {
        Console.Write("\nCédula (Escribir los 14 caracteres o de forma obligatoria escribir N/A para omitir): ");
        string cedula = Console.ReadLine()!;

        if (cedula == "N/A")
            return "N/A";
        if (cedula.Length == 14)
            return cedula;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Cédula inválida. Debe tener 14 caracteres o escribir N/A.");
        Console.ResetColor();
    }
}

string LeerOpcional(string nombreCampo)
{
    while (true)
    {
        Console.Write($"\n{nombreCampo} (Obligatorio escribir N/A para omitir): ");
        string entrada = Console.ReadLine()!;

        if (entrada == "N/A")
            return "N/A";
        if (entrada != "")
            return entrada;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Este campo no se puede dejar en blanco, de forma obligatoria para salir ponga N/A.");
        Console.ResetColor();
    }
}

//OP MENU 2: EDICIÓN DE VEHÍCULOS REGISTRADOS

void EditarVehiculo()
{
    Console.Clear();
    Console.WriteLine(" Modo edición de Vehículos registrados ");
    if (i == 0)
    {
        Console.WriteLine("No hay vehículos registrados.");
        return;
    }

    Console.WriteLine($"Vehículos registrados: {i}");
    for (int r = 0; r < i; r++)
    {
        Console.WriteLine($"[{r + 1}] Placa: {vehiculos[r].placa}  Conductor: {vehiculos[r].conductor}");
    }

    int num = 0;
    while (true)
    {
        Console.Write($"Ingrese el número de vehículo a modificar (1 al {i}) o escriba N (No), para salir del modo edición: ");
        string entrada = Console.ReadLine()!;

        if (entrada == "N")
        {
            return;
        }

        if (int.TryParse(entrada, out num) && num > 0 && num <= i)
            break;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Valor inválido por favor ingrese N o seleccione el número entero del vehículo correspondiente a editar.");
        Console.ResetColor();
    }

    int pos = num - 1;
    Console.WriteLine($"Modificando Vehículo #{num} (Actual: {vehiculos[pos].placa})");
    Console.WriteLine("---------------------------------------------");

    vehiculos[pos].tipo = SeleccionarTipo();
    vehiculos[pos].placa = LeerPlaca();
    vehiculos[pos].conductor = LeerConductor();
    vehiculos[pos].cedula = LeerCedula();
    vehiculos[pos].destino = LeerOpcional("Nuevo Destino");
    vehiculos[pos].detalles = LeerOpcional("Nuevos Detalles");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("¡Vehículo modificado correctamente!");
    Console.ResetColor();
}


//OP MENU 3: BÚSQUEDA RÁPIDA DE VEHÍCULO
//====================================
// BÚSQUEDA RÁPIDA DE VEHÍCULO
//====================================
void BuscarVehiculo()
{
    Console.Clear();
    Console.WriteLine("===== BÚSQUEDA RÁPIDA DE VEHÍCULO =====");

    Console.Write("Ingrese la placa del vehículo: ");
    string placaBuscada = Console.ReadLine()!.ToUpper();

    bool encontrado = false;

    for (int j = 0; j < i; j++)
    {
        // Validamos que el vehículo no sea nulo y comparamos la placa
        if (vehiculos[j].placa != null && vehiculos[j].placa.ToUpper() == placaBuscada)
        {
            Console.WriteLine("\nVEHÍCULO ENCONTRADO");
            Console.WriteLine("================================");
            Console.WriteLine($"Tipo: {vehiculos[j].tipo}");
            Console.WriteLine($"Placa: {vehiculos[j].placa}");
            Console.WriteLine($"Conductor: {vehiculos[j].conductor}");
            Console.WriteLine($"Cédula: {vehiculos[j].cedula}");
            Console.WriteLine($"Destino: {vehiculos[j].destino}");
            Console.WriteLine($"Detalles: {vehiculos[j].detalles}");
            Console.WriteLine("================================");

            encontrado = true;
            break; // Detiene la búsqueda al encontrarlo
        }
    }

    if (!encontrado)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNo se encontró ningún vehículo con esa placa en la sesión actual.");
        Console.ResetColor();
    }
}


//OPCION MENU 4: MOSTRAR REGISTROS DE LA SESIÓN ACTUAL
// ==========================================
// FUNCIÓN PARA MOSTRAR LOS REGISTROS DE LA SESIÓN ACTUAL
// ==========================================
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

}
//Nuevo struct para los guardas.
struct Guardia
{
    public string id;            
    public string nombre;        
    public string apellido;      
    public string nombreUsuario;
    public DateTime horaInicio; 
}
struct Vehiculo
{
    public string tipo;
    public string placa;
    public string conductor;
    public string cedula;
    public string destino;
    public string detalles;
}