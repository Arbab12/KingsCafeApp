using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using KingsCafeApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KingsCafeApp.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtphone.Text) || string.IsNullOrEmpty(txtemail.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    await DisplayAlert("Error", "Please! Fill the required fields", "OK");
                    return;
                }

                if (txtCPassword.Text != txtPassword.Text)
                {
                    await DisplayAlert("Error", "Password do no match", "OK");
                    return;
                }

                LoadingInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("Users").OnceAsync<Users> ()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Users").OnceAsync<Users> ()).Max(a => a.Object.UserId);
                    NewID = ++LastID;
                }

                Users u = new Users()
                {
                    UserId = NewID,
                    Name = txtname.Text,
                    Email = txtemail.Text,
                    Password = txtPassword.Text,
                    Phone = txtphone.Text,
                };
                await App.firebaseDatabase.Child("Users").PostAsync(u);

                LoadingInd.IsRunning = false;

               
                await DisplayAlert("Success", "Your Account is Registered", "OK");
                await Navigation.PushAsync(new Login());
            }
            catch (Exception ex)

            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong! please try again later \nError:" + ex.Message, "OK");

            }
        }
    }
}