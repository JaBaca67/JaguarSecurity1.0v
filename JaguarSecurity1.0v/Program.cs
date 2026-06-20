using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

Vehiculo[] vehiculos = new Vehiculo[1000];
Guardia[] guardias = new Guardia[100]; // Capacidad para 100 turnos
int totalLogins = 0; // Contador de cuántos guardias han iniciado sesión
int i = 0;

void Main()
{
    while (true)
    {
        IniciarSesionGuardia();
        int op;

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

                    bool turnoCerradoConExito = CerrarTurnoYGenerarReporte();

                    if (turnoCerradoConExito == false)
                    {
                        // Evitamos que se rompa el ciclo cambiando la opción a 0
                        op = 0;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n >> Regresando al menú principal de la sesión actual...");
                        Console.ResetColor();
                    }
                    break;
                case 7:
                    Console.Clear();
                    AnimacionCargando("SALIR DEL PROGRAMA");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSaliendo del programa... ¡Hasta luego!\n");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nOpción Inválida.");
                    break;
            }

            // Si no eligió salir, hacemos una pequeña pausa para ver el mensaje antes de volver a pintar el menú
            if (op != 7)
            {
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }

        } while (op != 6 && op != 7);
        i = 0;
    }
}

Main();

void LimpiarAreaConsola(int lineaInicio)
{
    // Capturar la posicion de la linea actual
    int lineaActual = Console.CursorTop;

    // borrar con un for
    for (int i = lineaInicio; i <= lineaActual; i++)
    {
        Console.SetCursorPosition(0, i);
        Console.Write(new string(' ', Console.WindowWidth - 1)); // Borra la línea sin dar saltos extra
    }

    // Regresamos el cursor exactamente al inicio
    Console.SetCursorPosition(0, lineaInicio);
}

