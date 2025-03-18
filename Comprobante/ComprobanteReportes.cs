using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using SistemaGestion.Models;

namespace SistemaGestion.Comprobante
{
    public static class ComprobanteReportes
    {
        public static void GenerarComprobantePDF(IEnumerable<ReporteModel> reportes, DateTime fechaInicio, DateTime fechaFin)
        {
            if (reportes == null || !reportes.Any())
            {
                MessageBox.Show("No hay datos para imprimir.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Definir la ruta y nombre del archivo PDF
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

                // Determinar título dinámico según el primer registro
                string tituloReporte = "Reporte de Transacciones";
                var primerReporte = reportes.First();
                if (primerReporte.Tipo.Equals("Venta", StringComparison.OrdinalIgnoreCase))
                    tituloReporte = "Reporte de Ventas";
                else if (primerReporte.Tipo.Equals("Compra", StringComparison.OrdinalIgnoreCase))
                    tituloReporte = "Reporte de Compras";

                // Encabezado principal
                Paragraph titulo = new Paragraph(tituloReporte, new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLUE));
                titulo.Alignment = Element.ALIGN_CENTER;
                documento.Add(titulo);
                documento.Add(new Paragraph(" "));

                // Información adicional: fecha de generación, rango de consulta y total de registros
                Paragraph info = new Paragraph(
                    $"Fecha de generación: {DateTime.Now:g}\n" +
                    $"Rango de consulta: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}\n" +
                    $"Total de registros: {reportes.Count()}",
                    new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL)
                );
                info.Alignment = Element.ALIGN_CENTER;
                documento.Add(info);
                documento.Add(new Paragraph(" "));

                // Línea divisoria
                var separador = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separador));
                documento.Add(new Paragraph(" "));

                // Crear tabla con 6 columnas: Id, Nombre, Fecha, Precio Total, Estado y Tipo
                PdfPTable tabla = new PdfPTable(6)
                {
                    WidthPercentage = 100
                };

                // Encabezados de la tabla
                tabla.AddCell(new PdfPCell(new Phrase("Id", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                tabla.AddCell(new PdfPCell(new Phrase("Nombre", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                tabla.AddCell(new PdfPCell(new Phrase("Fecha", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                tabla.AddCell(new PdfPCell(new Phrase("Precio Total", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                tabla.AddCell(new PdfPCell(new Phrase("Estado", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                tabla.AddCell(new PdfPCell(new Phrase("Tipo", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

                // Recorrer cada reporte y agregar las filas a la tabla
                foreach (var reporte in reportes)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(reporte.Id.ToString(), new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase(reporte.Nombre, new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    tabla.AddCell(new PdfPCell(new Phrase(reporte.Fecha.ToShortDateString(), new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_CENTER });

                    // Mostrar Total para ventas y TotalCompra para compras en una única columna
                    string precioTotal = reporte.Tipo.Equals("Venta", StringComparison.OrdinalIgnoreCase)
                        ? reporte.Total.ToString("C")
                        : reporte.TotalCompra.ToString("C");
                    tabla.AddCell(new PdfPCell(new Phrase(precioTotal, new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_RIGHT });

                    tabla.AddCell(new PdfPCell(new Phrase(reporte.Estado, new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase(reporte.Tipo, new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_CENTER });
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
