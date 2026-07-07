// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;
using System.Collections.Generic;

namespace ParcialProg2
{
    internal class CControlador
    {
        private CVista vista;
        private CConjunto conjunto;

        public CControlador(CVista vista, CConjunto conjunto)
        {
            this.vista = vista;
            this.conjunto = conjunto;
            CargarDatosDePrueba(); // Para pruebas del parcial
        }

        private void CargarDatosDePrueba()
        {
            conjunto.AgregarTrabajador(new CVeterinario { Legajo = 1001, Apellido = "Pérez", Nombre = "Juan", Especialidad = "Clínica", MatriculaProfesional = 5050 });
            conjunto.AgregarTrabajador(new CVeterinario { Legajo = 1002, Apellido = "Gómez", Nombre = "María", Especialidad = "Cirugía", MatriculaProfesional = 6060 });
            conjunto.AgregarTrabajador(new CAsistente { Legajo = 2001, Apellido = "López", Nombre = "Carlos", CertificacionManejoAnimal = true });
            conjunto.AgregarTrabajador(new CAsistente { Legajo = 2002, Apellido = "Díaz", Nombre = "Ana", CertificacionManejoAnimal = false });

            conjunto.AgregarConsultorio(new CConsultorioGeneral { Numero = 1, Piso = 1, Sector = "A" });
            conjunto.AgregarConsultorio(new CQuirofano { Numero = 2, Piso = 2, Sector = "B", CapacidadMaximaAsistentes = 3 });
        }

        public void Iniciar()
        {
            string opcion;
            do
            {
                vista.Menu();
                opcion = vista.PedirTexto("Ingresar opcion... ");
                Console.Clear();
                switch (opcion)
                {
                    case "1":
                        RegistrarAtencion();
                        break;
                    case "2":
                        break; // Salir
                    default:
                        vista.MostrarMensaje("Opción no válida.\n");
                        break;
                }
            } while (opcion != "2");
        }

        public void RegistrarAtencion()
        {
            try
            {
                vista.MostrarMensaje("=== REGISTRO DE ATENCIÓN ===");
                ulong legajoVet = vista.PedirULong("Legajo del Veterinario (Ej: 1001 Clínica, 1002 Cirugía): ");
                var veterinario = conjunto.ObtenerVeterinario(legajoVet);
                
                if (veterinario == null)
                {
                    throw new Exception($"Trabajador inexistente: No existe un veterinario con el legajo {legajoVet}.");
                }

                ushort numConsultorio = vista.PedirUShort("Número de Consultorio (Ej: 1 General, 2 Quirófano): ");
                CConsultorioGeneral consultorio = conjunto.ObtenerConsultorio(numConsultorio);
                
                if (consultorio == null)
                {
                    throw new Exception($"Consultorio inexistente: No existe un consultorio con el número {numConsultorio}.");
                }

                ushort capacidadAsistentes = 0;

                if (consultorio is CQuirofano quiro)
                {
                    capacidadAsistentes = quiro.CapacidadMaximaAsistentes;
                    if (veterinario.Especialidad.ToLower() != "cirugía" && veterinario.Especialidad.ToLower() != "cirugia")
                    {
                        throw new Exception("Especialidad no habilitada: El veterinario no tiene especialidad en Cirugía y no puede atender en el Quirófano.");
                    }
                }
                else if (consultorio is CConsultorioGeneral gen)
                {
                    capacidadAsistentes = 1; // Por regla del sistema, el general permite máx 1
                }

                ushort cantAsistentes = vista.PedirUShort("¿Cuántos asistentes van a participar?: ");
                List<CAsistente> asistentesAtencion = new List<CAsistente>();

                for (int i = 0; i < cantAsistentes; i++)
                {
                    ulong legajoAsis = vista.PedirULong($"Legajo del Asistente {i + 1} (Ej: 2001, 2002): ");
                    var asistente = conjunto.ObtenerAsistente(legajoAsis);
                    if (asistente == null)
                    {
                        throw new Exception($"Trabajador inexistente: No existe un asistente con el legajo {legajoAsis}.");
                    }
                    asistentesAtencion.Add(asistente);
                }

                if (asistentesAtencion.Count > capacidadAsistentes)
                {
                    throw new Exception($"Exceso de asistentes: Se superó la capacidad máxima de asistentes. Capacidad: {capacidadAsistentes}, Intentados: {asistentesAtencion.Count}.");
                }

                CAtencion nuevaAtencion = new CAtencion
                {
                    Id = new Random().Next(1, 1000),
                    Fecha = DateTime.Now,
                    VeterinarioACargo = veterinario,
                    ConsultorioAsignado = consultorio,
                    Asistentes = asistentesAtencion
                };

                conjunto.RegistrarAtencion(nuevaAtencion);
                
                Console.Clear();
                vista.MostrarMensaje("-> Éxito: Atención registrada correctamente.\n");
            }
            catch (Exception ex)
            {
                Console.Clear();
                vista.MostrarMensaje($"ERROR: {ex.Message}\n");
            }
        }
    }
}
