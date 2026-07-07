// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;

namespace ParcialProg2
{
    internal class CEjecutora
    {
        static void Main(string[] args)
        {
            CVista interfaz = new CVista();
            CConjunto lista = new CConjunto();
            CControlador controlador = new CControlador(interfaz, lista);

            controlador.Iniciar();
        }
    }
}
