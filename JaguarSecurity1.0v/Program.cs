//====================================
// BÚSQUEDA RÁPIDA DE VEHÍCULO
//====================================
void BuscarVehiculo(string[] placa, string[] nombreConductor,
                    string[] tipoVehiculo, string[] horaEntrada,
                    int contador)
{
    Console.Clear();
    Console.WriteLine("===== BÚSQUEDA RÁPIDA DE VEHÍCULO =====");
    Console.Write("Ingrese la placa del vehículo: ");
    string placaBuscada = Console.ReadLine()!.ToUpper();

    bool encontrado = false;

    for (int i = 0; i < contador; i++)
    {
        if (placa[i].ToUpper() == placaBuscada)
        {
            Console.WriteLine("\nVEHÍCULO ENCONTRADO");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Placa: {placa[i]}");
            Console.WriteLine($"Conductor: {nombreConductor[i]}");
            Console.WriteLine($"Tipo de vehículo: {tipoVehiculo[i]}");
            Console.WriteLine($"Hora de entrada: {horaEntrada[i]}");
            Console.WriteLine("--------------------------------");

            encontrado = true;
            break;
        }
    }

    if (!encontrado)
    {
        Console.WriteLine("\nNo se encontró ningún vehículo con esa placa.");
    }
}
.,.,.,.,.,.,..,
