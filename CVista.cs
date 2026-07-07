// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;

namespace ParcialProg2
{
    internal class CVista
    {
        // Imprime el menú principal
        public void Menu()
        {
            Console.WriteLine("=== SISTEMA CLÍNICA VETERINARIA ===");
            Console.WriteLine("1. Registrar Atención");
            Console.WriteLine("2. Cargar Veterinario");
            Console.WriteLine("3. Cargar Asistente");
            Console.WriteLine("4. Cargar Consultorio General");
            Console.WriteLine("5. Cargar Quirófano");
            Console.WriteLine("6. Salir");
            Console.WriteLine("===================================");
        }

        // Método genérico para imprimir cualquier texto por pantalla
        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        // Muestra un mensaje y espera a que el usuario escriba un texto
        public string PedirTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }

        // Pide un número corto sin signo (ushort). Usado para números de consultorio y piso
        public ushort PedirUShort(string mensaje)
        {
            Console.Write(mensaje);
            ushort.TryParse(Console.ReadLine(), out ushort resultado);
            return resultado;
        }

        // Pide un número largo sin signo (ulong). Usado para los Legajos que suelen ser muy grandes
        public ulong PedirULong(string mensaje)
        {
            Console.Write(mensaje);
            ulong.TryParse(Console.ReadLine(), out ulong resultado);
            return resultado;
        }

        // Pide un número entero sin signo (uint). Usado para la matrícula
        public uint PedirUInt(string mensaje)
        {
            Console.Write(mensaje);
            uint.TryParse(Console.ReadLine(), out uint resultado);
            return resultado;
        }
    }
}
