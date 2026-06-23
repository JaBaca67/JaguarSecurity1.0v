using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;

Vehiculo[] vehiculos = new Vehiculo[1000];
Guardia[] guardias = new Guardia[100]; // Capacidad para 100 turnos
int totalLogins = 0; // Contador de cuántos guardias han iniciado sesion
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
                    break;
                case 3:
                    Console.Clear();
                    AnimacionCargando("BUSCAR ANTECEDENTES DE PLACA (HISTORIAL GLOBLAL EN .CSV");
                    BuscarVehiculo();
                    break;
                case 4:
                    Console.Clear();
                    AnimacionCargando("INFORMACIÓN DEL OPERADOR E HISTORIAL DE ACCESOS");
                    InformacionOperadorHistorialAccesos();
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
                        // Evitamos que se rompa el ciclo cambiando la opcion a 0
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

// Funcion para validar que el texto solo contenga letras y espacios
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
        Console.WriteLine("  [3] Portón Este\n");

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

    nomEntrada = entrada == 1 ? "Portón Principal" : entrada == 2 ? "Portón Sur" : "Portón Este";
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
    int indiceActual = totalLogins - 1;
    Guardia operador = guardias[indiceActual];

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

        //Imprimimos la barra de bienvenida personalizada
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

        // Intenta convertir la entrada a numero y valida que esté en el rango correcto
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
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    SISTEMA JAGUAR SECURITY v1.0                      ║");
        Console.WriteLine("║               MÓDULO DE REGISTRO DE INGRESO VEHICULAR                ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        if (i >= 1000)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [!] Límite alcanzado: No hay más espacio para ingresar más vehículos (Máx 1000).");
            Console.ResetColor();
            break;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"--- Ingresando Registro #{i + 1}  ---\n");
        Console.ResetColor();

        vehiculos[i].tipo = SeleccionarTipo();
        vehiculos[i].placa = LeerPlaca();
        vehiculos[i].conductor = LeerConductor();
        vehiculos[i].cedula = LeerCedula();
        vehiculos[i].destino = LeerOpcional(">> Destino");
        vehiculos[i].detalles = LeerOpcional(">> Detalles del conductor o vehículo");
        vehiculos[i].horaIngreso = DateTime.Now;

        i++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==========================================================");
        Console.WriteLine(" [ OK ] ¡Vehículo registrado exitosamente en el sistema! ");
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        int lineaInicio = Console.CursorTop;
        while (true)
        {
            LimpiarAreaConsola(lineaInicio);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n  >> ¿Desea registrar otro vehículo en este momento? (S/N): ");
            Console.ResetColor();

            continuar = Console.ReadLine()!.Trim().ToUpper();

            if (continuar == "S" || continuar == "N")
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  [!] Error: Solo se aceptan las letras 'S' (Sí) o 'N' (No).");
            Console.ResetColor();
            Console.WriteLine("  Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}

string SeleccionarTipo()
{
    int lineaInicio = Console.CursorTop;
    int op;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[!] Selecciona el tipo: 1. Vehículo liviano | 2. Motocicleta | 3. Otro");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(">> Digite su opción (1-3): ");
        Console.ResetColor();

        if (int.TryParse(Console.ReadLine()!.Trim(), out op) && op >= 1 && op <= 3)
        {
            if (op == 1) return "Vehículo liviano";
            if (op == 2) return "Motocicleta";

            int lineaOtro = Console.CursorTop;

            while (true)
            {
                LimpiarAreaConsola(lineaOtro);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" >> Escriba el tipo de vehículo: ");
                Console.ResetColor();

                string otro = Console.ReadLine()!.Trim();

                if (string.IsNullOrEmpty(otro))
                {
                    MostrarError("Este campo no puede quedar en blanco. Por favor ingrese el tipo de vehículo.");
                    continue;
                }

                bool soloLetras = true;
                foreach (char c in otro)
                {
                    if (!char.IsLetter(c) && c != ' ') // Sino no esta letra o tiene espacios
                    {
                        soloLetras = false;
                        break;
                    }
                }

                if (!soloLetras)
                {
                    MostrarError("Entrada inválida. Solo se aceptan letras y espacios (no números ni símbolos).");
                    continue;
                }

                return otro;

            }
        }

        MostrarError("Opción Inválida, por favor ingrese solo el número (1, 2 o 3).");
    }
}

