
Vehiculo[] vehiculos = new Vehiculo[1000];
int i = 0;

void RegistroVehiculo()
{
    string continuar = "S";

    while (continuar == "S")
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║           SISTEMA DE REGISTRO DE VEHÍCULOS            ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        if (i >= 1000)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Límite alcanzado: No hay más espacio para ingresar más vehículos (Máx 1000).");
            Console.ResetColor();
            break;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"--- Ingresando Registro #{i + 1} de 1000 ---");
        Console.ResetColor();

        vehiculos[i].tipo = SeleccionarTipo();
        vehiculos[i].placa = LeerPlaca();
        vehiculos[i].conductor = LeerConductor();
        vehiculos[i].cedula = LeerCedula();
        vehiculos[i].destino = LeerOpcional("Destino");
        vehiculos[i].detalles = LeerOpcional("Detalles del conductor o vehículo");

        i++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==========================================================");
        Console.WriteLine(" ¡Vehículo registrado exitosamente en el sistema!");
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        int startTop = Console.CursorTop;
        while (true)
        {
            Console.SetCursorPosition(0, startTop);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, startTop + 1);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, startTop);

            Console.Write("¿Desea registrar otro vehículo? (S/N): ");
            continuar = Console.ReadLine().ToUpper();

            if (continuar == "S" || continuar == "N")
                break;

            Console.SetCursorPosition(0, startTop + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Carácter inválido, solo se aceptan las letras S (Si) o N (No).");
            Console.ResetColor();

            System.Threading.Thread.Sleep(2000);
        }
    }
}

void EditarVehiculo()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║              MODO EDICIÓN DE VEHÍCULOS                 ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    if (i == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No hay vehículos registrados actualmente.");
        Console.ResetColor();
        return;
    }

    Console.WriteLine($"Vehículos registrados: {i}");
    for (int r = 0; r < i; r++)
    {
        Console.WriteLine($"[{r + 1}] Placa: {vehiculos[r].placa} | Conductor: {vehiculos[r].conductor}");
    }
    Console.WriteLine();

    int num = 0;
    int startTop = Console.CursorTop;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop);

        Console.Write($"Ingrese el número a modificar (1 al {i}) o N para salir: ");
        string entrada = Console.ReadLine().ToUpper();

        if (entrada == "N")
            return;

        if (int.TryParse(entrada, out num) && num > 0 && num <= i)
            break;

        Console.SetCursorPosition(0, startTop + 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Valor inválido. Seleccione el número de vehículo o N para salir.");
        Console.ResetColor();

        System.Threading.Thread.Sleep(2000);
    }

    int pos = num - 1;
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"--- Modificando Vehículo #{num} (Actual: {vehiculos[pos].placa}) ---");
    Console.ResetColor();

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
    int startTop = Console.CursorTop;
    int op;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 2);
        Console.Write(new string(' ', Console.WindowWidth - 1));

        Console.SetCursorPosition(0, startTop);
        Console.WriteLine("Selecciona el tipo: 1. Vehículo liviano | 2. Motocicleta | 3. Otro");
        Console.Write("Digite su opción: ");

        if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= 3)
        {
            if (op == 1)
                return "Vehículo liviano";
            if (op == 2)
                return "Motocicleta";

            int otroTop = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, otroTop);
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, otroTop + 1);
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, otroTop);

                Console.Write("Escriba el tipo de vehículo: ");
                string otro = Console.ReadLine();
                if (otro != "")
                    return otro;

                Console.SetCursorPosition(0, otroTop + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Este campo no puede quedar en blanco. Por favor ingrese el tipo de vehiculo");
                Console.ResetColor();

                System.Threading.Thread.Sleep(2000);
            }
        }
        Console.SetCursorPosition(0, startTop + 2);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Opción Inválida, por favor ingrese solo los número (1, 2 o 3).");
        Console.ResetColor();

        System.Threading.Thread.Sleep(2000);
    }
}

string LeerPlaca()
{
    int startTop = Console.CursorTop;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop);

        Console.Write("Placa del vehículo (Máx 15 caracteres), ej: M123456: ");
        string placa = Console.ReadLine();

        if (placa != "" && placa.Length <= 15)
        {
            placa = placa.ToUpper();
            Console.SetCursorPosition(0, startTop);
            Console.WriteLine($"Placa del vehículo (Máx 15 caracteres), ej: M123456: {placa} ");
            return placa;
        }

        Console.SetCursorPosition(0, startTop + 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Este campo no puede quedar vacío o superar los 15 caracteres.");
        Console.ResetColor();

        System.Threading.Thread.Sleep(2000);
    }
}

string LeerConductor()
{
    int startTop = Console.CursorTop;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop);

        Console.Write("Nombre del conductor: ");
        string nombre = Console.ReadLine();

        if (nombre == "")
        {
            Console.SetCursorPosition(0, startTop + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Este campo no puede quedar vacío, por favor ingrese el nombre del conductor.");
            Console.ResetColor();
            System.Threading.Thread.Sleep(2000);
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
            Console.SetCursorPosition(0, startTop + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Nombre inválido,por favor solo ingrese letras.");
            Console.ResetColor();
            System.Threading.Thread.Sleep(2000);
        }
        else
        {
            nombre = nombre.ToUpper();
            Console.SetCursorPosition(0, startTop);
            Console.WriteLine($"Nombre del conductor: {nombre} ");
            return nombre;
        }
    }
}

string LeerCedula()
{
    int startTop = Console.CursorTop;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop);

        Console.Write("Cédula (14 caracteres o N/A para omitir): ");
        string cedula = Console.ReadLine().ToUpper();

        if (cedula == "N/A" || cedula.Length == 14)
        {
            Console.SetCursorPosition(0, startTop);
            Console.WriteLine($"Cédula (14 caracteres o N/A para omitir): {cedula} ");
            return cedula;
        }

        Console.SetCursorPosition(0, startTop + 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Cédula inválida. La cédula no debe contener guiones y contener de longitud 14 caracteres o escribir N/A.");
        Console.ResetColor();

        System.Threading.Thread.Sleep(3000);
    }
}

string LeerOpcional(string nombreCampo)
{
    int startTop = Console.CursorTop;
    while (true)
    {
        Console.SetCursorPosition(0, startTop);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop + 1);
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, startTop);

        Console.Write($"{nombreCampo} (Obligatorio escribir N/A para omitir): ");
        string entrada = Console.ReadLine();

        if (entrada.ToUpper() == "N/A")
            return "N/A";
        if (entrada != "")
            return entrada;

        Console.SetCursorPosition(0, startTop + 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("No se puede dejar este campo vacío, por favor escriba el dato o N/A para omitir.");
        Console.ResetColor();

        System.Threading.Thread.Sleep(2000);
    }
}

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                    MENÚ PRINCIPAL                      ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine("1. Registrar Vehículo");
    Console.WriteLine("2. Modo Edición de Vehículos");
    Console.WriteLine("3. Salir");
    Console.WriteLine("==========================================================");
    Console.Write("Seleccione una opción (1-3): ");
    string opcionMenu = Console.ReadLine();

    if (opcionMenu == "1")
    {
        RegistroVehiculo();
    }
    else if (opcionMenu == "2")
    {
        EditarVehiculo();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPresione cualquier tecla para regresar al Menú Principal...");
        Console.ResetColor();
        Console.ReadKey(); 
    }
    else if (opcionMenu == "3")
    {
        break;
    }
}

struct Vehiculo
{
    public string tipo;
    public string placa;
    public string conductor;
    public string cedula;
    public string destino;
    public string detalles;
}