static void IniciarSesionGuardia()
{
    string nombre, apellido;
    int entrada;
    string nomEntrada = "";
    DateTime horaInicio;
    bool sesionActiva = false;

    while (!sesionActiva)
    {
        Console.Write("Ingrese su nombre: ");
        nombre = Console.ReadLine();

        Console.Write("Ingrese su apellido: ");
        apellido = Console.ReadLine();

        bool nombreValido = true;
        bool apellidoValido = true;

        // Validar nombre
        foreach (char c in nombre)
        {
            if (!char.IsLetter(c))
            {
                nombreValido = false;
                break;
            }
        }

        // Validar apellido
        foreach (char c in apellido)
        {
            if (!char.IsLetter(c))
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
                entrada = Convert.ToInt32(Console.ReadLine());

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