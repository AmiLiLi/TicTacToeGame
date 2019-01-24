using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe.View
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.initForm();

        }

        /// <summary>
        /// Plaies the button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void PlayButton_Clicked(object sender, System.EventArgs e)
        {
            //set the next page(here is GamePage) into the Navigation stack
            Application.Current.MainPage.Navigation.PushAsync(new GamePage());
        }

        /// <summary>
        /// Deletes the button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties[Global.PROPERTY_PLAY_COUNT] = 0;
            Application.Current.Properties[Global.PROPERTY_WIN_COUNT] = 0;
            Application.Current.Properties[Global.PROPERTY_LOST_COUNT] = 0;

            this.initForm();

        }

        /// <summary>
        /// Inits the form.
        /// </summary>
        async void initForm()
        {
            int playCount = (int)Application.Current.Properties[Global.PROPERTY_PLAY_COUNT];
            int winCount = (int)Application.Current.Properties[Global.PROPERTY_WIN_COUNT];
            int lostCount = (int)Application.Current.Properties[Global.PROPERTY_LOST_COUNT];

            double value = (winCount + lostCount) == 0 ? 0.0 : (float)(winCount * 100 / (winCount + lostCount));

            if (playCount <= 0)
            {
                ResultBar.IsVisible = false;
                ResultLabel.IsVisible = false;
            }
            else
            {
                ResultBar.IsVisible = true;
                ResultLabel.IsVisible = true;

                // 1000ms 
                await ResultBar.ProgressTo(value / 100, 1000, Easing.Linear);

                ResultLabel.Text = value.ToString() + "%  matches";
            }

        }
    }
}
