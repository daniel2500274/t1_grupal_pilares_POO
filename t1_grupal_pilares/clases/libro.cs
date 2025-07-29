namespace DefaultNamespace;
using System;

namespace BibliotecaPOO.Clases
{
    /// <summary>
    /// Clase que representa un libro en la biblioteca
    /// PILAR POO: ENCAPSULAMIENTO - Campos privados con acceso controlado
    /// </summary>
    public class Libro
    {
        // ========================================================
        // ENCAPSULAMIENTO: Campos privados para proteger datos
        // ========================================================
        private static int _contadorId = 1; // Contador estático para IDs únicos
        private int _id;                     // Identificador único del libro
        private string _titulo;              // Título del libro
        private string _autor;               // Autor del libro
        private bool _disponible;            // Estado de disponibilidad
        private string _usuarioActual;       // Usuario que tiene el libro prestado
        
        // ========================================================
        // Constructor: Inicializa el libro con datos válidos
        // ========================================================
        public Libro(string titulo, string autor)
        {
            _id = _contadorId++;  // Asigna ID único y auto-incrementa
            _titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            _autor = autor ?? throw new ArgumentNullException(nameof(autor));
            _disponible = true;   // Por defecto está disponible
            _usuarioActual = null; // Sin usuario asignado inicialmente
        }
        
        // ========================================================
        // ENCAPSULAMIENTO: Propiedades públicas de SOLO LECTURA
        // Permiten acceso controlado sin exponer campos privados
        // ========================================================
        public int Id => _id;
        public string Titulo => _titulo;
        public string Autor => _autor;
        public bool Disponible => _disponible;
        public string UsuarioActual => _usuarioActual; // Solo lectura del usuario actual
        
        // ========================================================
        // ENCAPSULAMIENTO: Métodos INTERNOS para cambiar estado
        // Solo las clases del mismo ensamblado pueden modificar el estado
        // ========================================================
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método interno para prestar el libro
        /// Solo la clase Biblioteca puede ejecutar esta operación
        /// </summary>
        /// <param name="usuario">Nombre del usuario que toma prestado el libro</param>
        internal void Prestar(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                throw new ArgumentException("El nombre del usuario no puede estar vacío");
            
            _disponible = false;
            _usuarioActual = usuario;
        }
        
        /// <summary>
        /// ENCAPSULAMIENTO: Método interno para devolver el libro
        /// Solo la clase Biblioteca puede ejecutar esta operación
        /// </summary>
        internal void Devolver()
        {
            _disponible = true;
            _usuarioActual = null;
        }
        
        // ========================================================
        // Método público para representación en cadena
        // ========================================================
        public override string ToString()
        {
            string estado = _disponible ? "Disponible" : $"Prestado a {_usuarioActual}";
            return $"[{_id}] {_titulo} - {_autor} ({estado})";
        }
        
        // ========================================================
        // ENCAPSULAMIENTO: Método para comparar libros por título
        // Encapsula la lógica de comparación interna
        // ========================================================
        public bool TieneMismoTitulo(string titulo)
        {
            return _titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase);
        }
    }
}