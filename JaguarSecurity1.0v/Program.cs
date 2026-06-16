static void IniciarSesionGuardia()
{
    string nombre, apellido;
    int entrada = 0;
    string nomEntrada = "";
    DateTime horaInicio;
    bool sesionActiva = false;

    while (!sesionActiva)
    {
        Console.Write("Ingrese su nombre: ");
        nombre = Console.ReadLine()!.Trim();// .Trim() quita espacios accidentales al inicio y final

        Console.Write("Ingrese su apellido: ");
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
            // Validar entrada UAM
            do
            {
                Console.WriteLine("\nEntrada UAM");
                Console.WriteLine("1. Portón Principal");
                Console.WriteLine("2. Portón Sur");
                Console.WriteLine("3. Portón Norte");
                Console.Write("Seleccione una entrada (1-3): ");

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
                        Console.WriteLine("\nError: Entrada inválida. Seleccione una opción entre 1 y 3.\n");
                        break;
                }

            } while (entrada < 1 || entrada > 3);

            horaInicio = DateTime.Now; // Hora automática
            sesionActiva = true;

            Console.Clear(); // Limpiar pantalla para mostrar solo la información relevante después de iniciar sesión
            Console.WriteLine("\n[OK] Autenticación exitosa...");
            Console.WriteLine("Operador: " + nombre + " " + apellido);
            Console.WriteLine("Hora exacta: " + horaInicio);
            Console.WriteLine("Ubicación: " + nomEntrada);
        }
        else
        {
            Console.WriteLine("\nError: El nombre y apellido solo pueden contener letras. Intente nuevamente.\n");
        }
    }
}
