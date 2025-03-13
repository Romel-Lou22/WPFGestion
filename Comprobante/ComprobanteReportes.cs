using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using SistemaGestion.Models;

namespace SistemaGestion.Comprobante
{
    public static class ComprobanteReportes
    {
        public static void GenerarComprobantePDF(IEnumerable<ReporteModel> reportes)
        {
            if (reportes == null)
            {
                MessageBox.Show("No hay datos para imprimir.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ruta y nombre del archivo PDF
            string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string rutaBase = Path.Combine(rutaDocumentos, "Comprobantes");
            Directory.CreateDirectory(rutaBase);
            string nombreArchivo = $"Reporte_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

            Document documento = new Document(PageSize.A4, 50, 50, 50, 50);

            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaCompleta, FileMode.Create));
                documento.Open();

                // Encabezado del reporte
                Paragraph encabezado = new Paragraph("Reporte de Transacciones", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                encabezado.Alignment = Element.ALIGN_CENTER;
                documento.Add(encabezado);
                documento.Add(new Paragraph(" "));

                // Tabla con 6 columnas: Id, Fecha, Total, TotalCompra, Estado y Tipo
                PdfPTable tabla = new PdfPTable(6)
                {
                    WidthPercentage = 100
                };
                tabla.AddCell("Id");
                tabla.AddCell("Fecha");
                tabla.AddCell("Total");
                tabla.AddCell("Total Compra");
                tabla.AddCell("Estado");
                tabla.AddCell("Tipo");

                foreach (var reporte in reportes)
                {
                    tabla.AddCell(reporte.Id.ToString());
                    tabla.AddCell(reporte.Fecha.ToShortDateString());
                    tabla.AddCell(reporte.Total.ToString("C"));
                    tabla.AddCell(reporte.TotalCompra.ToString("C"));
                    tabla.AddCell(reporte.Estado);
                    tabla.AddCell(reporte.Tipo);
                }
                documento.Add(tabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el comprobante: " + ex.Message);
            }
            finally
            {
                documento.Close();
            }

            MessageBox.Show($"Comprobante PDF generado exitosamente.\nRuta: {rutaCompleta}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            try
            {
                Process.Start(new ProcessStartInfo(rutaCompleta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el PDF: " + ex.Message);
            }
        }
    }
}
