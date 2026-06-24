

void RegistroVehiculo()
    
    Vehiculo[] vehiculos = new Vehiculo[1000];
    int i = 0;
{


    string continuar = "S";

    while (continuar == "S")
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    SISTEMA JAGUAR SECURITY v1.0                      ║");
        Console.WriteLine("║               MÓDULO DE REGISTRO DE INGRESO VEHICULAR                ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        if (i >= 1000)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [!] Límite alcanzado: No hay más espacio para ingresar más vehículos (Máx 1000).");
            Console.ResetColor();
            break;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"--- Ingresando Registro #{i + 1}  ---\n");
        Console.ResetColor();

        vehiculos[i].tipo = SeleccionarTipo();
        vehiculos[i].placa = LeerPlaca();
        vehiculos[i].conductor = LeerConductor();
        vehiculos[i].cedula = LeerCedula();
        vehiculos[i].destino = LeerOpcional("Destino");
        vehiculos[i].detalles = LeerOpcional("Detalles del conductor o vehículo");
        vehiculos[i].horaIngreso = DateTime.Now;

        i++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==========================================================");
        Console.WriteLine(" [ OK ] ¡Vehículo registrado exitosamente en el sistema! ");
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        int lineaInicio = Console.CursorTop;
        while (true)
        {
            LimpiarAreaConsola(lineaInicio);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n  >> ¿Desea registrar otro vehículo en este momento? (S/N): ");
            Console.ResetColor();

            continuar = Console.ReadLine()!.Trim().ToUpper();

            if (continuar == "S" || continuar == "N")
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  [!] Error: Solo se aceptan las letras 'S' (Sí) o 'N' (No).");
            Console.ResetColor();
            Console.WriteLine("  Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}

string SeleccionarTipo()
{
    int lineaInicio = Console.CursorTop;
    int op;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[!] Selecciona el tipo: 1. Vehículo liviano | 2. Motocicleta | 3. Otro");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(">> Digite su opción (1-3): ");
        Console.ResetColor();

        if (int.TryParse(Console.ReadLine()!.Trim(), out op) && op >= 1 && op <= 3)
        {
            if (op == 1) return "Vehículo liviano";
            if (op == 2) return "Motocicleta";

            int lineaOtro = Console.CursorTop;

            while (true)
            {
                LimpiarAreaConsola(lineaOtro);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" >> Escriba el tipo de vehículo: ");
                Console.ResetColor();

                string otro = Console.ReadLine()!.Trim();

                if (string.IsNullOrEmpty(otro))
                {
                    MostrarError("Este campo no puede quedar en blanco. Por favor ingrese el tipo de vehículo.");
                    continue;
                }

                bool soloLetras = true;
                foreach (char c in otro)
                {
                    if (!char.IsLetter(c) && c != ' ') 
                    {
                        soloLetras = false;
                        break;
                    }
                }

                if (!soloLetras)
                {
                    MostrarError("Entrada inválida. Solo se aceptan letras y espacios (no números ni símbolos).");
                    continue;
                }

                return otro;

            }
        }

        MostrarError("Opción Inválida, por favor ingrese solo el número (1, 2 o 3).");
    }
}

string LeerPlaca()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Placa del vehículo (Máx 15 caracteres, ej: M123456): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(entrada) || entrada.Length > 15)
        {
            MostrarError("Este campo no puede quedar vacío o superar los 15 caracteres.");
            continue;
        }

        
        bool esAlfanumerico = true;
        foreach (char c in entrada)
        {
            if (!char.IsLetterOrDigit(c)) 
            {
                esAlfanumerico = false;
                break;
            }
        }

        if (!esAlfanumerico)
        {
            MostrarError("La placa solo debe contener letras y números (sin espacios, guiones ni símbolos).");
            continue;
        }

        string placaMayuscula = entrada.ToUpper();

        
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Placa del vehículo (Máx 15 caracteres, ej: M123456): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.WriteLine(placaMayuscula);
        Console.ResetColor();


        return placaMayuscula;
    }
}

string LeerConductor()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Nombre del conductor: ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();


        if (string.IsNullOrEmpty(entrada))
        {
            MostrarError("Este campo no puede quedar vacío. Ingrese el nombre del conductor.");
            continue;
        }

        bool esValido = true;
        foreach (char c in entrada)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                esValido = false;
                break; 
            }
        }

        if (!esValido)
        {
            MostrarError("Nombre inválido. Por seguridad, solo se permiten letras y espacios.");
            continue;
        }

        string nombreMayuscula = entrada.ToUpper();

        
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Nombre del conductor: ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;   
        Console.WriteLine(nombreMayuscula);
        Console.ResetColor();

        return nombreMayuscula;

    }
}

