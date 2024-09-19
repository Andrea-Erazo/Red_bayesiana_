using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        RedBayesiana red = new RedBayesiana();

        // Ejemplo de preferencias del usuario
        List<string> generosPreferidos = new List<string> { "Ficción", "Ciencia Ficción" };

        Console.WriteLine("Recomendación de libros basada en tus géneros preferidos.");
        red.RecomendarLibros(generosPreferidos);
    }
}

public class RedBayesiana
{
    private Dictionary<string, double> probabilidadLibro = new Dictionary<string, double>
    {
        {"Cien años de soledad", 0.1},
        {"1984", 0.05},
        {"Dune", 0.07},
        {"Harry Potter", 0.08},
        {"Sapiens", 0.1},
        {"El Hobbit", 0.04},
        {"Fundación", 0.06},
        {"Orgullo y prejuicio", 0.09},
        {"El diario de una pasión", 0.03}
    };

    private Dictionary<string, Dictionary<string, double>> probabilidadGeneroDadoLibro = new Dictionary<string, Dictionary<string, double>>
    {
        {"Cien años de soledad", new Dictionary<string, double> {{"Ficción", 0.9}, {"No Ficción", 0.1}}},
        {"1984", new Dictionary<string, double> {{"Ficción", 0.95}, {"Ciencia Ficción", 0.9}}},
        {"Dune", new Dictionary<string, double> {{"Ciencia Ficción", 0.95}, {"Fantasía", 0.2}}},
        {"Harry Potter", new Dictionary<string, double> {{"Ficción", 0.85}, {"Fantasía", 0.95}}},
        {"Sapiens", new Dictionary<string, double> {{"No Ficción", 0.9}}},
        {"El Hobbit", new Dictionary<string, double> {{"Fantasía", 0.95}}},
        {"Fundación", new Dictionary<string, double> {{"Ciencia Ficción", 0.9}}},
        {"Orgullo y prejuicio", new Dictionary<string, double> {{"Romance", 0.9}, {"Ficción", 0.7}}},
        {"El diario de una pasión", new Dictionary<string, double> {{"Romance", 0.95}}}
    };

    public void RecomendarLibros(List<string> generosPreferidos)
    {
        Dictionary<string, double> puntuacionesLibros = new Dictionary<string, double>();

        foreach (var libro in probabilidadLibro.Keys)
        {
            double puntuacion = probabilidadLibro[libro];

            foreach (var genero in generosPreferidos)
            {
                if (probabilidadGeneroDadoLibro[libro].ContainsKey(genero))
                {
                    puntuacion *= probabilidadGeneroDadoLibro[libro][genero];
                }
            }

            puntuacionesLibros[libro] = puntuacion;
        }

        // Ordenar libros por puntuación
        foreach (var libro in puntuacionesLibros)
        {
            Console.WriteLine($"Libro: {libro.Key}, Puntuación: {libro.Value:F2}");
        }

        // Filtrar y mostrar las recomendaciones más altas
        var librosRecomendados = puntuacionesLibros.OrderByDescending(x => x.Value).Take(5);
        Console.WriteLine("\nRecomendaciones:");
        foreach (var libro in librosRecomendados)
        {
            Console.WriteLine($"- {libro.Key}");
        }
    }
}