string LeerPlaca()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Placa del vehículo (Máx 15 caracteres, ej: M123456): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(entrada) || entrada.Length > 15)//Validar 
        {
            MostrarError("Este campo no puede quedar vacío o superar los 15 caracteres.");
            continue;
        }

        //Validacion 
        bool esAlfanumerico = true;
        foreach (char c in entrada)
        {
            if (!char.IsLetterOrDigit(c)) // Su no es letra ni digito, entonces es un caracter especial
            {
                esAlfanumerico = false;
                break;
            }
        }

        if (!esAlfanumerico)
        {
            MostrarError("La placa solo debe contener letras y números (sin espacios, guiones ni símbolos).");
            continue;
        }

        string placaMayuscula = entrada.ToUpper();

        // Volvemos a pintar exactamente la misma pregunta
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Placa del vehículo (Máx 15 caracteres, ej: M123456): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan; // Color celeste para datos ya aprobados
        Console.WriteLine(placaMayuscula);
        Console.ResetColor();


        return placaMayuscula;
    }
}

string LeerConductor()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Nombre del conductor: ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();


        if (string.IsNullOrEmpty(entrada))// Valir vacio
        {
            MostrarError("Este campo no puede quedar vacío. Ingrese el nombre del conductor.");
            continue;
        }

        bool esValido = true;// Validacion estricta
        foreach (char c in entrada)
        {
            if (!char.IsLetter(c) && c != ' ')// Si no es letra y no tiene espacios
            {
                esValido = false;
                break; // Detenemos el ciclo al encontrar el primer carácter prohibido
            }
        }

        if (!esValido)
        {
            MostrarError("Nombre inválido. Por seguridad, solo se permiten letras y espacios.");
            continue;
        }

        string nombreMayuscula = entrada.ToUpper();

        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;// Volvemos a pintar la pregunta
        Console.Write("\n>> Nombre del conductor: ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan; // Imprimimos el nombre formateado
        Console.WriteLine(nombreMayuscula);
        Console.ResetColor();

        return nombreMayuscula;

    }
}

string LeerCedula()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Cédula (14 caracteres o N/A para omitir): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();
        string cedulaMayuscula = entrada.ToUpper();//Tranformar la entrada en mayuscula.

        if (string.IsNullOrEmpty(entrada))//Validar vacio
        {
            MostrarError("Este campo no puede quedar vacío. Ingrese la cédula o escriba N/A.");
            continue;
        }

        if (cedulaMayuscula != "N/A")//Sino escribe N/A entonces pasar a la siguiente validacion.
        {
            if (entrada.Length != 14)  // Validacion de longitud exacta
            {
                MostrarError("Cédula inválida. Debe contener exactamente 14 caracteres (sin guiones) o escribir N/A.");
                continue;
            }

            bool esAlfanumerico = true; // Validacipn de caracteres fuera de numeros y letras
            foreach (char c in entrada)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    esAlfanumerico = false;
                    break;
                }
            }

            if (!esAlfanumerico)
            {
                MostrarError("La cédula no debe contener guiones, espacios ni símbolos. Solo letras y números.");
                continue;
            }
        }

        LimpiarAreaConsola(lineaInicio);//Despues de pasar las validaciones mostrar de nuevo el texto.

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Cédula (14 caracteres o N/A para omitir): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(cedulaMayuscula);
        Console.ResetColor();

        return cedulaMayuscula;
    }
}

