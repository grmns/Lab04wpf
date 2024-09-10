using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=DESKTOP-OT3T8Q7; Database=Neptuno; Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
            CargarProductos();
            CargarCategorias();
        }

        private void CargarProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ListarProductos", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            CantidadPorUnidad = reader["CantidadPorUnidad"].ToString(),
                            PrecioUnidad = Convert.ToDecimal(reader["PrecioUnidad"]),
                            UnidadesEnExistencia = Convert.ToInt32(reader["UnidadesEnExistencia"])
                        };

                        listaProductos.Add(producto);
                    }

                    reader.Close();
                }

                dgvProductos.ItemsSource = listaProductos;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los productos");
            }
        }
        private void CargarCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_ListarCategorias", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria
                        {
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                            NombreCategoria = reader["NombreCategoria"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"]),
                            CodCategoria = reader["CodCategoria"].ToString(),
                        };

                        listaCategorias.Add(categoria);
                    }

                    reader.Close();
                }

                dgvCategorias.ItemsSource = listaCategorias;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las categorías");
            }
        }
    }
}

    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CantidadPorUnidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int UnidadesEnExistencia { get; set; }
    }
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string CodCategoria { get; set; }
    }

