
Vehiculo[] vehiculos = new Vehiculo[10];
int i = 0; 

void RegistroVehiculo()
{
    
    if (i < 10)
    {
        Console.WriteLine($" Registro de Vehículo #{i + 1} de 10 ");

        int opTipo;
        string tipoFinal = "";
        while (true)
        {
            Console.WriteLine("Selecciona el tipo de vehículo:");
            Console.WriteLine("1. Vehículo liviano");
            Console.WriteLine("2. Motocicleta");
            Console.WriteLine("3. Otro");
            Console.Write("Digita tu opción: ");

            if (int.TryParse(Console.ReadLine()!, out opTipo) && opTipo >= 1 && opTipo <= 3)
            {
                if (opTipo == 1)
                {
                    tipoFinal = "Vehículo liviano";
                }
                else if (opTipo == 2)
                {
                    tipoFinal = "Motocicleta";
                }
                else if (opTipo == 3)
                {
                    Console.Write("Escriba el tipo de vehículo: ");
                    tipoFinal = Console.ReadLine()!;
                }
                break; 
            }
            else
            {
                Console.WriteLine("Opción Inválida. Por favor ingrese una de las 3 opciones disponibles");
            }
        }
        vehiculos[i].tipo = tipoFinal; 

        string placa;
        while (true)
        {
            Console.Write("Placa del vehículo (Ej: RACCN 123456 o M 123456): ");
            placa = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(placa) && placa.Length <= 15)
            {
                break; 
            }
            else
            {
                Console.WriteLine("Placa inválida. No puede estar vacía ni exceder los 15 caracteres.\n");
            }
        }
        vehiculos[i].placa = placa; 

        Console.Write("Nombre del conductor: ");
        vehiculos[i].conductor = Console.ReadLine()!;

        string cedula;
        while (true)
        {
            Console.Write("Cédula (14 caracteres sin guiones. Ej: 0012905940001U): ");
            cedula = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(cedula) && cedula.Length == 14)
            {
                break; 
            }
            else
            {
                Console.WriteLine("Cédula inválida. Debe tener exactamente 14 caracteres (números y letras).");
            }
        }
        vehiculos[i].cedula = cedula; 


        Console.Write("Destino: ");
        vehiculos[i].destino = Console.ReadLine()!;

        Console.Write("Detalles del conductor o vehículo: ");
        vehiculos[i].detalles = Console.ReadLine()!;


        i++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Vehículo registrado exitosamente.");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No hay más espacio disponible. Se ha alcanzado el límite de 10 registros.");
        Console.ResetColor();
    }
}
RegistroVehiculo();

//Correcion: Los struct tiene que ir al final del código, después de la función principal, para evitar errores de declaración.
struct Vehiculo
{
    public string tipo;
    public string placa;
    public string conductor;
    public string cedula;
    public string destino;
    public string detalles;
}
