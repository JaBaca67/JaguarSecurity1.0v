using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;

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
                    AnimacionCargando("VER BITÁCORA ACTUAL (VISUALIZAR, ELIMINAR O EDITAR)");
                    MostrarRegistros();
                    //Pediente agregar función para eliminar o editar registros específicos dentro de la sesión actual
                    break;
                case 3:
                    Console.Clear();
                    AnimacionCargando("BUSCAR ANTECEDENTES DE PLACA (HISTORIAL GLOBLAL EN .CSV");
                    BuscarVehiculo();
                    //Pendiente activar función para buscar en archivos .CSV de sesiones anteriores (historial global)
                    break;
                case 4:
                    Console.Clear();
                    AnimacionCargando("INFORMACIÓN DEL OPERADOR E HISTORIAL DE ACCESOS");
                    InformacionOperadorHistorialAccesos();
                    //Agregada finalmente la funcion.

                    break;
                case 5:
                    Console.Clear();
                    AnimacionCargando("MÓDULO DE AUDITORÍA (REVISIÓN DE SESIONES ANTERIORES)");
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ResetColor();
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
    guardias[totalLogins].porton = nomEntrada;
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
        Console.Write("\x1b[3J");
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
        Console.WriteLine("[1] REGISTRAR INGRESO VEHICULAR SELECTIVO");
        Console.WriteLine("[2] VER BITÁCORA ACTUAL (VISUALIZAR, ELIMINAR O EDITAR)");
        Console.WriteLine("[3] BUSCAR ANTECEDENTES DE PLACA (HISTORIAL GLOBLAL EN .CSV");
        Console.WriteLine("[4] INFORMACIÓN DEL OPERADOR E HISTORIAL DE ACCESOS");
        Console.WriteLine("[5] MÓDULO DE AUDITORÍA (REVISIÓN DE SESIONES ANTERIORES)");
        Console.WriteLine("[6] CERRAR TURNO Y GENERAR REPORTES (.CSV)");
        Console.WriteLine("\n[7] SALIR DEL PROGRAMA");
        Console.WriteLine("\n────────────────────────────────────────────────────────────────────");
        Console.Write(" >>Seleccione una opcion (1-7): ");

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
//OPCION 1 MENU 1: REGISTRAR INGRESO VEHICULAR SELECTIVO
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
            vehiculos[i].horaIngreso = DateTime.Now;//Se guarda la hora exacta de ese vehiculo

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


