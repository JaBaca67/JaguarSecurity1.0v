static void IniciarSesionGuardia()
{
    string nombre, apellido;
    int entrada = 0;
    string nomEntrada = "";
    DateTime horaInicio;
    bool sesionActiva = false;

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==============================================");
    Console.WriteLine("        BIENVENIDO A JAGUARSECURITY V.1       ");
    Console.WriteLine("==============================================");
    Console.ResetColor();
    Console.WriteLine("Para comenzar, por favor registre sus datos.\n");

    while (!sesionActiva)
    {
        Console.Write(">> Ingrese su nombre: ");
        nombre = Console.ReadLine()!.Trim();// .Trim() quita espacios accidentales al inicio y final

        Console.Write(">> Ingrese su apellido: ");
        apellido = Console.ReadLine()!.Trim();

        bool nombreValido = true;
        bool apellidoValido = true;

        // Validar nombre (Ahora permite letras y espacios)
        foreach (char c in nombre)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                nombreValido = false;
                break;
            }
        }

        // Validar apellido
        foreach (char c in apellido)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                apellidoValido = false;
                break;
            }
        }

        if (nombre != "" && apellido != "" && nombreValido && apellidoValido)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("¡Bienvenido, " + nombre + "! Proceda a seleccionar su ubicación.");
            Console.ResetColor();
            // Validar entrada UAM
            do
            {
                Console.WriteLine("\n--- Selección de Entrada UAM ---");
                Console.WriteLine("1. Portón Principal");
                Console.WriteLine("2. Portón Sur");
                Console.WriteLine("3. Portón Norte");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\nSeleccione una entrada (1-3): ");
                Console.ResetColor();

                /*Mejora de lider: Validar que la entrada sea un número entero
                 * si el guardia ingresa un letra (ej:A), el programa no se bloquea y muestra un mensaje de error */

                string entradaTexto = Console.ReadLine()!.Trim();
                if (!int.TryParse(entradaTexto, out entrada))
                {
                    entrada = 0; // Forzar un valor invalido para que se repita el ciclo
                }
                switch (entrada)
                {
                    case 1:
                        nomEntrada = "Portón Principal";
                        break;

                    case 2:
                        nomEntrada = "Portón Sur";
                        break;

                    case 3:
                        nomEntrada = "Portón Norte";
                        break;

                    default:
                        Console.WriteLine(">> Error: Opción inválida. Intente de nuevo.");
                        break;
                }

            } while (entrada < 1 || entrada > 3);

            horaInicio = DateTime.Now; // Hora automática
            sesionActiva = true;

            Console.Clear(); // Limpiar pantalla para mostrar solo la información relevante después de iniciar sesión
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**********************************************");
            Console.WriteLine("          SESIÓN INICIADA CORRECTAMENTE       ");
            Console.WriteLine("**********************************************");
            Console.ResetColor();
            Console.WriteLine($"Operador  : {nombre} {apellido}");
            Console.WriteLine($"Ubicación : {nomEntrada}");
            Console.WriteLine($"Fecha y Hora: {horaInicio.ToString("dd/MM/yyyy - HH:mm:ss")}");
            Console.WriteLine("**********************************************");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>> Error: Datos inválidos.\n");
            Console.ResetColor();
        }
    }
}
IniciarSesionGuardia();
