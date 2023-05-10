using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Agenda.Tablas;
using Agenda.Datos;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Registro : ContentPage
    {
        
        private SQLiteAsyncConnection conexion;
        public V_Registro()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            var DatosContacto = new T_Contacto
            {
                Nombre = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                Telefono = txtTelefono.Text,
            };
            conexion.InsertAsync(DatosContacto);
            limpiarformulario();
            volver();
            DisplayAlert("Confirmación", "El contacto se registró correctamente", "OK");  
                
            
        }

        private void volver()
        {
            Navigation.PushAsync(new Vistas.V_Consulta());

        }

        private void limpiarformulario() 
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
        }


    }
}