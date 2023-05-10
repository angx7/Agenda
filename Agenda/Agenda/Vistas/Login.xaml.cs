using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Admin" && txtPassword.Text == "samape")
            {
                Navigation.PushAsync(new Vistas.V_Principal());
            }
            else
            {
                DisplayAlert("Ops...", "El usuario y/o contraseña es incorrecto", "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Vistas.RegisterPage());
        }
    }
}