using Firebase.Database.Query;
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
    public partial class UsersList : ContentPage
    {
        public UsersList()
        {
            InitializeComponent();
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning= true;
                LoadData();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;

                await DisplayAlert("Error", "Something went wrong! please try again later \nError:" + ex.Message, "OK");

            }
           
        }
        async void LoadData()
        {

            DataList.ItemsSource = (await App.firebaseDatabase.Child("Users").OnceAsync<Users>()).Select(x => new Users

            {
                UserId = x.Object.UserId,
                Name = x.Object.Name,
                Email = x.Object.Email,
                Password = x.Object.Password,
                Phone = x.Object.Phone,
            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Users)e.Item;

            var item = (await App.firebaseDatabase.Child("Users").OnceAsync<Users> ()).FirstOrDefault(a => a.Object.UserId == selected.UserId);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit");
            
            
            if (choice == "Delete")
            {
                bool q = await DisplayAlert("Confrimation", "Are you sure to want to delete" +item.Object.Name, "Yes", "No");
                
                if (q)
                {
                    await App.firebaseDatabase.Child("Users").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confrimation",item.Object.Name+ " Are Successfully Deleted","Ok");
                }
            
            }
            if(choice == "View")
            {
                await DisplayAlert("Details",""+
                    "\nUser Id: "+item.Object.UserId +
                    "\nName: "+ item.Object.Name +
                    "\nPhone: "+ item.Object.Phone +
                    "\nEmail: "+ item.Object.Email +
                    "\nPassword: "+ "*******",
                    
                    
                    "","Ok");
            }
        }
    }
}