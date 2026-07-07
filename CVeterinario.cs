// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
namespace ParcialProg2
{
    public class CVeterinario : ITrabajador
    {
        public ulong Legajo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public uint MatriculaProfesional { get; set; }
        public string Especialidad { get; set; } // Clínica o Cirugía

        public string DarDatos()
        {
            return $"Veterinario: {Apellido}, {Nombre} - Legajo: {Legajo} - Matrícula: {MatriculaProfesional} - Especialidad: {Especialidad}";
        }
    }
}
