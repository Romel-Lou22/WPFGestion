using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestion.Repositories;

namespace SistemaGestion.Services
{
    class InventarioService
    {
        private readonly IStockRepository _stockRepository;

        // Inyectamos el repositorio de stock (puedes usar DI si lo tienes configurado)
        public InventarioService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        /// <summary>
        /// Actualiza el stock al registrar una compra: incrementa la cantidad disponible.
        /// </summary>
        /// <param name="detalleCompra">Detalle de la compra</param>
        public void ActualizarStockCompra(DetalleCompraModel detalleCompra)
        {
            // Obtener el registro de stock para el producto
            StockModel stock = _stockRepository.GetByProductoId(detalleCompra.ProductoId);
            if (stock == null)
            {
                // Si no existe, crear un nuevo registro con la cantidad comprada y el precio de compra
                stock = new StockModel
                {
                    ProductoId = detalleCompra.ProductoId,
                    NombreProducto = "", // Opcional: podrías obtener el nombre a partir del repositorio de productos
                    CantidadDisponible = detalleCompra.Cantidad,
                    PrecioUnitario = detalleCompra.PrecioUnitario, // Costo de compra
                    FechaActualizacion = DateTime.Now
                };
                _stockRepository.Add(stock);
            }
            else
            {
                // Si ya existe, incrementar la cantidad disponible.
                stock.CantidadDisponible += detalleCompra.Cantidad;
                // Opcional: actualizar el precio unitario según alguna política (por ejemplo, costo promedio)
                stock.PrecioUnitario = detalleCompra.PrecioUnitario;
                stock.FechaActualizacion = DateTime.Now;
                _stockRepository.Edit(stock);
            }
        }

     
        /// <param name="detalleVenta">Detalle de la venta</param>
        public void ActualizarStockVenta(DetalleVentaModel detalleVenta)
        {
            // Obtener el registro de stock para el producto
            StockModel stock = _stockRepository.GetByProductoId(detalleVenta.ProductoId);
            if (stock != null)
            {
                // Disminuir la cantidad vendida; controlar que no quede negativo
                stock.CantidadDisponible -= detalleVenta.Cantidad;
                if (stock.CantidadDisponible < 0)
                {
                    stock.CantidadDisponible = 0;
                    // O lanzar una excepción: throw new Exception("Stock insuficiente");
                }

                stock.FechaActualizacion = DateTime.Now;
                _stockRepository.Edit(stock);
            }
            else
            {
                // Opcional: si no existe registro de stock, podrías lanzar un error
                throw new Exception("No existe registro de stock para el producto vendido.");
            }
        }
    }
}
