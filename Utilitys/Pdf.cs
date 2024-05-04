using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec.wmf;
using Software_Proyecto.Dto;


    public class Pdf
    {
    public void CrearPdfMedicos(List<MedicoDto> medicos, string filePath)
    {
        Document doc = new Document(PageSize.A4); 
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

        doc.Open(); 

       
        BaseColor titleColor = new BaseColor(0, 51, 102); 
        Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, titleColor); 

        
        Paragraph titulo = new Paragraph("Lista de Médicos", titleFont)
        {
            Alignment = Element.ALIGN_CENTER 
        };
        doc.Add(titulo); 

        doc.Add(new Paragraph("\n")); 

        
        PdfPTable table = new PdfPTable(6); 

      
        BaseColor headerColor = new BaseColor(204, 204, 255); 

       
        table.AddCell(new PdfPCell(new Phrase("ID", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Nombres", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Apellidos", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Correo", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Género", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Especialidad", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });

        
        foreach (var medico in medicos)
        {
            table.AddCell(medico.persona.id_persona.ToString());
            table.AddCell(medico.persona.nombres);
            table.AddCell(medico.persona.apellidos);
            table.AddCell(medico.persona.correo);
            table.AddCell(medico.persona.genero);
            table.AddCell(medico.especialidad.ToString());
        }

        doc.Add(table); 

        doc.Close(); 
    }
    public void CrearPdfPacientes(List<PacienteDto> pacientes, string filePath)
    {
        Document doc = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

        doc.Open();


        BaseColor titleColor = new BaseColor(0, 51, 102);
        Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, titleColor);


        Paragraph titulo = new Paragraph("Lista de Pacientes", titleFont)
        {
            Alignment = Element.ALIGN_CENTER
        };
        doc.Add(titulo);

        doc.Add(new Paragraph("\n"));


        PdfPTable table = new PdfPTable(5);


        BaseColor headerColor = new BaseColor(204, 204, 255);


        table.AddCell(new PdfPCell(new Phrase("ID", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Nombres", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Apellidos", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Correo", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
        table.AddCell(new PdfPCell(new Phrase("Género", FontFactory.GetFont("Arial", 12, Font.BOLD))) { BackgroundColor = headerColor });
   


        foreach (var paciente in pacientes)
        {
            table.AddCell(paciente.persona.id_persona.ToString());
            table.AddCell(paciente.persona.nombres);
            table.AddCell(paciente.persona.apellidos);
            table.AddCell(paciente.persona.correo);
            table.AddCell(paciente.persona.genero);
        
        }

        doc.Add(table);

        doc.Close();
    }
}
