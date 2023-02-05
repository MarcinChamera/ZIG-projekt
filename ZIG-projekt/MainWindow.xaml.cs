using Microsoft.Win32;
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
    public partial class MainWindow : Window
    {
        private PlaceService PlaceService;

        public ICollection<Place> Places;

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

        private void AddPlaceConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PlaceNameBox.Text.Length > 0)
            {
                bool added = this.PlaceService.AddPlace(new string[] { this.PlaceNameBox.Text, this.PlaceNoteBox.Text });
                if (added)
                {
                    this.PlaceNameBox.Text = "";
                    this.PlaceNoteBox.Text = "";
                    this.UpdatePlaces();
                }

            }
        }

        private void UpdatePlaces()
        {
            this.Places = this.PlaceService.GetAllPlaces();
            this.PlacesList.ItemsSource = this.Places;
        }

        private void UpdateBirthsBook()
        {
            if (this.SelectedPlace != null)
            {
                this.BirthsBook = this.BirthsBookService.GetBirthsBookForPlace(this.SelectedPlace.Name);
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
                this.WeddingsBook = this.WeddingsBookService.GetWeddingsBookForPlace(this.SelectedPlace.Name);
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
                this.DeathsBook = this.DeathsBookService.GetDeathsBookForPlace(this.SelectedPlace.Name);
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

        private void RemovePlaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedPlace != null)
            {
                var result = this.PlaceService.RemovePlace(this.SelectedPlace.Name);

                if (result)
                {
                    this.UpdatePlaces();
                }
            }
        }

        private void RemoveBirthButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedBirth != null)
            {
                var result = this.BirthsBookService.RemoveBirth(PlacesList.Items.IndexOf(this.SelectedPlace), this.SelectedPlace.Name);

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
                var result = this.WeddingsBookService.RemoveWedding(PlacesList.Items.IndexOf(this.SelectedPlace), this.SelectedPlace.Name);

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
                var result = this.DeathsBookService.RemoveDeath(PlacesList.Items.IndexOf(this.SelectedPlace), this.SelectedPlace.Name);

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
                bool added = this.BirthsBookService.AddBirth(new string[] { this.BirthDateBox.Text, this.BirthTimeBox.Text, this.BirthFirstNameBox.Text, this.BirthLastNameBox.Text, this.BirthMothersNameBox.Text, this.BirthFathersNameBox.Text,
                    string.IsNullOrWhiteSpace(this.BirthCommentBox.Text) ? "-" : this.BirthCommentBox.Text }, this.SelectedPlace.Name);
                if (added)
                {
                    this.BirthDateBox.Text = "";
                    this.BirthTimeBox.Text = "";
                    this.BirthFirstNameBox.Text = "";
                    this.BirthLastNameBox.Text = "";
                    this.BirthMothersNameBox.Text = "";
                    this.BirthFathersNameBox.Text = "";
                    this.BirthCommentBox.Text = "";
                    this.UpdateBirthsBook();
                }
            }
            else
            {
                MessageBox.Show("Only Comment textfield can be empty. Try again.");
            }
        }

        private void AddWeddingConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WeddingDateBox.Text.Length > 0 && this.WeddingBridesFirstNameBox.Text.Length > 0 && this.WeddingBridesLastNameBox.Text.Length > 0 && this.WeddingBridesMothersFirstNameBox.Text.Length > 0 &&
                this.WeddingBridesMothersLastNameBox.Text.Length > 0 && this.WeddingBridesFathersFirstNameBox.Text.Length > 0 && this.WeddingBridesFathersLastNameBox.Text.Length > 0 &&
                this.WeddingGroomsFirstNameBox.Text.Length > 0 && this.WeddingGroomsLastNameBox.Text.Length > 0 && this.WeddingGroomsMothersFirstNameBox.Text.Length > 0 &&
                this.WeddingGroomsMothersLastNameBox.Text.Length > 0 && this.WeddingGroomsFathersFirstNameBox.Text.Length > 0 && this.WeddingGroomsFathersLastNameBox.Text.Length > 0)
            {
                bool added = this.WeddingsBookService.AddWedding(new string[] {
                    this.WeddingDateBox.Text,
                    this.WeddingBridesFirstNameBox.Text,
                    this.WeddingBridesLastNameBox.Text,
                    this.WeddingBridesMothersFirstNameBox.Text,
                    this.WeddingBridesMothersLastNameBox.Text,
                    this.WeddingBridesFathersFirstNameBox.Text,
                    this.WeddingBridesFathersLastNameBox.Text,
                    this.WeddingGroomsFirstNameBox.Text,
                    this.WeddingGroomsLastNameBox.Text,
                    this.WeddingGroomsMothersFirstNameBox.Text,
                    this.WeddingGroomsMothersLastNameBox.Text,
                    this.WeddingGroomsFathersFirstNameBox.Text,
                    this.WeddingGroomsFathersLastNameBox.Text,
                    string.IsNullOrWhiteSpace(this.WeddingCommentBox.Text) ? "-" : this.WeddingCommentBox.Text
                    }, this.SelectedPlace.Name);
                if (added)
                {
                    this.WeddingDateBox.Text = "";
                    this.WeddingBridesFirstNameBox.Text = "";
                    this.WeddingBridesLastNameBox.Text = "";
                    this.WeddingBridesMothersFirstNameBox.Text = "";
                    this.WeddingBridesMothersLastNameBox.Text = "";
                    this.WeddingBridesFathersFirstNameBox.Text = "";
                    this.WeddingBridesFathersLastNameBox.Text = "";
                    this.WeddingGroomsFirstNameBox.Text = "";
                    this.WeddingGroomsLastNameBox.Text = "";
                    this.WeddingGroomsMothersFirstNameBox.Text = "";
                    this.WeddingGroomsMothersLastNameBox.Text = "";
                    this.WeddingGroomsFathersFirstNameBox.Text = "";
                    this.WeddingGroomsFathersLastNameBox.Text = "";
                    this.WeddingCommentBox.Text = "";
                    this.UpdateWeddingsBook();
                }
            }
            else
            {
                MessageBox.Show("Only Comment textfield can be empty. Try again.");
            }
        }

        private void AddDeathConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DeathDateBox.Text.Length > 0 && this.DeathFirstNameBox.Text.Length > 0 && this.DeathLastNameBox.Text.Length > 0)
            {
                bool added = this.DeathsBookService.AddDeath(new string[] { this.DeathDateBox.Text, this.DeathFirstNameBox.Text, this.DeathLastNameBox.Text,
                    string.IsNullOrWhiteSpace(this.DeathCommentBox.Text) ? "-" : this.DeathCommentBox.Text }, this.SelectedPlace.Name);
                if (added)
                {
                    this.DeathDateBox.Text = "";
                    this.DeathFirstNameBox.Text = "";
                    this.DeathLastNameBox.Text = "";
                    this.DeathCommentBox.Text = "";
                    this.UpdateDeathsBook();
                }
            }
            else
            {
                MessageBox.Show("Only Comment textfield can be empty. Try again.");
            }
        }

        public void ExportPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.cvs|Text file (*.txt)|*.txt";
            bool exported = false;
            if (saveFileDialog.ShowDialog() == true)
                exported = this.PlaceService.ExportPlaces(saveFileDialog.FileName);

            if (exported)
            {
                MessageBox.Show("Successfull export!");
            }
        }

        public void ExportBirthsBookButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            bool exported = false;
            if (saveFileDialog.ShowDialog() == true)
                exported = this.BirthsBookService.ExportBirthsBook(saveFileDialog.FileName,this.SelectedPlace.Name);

            if (exported)
            {
                MessageBox.Show("Successfull export!");
            }
           
        }

        public void ExportWeddingsBookButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            bool exported = false;
            if (saveFileDialog.ShowDialog() == true)
                exported = this.WeddingsBookService.ExportWeddingsBook(saveFileDialog.FileName, this.SelectedPlace.Name);

            if (exported)
            {
                MessageBox.Show("Successfull export!");
            }
            
        }

        public void ExportDeathsBookButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            bool exported = false;
            if (saveFileDialog.ShowDialog() == true)
                exported = this.DeathsBookService.ExportDeathsBook(saveFileDialog.FileName, this.SelectedPlace.Name);

            if (exported)
            {
                MessageBox.Show("Successfull export!");
            }
        }

        private void BackToPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            this.SwitchToPlacePanel();
        }
    }
}
