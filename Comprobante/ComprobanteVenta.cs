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

            // Crear el nombre del archivo PDF con la fecha actual
            string nombreArchivo = $"Factura_{venta.VentaId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

            // Definir un tamaño más pequeño para el ticket térmico (80mm de ancho que es común en impresoras térmicas)
            // Alto es variable, se ajustará automáticamente
            Rectangle tamanoTicket = new Rectangle(226, 800); // ~80mm de ancho, altura automática
            Document documento = new Document(tamanoTicket, 10, 10, 10, 10); // Márgenes reducidos

            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaCompleta, FileMode.Create));
                documento.Open();

                // Crear fuentes más pequeñas para ajustarse al formato térmico
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fuenteTitulo = new Font(bf, 10, Font.BOLD, BaseColor.BLACK);
                Font fuenteSubtitulo = new Font(bf, 8, Font.BOLD, BaseColor.BLACK);
                Font fuenteNormal = new Font(bf, 8, Font.NORMAL, BaseColor.BLACK);
                Font fuentePequena = new Font(bf, 7, Font.NORMAL, BaseColor.BLACK);
                Font fuenteNegrita = new Font(bf, 8, Font.BOLD, BaseColor.BLACK);

                // Encabezado con Nombre de la Empresa y RUC (datos estáticos, adaptar según tus necesidades)
                Paragraph encabezadoEmpresa = new Paragraph();
                encabezadoEmpresa.Alignment = Element.ALIGN_CENTER;
                encabezadoEmpresa.Add(new Chunk("POSS.A.\n", fuenteTitulo));
                encabezadoEmpresa.Add(new Chunk("RUC: 9999999999001\n", fuenteSubtitulo));
                encabezadoEmpresa.Add(new Chunk("Dir. LATACUNGA\n", fuentePequena));
                encabezadoEmpresa.Add(new Chunk("Dir. LATACUNGA\n", fuentePequena));
                encabezadoEmpresa.Add(new Chunk("Obligado a llevar contabilidad: SI\n", fuentePequena));
                documento.Add(encabezadoEmpresa);

                // Línea separadora más larga
                documento.Add(new Paragraph(new string('-',80), fuentePequena));

                // Datos de la factura alineados a la izquierda
                Paragraph datosFact = new Paragraph();
                datosFact.Alignment = Element.ALIGN_LEFT;
                // "Factura Nro:" en negrita como solicitaste
                datosFact.Add(new Chunk("Factura Nro: ", fuenteNegrita));
                datosFact.Add(new Chunk($"{venta.VentaId}\n", fuenteNormal));
                datosFact.Add(new Chunk("Ambiente: ", fuenteNegrita));
                datosFact.Add(new Chunk("Producción\n", fuenteNormal));
                datosFact.Add(new Chunk("Fecha de Emisión: ", fuenteNegrita));
                datosFact.Add(new Chunk($"{venta.FechaVenta:dd/MM/yyyy}\n", fuenteNormal));
                datosFact.Add(new Chunk("Tipo de Emisión: ", fuenteNegrita));
                datosFact.Add(new Chunk("Normal\n", fuenteNormal));

                // Simular un número de autorización y clave de acceso
                string numAutorizacion = $"{venta.FechaVenta:ddMMyyyy}01999999999001{venta.VentaId}000000001";
                datosFact.Add(new Chunk("Número de Autorización:\n", fuenteNegrita));
                datosFact.Add(new Chunk($"{numAutorizacion}\n", fuentePequena));
                datosFact.Add(new Chunk("Clave de Acceso:\n", fuenteNegrita));
                datosFact.Add(new Chunk($"{numAutorizacion}2\n", fuentePequena));
                documento.Add(datosFact);

                // Línea separadora más larga
                documento.Add(new Paragraph(new string('-', 80), fuentePequena));

                // Datos del cliente alineados a la izquierda
                Paragraph datosCliente = new Paragraph();
                datosCliente.Alignment = Element.ALIGN_LEFT;
                if (cliente != null)
                {
                    datosCliente.Add(new Chunk("Cliente: ", fuenteNegrita));
                    datosCliente.Add(new Chunk($"{cliente.Nombre}\n", fuenteNormal));
                    datosCliente.Add(new Chunk("RUC/CI: ", fuenteNegrita));
                    datosCliente.Add(new Chunk($"{cliente.Cedula}\n", fuenteNormal));
                    // Añadir email si está disponible
                    if (!string.IsNullOrEmpty(cliente.Email))
                    {
                        datosCliente.Add(new Chunk("Email: ", fuenteNegrita));
                        datosCliente.Add(new Chunk($"{cliente.Email}\n", fuenteNormal));
                    }
                }
                else
                {
                    datosCliente.Add(new Chunk("Cliente: ", fuenteNegrita));
                    datosCliente.Add(new Chunk("CONSUMIDOR FINAL\n", fuenteNormal));
                    datosCliente.Add(new Chunk("RUC/CI: ", fuenteNegrita));
                    datosCliente.Add(new Chunk("\n", fuenteNormal));
                }
                documento.Add(datosCliente);

                // Línea separadora más larga
                documento.Add(new Paragraph(new string('-', 88), fuentePequena));

                // Detalles de la venta: Crear una tabla adaptada al formato térmico
                PdfPTable tablaDetalles = new PdfPTable(4);
                tablaDetalles.WidthPercentage = 100;
                tablaDetalles.SetWidths(new float[] { 15, 40, 20, 25 }); // Ajustar según necesidades

                // Encabezados simples
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Cant", fuenteNegrita)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Detalle", fuenteNegrita)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("P.Unit", fuenteNegrita)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                tablaDetalles.AddCell(new PdfPCell(new Phrase("Total", fuenteNegrita)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Agregar los productos
                foreach (var detalle in venta.Detalles)
                {
                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), fuenteNormal))
                    { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });

                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.NombreProducto, fuenteNormal))
                    { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });

                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.PrecioUnitario.ToString("0.00"), fuenteNormal))
                    { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                    tablaDetalles.AddCell(new PdfPCell(new Phrase(detalle.ImporteTotal.ToString("0.00"), fuenteNormal))
                    { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                }
                documento.Add(tablaDetalles);

                // Línea separadora más larga
                documento.Add(new Paragraph(new string('-', 88), fuentePequena));

                // Forma de pago y totales
                Paragraph formaPagoParrafo = new Paragraph();
                formaPagoParrafo.Add(new Chunk("FORMA PAGO: ", fuenteNegrita));
                formaPagoParrafo.Add(new Chunk(formaPago.ToUpper(), fuenteNormal));
                documento.Add(formaPagoParrafo);

                // Tabla para los totales (alineados a la derecha)
                PdfPTable tablaTotales = new PdfPTable(2);
                tablaTotales.WidthPercentage = 100;
                tablaTotales.SetWidths(new float[] { 60, 40 });

                // Subtotal
                tablaTotales.AddCell(new PdfPCell(new Phrase("SUB. TOTAL:", fuenteNegrita))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                tablaTotales.AddCell(new PdfPCell(new Phrase(subtotal.ToString("0.00"), fuenteNormal))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                // IVA
                tablaTotales.AddCell(new PdfPCell(new Phrase("IVA 15%:", fuenteNegrita))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                tablaTotales.AddCell(new PdfPCell(new Phrase(iva.ToString("0.00"), fuenteNormal))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Total
                tablaTotales.AddCell(new PdfPCell(new Phrase("TOTAL:", fuenteNegrita))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                tablaTotales.AddCell(new PdfPCell(new Phrase(total.ToString("0.00"), fuenteNegrita))
                { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                documento.Add(tablaTotales);

                // Línea separadora más larga
                documento.Add(new Paragraph(new string('-', 88), fuentePequena));

                // Información adicional como en el ejemplo
                Paragraph infoAdicional = new Paragraph();
                infoAdicional.Alignment = Element.ALIGN_CENTER;
                infoAdicional.Add(new Chunk("Contribuyente Agente de Retención\n", fuentePequena));
                infoAdicional.Add(new Chunk("Resolución Nro NAC-DGERCGC-00\n", fuentePequena));
                documento.Add(infoAdicional);

                // Agradecimiento final
                Paragraph gracias = new Paragraph("¡GRACIAS POR SU COMPRA!", fuenteNegrita);
                gracias.Alignment = Element.ALIGN_CENTER;
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
                var psi = new ProcessStartInfo
                {
                    FileName = rutaCompleta,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el PDF: " + ex.Message);
            }
        }
    }
}