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
    historialGuardias[totalLogins].id = idGenerado;
    historialGuardias[totalLogins].nombre = nom;
    historialGuardias[totalLogins].apellido = ape;
    historialGuardias[totalLogins].nombreUsuario = userGenerado;
    historialGuardias[totalLogins].horaInicio = DateTime.Now;
    historialGuardias[totalLogins].horaSalida = null; // Turno abierto por defecto

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