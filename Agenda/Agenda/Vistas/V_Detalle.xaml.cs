using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Agenda.Tablas;
using SQLite;
using Agenda.Datos;
using System.IO;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Detalle : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, ApSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<T_Contacto> ResultadoDelete;
        IEnumerable<T_Contacto> ResultadoUpdate;
        public V_Detalle(int id, string nom, string ap, string tel)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            NomSeleccionado = nom;
            ApSeleccionado = ap;
            TelSeleccionado = tel;
            btn_actualizar.Clicked += Btn_actualizar_Clicked;
            btn_eliminar.Clicked += Btn_eliminar_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblMensaje.Text = "ID :" + IdSeleccionado;
            txtNombre.Text = NomSeleccionado;
            txtApellidos.Text = ApSeleccionado;
            txtTelefono.Text = TelSeleccionado;
        }

        private async void Btn_eliminar_Clicked(object sender, EventArgs e)
        {
            
            bool res = await DisplayAlert("Confirmación", "Estas seguro que deseas eliminar el contacto", "Aceptar", "Cancelar");
            if (res == true)
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
                var db = new SQLiteConnection(rutaDB);
                ResultadoDelete = Delete(db, IdSeleccionado);
                Limpiar();
                await Navigation.PushAsync(new Vistas.V_Consulta());
                await DisplayAlert("Confirmacion", "El contacto se eliminó correctamente", "OK").ConfigureAwait(false);



            }
            //await Navigation.PushAsync(new Vistas.V_Consulta());

        }


        private async void Btn_actualizar_Clicked(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Confirmación", "Estas seguro que deseas actualizar el contacto", "Aceptar", "Cancelar");
            if (res == true)
            {
                if (txtNombre.Equals(""))
                {
                    await DisplayAlert("Ops", "El nombre del contacto no puede estar en blanco", "OK").ConfigureAwait(false);
                }
                else
                {
                    if (txtApellidos.Equals(""))
                    {
                        await DisplayAlert("Ops", "El campo de apellidos del contacto no puede estar en blanco", "OK").ConfigureAwait(false);
                    }
                    else
                    {
                        if (txtTelefono.Equals(""))
                        {
                            await DisplayAlert("Ops", "El contacto debe de tener un número de télefono", "OK").ConfigureAwait(false);
                        }
                        else
                        {
                            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgendaSQLite.db3");
                            var db = new SQLiteConnection(rutaDB);
                            ResultadoUpdate = Update(db, txtNombre.Text, txtApellidos.Text, txtTelefono.Text, IdSeleccionado);
                            await DisplayAlert("Confirmacion", "El contacto se actualizó correctamente", "OK").ConfigureAwait(false);
                        }
                    }
                }
            }            
        }

        public static IEnumerable<T_Contacto> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Contacto>("DELETE FROM T_CONTACTO WHERE Id = ?", id);
        }

        public static IEnumerable<T_Contacto> Update(SQLiteConnection db, string nombre, string apellido, string telefono, int id)
        {
            return db.Query<T_Contacto>("UPDATE T_Contacto SET Nombre = ?, Apellidos = ?, Telefono = ? WHERE Id =?", nombre, apellido, telefono, id);
        }

        public void Limpiar()
        {
            lblMensaje.Text = "";
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
        }
    }
}