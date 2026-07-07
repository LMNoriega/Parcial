// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParcialProg2
{
    internal class CConjunto
    {
        List<ITrabajador> listaTrabajadores = new List<ITrabajador>();
        List<CConsultorioGeneral> listaConsultorios = new List<CConsultorioGeneral>();
        List<CAtencion> listaAtenciones = new List<CAtencion>();

        public void AgregarTrabajador(ITrabajador trabajador)
        {
            listaTrabajadores.Add(trabajador);
        }

        public void AgregarConsultorio(CConsultorioGeneral consultorio)
        {
            listaConsultorios.Add(consultorio);
        }

        public bool ExisteTrabajador(ulong legajoBuscado)
        {
            return listaTrabajadores.Any(t => t.Legajo == legajoBuscado);
        }

        public CVeterinario ObtenerVeterinario(ulong legajoBuscado)
        {
            return listaTrabajadores.FirstOrDefault(t => t is CVeterinario v && v.Legajo == legajoBuscado) as CVeterinario;
        }

        public CAsistente ObtenerAsistente(ulong legajoBuscado)
        {
            return listaTrabajadores.FirstOrDefault(t => t is CAsistente a && a.Legajo == legajoBuscado) as CAsistente;
        }

        public CConsultorioGeneral ObtenerConsultorio(ushort numeroBuscado)
        {
            return listaConsultorios.FirstOrDefault(c => c.Numero == numeroBuscado);
        }

        public void RegistrarAtencion(CAtencion atencion)
        {
            listaAtenciones.Add(atencion);
        }
    }
}
