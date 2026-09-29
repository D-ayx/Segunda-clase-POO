using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Colombia.Ingenieria.Unidad2.Ejercicio1
{
    public class ItemFactura
    {
        public string Descripcion { get; }
        public decimal PrecioUnitarioCOP { get; }
        public int Cantidad { get; }
        public bool AplicaIVA { get; }
        public ItemFactura(string desc, decimal precio, int cant, bool aplicaIva)
        {
            if (precio < 0 || cant <= 0)
                throw new ArgumentException("Precio o cantidad no válidos.");

            Descripcion = desc;
            PrecioUnitarioCOP = precio;
            Cantidad = cant;
            AplicaIVA = aplicaIva;
        }

        public decimal CalcularSubtotalItem()
        {
            return PrecioUnitarioCOP * Cantidad;
        }

        public decimal CalcularIVAItem()
        {
            return AplicaIVA ? CalcularSubtotalItem() * 0.19m : 0.00m;
        }
    }
}
namespace Colombia.Ingenieria.Unidad2.Ejercicio1
{
    public class FacturaElectronicaDIAN
    {
        private readonly List<ItemFactura> _items = new();

        public string NITEmisor { get; }
        public string NITAdquirente { get; }
        public string NumeroFactura { get; }

        public FacturaElectronicaDIAN(
            string nitEmisor,
            string nitAdquirente,
            string numFactura)
        {
            NITEmisor = nitEmisor;
            NITAdquirente = nitAdquirente;
            NumeroFactura = numFactura;
        }

        public void AgregarItem(ItemFactura item)
        {
            _items.Add(item);
        }

        public decimal CalcularSubTotal()
        {
            decimal sub = 0m;

            foreach (var it in _items)
                sub += it.CalcularSubtotalItem();

            return sub;
        }

        public decimal CalcularTotalIVA()
        {
            decimal iva = 0m;

            foreach (var it in _items)
                iva += it.CalcularIVAItem();

            return iva;
        }

        public string GenerarCUFE()
        {
            string rawCadena =
                $"{NumeroFactura}{NITEmisor}{NITAdquirente}{CalcularSubTotal():F2}";

            using var sha256 = SHA256.Create();

            byte[] bytes = sha256.ComputeHash(
                Encoding.UTF8.GetBytes(rawCadena)
            );

            return BitConverter
                .ToString(bytes)
                .Replace("-", "")
                .ToLower();
        }

        public void ImprimirFacturaDIAN()
        {
            decimal sub = CalcularSubTotal();
            decimal iva = CalcularTotalIVA();

            decimal reteFuente = sub * 0.025m;
            decimal totalPagar = sub + iva - reteFuente;

            Console.WriteLine(
                $"===== FACTURA ELECTRONICA DE VENTA DIAN: {NumeroFactura} ====="
            );

            Console.WriteLine($"NIT EMISOR     : {NITEmisor}");
            Console.WriteLine($"NIT ADQUIRENTE : {NITAdquirente}");
            Console.WriteLine($"CUFE           : {GenerarCUFE()}");

            Console.WriteLine("\n===== DETALLE DE PRODUCTOS =====");

            foreach (var item in _items)
            {
                Console.WriteLine(
                    $"{item.Descripcion,-40} " +
                    $"Cantidad: {item.Cantidad,3} " +
                    $"Precio: {item.PrecioUnitarioCOP,12:N2} " +
                    $"Subtotal: {item.CalcularSubtotalItem(),12:N2}"
                );
            }

            Console.WriteLine("\n===== TOTALES =====");
            Console.WriteLine($"SUBTOTAL       : {sub,14:N2} COP");
            Console.WriteLine($"IVA (19%)      : {iva,14:N2} COP");
            Console.WriteLine($"RETEFUENTE 2.5%: -{reteFuente,13:N2} COP");
            Console.WriteLine($"TOTAL A PAGAR  : {totalPagar,14:N2} COP");
        }

        public class program1
        {
            public static void Main()
            {
                var factura = new FacturaElectronicaDIAN(
                    "900123456",
                    "80001234562",
                    "SETP99001024"
                );

                // ÍTEM 1
                factura.AgregarItem(
                    new ItemFactura(
                        "AWS SERVICES",
                        2130000,
                        1,
                        true
                    )
                );

                // ÍTEM 2
                factura.AgregarItem(
                    new ItemFactura(
                        "Consultoria de Datos - DataStore",
                        2121000,
                        1,
                        true
                    )
                );

                // ÍTEM 3
                factura.AgregarItem(
                    new ItemFactura(
                        "Licencia Microsoft 365",
                        450000,
                        2,
                        true
                    )
                );

                // ÍTEM 4
                factura.AgregarItem(
                    new ItemFactura(
                        "Servicio de Soporte Tecnico",
                        180000,
                        3,
                        true
                    )
                );

                // ÍTEM 5
                factura.AgregarItem(
                    new ItemFactura(
                        "Mantenimiento de Computadores",
                        250000,
                        2,
                        true
                    )
                );

                // ÍTEM 6
                factura.AgregarItem(
                    new ItemFactura(
                        "Instalacion de Software",
                        120000,
                        2,
                        true
                    )
                );

                // ÍTEM 7
                factura.AgregarItem(
                    new ItemFactura(
                        "Disco SSD 1TB",
                        320000,
                        3,
                        true
                    )
                );

                // ÍTEM 8
                factura.AgregarItem(
                    new ItemFactura(
                        "Memoria RAM 16GB",
                        280000,
                        2,
                        true
                    )
                );

                // ÍTEM 9
                factura.AgregarItem(
                    new ItemFactura(
                        "Teclado Inalambrico",
                        95000,
                        4,
                        true
                    )
                );

                // ÍTEM 10
                factura.AgregarItem(
                    new ItemFactura(
                        "Mouse Inalambrico",
                        75000,
                        4,
                        true
                    )
                );

                factura.ImprimirFacturaDIAN();
            }
        }
    }
}
