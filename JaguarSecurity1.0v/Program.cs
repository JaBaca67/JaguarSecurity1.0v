using System;

class Program
{
    static void Main()
    {
        IniciarSesionGuardia();
    }

    static void IniciarSesionGuardia()
    {
        string nombre, apellido;
        int entrada;
        string nomEntrada;
        DateTime horaInicio;
        bool sesionActiva = false;

        while (!sesionActiva)
        {
            Console.Write("Ingrese su nombre: ");
            nombre = Console.ReadLine();

            Console.Write("Ingrese su apellido: ");
            apellido = Console.ReadLine();

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
                    nomEntrada = "Desconocida";
                    break;
            }

            if (nombre != "" && apellido != "")
            {
                horaInicio = DateTime.Now; // Asignación automática
                sesionActiva = true;

                Console.WriteLine("\n[OK] Autenticación exitosa...");
                Console.WriteLine("Operador: " + nombre + " " + apellido);
                Console.WriteLine("Hora exacta: " + horaInicio);
                Console.WriteLine("Ubicación: " + nomEntrada);
            }
            else
            {
                Console.WriteLine("\nDatos inválidos. Intente de nuevo.\n");
            }
        }
    }
}