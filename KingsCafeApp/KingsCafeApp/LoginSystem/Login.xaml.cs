using KingsCafeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KingsCafeApp.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private  async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                await DisplayAlert("Error", "Please! Fill the required fields", "OK");
                return;
            }
            //var users = App.db.Table<Users>().ToList();

            try
            {
               
                LoadingInd.IsRunning = true;

                var check = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).FirstOrDefault(x=>x.Object.Email == txtEmail.Text && x.Object.Password == txtpassword.Text );

                if (check == null)
                {
                    LoadingInd.IsRunning = false;
                    await DisplayAlert("Error", "Invalid Email and Password", "OK");
                    return;

                }
                else
                {
                    await Navigation.PushAsync(new UsersList());

                }
            }
            catch (Exception ex)

            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong! please try again later \nError:" + ex.Message, "OK");

            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());  
        }
    }
}