// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;

namespace ParcialProg2
{
    internal class CVista
    {
        public void Menu()
        {
            Console.WriteLine("=== SISTEMA CLÍNICA VETERINARIA ===");
            Console.WriteLine("1. Registrar Atención");
            Console.WriteLine("2. Salir");
            Console.WriteLine("===================================");
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        public string PedirTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }

        public ushort PedirUShort(string mensaje)
        {
            Console.Write(mensaje);
            ushort.TryParse(Console.ReadLine(), out ushort resultado);
            return resultado;
        }

        public ulong PedirULong(string mensaje)
        {
            Console.Write(mensaje);
            ulong.TryParse(Console.ReadLine(), out ulong resultado);
            return resultado;
        }
    }
}