string LeerOpcional(string nombreCampo)
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"\n>> {nombreCampo} (Escriba N/A para omitir): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(entrada))  //Validacion de campo vacio
        {
            MostrarError("Este campo no puede quedar vacío. Escriba la información o digite N/A para omitir.");
            continue;
        }

        if (entrada.ToUpper() == "N/A")   //Validacion de n/a
        {
            LimpiarAreaConsola(lineaInicio);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n  ► {nombreCampo} (Escriba N/A para omitir): ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("N/A");
            Console.ResetColor();

            return "N/A";
        }

        bool esValido = true;
        foreach (char c in entrada)//Validar si es el texto no es letras o numeros.
        {
            if (!char.IsLetterOrDigit(c) && c != ' ' && c != '.' && c != ',')
            {
                esValido = false;
                break;
            }
        }

        if (!esValido)
        {
            MostrarError("Texto inválido. No se permiten símbolos especiales (@, #, $, -, _, etc.). Solo use letras, números, espacios, puntos o comas.");
            continue;
        }
        LimpiarAreaConsola(lineaInicio);

        // Volvemos a pintar la pregunta limpia
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"\n>> {nombreCampo} (Escriba N/A para omitir): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(entrada);
        Console.ResetColor();

        return entrada;
    }
}
void MostrarError(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  [!] Error: {mensaje}");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("  Presione ENTER para reintentar...");
    Console.ResetColor();
    Console.ReadKey();

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
            break;
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
            string hora = vehiculos[v].horaIngreso.ToString("HH:mm:ss"); //Convertimos la hora a este formato
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

        Console.WriteLine("\n  [ENTER] Volver al Menú   │   [E] Eliminar Registro   │   [M] Modificar Registro");//Barra de opciones
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
            int lineaSeleccion = Console.CursorTop;
            int numModificar = 0;

            while (true)
            {
                LimpiarAreaConsola(lineaSeleccion);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  ► Ingrese el Nº de registro a modificar (1 al {i}) o [C] para cancelar: ");
                Console.ResetColor();

                string entrada = Console.ReadLine()!.Trim().ToUpper();

                if (entrada == "C") break; // Cancela la modificación y vuelve a evaluar la acción

                if (int.TryParse(entrada, out numModificar) && numModificar > 0 && numModificar <= i)
                {
                    EditarVehiculo(numModificar - 1, numModificar);
                    break; 
                }

                MostrarError($"Número inválido. Por favor seleccione un rango real entre 1 y {i}.");
            }
        }
        else
        {
            MostrarError("Acción inválida. Presione ENTER para reintentar e ingrese E, M o ENTER.");
        }
    }
}

// ====================================================================
// SUB-PROCESO: ELIMINAR REGISTRO DE LA SESIÓN ACTUAL
// ====================================================================
void SubProcesoEliminar()
{
    int filaInicioAccion = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(filaInicioAccion);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Digite el Nº de registro que desea ELIMINAR (o presione ENTER para cancelar): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();

        // Cancelacion directa
        if (string.IsNullOrEmpty(entrada))
        {
            break;
        }

        // Validacion
        if (int.TryParse(entrada, out int registro) && registro > 0 && registro <= i)
        {
            int indiceBorrar = registro - 1; // Ajuste para el indice

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n  [!]  ¿Está seguro que desea eliminar permanentemente el Vehículo Nº {registro} (Placa: {vehiculos[indiceBorrar].placa})? (S/N): ");
            Console.ResetColor();

            string confirmar = Console.ReadLine()!.Trim().ToUpper();

            if (confirmar == "S")
            {
                // Desplazamos todos los elementos una posicion atras de su indice
                for (int j = indiceBorrar; j < i - 1; j++)
                {
                    vehiculos[j] = vehiculos[j + 1];
                }

                // Limpiamos el ultimo 
                vehiculos[i - 1] = default;

                // Restamos 1 al contador global
                i--;

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [ OK ] El registro ha sido eliminado de la memoria y la bitácora fue reajustada.");
                Console.ResetColor();
                Thread.Sleep(1300);
                break; //Cuando termina la tabla se vuelve a pintar
            }
            else
            {
                break;
            }
        }

        // Error por si acaso
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n  [!] Error: El Nº ingresado no existe en la tabla actual o es inválido.");
        Console.ResetColor();
        Console.WriteLine("  Presione ENTER para reintentar...");
        Console.ReadKey();

    }
}

