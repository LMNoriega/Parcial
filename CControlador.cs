// Legajo: 31219 - Apellidos: Noriega Morea - Nombres: Lucas Maximiliano
using System;
using System.Collections.Generic;

namespace ParcialProg2
{
    internal class CControlador
    {
        private CVista vista;
        private CConjunto conjunto;

        // Constructor: Vincula la Vista y el Conjunto para poder usarlos en el Controlador
        public CControlador(CVista vista, CConjunto conjunto)
        {
            this.vista = vista;
            this.conjunto = conjunto;
            CargarDatosDePrueba(); // Carga datos falsos para que puedas probar el registro de atención sin cargar nada a mano
        }

        // Método oculto solo para pruebas. Llena el sistema con Veterinarios, Asistentes y Consultorios por defecto
        private void CargarDatosDePrueba()
        {
            conjunto.AgregarTrabajador(new CVeterinario { Legajo = 1001, Apellido = "Pérez", Nombre = "Juan", Especialidad = "Clínica", MatriculaProfesional = 5050 });
            conjunto.AgregarTrabajador(new CVeterinario { Legajo = 1002, Apellido = "Gómez", Nombre = "María", Especialidad = "Cirugía", MatriculaProfesional = 6060 });
            conjunto.AgregarTrabajador(new CAsistente { Legajo = 2001, Apellido = "López", Nombre = "Carlos", CertificacionManejoAnimal = true });
            conjunto.AgregarTrabajador(new CAsistente { Legajo = 2002, Apellido = "Díaz", Nombre = "Ana", CertificacionManejoAnimal = false });

            conjunto.AgregarConsultorio(new CConsultorioGeneral { Numero = 1, Piso = 1, Sector = "A" });
            conjunto.AgregarConsultorio(new CQuirofano { Numero = 2, Piso = 2, Sector = "B", CapacidadMaximaAsistentes = 3 });
        }

