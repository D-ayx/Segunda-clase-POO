using System;

namespace P00_Unidad1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Automovil miAuto = new Automovil();

            miAuto.Marca = "Ford";
            miAuto.Modelo = "Mustang";
            miAuto.Anio = 2025;
            miAuto.VelocidadActual = 50;

            miAuto.Frenar(12);

            miAuto.MostrarInformacion();
        }
    }
}
    public class Automovil
    {
        // Atributos
        public string Marca;
        public string Modelo;
        public int Anio;
        public double VelocidadActual;

        // Métodos
        public void Acelerar(double inc)
        {
            VelocidadActual += inc;
        }

        public void Frenar(double dec)
        {
            if (VelocidadActual - dec >= 0)
                VelocidadActual -= dec;
            else
                VelocidadActual = 0;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("La Marca del auto es: " + Marca);
            Console.WriteLine("El Modelo es: " + Modelo);
            Console.WriteLine("El Anio es: " + Anio);
            Console.WriteLine("La Velocidad actual: " + VelocidadActual + " km/h");
        }
    }
