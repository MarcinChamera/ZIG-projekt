using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using ZIG_projekt_backend;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The destination service
        /// </summary>
        private PlaceService PlaceService;
        /// <summary>
        /// The destinations
        /// </summary>
        public ICollection<Place> Places;
        /// <summary>
        /// The selected destination
        /// </summary>
        private Place? SelectedPlace;

        private BirthService BirthsBookService;

        public ICollection<Birth> BirthsBook;

        private Birth? SelectedBirth;

        private WeddingService WeddingsBookService;

        public ICollection<Wedding> WeddingsBook;

        private Wedding? SelectedWedding;

        private DeathService DeathsBookService;

        public ICollection<Death> DeathsBook;

        private Death? SelectedDeath;

        public MainWindow()
        {
            InitializeComponent();

            this.PlaceService = new PlaceService();
            this.BirthsBookService = new BirthService();
            this.WeddingsBookService = new WeddingService();
            this.DeathsBookService = new DeathService();

            this.UpdatePlaces();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the DestinationsList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void PlacesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.RemovePlaceButton.Visibility = Visibility.Visible;
                this.AddBirthButton.Visibility = Visibility.Visible;
                this.AddWeddingButton.Visibility = Visibility.Visible;
                this.AddDeathButton.Visibility = Visibility.Visible;

                this.SelectedPlace = e.AddedItems[0] as Place;
            }
            else
            {
                this.RemovePlaceButton.Visibility = Visibility.Collapsed;
                this.AddBirthButton.Visibility= Visibility.Collapsed;
                this.AddWeddingButton.Visibility= Visibility.Collapsed;
                this.AddDeathButton.Visibility= Visibility.Collapsed;

                this.SelectedPlace = null;
            }
        }

        private void BirthsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.RemoveBirthButton.Visibility = Visibility.Visible;

                this.SelectedBirth = e.AddedItems[0] as Birth;
            }
            else
            {
                this.RemoveBirthButton.Visibility = Visibility.Collapsed;

                this.SelectedBirth = null;
            }
        }

        private void WeddingsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.RemoveWeddingButton.Visibility = Visibility.Visible;

                this.SelectedWedding = e.AddedItems[0] as Wedding;
            }
            else
            {
                this.RemoveWeddingButton.Visibility = Visibility.Collapsed;

                this.SelectedWedding = null;
            }
        }

        private void DeathsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.RemoveDeathButton.Visibility = Visibility.Visible;

                this.SelectedDeath = e.AddedItems[0] as Death;
            }
            else
            {
                this.RemoveDeathButton.Visibility = Visibility.Collapsed;

                this.SelectedDeath = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the AddDestinationConfirmButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddPlaceConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PlaceNameBox.Text.Length > 0 && this.PlaceDescriptionBox.Text.Length > 0)
            {
                bool added = this.PlaceService.AddNewPlace(this.PlaceNameBox.Text, this.PlaceDescriptionBox.Text);
                if (added)
                {
                    this.PlaceService.AddPlace(this.PlaceNameBox.Text);
                    this.PlaceNameBox.Text = "";
                    this.PlaceDescriptionBox.Text = "";
                    this.UpdatePlaces();
                }

            }
        }

        /// <summary>
        /// Updates the destinations.
        /// </summary>
        private void UpdatePlaces()
        {
            this.Places = this.PlaceService.GetAllPlaces();
            this.PlacesList.ItemsSource = this.Places;
        }

        private void UpdateBirthsBook()
        {
            if (this.SelectedPlace != null)
            {
                this.BirthsBook = this.BirthsBookService.GetBirthsBookForPlace(this.SelectedPlace.PlaceId);
                this.BirthsList.ItemsSource = this.BirthsBook;
            }
        }

        private void AddBirthButton_Click(object sender, RoutedEventArgs e)
        {
            this.SwitchToBirthsBookPanel();
            this.UpdateBirthsBook();
        }

        private void SwitchToBirthsBookPanel()
        {
            this.PlacesPanel.Visibility = Visibility.Collapsed;
            this.AddNewPlacePanel.Visibility = Visibility.Collapsed;
            this.PlacesButtonsGrid.Visibility = Visibility.Collapsed;

            this.BirthsBookPanel.Visibility = Visibility.Visible;
            this.AddNewBirthPanel.Visibility = Visibility.Visible;
            this.BirthsBookButtonsGrid.Visibility = Visibility.Visible;
        }

        private void UpdateWeddingsBook()
        {
            if (this.SelectedPlace != null)
            {
                this.WeddingsBook = this.WeddingsBookService.GetWeddingsBookForPlace(this.SelectedPlace.PlaceId);
                this.WeddingsList.ItemsSource = this.WeddingsBook;
            }
        }

        private void AddWeddingButton_Click(object sender, RoutedEventArgs e)
        {
            this.SwitchToWeddingsBookPanel();
            this.UpdateWeddingsBook();
        }

        private void SwitchToWeddingsBookPanel()
        {
            this.PlacesPanel.Visibility = Visibility.Collapsed;
            this.AddNewPlacePanel.Visibility = Visibility.Collapsed;
            this.PlacesButtonsGrid.Visibility = Visibility.Collapsed;

            this.WeddingsBookPanel.Visibility = Visibility.Visible;
            this.AddNewWeddingGrid.Visibility = Visibility.Visible;
            this.WeddingsBookButtonsGrid.Visibility = Visibility.Visible;
        }

        private void UpdateDeathsBook()
        {
            if (this.SelectedPlace != null)
            {
                this.DeathsBook = this.DeathsBookService.GetDeathsBookForPlace(this.SelectedPlace.PlaceId);
                this.DeathsList.ItemsSource = this.DeathsBook;
            }
        }

        private void AddDeathButton_Click(object sender, RoutedEventArgs e)
        {
            this.SwitchToDeathsBookPanel();
            this.UpdateDeathsBook();
        }

        private void SwitchToDeathsBookPanel()
        {
            this.PlacesPanel.Visibility = Visibility.Collapsed;
            this.AddNewPlacePanel.Visibility = Visibility.Collapsed;
            this.PlacesButtonsGrid.Visibility = Visibility.Collapsed;

            this.DeathsBookPanel.Visibility = Visibility.Visible;
            this.AddNewDeathPanel.Visibility = Visibility.Visible;
            this.DeathsBookButtonsGrid.Visibility = Visibility.Visible;
        }
        /////////////////////////////////////////////
        /// <summary>
        /// Switches to destination panel.
        /// </summary>
        private void SwitchToPlacePanel()
        {
            this.BirthsBookPanel.Visibility = Visibility.Collapsed;
            this.AddNewBirthPanel.Visibility = Visibility.Collapsed;
            this.BirthsBookButtonsGrid.Visibility = Visibility.Collapsed;

            this.WeddingsBookPanel.Visibility = Visibility.Collapsed;
            this.AddNewWeddingGrid.Visibility = Visibility.Collapsed;
            this.WeddingsBookButtonsGrid.Visibility = Visibility.Collapsed;

            this.DeathsBookPanel.Visibility = Visibility.Collapsed;
            this.AddNewDeathPanel.Visibility = Visibility.Collapsed;
            this.DeathsBookButtonsGrid.Visibility = Visibility.Collapsed;

            this.PlacesPanel.Visibility = Visibility.Visible;
            this.AddNewPlacePanel.Visibility = Visibility.Visible;
            this.PlacesButtonsGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Click event of the RemoveDestinationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RemovePlaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedPlace != null)
            {
                var result = this.PlaceService.RemovePlace(PlacesList.Items.IndexOf(this.SelectedPlace).ToString());

                if (result)
                {
                    this.UpdateBirthsBook();
                    this.UpdateWeddingsBook();
                    this.UpdateDeathsBook();
                    this.UpdatePlaces();
                }
            }
        }

        private void RemoveBirthButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedBirth != null)
            {
                var result = this.BirthsBookService.RemoveBirth(BirthsList.Items.IndexOf(this.SelectedBirth).ToString());

                if (result)
                {
                    this.UpdateBirthsBook();
                }
            }
        }

        private void RemoveWeddingButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedWedding != null)
            {
                var result = this.WeddingsBookService.RemoveWedding(WeddingsList.Items.IndexOf(this.SelectedWedding).ToString());

                if (result)
                {
                    this.UpdateWeddingsBook();
                }
            }
        }

        private void RemoveDeathButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedDeath != null)
            {
                var result = this.DeathsBookService.RemoveDeath(DeathsList.Items.IndexOf(this.SelectedDeath).ToString());

                if (result)
                {
                    this.UpdateDeathsBook();
                }
            }
        }

        private void AddBirthConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.BirthDateBox.Text.Length > 0 && this.BirthFirstNameBox.Text.Length > 0 && this.BirthLastNameBox.Text.Length > 0 && this.BirthMothersNameBox.Text.Length > 0 && this.BirthFathersNameBox.Text.Length > 0)
            {
                //bool added = this.BirthsBookService.AddNewBirth(this.BirthDateBox.Text, this.BirthFirstNameBox.Text, this.BirthLastNameBox.Text, this.BirthMothersNameBox.Text, this.BirthFathersNameBox.Text, this.BirthCommentBox.Text);
                //if (added)
                //{
                //    this.BirthDateBox.Text = "";
                //    this.BirthFirstNameBox.Text = "";
                //    this.BirthLastNameBox.Text = "";
                //    this.BirthMothersNameBox.Text = "";
                //    this.BirthFathersNameBox.Text = "";
                //    this.BirthCommentBox.Text = "";
                //    this.UpdatePlaces();
                //}
            }
        }

        private void AddWeddingConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WeddingDateBox.Text.Length > 0 && this.WeddingBridesFirstNameBox.Text.Length > 0 && this.WeddingBridesLastNameBox.Text.Length > 0 && this.WeddingBridesMothersFirstNameBox.Text.Length > 0 &&
                this.WeddingBridesMothersLastNameBox.Text.Length > 0 && this.WeddingBridesFathersFirstNameBox.Text.Length > 0 && this.WeddingBridesFathersLastNameBox.Text.Length > 0 &&
                this.WeddingGroomsFirstNameBox.Text.Length > 0 && this.WeddingGroomsLastNameBox.Text.Length > 0 && this.WeddingGroomsMothersFirstNameBox.Text.Length > 0 &&
                this.WeddingGroomsMothersLastNameBox.Text.Length > 0 && this.WeddingGroomsFathersFirstNameBox.Text.Length > 0 && this.WeddingGroomsFathersLastNameBox.Text.Length > 0)

            {
                //bool added = this.WeddingsBookService.AddNewWedding(
                //    this.WeddingDateBox.Text,
                //    this.WeddingBridesFirstNameBox.Text,
                //    this.WeddingBridesLastNameBox.Text,
                //    this.WeddingBridesMothersFirstNameBox.Text,
                //    this.WeddingBridesMothersLastNameBox.Text,
                //    this.WeddingBridesFathersFirstNameBox.Text,
                //    this.WeddingBridesFathersLastNameBox.Text,
                //    this.WeddingGroomsFirstNameBox.Text,
                //    this.WeddingGroomsLastNameBox.Text,
                //    this.WeddingGroomsMothersFirstNameBox.Text,
                //    this.WeddingGroomsMothersLastNameBox.Text,
                //    this.WeddingGroomsFathersFirstNameBox.Text,
                //    this.WeddingGroomsFathersLastNameBox.Text,
                //    this.WeddingCommentBox.Text);
                //if (added)
                //{
                //    this.WeddingDateBox.Text = "";
                //    this.WeddingBridesFirstNameBox.Text = "";
                //    this.WeddingBridesLastNameBox.Text = "";
                //    this.WeddingBridesMothersFirstNameBox.Text = "";
                //    this.WeddingBridesMothersLastNameBox.Text = "";
                //    this.WeddingBridesFathersFirstNameBox.Text = "";
                //    this.WeddingBridesFathersLastNameBox.Text = "";
                //    this.WeddingGroomsFirstNameBox.Text = "";
                //    this.WeddingGroomsLastNameBox.Text = "";
                //    this.WeddingGroomsMothersFirstNameBox.Text = "";
                //    this.WeddingGroomsMothersLastNameBox.Text = "";
                //    this.WeddingGroomsFathersFirstNameBox.Text = "";
                //    this.WeddingGroomsFathersLastNameBox.Text = "";
                //    this.WeddingCommentBox.Text = "";
                //    this.UpdatePlaces();
                //}
            }
        }

        private void AddDeathConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DeathDateBox.Text.Length > 0 && this.DeathFirstNameBox.Text.Length > 0 && this.DeathLastNameBox.Text.Length > 0)
            {
                //bool added = this.BirthsBookService.AddNewBirth(this.BirthDateBox.Text, this.BirthFirstNameBox.Text, this.BirthLastNameBox.Text, this.BirthMothersNameBox.Text, this.BirthFathersNameBox.Text, this.BirthCommentBox.Text);
                //if (added)
                //{
                //    this.BirthDateBox.Text = "";
                //    this.BirthFirstNameBox.Text = "";
                //    this.BirthLastNameBox.Text = "";
                //    this.BirthMothersNameBox.Text = "";
                //    this.BirthFathersNameBox.Text = "";
                //    this.BirthCommentBox.Text = "";
                //    this.UpdatePlaces();
                //}
            }
        }

        public void ExportPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            this.PlaceService.ExportPlaces();
        }

        public void ExportBirthsBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.BirthsBookService.ExportBirthsBook(PlacesList.Items.IndexOf(this.SelectedPlace).ToString());
        }

        public void ExportWeddingsBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeddingsBookService.ExportWeddingsBook(PlacesList.Items.IndexOf(this.SelectedPlace).ToString());
        }

        public void ExportDeathsBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.DeathsBookService.ExportDeathsBook(PlacesList.Items.IndexOf(this.SelectedPlace).ToString());
        }

        /// <summary>
        /// Handles the Click event of the BackToDestinationsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BackToPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            this.SwitchToPlacePanel();
        }
    }
}