string LeerCedula()
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Cédula (14 caracteres o N/A para omitir): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();
        string cedulaMayuscula = entrada.ToUpper();

        if (string.IsNullOrEmpty(entrada))
        {
            MostrarError("Este campo no puede quedar vacío. Ingrese la cédula o escriba N/A.");
            continue;
        }

        if (cedulaMayuscula != "N/A")
        {
            if (entrada.Length != 14)  
            {
                MostrarError("Cédula inválida. Debe contener exactamente 14 caracteres (sin guiones) o escribir N/A.");
                continue;
            }

            bool esAlfanumerico = true; 
            foreach (char c in entrada)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    esAlfanumerico = false;
                    break;
                }
            }

            if (!esAlfanumerico)
            {
                MostrarError("La cédula no debe contener guiones, espacios ni símbolos. Solo letras y números.");
                continue;
            }
        }

        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\n>> Cédula (14 caracteres o N/A para omitir): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(cedulaMayuscula);
        Console.ResetColor();

        return cedulaMayuscula;
    }
}

string LeerOpcional(string nombreCampo)
{
    int lineaInicio = Console.CursorTop;

    while (true)
    {
        LimpiarAreaConsola(lineaInicio);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"\n>> {nombreCampo} (Escriba N/A para omitir): ");
        Console.ResetColor();

        string entrada = Console.ReadLine()!.Trim();

        if (string.IsNullOrEmpty(entrada))  
        {
            MostrarError("Este campo no puede quedar vacío. Escriba la información o digite N/A para omitir.");
            continue;
        }

        if (entrada.ToUpper() == "N/A") 
        {
            LimpiarAreaConsola(lineaInicio);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n  ► {nombreCampo} (Escriba N/A para omitir): ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("N/A");
            Console.ResetColor();

            return "N/A";
        }

        bool esValido = true;
        foreach (char c in entrada)
        {
            if (!char.IsLetterOrDigit(c) && c != ' ' && c != '.' && c != ',')
            {
                esValido = false;
                break;
            }
        }

        if (!esValido)
        {
            MostrarError("Texto inválido. No se permiten símbolos especiales (@, #, $, -, _, etc.). Solo use letras, números, espacios, puntos o comas.");
            continue;
        }
        LimpiarAreaConsola(lineaInicio);

       
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"\n  ► {nombreCampo} (Escriba N/A para omitir): ");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(entrada);
        Console.ResetColor();

        return entrada;
    }
}
void MostrarError(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  [!] Error: {mensaje}");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("  Presione ENTER para reintentar...");
    Console.ResetColor();
    Console.ReadKey();

}


void MostrarRegistros()
{
    while (true)
    {
        Console.Clear();
        Console.Write("\x1b[3J"); 

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                               BITÁCORA DE CONTROL DE ACCESO VEHICULAR                                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        if (i == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  [i] La bitácora de la sesión actual se encuentra vacía. No hay vehículos registrados.");
            Console.ResetColor();
            break; 
        }

        string formatoTabla = " │ {0,-3} │ {1,-8} │ {2,-16} │ {3,-10} │ {4,-22} │ {5,-14} │ {6,-15} │";

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" ┌─────┬──────────┬──────────────────┬────────────┬────────────────────────┬────────────────┬─────────────────┐");
        Console.WriteLine(string.Format(formatoTabla, "Nº", "Hora", "Tipo Vehículo", "Placa", "Nombre Conductor", "Cédula", "Destino"));
        Console.WriteLine(" ├─────┼──────────┼──────────────────┼────────────┼────────────────────────┼────────────────┼─────────────────┤");
        Console.ResetColor();

        for (int v = 0; v < i; v++)
        {
            string num = (v + 1).ToString();
            string hora = vehiculos[v].horaIngreso.ToString("HH:mm:ss"); 
            string tipo = vehiculos[v].tipo.Length > 16 ? vehiculos[v].tipo.Substring(0, 13) + "..." : vehiculos[v].tipo;
            string placa = vehiculos[v].placa.Length > 10 ? vehiculos[v].placa.Substring(0, 10) : vehiculos[v].placa;
            string cond = vehiculos[v].conductor.Length > 22 ? vehiculos[v].conductor.Substring(0, 19) + "..." : vehiculos[v].conductor;
            string ced = vehiculos[v].cedula;
            string dest = vehiculos[v].destino.Length > 15 ? vehiculos[v].destino.Substring(0, 12) + "..." : vehiculos[v].destino;

            Console.WriteLine(string.Format(formatoTabla, num, hora, tipo, placa, cond, ced, dest));
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" └─────┴──────────┴──────────────────┴────────────┴────────────────────────┴────────────────┴─────────────────┘");
        Console.ResetColor();

        Console.WriteLine("\n  [ENTER] Volver al Menú   │   [E] Eliminar Registro   │   [M] Modificar Registro");//Barra de opciones
        Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("  >> Seleccione una acción: ");
        Console.ResetColor();

        string accion = Console.ReadLine()!.Trim().ToUpper();

        if (accion == "" || accion == "ENTER")
        {
            break;
        }
        else if (accion == "E")
        {
            SubProcesoEliminar();
        }
        else if (accion == "M")
        {
            EditarVehiculo();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [!] Acción inválida. Por favor, intente de nuevo.");
            Console.ResetColor();
            Console.WriteLine(" Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
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
    public DateTime horaIngreso; 
}