//OPCION MENU 2: MOSTRAR REGISTROS DE LA SESIÓN ACTUAL (CON OPCIONES DE ELIMINAR Y EDITAR)

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
            break; // Regresa al menú principal
        }

        // Orden y formato de la tabla optimizados para mejor legibilidad
        string formatoTabla = " │ {0,-3} │ {1,-8} │ {2,-16} │ {3,-10} │ {4,-22} │ {5,-14} │ {6,-15} │";

        // Dibujado de bordes alineado con el nuevo orden
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" ┌─────┬──────────┬──────────────────┬────────────┬────────────────────────┬────────────────┬─────────────────┐");
        Console.WriteLine(string.Format(formatoTabla, "Nº", "Hora", "Tipo Vehículo", "Placa", "Nombre Conductor", "Cédula", "Destino"));
        Console.WriteLine(" ├─────┼──────────┼──────────────────┼────────────┼────────────────────────┼────────────────┼─────────────────┤");
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

            // Se mandan las variables en el orden exacto del formato
            Console.WriteLine(string.Format(formatoTabla, num, hora, tipo, placa, cond, ced, dest));
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" └─────┴──────────┴──────────────────┴────────────┴────────────────────────┴────────────────┴─────────────────┘");
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
            //SubProcesoEliminar();
        }
        else if (accion == "M")
        {

            //SubProcesoModificar();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [!] Acción inválida. Por favor, intente de nuevo.");
            Console.ResetColor();
            Console.WriteLine(" Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}


//OPCION 3 MENU: BUSCAR ANTECEDENTES DE PLACA (HISTORIAL GLOBAL EN .CSV)
//FALTA POR ACTUALIZAR LA FUNCION DE BUSQUEDA EN LOS ARCHIVOS .CSV DE SESIONES ANTERIORES (HISTORIAL GLOBAL)
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


// =====================================================================
//   OPCIÓN 5: MÓDULO DE AUDITORÍA (REVISIÓN DE SESIONES ANTERIORES)
// =====================================================================
void ModuloAuditoria()
{
    while (true)
    {
        Console.Clear();
        Console.Write("\x1b[3J"); // Limpiamos el rastro del scroll
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║     MÓDULO DE AUDITORÍA: CONTROL DE SESIONES ANTERIORES UAM          ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        string rutaActual = Directory.GetCurrentDirectory();
        string[] archivosGuardados = Directory.GetFiles(rutaActual, "*.csv");

        // Ordenamos de más reciente a más antiguo
        Array.Sort(archivosGuardados, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));

        if (archivosGuardados.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >> No se han encontrado registros de sesiones anteriores (.CSV) en el sistema.");
            Console.ResetColor();
            break;
        }

        Console.WriteLine("Seleccione el número del archivo de registro que desea auditar:\n");

        for (int idx = 0; idx < archivosGuardados.Length; idx++)
        {
            string nombreLimpio = Path.GetFileNameWithoutExtension(archivosGuardados[idx]);
            Console.WriteLine($" [{idx + 1}] {nombreLimpio}");
        }

        int opcionSalir = archivosGuardados.Length + 1;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($" [{opcionSalir}] Volver al Menú Principal");
        Console.ResetColor();
        Console.WriteLine("────────────────────────────────────────────────────────────────────");

        int seleccion;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($" >> Digite una opción (1-{opcionSalir}): ");
        Console.ResetColor();
        string entrada = Console.ReadLine()!.Trim();

        if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= opcionSalir)
        {
            if (seleccion == opcionSalir) break;

            string rutaArchivoElegido = archivosGuardados[seleccion - 1];

            Console.Clear();
            Console.Write("\x1b[3J");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===============================================================================================================");
            Console.WriteLine($" VISUALIZANDO HISTORIAL DE AUDITORÍA: {Path.GetFileName(rutaArchivoElegido).ToUpper()}");
            Console.WriteLine("===============================================================================================================");
            Console.ResetColor();

            string[] filasHistorial = File.ReadAllLines(rutaArchivoElegido);

            if (filasHistorial.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n >> El archivo seleccionado está vacío.");
                Console.ResetColor();
            }
            else
            {
                // Definimos la plantilla de anchos fijos para la tabla de vehiculos nueva
                // Ejemplo de como quedaria la tabla: Nº(4) | Hora(10) | Placa(10) | Tipo(18) | Conductor(20) | Cédula(14) | Destino(15) | Detalles(15)
                // Los números negativos significan alineación a la izquierda
                string formatoTabla = " │ {0,-3} │ {1,-8} │ {2,-9} │ {3,-17} │ {4,-19} │ {5,-13} │ {6,-14} │ {7,-14} │";

                foreach (string fila in filasHistorial)
                {
                    if (string.IsNullOrWhiteSpace(fila))
                    {
                        Console.WriteLine();
                        continue;
                    }

                    // Separamos por punto y coma
                    string[] celdas = fila.Contains(";") ? fila.Split(';') : fila.Split(',');

                    // 1. Pintamos solo si es un titulo de color cyan
                    if (fila.StartsWith("===") || fila.StartsWith("---"))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(fila);
                        Console.ResetColor();
                    }
                    // 2. Si es la cabecera oficial de las columnas de vehículos entonces:
                    else if (celdas[0].Trim() == "Nº" || celdas[0].Trim() == "N")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(" ┌─────┬──────────┬───────────┬───────────────────┬─────────────────────┬───────────────┬────────────────┬────────────────┐");
                        Console.WriteLine(string.Format(formatoTabla, "Nº", "Hora", "Placa", "Tipo Vehículo", "Conductor", "Cédula", "Destino", "Detalles"));
                        Console.WriteLine(" ├─────┼──────────┼───────────┼───────────────────┼─────────────────────┼───────────────┼────────────────┼────────────────┤");
                        Console.ResetColor();
                    }
                    // 3. Si la fila empieza con un numero, sabemos que el registro del vehiculo
                    else if (celdas.Length >= 8 && int.TryParse(celdas[0], out _))
                    {
                        // Aca se recortan los textos para que alcancen
                        string num = celdas[0].Trim();
                        string hora = celdas[1].Length > 8 ? celdas[1].Substring(0, 8) : celdas[1].Trim();
                        string placa = celdas[2].Length > 9 ? celdas[2].Substring(0, 9) : celdas[2].Trim();
                        string tipo = celdas[3].Length > 17 ? celdas[3].Substring(0, 17) : celdas[3].Trim();
                        string conductor = celdas[4].Length > 19 ? celdas[4].Substring(0, 19) : celdas[4].Trim();
                        string cedula = celdas[5].Length > 13 ? celdas[5].Substring(0, 13) : celdas[5].Trim();
                        string destino = celdas[6].Length > 14 ? celdas[6].Substring(0, 14) : celdas[6].Trim();
                        string detalles = celdas[7].Length > 14 ? celdas[7].Substring(0, 14) : celdas[7].Trim();

                        // Imprimimos la fila de datos
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format(formatoTabla, num, hora, placa, tipo, conductor, cedula, destino, detalles));
                        Console.ResetColor();

                        // Si es el último vehículo, se cierra la tabla
                        int indiceSiguiente = Array.IndexOf(filasHistorial, fila) + 1;
                        if (indiceSiguiente < filasHistorial.Length)
                        {
                            string sigFila = filasHistorial[indiceSiguiente];
                            string[] sigCeldas = sigFila.Contains(";") ? sigFila.Split(';') : sigFila.Split(',');
                            if (string.IsNullOrWhiteSpace(sigFila) || !int.TryParse(sigCeldas[0], out _))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(" └─────┴──────────┴───────────┴───────────────────┴─────────────────────┴───────────────┴────────────────┴────────────────┘");
                                Console.ResetColor();
                            }
                        }
                    }
                    // 4. Datos generales del guarda
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (celdas.Length > 1)
                        {
                            // Buscamos eliminar los puntos y comas feos
                            Console.WriteLine($"  {celdas[0].Trim()} {celdas[1].Trim()}");
                        }
                        else
                        {
                            Console.WriteLine($"  {fila.Trim()}");
                        }
                        Console.ResetColor();
                    }
                }
            }

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
    guardias[indiceActual].vehiculosRegistrados = i;

    // Crear un variable de tipo TimeSpan para calcular el tiempo trabajado
    //Nota: TimeSpan es una estructura de C# que representa un intervalo de tiempo, en este caso lo usamos para calcular la duración del turno del guardia.
    Guardia operador = guardias[indiceActual];
    TimeSpan tiempoTrabajado = operador.horaSalida!.Value - operador.horaInicio;

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
    public int vehiculosRegistrados;
    public string id;
    public string nombre;
    public string apellido;
    public string nombreUsuario;
    public string porton;
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