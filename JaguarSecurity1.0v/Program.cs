void BuscarVehiculo()
{
    Console.Clear();
    Console.WriteLine("===== BÚSQUEDA RÁPIDA DE VEHÍCULO =====");

    Console.Write("Ingrese la placa del vehículo: ");
    string placaBuscada = Console.ReadLine()!.ToUpper();

    bool encontrado = false;

    for (int i = 0; i < contador; i++)
    {
        if (vehiculos[i].placa.ToUpper() == placaBuscada)
        {
            Console.WriteLine("\nVEHÍCULO ENCONTRADO");
            Console.WriteLine("================================");
            Console.WriteLine($"Tipo: {vehiculos[i].tipo}");
            Console.WriteLine($"Placa: {vehiculos[i].placa}");
            Console.WriteLine($"Conductor: {vehiculos[i].conductor}");
            Console.WriteLine($"Cédula: {vehiculos[i].cedula}");
            Console.WriteLine($"Destino: {vehiculos[i].destino}");
            Console.WriteLine($"Detalles: {vehiculos[i].detalles}");
            Console.WriteLine("================================");

            encontrado = true;
            break;
        }
    }

    if (!encontrado)
    {
        Console.WriteLine("\nNo se encontró ningún vehículo con esa placa.");
    }

    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
}