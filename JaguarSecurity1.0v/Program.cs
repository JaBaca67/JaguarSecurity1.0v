
Vehiculo[] vehiculos = new Vehiculo[1000];
int i = 0;

void RegistroVehiculo()
{
    string continuar = "S";

    while (continuar == "S")
    {
        int cantidad = 0;
        while (true)
        {
            Console.Write("¿Cuántos vehículos desea registrar en este momento?: ");
            if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0)
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Valor ingresado inválido. Por favor ingrese un número entero mayor a 0.");
            Console.ResetColor();
        }

        for (int contador = 0; contador < cantidad; contador++)
        {
            if (i >= 1000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay espacio disponible para almacenar más vehículos. Se alcanzó el límite de 1000 por hoja de registro.");
                Console.ResetColor();
                break;
            }

            Console.WriteLine($"Registro de Vehículo #{i + 1} de 1000 ");

            vehiculos[i].tipo = SeleccionarTipo();
            vehiculos[i].placa = LeerPlaca();
            vehiculos[i].conductor = LeerConductor();
            vehiculos[i].cedula = LeerCedula();
            vehiculos[i].destino = LeerOpcional("Destino");
            vehiculos[i].detalles = LeerOpcional("Detalles del conductor o vehículo");

            i++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vehículo registrado exitosamente.");
            Console.ResetColor();
        }

        while (true)
        {
            Console.Write("¿Desea registrar más vehículos? (S/N): ");
            continuar = Console.ReadLine();
            if (continuar == "S" || continuar == "N")
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Carácter inválido, solo se aceptan las letras S (Si) o N (No).");
            Console.ResetColor();
        }
    }
}

void EditarVehiculo()
{
    Console.WriteLine(" Modo edición de Vehículos registrados ");
    if (i == 0)
    {
        Console.WriteLine("No hay vehículos registrados.");
        return;
    }

    Console.WriteLine($"Vehículos registrados: {i}");
    for (int r = 0; r < i; r++)
    {
        Console.WriteLine($"[{r + 1}] Placa: {vehiculos[r].placa}  Conductor: {vehiculos[r].conductor}");
    }

    int num = 0;
    while (true)
    {
        Console.Write($"Ingrese el número de vehículo a modificar (1 al {i}) o escriba N (No), para salir del modo edición: ");
        string entrada = Console.ReadLine();

        if (entrada == "N")
        {
            return;
        }

        if (int.TryParse(entrada, out num) && num > 0 && num <= i) 
            break;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Valor inválido por favor ingrese N o seleccione el número entero del vehículo correspondiente a editar.");
        Console.ResetColor();
    }

    int pos = num - 1;
    Console.WriteLine($"Modificando Vehículo #{num} (Actual: {vehiculos[pos].placa})");
    Console.WriteLine("---------------------------------------------");

    vehiculos[pos].tipo = SeleccionarTipo();
    vehiculos[pos].placa = LeerPlaca();
    vehiculos[pos].conductor = LeerConductor(); 
    vehiculos[pos].cedula = LeerCedula();
    vehiculos[pos].destino = LeerOpcional("Nuevo Destino");
    vehiculos[pos].detalles = LeerOpcional("Nuevos Detalles");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("¡Vehículo modificado correctamente!");
    Console.ResetColor();
}


string SeleccionarTipo()
{
    int op;
    while (true)
    {
        Console.WriteLine("Selecciona el tipo: 1. Vehículo liviano | 2. Motocicleta | 3. Otro");
        Console.Write("Digite su opción: ");
        if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= 3)
        {
            if (op == 1) 
                return "Vehículo liviano";
            if (op == 2) 
                return "Motocicleta";

            while (true)
            {
                Console.Write("Escriba el tipo de vehículo: ");
                string otro = Console.ReadLine();
                if (otro != "") 
                    return otro;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El tipo de vehículo no puede quedar en blanco.");
                Console.ResetColor();
            }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Opción Inválida, por favor ingrese solo el número entero de las 3 opciones disponibles.");
        Console.ResetColor();
    }
}

string LeerPlaca()
{
    while (true)
    {
        Console.Write("Placa del vehículo (Máx 15 caracteres), ej:M123456 o M 123456 ");
        string placa = Console.ReadLine();
        if (placa != "" && placa.Length <= 15)
            return placa;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Placa inválida, este campo no puede estar vacío o ser mayor a 15 caracteres).");
        Console.ResetColor();
    }
}

string LeerConductor()
{
    while (true)
    {
        Console.Write("Nombre del conductor: ");
        string nombre = Console.ReadLine();

        
        if (nombre == "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nombre inválido. Este campo no puede quedar en blanco.");
            Console.ResetColor();
            continue; 
        }


        bool tieneNumeros = false;
        for (int c = 0; c < nombre.Length; c++)
        {
            if (nombre[c] == '0' || nombre[c] == '1' || nombre[c] == '2' ||
                nombre[c] == '3' || nombre[c] == '4' || nombre[c] == '5' ||
                nombre[c] == '6' || nombre[c] == '7' || nombre[c] == '8' || nombre[c] == '9')
            {
                tieneNumeros = true;
                break; 
            }
        }

       
        if (tieneNumeros)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nombre inválido. Solo se aceptan letras , no se permite la adición de números.");
            Console.ResetColor();
        }
        else
        {
            return nombre;
        }
    }
}

string LeerCedula()
{
    while (true)
    {
        Console.Write("Cédula (Escribir los 14 caracteres o de forma obligatoria escribir N/A para omitir): ");
        string cedula = Console.ReadLine();

        if (cedula == "N/A" ) 
            return "N/A";
        if (cedula.Length == 14) 
            return cedula;

        Console.WriteLine("Cédula inválida. Debe tener 14 caracteres o escribir N/A.");
    }
}

string LeerOpcional(string nombreCampo)
{
    while (true)
    {
        Console.Write($"{nombreCampo} (Obligatorio escribir N/A para omitir): ");
        string entrada = Console.ReadLine();

        if (entrada == "N/A" ) 
            return "N/A";
        if (entrada != "") 
            return entrada;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ESte campo no se puede dejar en blanco, de forma obligatoria para salir ponga N/A.");
        Console.ResetColor();
    }
}

RegistroVehiculo();
EditarVehiculo();

struct Vehiculo
{
    public string tipo;
    public string placa;
    public string conductor;
    public string cedula;
    public string destino;
    public string detalles;
}