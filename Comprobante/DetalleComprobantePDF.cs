using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace SistemaGestion.Comprobante
{
    public static class DetalleComprobantePDF
    {
        public static void GenerarPDFDetalle(ReporteModel reporte, IEnumerable<object> detalles)
        {
            if (reporte == null)
            {
                MessageBox.Show("No se ha seleccionado ninguna transacción.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Definir ruta y nombre de archivo
            string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string rutaBase = Path.Combine(rutaDocumentos, "Comprobantes");
            Directory.CreateDirectory(rutaBase);
            string nombreArchivo = $"Detalle_{reporte.Tipo}_{reporte.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

            Document documento = new Document(PageSize.A4, 50, 50, 50, 50);
            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaCompleta, FileMode.Create));
                documento.Open();

                // Título dinámico según el tipo
                string tituloReporte = reporte.Tipo.Equals("Venta", StringComparison.OrdinalIgnoreCase)
                    ? "Detalle de Venta"
                    : "Detalle de Compra";
                Paragraph titulo = new Paragraph(tituloReporte, new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLUE));
                titulo.Alignment = Element.ALIGN_CENTER;
                documento.Add(titulo);
                documento.Add(new Paragraph(" "));

                // Información principal del reporte
                Paragraph info = new Paragraph(
                    $"ID: {reporte.Id}\n" +
                    $"Nombre: {reporte.Nombre}\n" +
                    $"Fecha: {reporte.Fecha:dd/MM/yyyy}\n" +
                    (reporte.Tipo.Equals("Venta", StringComparison.OrdinalIgnoreCase)
                        ? $"Total: {reporte.Total.ToString("C")}\n"
                        : $"Total: {reporte.TotalCompra.ToString("C")}\n") +
                    $"Estado: {reporte.Estado}",
                    new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL)
                );
                info.Alignment = Element.ALIGN_LEFT;
                documento.Add(info);
                documento.Add(new Paragraph(" "));

                // Encabezado para el detalle
                if (detalles != null && detalles.Any())
                {
                    Paragraph detalleTitulo = new Paragraph("Detalle de productos", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                    detalleTitulo.Alignment = Element.ALIGN_CENTER;
                    documento.Add(detalleTitulo);
                    documento.Add(new Paragraph(" "));

                    // Tabla de detalles con 4 columnas
                    PdfPTable tabla = new PdfPTable(4) { WidthPercentage = 100 };

                    // Encabezados
                    tabla.AddCell(new PdfPCell(new Phrase("Producto", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase("Cantidad", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase("Precio Unitario", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase("Importe", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

                    // Recorremos los detalles
                    foreach (var detalleObj in detalles)
                    {
                        // Se asume que ambos modelos de detalle (venta y compra) tienen:
                        // NombreProducto, Cantidad, PrecioUnitario y la propiedad ImporteTotal.
                        dynamic detalle = detalleObj;
                        tabla.AddCell(new PdfPCell(new Phrase(detalle.NombreProducto, new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        tabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tabla.AddCell(new PdfPCell(new Phrase(detalle.PrecioUnitario.ToString("C"), new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // Cambiamos "TotalDetalle" por "ImporteTotal"
                        tabla.AddCell(new PdfPCell(new Phrase(detalle.ImporteTotal.ToString("C"), new Font(Font.FontFamily.HELVETICA, 9))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    documento.Add(tabla);
                }
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
