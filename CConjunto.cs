// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParcialProg2
{
    internal class CConjunto
    {
        // ================= BASE DE DATOS EN MEMORIA =================
        // Acá se guardan todas las listas del sistema
        List<ITrabajador> listaTrabajadores = new List<ITrabajador>();
        List<CConsultorioGeneral> listaConsultorios = new List<CConsultorioGeneral>();
        List<CAtencion> listaAtenciones = new List<CAtencion>();

        // ================= MÉTODOS DE INSERCIÓN (ALTAS) =================
        public void AgregarTrabajador(ITrabajador trabajador)
        {
            listaTrabajadores.Add(trabajador);
        }

        public void AgregarConsultorio(CConsultorioGeneral consultorio)
        {
            listaConsultorios.Add(consultorio);
        }

        public void RegistrarAtencion(CAtencion atencion)
        {
            listaAtenciones.Add(atencion);
        }

        // ================= MÉTODOS DE BÚSQUEDA Y CONSULTAS (QUERIES) =================

        // Busca si algún trabajador genérico existe usando LINQ (.Any)
        public bool ExisteTrabajador(ulong legajoBuscado)
        {
            return listaTrabajadores.Any(t => t.Legajo == legajoBuscado);
        }

        // Busca, verifica que sea Veterinario (Pattern Matching), y lo devuelve como Veterinario ("as")
        public CVeterinario ObtenerVeterinario(ulong legajoBuscado)
        {
            return listaTrabajadores.FirstOrDefault(t => t is CVeterinario v && v.Legajo == legajoBuscado) as CVeterinario;
        }

        // Busca, verifica que sea Asistente, y lo devuelve como Asistente
        public CAsistente ObtenerAsistente(ulong legajoBuscado)
        {
            return listaTrabajadores.FirstOrDefault(t => t is CAsistente a && a.Legajo == legajoBuscado) as CAsistente;
        }

        // Busca un consultorio por número y si no lo encuentra devuelve null (.FirstOrDefault)
        public CConsultorioGeneral ObtenerConsultorio(ushort numeroBuscado)
        {
            return listaConsultorios.FirstOrDefault(c => c.Numero == numeroBuscado);
        }
    }
}