// Funcion para validar que el texto solo contenga letras y espacios, y no esté vacío
bool EsTextoValido(string texto)
{

    if (string.IsNullOrWhiteSpace(texto)) return false;

    foreach (char c in texto)
    {

        if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
        {
            return false;
        }
    }
    return true;
}
void IniciarSesionGuardia()
{
    int entrada = 0;
    string nomEntrada = "";
    string nom = "";
    string ape = "";

    // Interfaz inicial
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                                                                      ║");
    Console.WriteLine("║                 SISTEMA DE CONTROL DE ACCESO UAM                     ║");
    Console.WriteLine("║                       JAGUAR SECURITY v1.0                           ║");
    Console.WriteLine("║                                                                      ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine("\n  Por favor, ingrese sus credenciales oficiales para iniciar turno.\n");

    // Ingreso de nombre con validacion
    int lineaInicioNombres = Console.CursorTop;
    do
    {
        LimpiarAreaConsola(lineaInicioNombres);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Nombres Completos (Ej: José Antonio): ");
        Console.ResetColor();

        nom = Console.ReadLine()!.Trim();

        if (!EsTextoValido(nom))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  [!] Error: El nombre no puede estar vacío ni contener números o símbolos.");
            Console.WriteLine("  Presione ENTER para reintentar...");
            Console.ResetColor();
            Console.ReadLine();
        }
    } while (!EsTextoValido(nom));

    // Ingreso de apellido con validacion
    int lineaInicioApellidos = Console.CursorTop;
    do
    {
        LimpiarAreaConsola(lineaInicioApellidos);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Apellidos Completos (Ej: Baca Silva): ");
        Console.ResetColor();

        ape = Console.ReadLine()!.Trim();

        if (!EsTextoValido(ape))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  [!] Error: El apellido no puede estar vacío ni contener números o símbolos.");
            Console.WriteLine("  Presione ENTER para reintentar...");
            Console.ResetColor();
            Console.ReadLine();
        }
    } while (!EsTextoValido(ape));

    // Creacion de ID e usuario
    string[] nombresArray = nom.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] apellidosArray = ape.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    string primerNombre = nombresArray.Length > 0 ? nombresArray[0] : nom;
    string primerApellido = apellidosArray.Length > 0 ? apellidosArray[0] : ape;

    Random rand = new Random();
    string idGenerado = "SEC-" + rand.Next(1000, 9999);
    string userGenerado = (primerNombre.Substring(0, 1) + primerApellido).ToLower();

    // Guardar en el struct
    guardias[totalLogins].id = idGenerado;
    guardias[totalLogins].nombre = nom;
    guardias[totalLogins].apellido = ape;
    guardias[totalLogins].nombreUsuario = userGenerado;
    guardias[totalLogins].horaInicio = DateTime.Now;
    guardias[totalLogins].horaSalida = null; // Turno abierto por defecto

    // Interfaz seleccion del porton
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                    ASIGNACIÓN DE PUESTO DE CONTROL                   ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    int lineaInicioMenu = Console.CursorTop;
    do
    {
        LimpiarAreaConsola(lineaInicioMenu);

        Console.WriteLine("  [1] Portón Principal");
        Console.WriteLine("  [2] Portón Sur");
        Console.WriteLine("  [3] Portón Norte\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Seleccione el puesto a cubrir (1-3): ");
        Console.ResetColor();

        string entradaTexto = Console.ReadLine()!.Trim();

        if (!int.TryParse(entradaTexto, out entrada) || entrada < 1 || entrada > 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  [!] Error: Selección inválida. Presione ENTER para reintentar...");
            Console.ResetColor();
            Console.ReadLine();
        }
    } while (entrada < 1 || entrada > 3);

    nomEntrada = entrada == 1 ? "Portón Principal" : entrada == 2 ? "Portón Sur" : "Portón Norte";

    totalLogins++;

    //Interfaz de acceso autorizado con detalles del turno
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                     ACCESO AUTORIZADO AL SISTEMA                     ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"\n  Registro interno #{totalLogins} - Nivel de acceso: Operador\n");
    Console.ResetColor();

    Console.WriteLine($"  ├─ Operador Titular : {nom} {ape}");
    Console.WriteLine($"  ├─ Credencial (ID)  : {idGenerado}");
    Console.WriteLine($"  ├─ Usuario de Red   : {userGenerado}");
    Console.WriteLine($"  ├─ Puesto Asignado  : {nomEntrada}");
    Console.WriteLine($"  └─ Fecha y hora     : {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}\n");

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("========================================================================");
    Console.WriteLine("  Presione [ENTER] para acceder al Panel de Control...");
    Console.WriteLine("========================================================================");
    Console.ResetColor();

    Console.ReadLine();
}

