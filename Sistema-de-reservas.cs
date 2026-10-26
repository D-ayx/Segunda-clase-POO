using System;
using System.Collections.Generic;
public class HabitacionHotel
{
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;
    public int Numero => numero; 
    public string Tipo
    {
        get => tipo;
        set
        {
            if (value == "Individual" || value == "Doble" || value == "Suite")
            {
                tipo = value;
            }
            else
            {
                throw new ArgumentException("El tipo debe ser 'Individual', 'Doble' o 'Suite'.");
            }
        }
    }
    public double PrecioPorNoche
    {
        get => precioPorNoche;
        set
        {
            if (value > 0)
            {
                precioPorNoche = value;
            }
            else
            {
                throw new ArgumentException("El precio por noche debe ser mayor a 0.");
            }
        }
    }
    public bool Disponible => disponible; 
    public string ClienteActual => clienteActual; 
    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        this.numero = numero;
        Tipo = tipo; 
        PrecioPorNoche = precioPorNoche;
        this.disponible = true;
        this.clienteActual = "";
    }
    public double Reservar(string nombreCliente, int noches)
    {
        if (disponible)
        {
            disponible = false;
            clienteActual = nombreCliente;
            return precioPorNoche * noches;
        }
        return -1; 
    }
    public bool Liberar()
    {
        if (!disponible)
        {
            disponible = true;
            clienteActual = "";
            return true;
        }
        return false; 
    }
    public string MostrarInfo()
    {
        string estado = disponible ? "Disponible" : $"Ocupada por {clienteActual}";
        return $"Habitacion: {numero} | Tipo: {tipo} | Precio por noche: ${precioPorNoche:N0} | Estado: {estado}";
    }
    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje > 0 && porcentaje <= 100)
        {
            double descuento = precioPorNoche * (porcentaje / 100.0);
            precioPorNoche -= descuento;
            if (precioPorNoche < 0) precioPorNoche = 0;
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        HabitacionHotel h101 = new HabitacionHotel(101, "Individual", 50000);
        HabitacionHotel h202 = new HabitacionHotel(202, "Doble", 80000);
        HabitacionHotel h303 = new HabitacionHotel(303, "Suite", 150000);
        
        List<HabitacionHotel> listaHabitaciones = new List<HabitacionHotel> { h101, h202, h303 };

        Console.WriteLine("=== ESTADO INICIAL DE LAS HABITACIONES ===");
        foreach (var h in listaHabitaciones)
        {
            Console.WriteLine(h.MostrarInfo());
        }
        Console.WriteLine();

        Console.WriteLine("=== SIMULACION DE LAS ACCIONES ===");

        double costo101 = h101.Reservar("Dayanna Polo", 3);
        Console.WriteLine($"Reservar Habitacion 101 para Dayanna Polo (3 noches). Costo total: ${costo101:N0}");

        double costo202 = h202.Reservar("Camilo Angarita", 2);
        Console.WriteLine($"Reservar Habitacion 202 para Camilo Angarita (2 noches). Costo total: ${costo202:N0}");

        double intentoFallido = h101.Reservar("Otro Cliente", 1);
        Console.WriteLine($"Intentar reservar la 101 nuevamente. Resultado: {intentoFallido}");

        bool liberada = h101.Liberar();
        Console.WriteLine($"Liberar Habitacion 101. Exitoso?: {liberada}");

        h303.AplicarDescuento(10);
        Console.WriteLine("Aplicar descuento del 10% a la Habitacion 303.");

        double costoCarlos = h101.Reservar("Carlos Ruiz", 1);
        Console.WriteLine($"Reservar Habitacion 101 para Carlos Ruiz (1 noche). Costo total: ${costoCarlos:N0}");
        Console.WriteLine();

        Console.WriteLine("=== ESTADO FINAL DE LAS HABITACIONES ===");
        foreach (var h in listaHabitaciones)
        {
            Console.WriteLine(h.MostrarInfo());
        }
        Console.WriteLine();
        int contadorOcupadas = 0;
        foreach (var h in listaHabitaciones)
        {
            if (!h.Disponible)
            {
                contadorOcupadas++;
            }
        }
        Console.WriteLine("=== RESUMEN FINAL DE LAS HABITACIONES ===");
        Console.WriteLine($"Cantidad de habitaciones actualmente ocupadas: {contadorOcupadas}");
    }
}
