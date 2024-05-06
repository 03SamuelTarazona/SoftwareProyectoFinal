using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

public class MedicoService
{
    public List<AgendaDto> Lista_Citas()
    {
        MedicoRepository medicoRepo = new MedicoRepository();
        var Lista = medicoRepo.MostrarCitas();
        return Lista;
    }
    public int ActualizarAgenda(AgendaDto agenda)
    {
        int fila = 0;
       MedicoRepository medicoRepository = new MedicoRepository();  

        try
        {
           
            agenda.estado = "Revisado";
            fila = medicoRepository.Detalles(agenda);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return fila;
    }
    public List<AgendaDto> Lista_Historial()
    {
        MedicoRepository medicoRepo = new MedicoRepository();
        var Lista = medicoRepo.MostrarPersonasAtendidas();
        return Lista;
    }
    public string CrearPdfHistorial()
    {
        ReportesUtility reporte = new ReportesUtility();
        var lista = Lista_Historial();

        string tempFilePath = Path.Combine(Path.GetTempPath(), "Historial.pdf");
        reporte.CrearPdfHistorial(lista, tempFilePath);

        return tempFilePath;
    }

}

