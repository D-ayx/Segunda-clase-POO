// Una empresa de desarrollo de software gestiona dos tipos de empleados.
// Con distintas estructuras salariales:
// Dev: Salario base + bono variable por cada proyecto completado.
// Gerentes: Salario base + bono fijo por gestion directiva.
// Objetivo: Calcular el salario total y mostrar la información consolidada utilizando Herencia y polimorfismo.
using System;
using System.Collections.Generic;

public class Empleados
{
    private string _nombre;
    private int edad;
    protected decimal salarioBase;

    public Empleados(string nombre, int edad, decimal salarioBase)
    {
        Nombre = nombre;
        Edad = edad;
        this.salarioBase = salarioBase;
    }

    public string Nombre
    {
        get { return _nombre; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no debe estar vacío.");

            _nombre = value;
        }
    }

    public int Edad
    {
        get { return edad; }
        set
        {
            if (value < 18 || value > 65)
                throw new ArgumentException("¡Edad no válida para trabajar!");

            edad = value;
        }
    }

    public virtual decimal CalcularSalario()
    {
        return salarioBase;
    }

    public virtual string MostrarInf()
    {
        return $"Nombre: {Nombre}, Edad: {Edad}, Salario total: {CalcularSalario():C}";
    }

    public class Gerente : Empleados
    {
        private const decimal BonoGestion = 800000;

        public Gerente(string nombre, int edad, decimal salarioBase)
            : base(nombre, edad, salarioBase)
        {
        }

        public override decimal CalcularSalario()
        {
            return salarioBase + BonoGestion;
        }

        public override string MostrarInf()
        {
            return base.MostrarInf() + " | Rol: Gerente";
        }
    }

    public static void Main(string[] args)
    {
        List<Empleados> empleados = new List<Empleados>
        {
            new Gerente("Dayanna Michelle", 19, 4000000)
        };

        foreach (Empleados e in empleados)
        {
            Console.WriteLine(e.MostrarInf());
        }
    }
}
