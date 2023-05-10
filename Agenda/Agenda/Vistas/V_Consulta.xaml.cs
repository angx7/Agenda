using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Agenda.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using Agenda.Datos;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Consulta : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Contacto> TablaContacto;
        public V_Consulta()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaContactos.ItemSelected += ListaContactos_ItemSelected;
        }

        private void ListaContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Contacto)e.SelectedItem;
            var item = Obj.Id.ToString();
            var tel = Obj.Telefono;
            var nom = Obj.Nombre;
            var ap = Obj.Apellidos;
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new V_Detalle(ID, nom, ap, tel));
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResulResgistros = await conexion.Table<T_Contacto>().ToListAsync();
            TablaContacto= new ObservableCollection<T_Contacto>(ResulResgistros);
            ListaContactos.ItemsSource = TablaContacto;
            base.OnAppearing();
        }
    }
}