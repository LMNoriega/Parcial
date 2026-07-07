// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;
using System.Collections.Generic;

namespace ParcialProg2
{
    public class CAtencion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public CVeterinario VeterinarioACargo { get; set; }
        public CConsultorioGeneral ConsultorioAsignado { get; set; } // Puede ser CConsultorioGeneral o CQuirofano
        public List<CAsistente> Asistentes { get; set; }

        public CAtencion()
        {
            Asistentes = new List<CAsistente>();
        }
    }
}
