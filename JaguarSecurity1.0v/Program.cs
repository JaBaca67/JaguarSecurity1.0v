void BuscarVehiculo()
{
    Console.Clear();
    Console.WriteLine("=== BÚSQUEDA RÁPIDA DE VEHÍCULO ===");

    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay vehículos registrados.");
        Console.ResetColor();
        return;
    }

    Console.Write("Ingrese la placa del vehículo a buscar: ");
    string placaBuscar = Console.ReadLine()!;

    bool encontrado = false;

    for (int r = 0; r < i; r++)
    {
        if (vehiculos[r].placa.ToUpper() == placaBuscar.ToUpper())
        {
            encontrado = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVehículo encontrado:");
            Console.ResetColor();

            Console.WriteLine($"Tipo      : {vehiculos[r].tipo}");
            Console.WriteLine($"Placa     : {vehiculos[r].placa}");
            Console.WriteLine($"Conductor : {vehiculos[r].conductor}");
            Console.WriteLine($"Cédula    : {vehiculos[r].cedula}");
            Console.WriteLine($"Destino   : {vehiculos[r].destino}");
            Console.WriteLine($"Detalles  : {vehiculos[r].detalles}");

            string opcion;
            do
            {
                Console.Write("\n¿Desea eliminar este vehículo? (S/N): ");
                opcion = Console.ReadLine()!.ToUpper();

                if (opcion == "S")
                {
                    // Desplazar todos los registros una posición hacia atrás
                    for (int j = r; j < i - 1; j++)
                    {
                        vehiculos[j] = vehiculos[j + 1];
                    }

                    // Limpiar la última posición
                    vehiculos[i - 1] = new Vehiculo();
                    i--;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nVehículo eliminado correctamente.");
                    Console.ResetColor();
                }
                else if (opcion == "N")
                {
                    Console.WriteLine("\nNo se eliminó el vehículo.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida. Ingrese únicamente S o N.");
                    Console.ResetColor();
                }

            } while (opcion != "S" && opcion != "N");

            break;
        }
    }

    if (!encontrado)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNo se encontró ningún vehículo con esa placa.");
        Console.ResetColor();
    }
}