using System;
using System.Collections.Generic;

string nombreGuardia = "";
string apellidoGuardia = "";
void Main()
{
    IniciarSesionGuardia();
    int op;

    do
    {
        // Llamamos a la función menu y guardamos el resultado en 'op'
        op = menu();

        // Evaluamos la opción seleccionada
        switch (op)
        {
            case 1:
                Console.WriteLine("\n[Ejecutando] REGISTRAR INGRESO VEHICULAR SELECTIVO");
                break;
            case 2:
                Console.WriteLine("\n[Ejecutando] BÚSQUEDA RÁPIDA DE VEHÍCULO");
                //Aquí se podría poner lo de eliminar un registro específico, 
                break;
            case 3:
                Console.WriteLine("\n[Ejecutando] MOSTRAR REGISTROS DE LA SESIÓN ACTUAL");
                break;
            case 4:
                Console.WriteLine("\n[Ejecutando] MÓDULO DE AUDITORÍA (EXCLUSIVO SUPERVISOR)");
                break;
            case 5:
                Console.WriteLine("\n[Ejecutando] CERRAR TURNO Y GENERAR REPORTES (.CSV)");
                break;
            case 6:
                Console.WriteLine("\nSaliendo del programa... ¡Hasta luego!");
                break;
            default:
                Console.WriteLine("\nOpción Inválida.");
                break;
        }

        // Si no eligió salir, hacemos una pequeña pausa para ver el mensaje antes de volver a pintar el menú
        if (op != 6)
        {
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

    } while (op != 6);

}

Main();

 void IniciarSesionGuardia()
{
    
    int entrada = 0;
    string nomEntrada = "";
    DateTime horaInicio;
    bool sesionActiva = false;

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==============================================");
    Console.WriteLine("        BIENVENIDO A JAGUARSECURITY v1.0      ");
    Console.WriteLine("==============================================");
    Console.ResetColor();
    Console.WriteLine("Para comenzar, por favor registre sus datos.\n");

    while (!sesionActiva)
    {
        Console.Write(">> Ingrese su nombre: ");
        nombreGuardia = Console.ReadLine()!.Trim();// .Trim() quita espacios accidentales al inicio y final

        Console.Write(">> Ingrese su apellido: ");
        apellidoGuardia = Console.ReadLine()!.Trim();

        bool nombreValido = true;
        bool apellidoValido = true;

        // Validar nombre (Ahora permite letras y espacios)
        foreach (char c in nombreGuardia)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                nombreValido = false;
                break;
            }
        }

        // Validar apellido
        foreach (char c in apellidoGuardia)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                apellidoValido = false;
                break;
            }
        }

        if (nombreGuardia != "" && apellidoGuardia != "" && nombreValido && apellidoValido)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("¡Bienvenido, " + nombreGuardia + "! Proceda a seleccionar su ubicación.");
            Console.ResetColor();
            // Validar entrada UAM
            do
            {
                Console.WriteLine("\n--- Selección de Entrada UAM ---");
                Console.WriteLine("1. Portón Principal");
                Console.WriteLine("2. Portón Sur");
                Console.WriteLine("3. Portón Norte");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\nSeleccione una entrada (1-3): ");
                Console.ResetColor();

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
                        Console.WriteLine(">> Error: Opción inválida. Intente de nuevo.");
                        break;
                }

            } while (entrada < 1 || entrada > 3);

            horaInicio = DateTime.Now; // Hora automática
            sesionActiva = true;

            Console.Clear(); // Limpiar pantalla para mostrar solo la información relevante después de iniciar sesión
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**********************************************");
            Console.WriteLine("          SESIÓN INICIADA CORRECTAMENTE       ");
            Console.WriteLine("**********************************************");
            Console.ResetColor();
            Console.WriteLine($"Operador  : {nombreGuardia} {apellidoGuardia}");
            Console.WriteLine($"Ubicación : {nomEntrada}");
            Console.WriteLine($"Fecha y Hora: {horaInicio.ToString("dd/MM/yyyy - HH:mm:ss")}");
            Console.WriteLine("**********************************************");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>> Error: Datos inválidos.\n");
            Console.ResetColor();
           
        }
    }
}


// ==========================================
// FUNCIÓN DEL MENÚ (CON VALIDACIÓN EN BUCLE)
// ==========================================
int menu()
{
    int opc;
    while (true)
    {
        Console.Clear();
        // Encabezado decorativo
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            SISTEMA DE SEGURIDAD INTEGRAL: JAGUAR V1.0            ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        // Saludo de bienvenida
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n      ¡Hola, oficial {nombreGuardia} {apellidoGuardia}! El sistema está listo para operar.\n");
        Console.ResetColor();

        // Opciones del menú
        Console.WriteLine("1. REGISTRAR INGRESO VEHICULAR SELECTIVO");
        Console.WriteLine("2. BÚSQUEDA RÁPIDA DE VEHÍCULO");
        Console.WriteLine("3. MOSTRAR REGISTROS DE LA SESIÓN ACTUAL");
        Console.WriteLine("4. MÓDULO DE AUDITORÍA(REVISIÓN DE SESIONES ANTERIORES)");
        Console.WriteLine("5. CERRAR TURNO Y GENERAR REPORTES (.CSV)");
        Console.WriteLine("6. SALIR DEL PROGRAMA");
        Console.WriteLine("\n────────────────────────────────────────────────────────────────────");
        Console.Write(" >>Digita tu opción (1-6): ");

        // Intenta convertir la entrada a número y valida que esté en el rango correcto
        if (int.TryParse(Console.ReadLine()!, out opc) && opc >= 1 && opc <= 6)
        {
            return opc; // Retorna la opción válida y rompe el bucle interno
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n  >>Opción Inválida. Por favor, intente de nuevo.");
            Console.ResetColor();
            Console.WriteLine("  Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}

