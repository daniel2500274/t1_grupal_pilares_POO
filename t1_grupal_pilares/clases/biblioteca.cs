using System;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace;

namespace BibliotecaPOO
{
    // =====================================================================
    // EJERCICIO 1: GESTIÓN DE BIBLIOTECA
    // =====================================================================
    
    /// <summary>
    /// Clase que representa un libro en la biblioteca
    /// ENCAPSULAMIENTO: Campos privados con acceso controlado
    /// </summary>
    public class Libro
    {
        // ENCAPSULAMIENTO: Campos privados para proteger la información interna
        private static int _contadorId = 1;
        private int _id;
        private string _titulo;
        private string _autor;
        private bool _disponible;
        private string _usuarioActual; // Usuario que tiene el libro prestado
        
        // Constructor
        public Libro(string titulo, string autor)
        {
            _id = _contadorId++;
            _titulo = titulo;
            _autor = autor;
            _disponible = true;
            _usuarioActual = null;
        }
        
        // ENCAPSULAMIENTO: Propiedades públicas para acceso controlado
        public int Id => _id; // Solo lectura
        public string Titulo => _titulo; // Solo lectura
        public string Autor => _autor; // Solo lectura
        public bool Disponible => _disponible; // Solo lectura
        
        // ENCAPSULAMIENTO: Métodos internos para cambiar el estado
        internal void Prestar(string usuario)
        {
            _disponible = false;
            _usuarioActual = usuario;
        }
        
        internal void Devolver()
        {
            _disponible = true;
            _usuarioActual = null;
        }
        
        public override string ToString()
        {
            return $"[{_id}] {_titulo} - {_autor} ({(_disponible ? "Disponible" : "Prestado")})";
        }
    }
    
    /// <summary>
    /// Clase que administra la biblioteca y los préstamos
    /// ENCAPSULAMIENTO: Control total sobre la colección de libros
    /// </summary>
    public class Biblioteca
    {
        // ENCAPSULAMIENTO: Colecciones privadas para proteger los datos internos
        private List<Libro> _libros;
        private Dictionary<int, string> _prestamos; // ID del libro -> Usuario
        
        public Biblioteca()
        {
            _libros = new List<Libro>();
            _prestamos = new Dictionary<int, string>();
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método público para registrar libros con validación interna
        /// </summary>
        public bool RegistrarLibro(string titulo, string autor)
        {
            // Validación interna: no duplicar títulos
            if (_libros.Any(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Error: El libro '{titulo}' ya está registrado en la biblioteca.");
                return false;
            }
            
            var nuevoLibro = new Libro(titulo, autor);
            _libros.Add(nuevoLibro);
            Console.WriteLine($"Libro registrado exitosamente: {nuevoLibro}");
            return true;
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método público para prestar libros con validaciones internas
        /// </summary>
        public bool PrestarLibro(int idLibro, string nombreUsuario)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == idLibro);
            
            if (libro == null)
            {
                Console.WriteLine($"Error: No se encontró el libro con ID {idLibro}.");
                return false;
            }
            
            if (!libro.Disponible)
            {
                Console.WriteLine($"Error: El libro '{libro.Titulo}' no está disponible.");
                return false;
            }
            
            // ENCAPSULAMIENTO: Modificación interna del estado del libro
            libro.Prestar(nombreUsuario);
            _prestamos[idLibro] = nombreUsuario;
            
            Console.WriteLine($"Libro '{libro.Titulo}' prestado exitosamente a {nombreUsuario}.");
            return true;
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método público para devolver libros
        /// </summary>
        public bool DevolverLibro(int idLibro)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == idLibro);
            
            if (libro == null)
            {
                Console.WriteLine($"Error: No se encontró el libro con ID {idLibro}.");
                return false;
            }
            
            if (libro.Disponible)
            {
                Console.WriteLine($"Error: El libro '{libro.Titulo}' no está prestado.");
                return false;
            }
            
            // ENCAPSULAMIENTO: Modificación interna del estado
            libro.Devolver();
            _prestamos.Remove(idLibro);
            
            Console.WriteLine($"Libro '{libro.Titulo}' devuelto exitosamente.");
            return true;
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método público que expone información sin dar acceso directo a las estructuras
        /// </summary>
        public void MostrarLibrosDisponibles()
        {
            var librosDisponibles = _libros.Where(l => l.Disponible).ToList();
            
            Console.WriteLine("\n=== LIBROS DISPONIBLES ===");
            if (librosDisponibles.Any())
            {
                foreach (var libro in librosDisponibles)
                {
                    Console.WriteLine(libro);
                }
            }
            else
            {
                Console.WriteLine("No hay libros disponibles.");
            }
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método público para mostrar préstamos activos sin exponer estructuras internas
        /// </summary>
        public void MostrarPrestamosActivos()
        {
            Console.WriteLine("\n=== PRÉSTAMOS ACTIVOS ===");
            if (_prestamos.Any())
            {
                foreach (var prestamo in _prestamos)
                {
                    var libro = _libros.First(l => l.Id == prestamo.Key);
                    Console.WriteLine($"{libro.Titulo} - Prestado a: {prestamo.Value}");
                }
            }
            else
            {
                Console.WriteLine("No hay préstamos activos.");
            }
        }
    }