// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
namespace ParcialProg2
{
    public class CAsistente : ITrabajador
    {
        public ulong Legajo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public bool CertificacionManejoAnimal { get; set; }

        public string DarDatos()
        {
            string cert;
            if (CertificacionManejoAnimal)
                cert = "Sí";
            else
                cert = "No";
            return $"Asistente: {Apellido}, {Nombre} - Legajo: {Legajo} - Certificación: {cert}";
        }
    }
}
