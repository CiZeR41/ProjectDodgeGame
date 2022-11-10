using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.Media.Playback;
using Windows.Media.Core;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectDoogeGame // Do wall limits, Moving enemys, timers and all others
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // int difficul;
        //Image _img;
        Game _game;

        MediaPlayer mainsong = new MediaPlayer();

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            _game = new Game(_playground);
            ImageBrush im = new ImageBrush();
            _playground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/MainBackground.jpg")) };
            TextBlock meitarx = new TextBlock();
            MainDialog();
        }

        void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.P:
                    _game.pbutton();
                    break;
                case Windows.System.VirtualKey.Up:
                    _game.MoveUp();
                    //פה מטודה שתזיז את התמונה למעלה
                    break;
                case Windows.System.VirtualKey.Down:
                    _game.MoveDown();
                    //פה מטודה שתזיז את התמונה למטה
                    break;
                case Windows.System.VirtualKey.Right:
                    _game.MoveRight();
                    //פה מטודה שתזיז את התמונה ימינה
                    break;
                case Windows.System.VirtualKey.Left:
                    _game.MoveLeft();
                    //פה מטודה שתזיז את התמונה שמאלה
                    break;
                case Windows.System.VirtualKey.Space:
                    _game.DoSpace();

                    //פה מטודה שתקפיץ אותי למקום רנדומאלי במפה
                    break;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WelcomeDialog();
        }
        private void IsMute(object sender, RoutedEventArgs e)
        {
            _game.IsMuted();
        }

        private void IsUnMute(object sender, RoutedEventArgs e)
        {
            _game.IsUnMuted();
        }


        private void btn_Mute(object sender, RoutedEventArgs e)
        {
        }


        public void StartGameBackground()
        {
            _playground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets///GameBackGround.png")) };

        }
        private async void HelpDialog() // Welcome dialog + Choose level
        {
            ContentDialog DifficulChoose = new ContentDialog
            {
                Title = "\t\t *** Info && How to Play ***",
                Content = "\t   *** Hello dear Player! Here is the game instructions *** \t\n\n\t * To move your player use: UP,DOWN,LEFT,RIGHT keys *\t\t\t\n" +
                "\t * To jump to a random place on the map Type: Space key *\t\t\t\n" +
                "\t * You can can pasue/resume the game by pressing 'P' key *\t\t\t\n" +
                "\t * You can save/load your game by the menu below *\t\t\t\n" +
                "\t * You can start the game again with a different difficulty level *\t\t\t\n" +
                "\n\n\t\t\t*** Tips && Tricks***\t\t\t\n" +
                "\t     * You can mute the sound by the menu below *\t\t\t\n" +
                "\t   * DO NOT use the space key unless you are goin to die *\t\t\t\n" +
                "\t   * To complete the mission you must be the last man standing *\t\t\t\n" +
                "\t\n\n\t\t            *** Good luck Player ! ***\t\t\t\n"
                ,
                CloseButtonText = "OK",
            };
            ContentDialogResult result = await DifficulChoose.ShowAsync();

        }
        private async void MainDialog() // Welcome dialog + Choose level
        {
            ContentDialog DifficulChoose = new ContentDialog
            {
                Title = "\t      *** SpongeBob Dooge Game ***",
                Content = "\t   *** Hello dear Player! welcome to Meitar Dooge Game *** \t\n\n\t * To play please use the bar buttons below *\t\t\t\n" +
                "\t * Your main goal is to run away from the jelly fish *\t\t\t\n" +
                "\t * You can will win when only 1 jelly fish stay alive and all other will died. *\t\t\t\n" +
                "\t * When jellyfish touch another jelly fish on of them will die *\t\t\t\n" +
                "\t * Before you start play you can easly open the help button below *\t\t\t\n" +

                "\t\n\n\t\t            *** Good luck Player ! ***\t\t\t\n"
                ,
                CloseButtonText = "OK",
            };
            ContentDialogResult result = await DifficulChoose.ShowAsync();

        }
        private async void WelcomeDialog() // Welcome dialog + Choose level
        {
            ContentDialog DifficulChoose = new ContentDialog
            {
                Title = "Welcome to my 'SpongeBob SquarePants Dooge Game'",
                Content = "Please choose the Level of difficulty:",
                PrimaryButtonText = "Beginner",
                SecondaryButtonText = "Advanced",
                CloseButtonText = "Expert",
            };
            ContentDialogResult result = await DifficulChoose.ShowAsync();

            if (result == ContentDialogResult.Primary) // Begginer
            {
                //_game.Start(GoodXSize,GoodYSize,BadXSize,BadYSize,ColSpeed);
                _game.Start(50, 50, 60, 60, 1); //Easy mode, Good size - 150, bad size - 65 - Bad Speed - 1
                // 5 Seconds Timer an then go with begginer level
            }
            else if (result == ContentDialogResult.Secondary)
            {
                _game.Start(50, 50, 70, 70, 2); //Advanced mode, Good size - 75, bad size - 60 - Bad Speed - 2
                // 3 Seconds Timer and then go with Advanced level

            }
            else
            {
                _game.Start(50, 50, 80, 80, 3); //Expert mode, Good size - 150, bad - Bad Speed - 3
                // 3 Seconds Timer and then go with Expert level
            }
            btnPlay.Icon = new SymbolIcon(Symbol.Pause);
            StartGameBackground();
            mainsong.Pause();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _game.SaveGame();

        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            _playground.Children.Clear();
            StartGameBackground();
            _game.LoadGame();

        }
        private void btnplaypause_Click(object sender, RoutedEventArgs e)
        {
            // For now an empy void because I used the check/uncheck method
        }

        private void is_Checked(object sender, RoutedEventArgs e)
        {
            btnPlay.Icon = new SymbolIcon(Symbol.Play);
            _game.pausegame();
            //Stop the game
        }

        private void is_Unchecked(object sender, RoutedEventArgs e)
        {
            btnPlay.Icon = new SymbolIcon(Symbol.Pause);
            _game.resumegame();
            //Continue the game
        }
        private void btn_info(object sender, RoutedEventArgs e)
        {
            HelpDialog();
        }

    }
}