//Editar Vehiculos funcion.
void EditarVehiculo(int pos, int num)
{
    // Limpieza total 
    Console.Clear();
    Console.Write("\x1b[3J");

    // Encabezado del formulario
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                               FORMULARIO DE REESCRITURA Y ACTUALIZACIÓN DE DATOS                         ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    // Informacion antes de modificar
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.Write("  [i] Modificando Registro ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"#{num}");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($" -> Valores actuales en memoria: (Tipo: {vehiculos[pos].tipo} | Placa: {vehiculos[pos].placa} │ Conductor: {vehiculos[pos].conductor} | Cédula: {vehiculos[pos].cedula})\n");
    Console.ResetColor();

    //Ejecutar todas las funciones para modificar
    vehiculos[pos].tipo = SeleccionarTipo();
    vehiculos[pos].placa = LeerPlaca();
    vehiculos[pos].conductor = LeerConductor();
    vehiculos[pos].cedula = LeerCedula();
    vehiculos[pos].destino = LeerOpcional("Destino Actualizado");
    vehiculos[pos].detalles = LeerOpcional("Detalles Actualizados");

    // Cuadro final
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n  ╔══════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("  ║   [ OK ] ¡El registro vehicular ha sido actualizado correctamente!       ║");
    Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("\n  Presione cualquier tecla para aplicar los cambios y refrescar la bitácora...");
    Console.ResetColor();
    Console.ReadKey(true);
}

// =====================================================================
// OPCIÓN 3 MENU: BUSCAR ANTECEDENTES DE PLACA (HISTORIAL GLOBAL EN .CSV)
// =====================================================================
void BuscarVehiculo()
{
    Console.Clear();
    Console.Write("\x1b[3J"); // Limpia el rastro del scroll

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                                              BUSCAR ANTECEDENTES HISTÓRICOS DE PLACA                                                     ║");
    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
    Console.ResetColor();

    Console.Write(" >> Ingrese la PLACA a consultar (Mínimo 3 caracteres): ");
    string placaBuscar = Console.ReadLine()!.Trim().ToUpper().Replace(" ", "");

    // Validar para que la placa tegnga que se mayor a 3.
    if (string.IsNullOrEmpty(placaBuscar) || placaBuscar.Length < 3)//Si cambiamos este 3 cambia el limite de caracteres.
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n [!] Error: Debe ingresar al menos 3 caracteres de la placa para realizar una búsqueda global.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("\n [i] Escaneando registros en memoria y archivos locales .CSV, espere...");
    Console.ResetColor();
    Thread.Sleep(600);

    // Formato de tabla adaptado para 9 columnas. El índice {8} (Origen) tiene un margen amplio de 50 para no cortar el texto.
    string formatoBusqueda = " │ {0,-5} │ {1,-8} │ {2,-10} │ {3,-16} │ {4,-16} │ {5,-14} │ {6,-12} │ {7,-15} │ {8,-50} │";
    bool seEncontraronResultados = false;

    void DibujarEncabezadoTabla()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" ┌───────┬──────────┬────────────┬──────────────────┬──────────────────┬────────────────┬──────────────┬─────────────────┬────────────────────────────────────────────────────┐");
        Console.WriteLine(string.Format(formatoBusqueda, "Nº", "Hora", "Placa", "Tipo Vehículo", "Conductor", "Cédula", "Destino", "Detalles", "Origen del Dato (Archivo)"));
        Console.WriteLine(" ├───────┼──────────┼────────────┼──────────────────┼──────────────────┼────────────────┼──────────────┼─────────────────┼────────────────────────────────────────────────────┤");
        Console.ResetColor();
    }

    void DibujarPieTabla()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" └───────┴──────────┴────────────┴──────────────────┴──────────────────┴────────────────┴──────────────┴─────────────────┴────────────────────────────────────────────────────┘");
        Console.ResetColor();
    }

    //Busqueda en la memoria RAM de la sesion actual
    for (int v = 0; v < i; v++)
    {
        string placaActual = vehiculos[v].placa.ToUpper().Replace(" ", "");
        if (placaActual.Contains(placaBuscar))
        {
            if (!seEncontraronResultados)
            {
                DibujarEncabezadoTabla();
                seEncontraronResultados = true;
            }
            else
            {
                //Cada que encuentre otro resultado agrega una linea en medio.
                Console.WriteLine(" ├───────┼──────────┼────────────┼──────────────────┼──────────────────┼────────────────┼──────────────┼─────────────────┼────────────────────────────────────────────────────┤");
            }
            string num = (v + 1).ToString();
            string horaFormateada = vehiculos[v].horaIngreso.ToString("HH:mm:ss");
            string placaFormateada = vehiculos[v].placa.Length > 10 ? vehiculos[v].placa.Substring(0, 10) : vehiculos[v].placa;
            string tipoFormateado = vehiculos[v].tipo.Length > 16 ? vehiculos[v].tipo.Substring(0, 16) : vehiculos[v].tipo;
            string conductorFormateado = vehiculos[v].conductor.Length > 16 ? vehiculos[v].conductor.Substring(0, 13) + "..." : vehiculos[v].conductor;
            string cedulaFormateada = vehiculos[v].cedula.Length > 14 ? vehiculos[v].cedula.Substring(0, 14) : vehiculos[v].cedula;
            string destinoFormateado = vehiculos[v].destino.Length > 12 ? vehiculos[v].destino.Substring(0, 9) + "..." : vehiculos[v].destino;
            string detallesFormateados = vehiculos[v].detalles.Length > 15 ? vehiculos[v].detalles.Substring(0, 12) + "..." : vehiculos[v].detalles;

            // El archivo de origen se muestra completo en memoria RAM
            Console.WriteLine(string.Format(formatoBusqueda, num, horaFormateada, placaFormateada, tipoFormateado, conductorFormateado, cedulaFormateada, destinoFormateado, detallesFormateados, "Sesión en Curso (RAM)"));
        }
    }
    //Buscar en los archivos .CSV de sesiones anteriores (historial global)
    try
    {
        string rutaDirectorio = AppDomain.CurrentDomain.BaseDirectory;
        string[] archivosCsv = Directory.GetFiles(rutaDirectorio, "*.csv");

        foreach (string archivo in archivosCsv)
        {
            string nombreArchivoCorto = Path.GetFileName(archivo);
            string[] lineas = File.ReadAllLines(archivo);

            // Empezamos desde la fila 15 para saltar los encabezados del CSV
            for (int f = 15; f < lineas.Length; f++)
            {
                string linea = lineas[f];
                if (string.IsNullOrWhiteSpace(linea)) continue;

                string[] columnas = linea.Split(';');

                // Aseguramos que haya datos suficientes en la línea
                if (columnas.Length >= 5)
                {
                    string csvPlaca = columnas.Length > 2 ? columnas[2].Trim() : "";
                    string csvPlacaLimpia = csvPlaca.ToUpper().Replace(" ", "");

                    if (csvPlacaLimpia.Contains(placaBuscar))
                    {
                        if (!seEncontraronResultados)
                        {
                            DibujarEncabezadoTabla();
                            seEncontraronResultados = true;
                        }
                        else
                        {
                            //Cada que encuentre otro resultado agrega una linea entre medio.
                            Console.WriteLine(" ├───────┼──────────┼────────────┼──────────────────┼──────────────────┼────────────────┼──────────────┼─────────────────┼────────────────────────────────────────────────────┤");
                        }

                        // Asi quedan los indices de la columna.
                        // [0]=Posicion, [1]=Hora, [2]=Placa, [3]=Tipo, [4]=Conductor, [5]=Cedula, [6]=Destino, [7]=Detalles
                        string num = columnas[0];
                        string hora = columnas.Length > 1 ? columnas[1].Trim() : "N/A";
                        string placa = csvPlaca.Length > 10 ? csvPlaca.Substring(0, 10) : csvPlaca;
                        string tipo = columnas.Length > 3 ? (columnas[3].Length > 16 ? columnas[3].Substring(0, 16) : columnas[3]) : "N/A";
                        string cond = columnas.Length > 4 ? (columnas[4].Length > 16 ? columnas[4].Substring(0, 13) + "..." : columnas[4]) : "N/A";
                        string ced = columnas.Length > 5 ? (columnas[5].Length > 14 ? columnas[5].Substring(0, 14) : columnas[5]) : "N/A";
                        string dest = columnas.Length > 6 ? (columnas[6].Length > 12 ? columnas[6].Substring(0, 9) + "..." : columnas[6]) : "N/A";
                        string det = columnas.Length > 7 ? (columnas[7].Length > 15 ? columnas[7].Substring(0, 12) + "..." : columnas[7]) : "N/A";

                        string fuente = nombreArchivoCorto;

                        Console.WriteLine(string.Format(formatoBusqueda, num, hora, placa, tipo, cond, ced, dest, det, fuente));
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n [!] Nota: Hubo un inconveniente al leer el almacenamiento físico (.CSV): {ex.Message}");
        Console.ResetColor();
    }

    if (seEncontraronResultados)
    {
        DibujarPieTabla();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n [ OK ] Búsqueda global finalizada con éxito.");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n [!] No se encontraron antecedentes ni registros para la placa que contenga '{placaBuscar}'.");
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

        // Imprimimos asegurando que el orden coincide exactamente con los ttulos de arriba
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" │");
        Console.ResetColor(); Console.Write(num.PadRight(4));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(nombreMostrar.PadRight(19));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(usuario.PadRight(12));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(credencial.PadRight(10));
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(vehiculosTxt.PadRight(10)); // Los vehiculos van aqui
        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("│");
        Console.ResetColor(); Console.Write(portonMostrar.PadRight(16)); // Porton aca
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

        // Ordenamos de mas reciente a mas viejo
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
                    // Datos generales del guarda
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (celdas.Length > 1)
                        {                 
                            Console.WriteLine($"  {celdas[0].Trim()} {celdas[1].Trim()}");//Esta linea printea en la pantalla todas las filas que no sean vacios y encabezados.
                        }
                        else
                        {
                            Console.WriteLine($"  {fila.Trim()}");//Esta linea printea lo que no sea encabezado o registro de vehiculos en el caso que no sea cortado en el array.split
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

    Guardia operador = guardias[indiceActual];
    TimeSpan tiempoTrabajado = operador.horaSalida!.Value - operador.horaInicio; //Nota: TimeSpan es una estructura de C# que representa un intervalo de tiempo, en este caso lo usamos para calcular la duración del turno del guardia.

    // Aqui generamos el nombre del archivo de forma dinamica con el formato: nombre_apellido_fecha_id.csv
    string nombreLimpio = operador.nombre.Split(' ')[0].ToLower();
    string apellidoLimpio = operador.apellido.Split(' ')[0].ToLower();
    string idGuardia = operador.id;
    string fechaHoy = DateTime.Now.ToString("dd-MM-yyyy");

    string nombreArchivo = $"{nombreLimpio}_{apellidoLimpio}_{fechaHoy}_{idGuardia}.csv";
    try
    {
        // Abrimos el archivo para esribir directamente dentro
        using (StreamWriter sw = new StreamWriter(nombreArchivo, false, System.Text.Encoding.UTF8))
        {
            sw.WriteLine("======================================================================================");
            sw.WriteLine("                       REPORTE OFICIAL DE CONTROL DE ACCESO UAM                       ");
            sw.WriteLine("                                 SISTEMA JAGUAR SECURITY                              ");
            sw.WriteLine("======================================================================================");
            sw.WriteLine("");

            // Datos del guarda
            sw.WriteLine("--- I. DATOS DEL OPERADOR ---");
            sw.WriteLine($"Nombre Completo:;{operador.nombre} {operador.apellido}");
            sw.WriteLine($"Credencial (ID):;{operador.id}");
            sw.WriteLine($"Usuario de Red:;{operador.nombreUsuario}");
            sw.WriteLine($"Portón Asigado:;{operador.porton}");
            sw.WriteLine($"Fecha y Hora de Entrada:;{operador.horaInicio.ToString("dd/MM/yyyy HH:mm:ss")}");
            sw.WriteLine($"Fecha y Hora de Salida:;{operador.horaSalida.Value.ToString("dd/MM/yyyy HH:mm:ss")}");
            sw.WriteLine($"Duración Total del Turno:;{tiempoTrabajado.Hours} Horas con {tiempoTrabajado.Minutes} Minutos");
            sw.WriteLine("");

            // Bitacora de vehiculos registrados
            sw.WriteLine("--- II. BITÁCORA DE VEHÍCULOS INGRESADOS ---");
            sw.WriteLine("Nº;Hora;Placa;Tipo de Vehículo;Conductor;Cédula;Destino;Detalles Adicionales");

            for (int r = 0; r < i; r++)
            {
                string horaStr = vehiculos[r].horaIngreso.ToString("HH:mm:ss");
                string placa = vehiculos[r].placa.Replace(";", "-");
                string tipo = vehiculos[r].tipo.Replace(";", "-");
                string conductor = vehiculos[r].conductor.Replace(";", "-");
                string cedula = vehiculos[r].cedula.Replace(";", "-");
                string destino = vehiculos[r].destino.Replace(";", "-");
                string detalles = vehiculos[r].detalles.Replace(";", "-");

                // Escribe la linea del vehiculo
                sw.WriteLine($"{r + 1};{horaStr};{placa};{tipo};{conductor};{cedula};{destino};{detalles}");
            }
            sw.WriteLine("");

            // Cierre
            sw.WriteLine("--- III. RESUMEN DE OPERACIONES ---");
            sw.WriteLine($"Total de Vehículos Procesados:;{i}");
            sw.WriteLine($"Estado del Turno:;CERRADO Y AUDITADO");
            sw.WriteLine("======================================================================================");
        }

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