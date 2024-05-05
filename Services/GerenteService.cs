using Software_Proyecto.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Services
{
    public class GerenteService
    {
        public MedicoDto registrarMedico(MedicoDto medico)
        {
            MedicoDto medico1 = new MedicoDto();    
           MedicoRepository medicoRepository = new MedicoRepository();
                medico.persona.id_rol = 2;
            

                Encrypt encrypt = new Encrypt();            
                medico.persona.contrasena = encrypt.encriptarSHA512(medico.persona.contrasena);
                int res = medicoRepository.registroMedico(medico);
             
            return medico1;
        }
        public List<MedicoDto> Lista_Medicos()
        {
            MedicoRepository medicoRepo = new MedicoRepository();   
            var Lista = medicoRepo.MostrarMedicos();
            return Lista;
        }
        public List<PacienteDto> Lista_Pacientes()
        {
            PacienteRepository pacienteRepository = new PacienteRepository();
            var lista_p =pacienteRepository.MostrarPacientes();
            return lista_p;
        }

        public int EliminarMedico(int id)
        {
            int f = 0;
            MedicoRepository medicoRepository = new MedicoRepository();         
            f = medicoRepository.EliminarMedico(id);
            return f;
             }

        public string CrearPdfMedicos()
        {
            Pdf reporte = new Pdf();
            var lista = Lista_Medicos();

            string tempFilePath = Path.Combine(Path.GetTempPath(), "Lista_Medicos.pdf");
            reporte.CrearPdfMedicos(lista, tempFilePath);

            return tempFilePath; 
        }
        public string CrearPdfPacientes()
        {
            Pdf reporte = new Pdf();
            var lista = Lista_Pacientes();

            string tempFilePath = Path.Combine(Path.GetTempPath(), "Lista_Pacientes.pdf");
            reporte.CrearPdfPacientes(lista, tempFilePath);

            return tempFilePath;
        }

    }
    }
