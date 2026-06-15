using System;


// ==========================================
// FUNCIÓN DEL MENÚ (CON VALIDACIÓN EN BUCLE)
// ==========================================
int menu()
{
    int opc;
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=====================================================================");
        Console.WriteLine("                     SISTEMA JAGUAR SECURITY v1.0");
        Console.WriteLine("=====================================================================");
        Console.WriteLine("1. REGISTRAR INGRESO VEHICULAR SELECTIVO");
        Console.WriteLine("2. BÚSQUEDA RÁPIDA DE VEHÍCULO");
        Console.WriteLine("3. VER BITÁCORA DE LA SESIÓN ACTUAL");
        Console.WriteLine("4. MÓDULO DE AUDITORÍA (EXCLUSIVO SUPERVISOR)");
        Console.WriteLine("5. CERRAR TURNO Y GENERAR REPORTES (.CSV)");
        Console.WriteLine("6. SALIR DEL PROGRAMA");
        Console.WriteLine("=====================================================================");
        Console.Write("Digita tu opción (1-6): ");

        // Intenta convertir la entrada a número y valida que esté en el rango correcto
        if (int.TryParse(Console.ReadLine()!, out opc) && opc >= 1 && opc <= 6)
        {
            return opc; // Retorna la opción válida y rompe el bucle interno
        }
        else
        {
            Console.WriteLine("\nOpción Inválida. Inténtalo de nuevo.");
            Console.WriteLine("Presione cualquier tecla para reintentar...");
            Console.ReadKey();
        }
    }
}




//ESTO VA EN EL MAIN// BUCLE PRINCIPAL DEL PROGRAMA.

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
            Console.WriteLine("\n[Ejecutando] VER BITÁCORA DE LA SESIÓN ACTUAL");
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