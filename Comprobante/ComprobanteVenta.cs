using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using SistemaGestion.Models;

namespace SistemaGestion.Comprobante
{
    public static class ComprobanteVenta
    {
        public static void GenerarComprobantePDF(VentaModel venta, ClienteModel cliente, string formaPago, decimal subtotal, decimal iva, decimal total)
        {
            // Obtener la carpeta "Mis Documentos" y crear la carpeta "Comprobantes" dentro de ella
            string rutaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string rutaBase = Path.Combine(rutaDocumentos, "Comprobantes");
            Directory.CreateDirectory(rutaBase);

            // Crear el nombre del archivo PDF
            string nombreArchivo = $"Factura_{venta.VentaId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

            // Definir el tamaño de papel (A4) y márgenes
            Document documento = new Document(PageSize.A4, 36, 36, 36, 36);

            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaCompleta, FileMode.Create));
                documento.Open();

                // Crear fuentes
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fuenteTitulo = new Font(bf, 16, Font.BOLD, BaseColor.BLACK);
                Font fuenteSubtitulo = new Font(bf, 12, Font.BOLD, BaseColor.BLACK);
                Font fuenteNormal = new Font(bf, 12, Font.NORMAL, BaseColor.BLACK);

                // Encabezado: Factura Nro y Fecha Emisión
                Paragraph encabezado = new Paragraph
                {
                    Alignment = Element.ALIGN_CENTER
                };
                encabezado.Add(new Chunk($"Factura Nro: {venta.VentaId}\n", fuenteTitulo));
                encabezado.Add(new Chunk($"Fecha Emisión: {venta.FechaVenta:dd/MM/yyyy}\n", fuenteSubtitulo));
                documento.Add(encabezado);

                // Separador
                documento.Add(new Paragraph(new string('-', 60), fuenteNormal));
                documento.Add(new Paragraph("\n"));

                // Datos del cliente
                if (cliente != null)
                {
                    documento.Add(new Paragraph($"Cliente: {cliente.Nombre}", fuenteNormal));
                    documento.Add(new Paragraph($"RUC/CI: {cliente.Id}", fuenteNormal));
                    // Agrega otros datos, por ejemplo, Email, si lo deseas:
                    // documento.Add(new Paragraph($"Email: {cliente.Email}", fuenteNormal));
                }
                else
                {
                    documento.Add(new Paragraph("Cliente: Consumidor Final", fuenteNormal));
                }
                documento.Add(new Paragraph("\n"));

                // Detalles de la venta: Crear una tabla de 4 columnas (Producto, Cantidad, P. Unit., Total)
                PdfPTable tablaDetalles = new PdfPTable(4)
                {
                    WidthPercentage = 100
                };
                tablaDetalles.SetWidths(new float[] { 40, 15, 20, 25 });
                // Encabezados
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Producto", fuenteSubtitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Cantidad", fuenteSubtitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("P. Unit.", fuenteSubtitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Total", fuenteSubtitulo)) { HorizontalAlignment = Element.ALIGN_CENTER });

                foreach (var detalle in venta.Detalles)
                {
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.NombreProducto, fuenteNormal)));
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), fuenteNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.PrecioUnitario.ToString("C2"), fuenteNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.ImporteTotal.ToString("C2"), fuenteNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                }
                documento.Add(tablaDetalles);
                documento.Add(new Paragraph("\n"));

                // Resumen de pago y forma de pago en una tabla de 2 columnas
                PdfPTable tablaResumen = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                tablaResumen.SetWidths(new float[] { 50, 50 });

                // Columna 1: Forma de Pago
                PdfPCell celdaPago = new PdfPCell(new Phrase($"Forma de Pago: {formaPago}", fuenteNormal))
                {
                    Border = Rectangle.NO_BORDER
                };
                tablaResumen.AddCell(celdaPago);

                // Columna 2: Resumen de montos (SUBTOTAL, IVA, TOTAL)
                PdfPCell celdaTotales = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                celdaTotales.AddElement(new Paragraph($"SUBTOTAL: {subtotal:C2}", fuenteNormal));
                celdaTotales.AddElement(new Paragraph($"IVA (15%): {iva:C2}", fuenteNormal));
                celdaTotales.AddElement(new Paragraph($"TOTAL: {total:C2}", fuenteSubtitulo));
                tablaResumen.AddCell(celdaTotales);

                documento.Add(tablaResumen);
                documento.Add(new Paragraph("\n"));

                // Mensaje final
                Paragraph gracias = new Paragraph("Gracias por su compra.", fuenteSubtitulo)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                documento.Add(gracias);
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
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = rutaCompleta,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el PDF: " + ex.Message);
            }

        }
    }
}