// ==========================================
// FUNCIÓN DEL MENÚ (CON VALIDACIÓN EN BUCLE)
// ==========================================
int menu()
{
    // 1. Identificamos al guardia que acaba de iniciar sesión
    int indiceActual = totalLogins - 1;
    Guardia operador = guardias[indiceActual];

    // 2. Extraemos solo el primer nombre y primer apellido para que no sea muy largo
    string primerNombre = operador.nombre.Split(' ')[0].ToUpper();
    string primerApellido = operador.apellido.Split(' ')[0].ToUpper();
    int opc;
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 SISTEMA DE CONTROL JAGUAR SECURITY                   ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");

        // 3. Imprimimos la barra de bienvenida personalizada
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  OFICIAL EN TURNO: {primerNombre} {primerApellido}  |  CREDENCIAL: {operador.id} | USUARIO: {operador.nombreUsuario}");
        Console.WriteLine("────────────────────────────────────────────────────────────────────────");
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
            break; 
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

        // Nueva columna que faltaba por agregarse en la tabla.
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
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║     MÓDULO DE AUDITORÍA: CONTROL DE SESIONES ANTERIORES UAM          ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        //En arreglo almacenamos los archivos csv.
        string rutaActual = Directory.GetCurrentDirectory();
        string[] archivosGuardados = Directory.GetFiles(rutaActual, "*.csv");
        Array.Sort(archivosGuardados, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));

        if (archivosGuardados.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >> No se han encontrado registros de sesiones anteriores (.CSV) en el sistema.");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
            break;
        }

        Console.WriteLine("Seleccione el número del archivo de registro que desea auditar:\n");

        for (int idx = 0; idx < archivosGuardados.Length; idx++)
        {
            string nombreLimpio = Path.GetFileNameWithoutExtension(archivosGuardados[idx]);
            Console.WriteLine($" [{idx + 1}] {nombreLimpio}");
        }

        //Opcion para volver al menu principal
        int opcionSalir = archivosGuardados.Length + 1;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($" [{opcionSalir}] Volver al Menú Principal");
        Console.ResetColor();
        Console.WriteLine("────────────────────────────────────────────────────────────────────");

        // Validamos la seleccion del usuario
        int seleccion;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($" >> Digite una opción (1-{opcionSalir}): ");
        Console.ResetColor();
        string entrada = Console.ReadLine()!.Trim();

        if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= opcionSalir)
        {
            if (seleccion == opcionSalir) break;

            string rutaArchivoElegido = archivosGuardados[seleccion - 1];

            // Limpiamos la pantalla explícitamente antes de cargar el nuevo archivo
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=====================================================================================================");
            Console.WriteLine($" VISUALIZANDO HISTORIAL DE AUDITORÍA: {Path.GetFileName(rutaArchivoElegido).ToUpper()}");
            Console.WriteLine("=====================================================================================================");
            Console.ResetColor();

            string[] filasHistorial = File.ReadAllLines(rutaArchivoElegido);

            //Validacion de que el archivo no este vacio, por si acaso
            if (filasHistorial.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n >> El archivo seleccionado está vacío.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                foreach (string fila in filasHistorial)
                {
                    // Si detecta los separadores decorativos del reporte los pinta en la consola
                    if (fila.StartsWith("===") || fila.StartsWith("---"))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n{fila}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    // Mantiene los saltos de linea
                    else if (string.IsNullOrWhiteSpace(fila))
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        //Aqui remplazamos los ; por │ para que se vea mas bonito :D
                        string filaLimpia = fila.Replace(";", " │ ").Replace(",", " │ ");
                        Console.WriteLine(" " + filaLimpia);
                    }
                }
                Console.ResetColor();
            }

            Console.WriteLine("\n=====================================================================================================");
            Console.WriteLine("Presione cualquier tecla para regresar al listado numérico de auditoría...");
            Console.ReadKey();
        }
        else
        {
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
        lineasCsv.Add("Nº;Placa;Tipo de Vehículo;Conductor;Cédula;Destino;Detalles Adicionales");

        for (int r = 0; r < i; r++)
        {
            //Remplazar los ; por que se veian horribles
            string placa = vehiculos[r].placa.Replace(";", "-");
            string tipo = vehiculos[r].tipo.Replace(";", "-");
            string conductor = vehiculos[r].conductor.Replace(";", "-");
            string cedula = vehiculos[r].cedula.Replace(";", "-");
            string destino = vehiculos[r].destino.Replace(";", "-");
            string detalles = vehiculos[r].detalles.Replace(";", "-");

            lineasCsv.Add($"{r + 1};{placa};{tipo};{conductor};{cedula};{destino};{detalles}");
        }
        lineasCsv.Add("");

        // Cierre
        lineasCsv.Add("--- III. RESUMEN DE OPERACIONES ---");
        lineasCsv.Add($"Total de Vehículos Procesados:;{i}");
        lineasCsv.Add($"Estado del Turno:;CERRADO Y AUDITADO");
        lineasCsv.Add("======================================================================================");

        File.WriteAllLines(nombreArchivo, lineasCsv, System.Text.Encoding.UTF8);

        // Mensajes de exito con formato
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  [ OK ] Sincronización de datos completada.");
        Console.WriteLine("  [ OK ] Turno cerrado formalmente en la base de datos local.\n");
        Console.ResetColor();

        Console.WriteLine("  El reporte oficial ha sido generado y tabulado correctamente:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  >> Directorio actual \\ {nombreArchivo}\n");
        Console.ResetColor();

        //Devolver true despues de generar el archivo
        return true;
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
}