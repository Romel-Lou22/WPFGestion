using FontAwesome.Sharp;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SistemaGestion.Services
{
    public class NotificationService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteRepository;

        public NotificationService()
        {
            _ventaRepository = new VentaRepository();
            _clienteRepository = new ClienteRepository();
        }

        public ObservableCollection<Notificacion> GetNotificationsForToday()
        {
            var notifications = new ObservableCollection<Notificacion>();
            DateTime hoy = DateTime.Today;

            // Ordena las ventas del día por fecha descendente
            var ventasHoy = _ventaRepository.GetReportes(hoy, hoy)
                                            .OrderByDescending(v => v.FechaVenta)
                                            .ToList();
            if (ventasHoy.Any())
            {
                var ultimaVenta = ventasHoy.First();
                string nombreCliente = "Consumidor Final";
                if (ultimaVenta.ClienteId.HasValue)
                {
                    var cliente = _clienteRepository.GetById(ultimaVenta.ClienteId.Value);
                    if (cliente != null)
                        nombreCliente = cliente.Nombre;
                }

                // Notificación 1: Nombre del cliente
                notifications.Add(new Notificacion
                {
                    Mensaje = $"Nueva venta a: {nombreCliente}",
                    Icono = "UserTie",
                    IconoColor = "#FF5722"
                });

                // Notificación 2: Monto de la venta
                notifications.Add(new Notificacion
                {
                    Mensaje = $"Monto de la venta: {ultimaVenta.Total:C}",
                    Icono = "CheckCircle",
                    IconoColor = "#4CAF50"
                });
            }
            else
            {
                // Si no hay ventas hoy, agregamos una notificación básica
                notifications.Add(new Notificacion
                {
                    Mensaje = "No hay ventas registradas hoy.",
                    Icono = "Info",
                    IconoColor = "#2196F3"
                });
            }

            return notifications;
        }

    }
}