        // Bucle principal del programa: Muestra el menú hasta que el usuario elija "2" para salir
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
                        RegistrarAtencion(); // Llama al método principal del parcial
                        break;
                    case "2":
                        CargarVeterinario();
                        break;
                    case "3":
                        CargarAsistente();
                        break;
                    case "4":
                        CargarConsultorioGeneral();
                        break;
                    case "5":
                        CargarQuirofano();
                        break;
                    case "6":
                        break; // Sale del programa
                    default:
                        vista.MostrarMensaje("Opción no válida.\n");
                        break;
                }
            } while (opcion != "6");
        }

        // Método principal de negocio que evalúa el parcial (Validaciones con Excepciones)
        public void RegistrarAtencion()
        {
            try
            {
                vista.MostrarMensaje("=== REGISTRO DE ATENCIÓN ===");
                
                // 1. VALIDAR VETERINARIO
                ulong legajoVet = vista.PedirULong("Legajo del Veterinario (Ej: 1001 Clínica, 1002 Cirugía): ");
                var veterinario = conjunto.ObtenerVeterinario(legajoVet);
                
                if (veterinario == null)
                {
                    throw new Exception($"Trabajador inexistente: No existe un veterinario con el legajo {legajoVet}.");
                }

                // 2. VALIDAR CONSULTORIO Y ESPECIALIDAD
                ushort numConsultorio = vista.PedirUShort("Número de Consultorio (Ej: 1 General, 2 Quirófano): ");
                CConsultorioGeneral consultorio = conjunto.ObtenerConsultorio(numConsultorio);
                
                if (consultorio == null)
                {
                    throw new Exception($"Consultorio inexistente: No existe un consultorio con el número {numConsultorio}.");
                }

                ushort capacidadAsistentes = 0; // Usamos esto para guardar cuántos asistentes deja meter el consultorio

                // REGLA DE POLIMORFISMO: Primero verificamos si es hijo (Quirófano) y si no, si es Padre (General)
                if (consultorio is CQuirofano quiro)
                {
                    capacidadAsistentes = quiro.CapacidadMaximaAsistentes;
                    // El quirófano solo permite veterinarios Cirujanos
                    if (veterinario.Especialidad.ToLower() != "cirugía" && veterinario.Especialidad.ToLower() != "cirugia")
                    {
                        throw new Exception("Especialidad no habilitada: El veterinario no tiene especialidad en Cirugía y no puede atender en el Quirófano.");
                    }
                }
                else if (consultorio is CConsultorioGeneral gen)
                {
                    capacidadAsistentes = 1; // Por regla del sistema, el Consultorio General permite máx 1 asistente
                }

                // 3. VALIDAR ASISTENTES Y CANTIDAD
                ushort cantAsistentes = vista.PedirUShort("¿Cuántos asistentes van a participar?: ");
                List<CAsistente> asistentesAtencion = new List<CAsistente>();

                // Ciclo FOR para cargar la cantidad de asistentes ingresada y verificar si existen
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

                // Verificamos si los asistentes cargados no superan el límite del consultorio
                if (asistentesAtencion.Count > capacidadAsistentes)
                {
                    throw new Exception($"Exceso de asistentes: Se superó la capacidad máxima de asistentes. Capacidad: {capacidadAsistentes}, Intentados: {asistentesAtencion.Count}.");
                }

                // 4. SI LLEGA HASTA ACÁ: Se pasaron todas las validaciones y guardamos la atención
                CAtencion nuevaAtencion = new CAtencion
                {
                    Id = new Random().Next(1, 1000), // Genera un ID random de atención
                    Fecha = DateTime.Now,            // Toma la fecha y hora de la PC actual
                    VeterinarioACargo = veterinario,
                    ConsultorioAsignado = consultorio,
                    Asistentes = asistentesAtencion
                };

                conjunto.RegistrarAtencion(nuevaAtencion); // Lo manda a CConjunto para guardar en la lista final
                
                Console.Clear();
                vista.MostrarMensaje("-> Éxito: Atención registrada correctamente.\n");
            }
            catch (Exception ex)
            {
                // Si CUALQUIER throw se ejecuta arriba, el código frena y cae acá mostrando el error en pantalla
                Console.Clear();
                vista.MostrarMensaje($"ERROR: {ex.Message}\n");
            }
        }
        // ================= MÉTODOS DE CARGA MANUAL (ABM) =================
        public void CargarVeterinario()
        {
            vista.MostrarMensaje("--- Cargar Nuevo Veterinario ---");
            ulong legajo = vista.PedirULong("Legajo: ");
            if (conjunto.ExisteTrabajador(legajo))
            {
                vista.MostrarMensaje("Error: Ya existe un trabajador con ese legajo.\n");
                return;
            }
            string apellido = vista.PedirTexto("Apellido: ");
            string nombre = vista.PedirTexto("Nombre: ");
            uint matricula = vista.PedirUInt("Matrícula Profesional: ");
            string especialidad = vista.PedirTexto("Especialidad (Clínica/Cirugía): ");

            CVeterinario vet = new CVeterinario { Legajo = legajo, Apellido = apellido, Nombre = nombre, MatriculaProfesional = matricula, Especialidad = especialidad };
            conjunto.AgregarTrabajador(vet);
            Console.Clear();
            vista.MostrarMensaje("Veterinario cargado exitosamente.\n");
        }

        public void CargarAsistente()
        {
            vista.MostrarMensaje("--- Cargar Nuevo Asistente ---");
            ulong legajo = vista.PedirULong("Legajo: ");
            if (conjunto.ExisteTrabajador(legajo))
            {
                vista.MostrarMensaje("Error: Ya existe un trabajador con ese legajo.\n");
                return;
            }
            string apellido = vista.PedirTexto("Apellido: ");
            string nombre = vista.PedirTexto("Nombre: ");
            string certInput = vista.PedirTexto("¿Tiene certificación en manejo animal? (s/n): ");
            bool cert = (certInput.ToLower() == "s" || certInput.ToLower() == "si" || certInput.ToLower() == "sí");

            CAsistente asis = new CAsistente { Legajo = legajo, Apellido = apellido, Nombre = nombre, CertificacionManejoAnimal = cert };
            conjunto.AgregarTrabajador(asis);
            Console.Clear();
            vista.MostrarMensaje("Asistente cargado exitosamente.\n");
        }

        public void CargarConsultorioGeneral()
        {
            vista.MostrarMensaje("--- Cargar Consultorio General ---");
            ushort numero = vista.PedirUShort("Número: ");
            if (conjunto.ObtenerConsultorio(numero) != null)
            {
                vista.MostrarMensaje("Error: Ya existe un consultorio con ese número.\n");
                return;
            }
            string sector = vista.PedirTexto("Sector: ");
            ushort piso = vista.PedirUShort("Piso: ");

            CConsultorioGeneral gen = new CConsultorioGeneral { Numero = numero, Sector = sector, Piso = piso };
            conjunto.AgregarConsultorio(gen);
            Console.Clear();
            vista.MostrarMensaje("Consultorio General cargado exitosamente.\n");
        }

        public void CargarQuirofano()
        {
            vista.MostrarMensaje("--- Cargar Quirófano ---");
            ushort numero = vista.PedirUShort("Número: ");
            if (conjunto.ObtenerConsultorio(numero) != null)
            {
                vista.MostrarMensaje("Error: Ya existe un consultorio con ese número.\n");
                return;
            }
            string sector = vista.PedirTexto("Sector: ");
            ushort piso = vista.PedirUShort("Piso: ");
            ushort capacidad = vista.PedirUShort("Capacidad máxima de asistentes: ");

            CQuirofano quiro = new CQuirofano { Numero = numero, Sector = sector, Piso = piso, CapacidadMaximaAsistentes = capacidad };
            conjunto.AgregarConsultorio(quiro);
            Console.Clear();
            vista.MostrarMensaje("Quirófano cargado exitosamente.\n");
        }
    }
}
