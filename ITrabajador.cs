// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
namespace ParcialProg2
{
    public interface ITrabajador
    {
        ulong Legajo { get; set; }
        string Apellido { get; set; }
        string Nombre { get; set; }

        string DarDatos();
    }
}
