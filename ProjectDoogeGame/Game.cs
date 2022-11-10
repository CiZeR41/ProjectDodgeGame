using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;
using System.Threading;

namespace ProjectDoogeGame
{
    class Game
    {
        //MainPage main = new MainPage();
        //Monsters monsters = new Monsters();
        MediaPlayer StartSound = new MediaPlayer(); // Start Sound
        bool pbtn = false; // Pause/resume button, if false game is pause.
        bool Gamerunning; // True mean game is running.
        Random rnd = new Random(); // Random that use for teleport players random.
        Monsters[] _players; // Array of all players in the game.
        Canvas _playground; // Canvas of the game.
        DispatcherTimer _timer; //Timer of the game
        double Colspeed; //Collision speed by difficul - Monster will chase faster.
        int gy; //Goodie size that Start function got
        int gx; //Goodie size that Start function got
        int _a; //The size of the goodie by diificul
        int _b; // To make it update the rect by the difficul
        int _c; //The size of the Baddie by diificul
        int _d; // To make it update the rect by the difficul
        int counter; // Counter thats check if goddie win.
        MediaPlayer Victorysound = new MediaPlayer();
        MediaPlayer LoseSound = new MediaPlayer();
        MediaPlayer mainsong = new MediaPlayer();
        MediaPlayer ReadySound = new MediaPlayer();

        public Game(Canvas playground)
        {

            mainsong.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/mainsong.mp3", UriKind.RelativeOrAbsolute));
            mainsong.Play();
            _playground = playground;
            _players = new Monsters[13];
            _timer = new DispatcherTimer();
            DispatcherTimerTick();
            Gamerunning = false;

        }
        public void DispatcherTimerTick()
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            _timer.Tick += OntickHandler;
        }
        private void OntickHandler(object s, object e)
        {
            if (counter == _players.Length - 2)
                GameOver(1);
            checkcolgoodie();
            chasegoodie(Colspeed); // Difficul easy - 1, Crazy - 4
            checkcol();

            //main.meitary.Text = monsters.Top.ToString();

        }
        public void chasegoodie(double speed)
        {
            if (Gamerunning == true)

                for (int i = 1; i < _players.Length; i++)
                {
                    if (_players[i] != null)
                        _players[i].Chase(_players[0].Left, _players[0].Top, speed);
                }
        }
        public void checkcol()
        {
            for (int i = 1; i < _players.Length - 1; i++)
            {
                for (int j = i + 1; j < _players.Length; j++)
                {
                    if (_players[i] != null && _players[j] != null)
                    {

                        Rect Baddiehit = new Rect(_players[i].Left, _players[i].Top, _b - 20, _b - 20); // - 25 Because the png as size over by 15 and it's make the col a bit wired, that fix this.
                        Rect Baddiehit1 = new Rect(_players[j].Left, _players[j].Top, _b - 20, _b - 20);
                        Baddiehit.Intersect(Baddiehit1);
                        if (Baddiehit != Rect.Empty)
                        {
                            _players[i].Kill();
                            _players[i] = null;
                            counter++;
                        }

                    }

                }
            }
        }
        public void checkcolgoodie()
        {
            for (int i = 1; i < _players.Length - 1; i++)
            {
                if (_players[0] != null && Gamerunning == true && _players[i] != null)
                {
                    Rect goodiehit = new Rect(_players[0].Left, _players[0].Top, _a, _a); // need to update by difficul
                    Rect goodiehit1 = new Rect(_players[i].Left, _players[i].Top, _b - 35, _b - 35);
                    goodiehit.Intersect(goodiehit1);
                    if (goodiehit != Rect.Empty)
                    {
                        GameOver(0);
                    }
                }
            }
        }
        public async void GameOver(int gameover)
        {
            _timer.Stop();
            _playground.Children.Clear();
            Gamerunning = false;
            if (gameover == 1)
            {
                StartSound.Volume = 0;
                Victorysound.Volume = 1;
                Victorysound.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/vicotrysound.mp3", UriKind.RelativeOrAbsolute));
                Victorysound.Play();
                _playground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/WonBackgroud.gif")) };
                await Task.Delay(3000);
                var Msg = new MessageDialog("Spongebob Dooge Game", "Congratulations all the enemies have been defeated! \nYou are the winner");
                await Msg.ShowAsync();
            }
            else
            {
                StartSound.Volume = 0;
                LoseSound.Volume = 1;
                _playground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/lose.gif")) };
                LoseSound.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/crying.mp3", UriKind.RelativeOrAbsolute));
                LoseSound.Play();
                await Task.Delay(4000);
                var Msg = new MessageDialog("Spongebob Dooge Game", "GAME OVER" +
                    "\nThe jellyfish have managed to reach you, and electrify you to death");
                await Msg.ShowAsync();
            }
        }
        public void Start(int GSizeX, int GSizeY, int BSizeX, int BSizeY, double speed)
        {
            mainsong.Pause();
            LoseSound.Volume = 0;
            Victorysound.Volume = 0;
            StartSound.Volume = 1;
            StartSound.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/song.mp3", UriKind.RelativeOrAbsolute));
            StartSound.Play();
            ReadySound.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/iamready.mp3", UriKind.RelativeOrAbsolute));
            ReadySound.Play();
            counter = 0;
            Colspeed = speed;
            gy = GSizeY;
            gx = GSizeX;
            _playground.Children.Clear();
            Gamerunning = true;
            CreateMonsters(GSizeX, GSizeY, BSizeX, BSizeY); //Create the size of the players by the difiicul the user choose.
            _timer.Start();
        }
        public void IsMuted()
        {
            if (Gamerunning == true)
                StartSound.Volume = 0;
            else
                mainsong.Volume = 0;
        }

        public void IsUnMuted()
        {
            if (Gamerunning == true)
                StartSound.Volume = 1;
            else
                mainsong.Volume = 1;
        }
        public void StopGame()
        {
            _timer.Stop();

        }
        public void ContinueGame()
        {
            _timer.Start();
        }

        public void CreateMonsters(int a, int b, int c, int d)
        {
            _a = a; // sizes of the goodie by the difficul
            _b = b; // sizes of the goodie by the difficul
            _c = c; // sizes of the baddie by the difficul
            _d = d; // sizes of the baddie by the difficul
            _players[0] = new good(a, b, "ms-appx:///Assets/goodie.png", _playground, rnd.Next(0, 100), rnd.Next(100, 200));

            for (int i = 1; i < _players.Length; i++)
            {
                _players[i] = new bads(c, d, "ms-appx:///Assets/beddie.gif", _playground, rnd.Next(250, 1000), rnd.Next(300, 600));
            }
        }

        public void pausegame()
        {
            _timer.Stop();
            Gamerunning = false;
            pbtn = true;
        }
        public void resumegame()
        {
            _timer.Start();
            Gamerunning = true;
            pbtn = false;
        }

        public void pbutton()
        {
            if (pbtn == false)
            {
                pausegame();
            }
            else
            {
                resumegame();
            }
        }

        //Move Method
        //Checks if game not pause + goodie alive + if he stay on the canvas limit
        //Else? won't move.
        public void MoveUp()
        {
            if (Gamerunning && _players[0] != null && _players[0].Top > 5)
                _players[0].GoUp();
        }
        public void MoveDown()
        {
            if (Gamerunning && _players[0] != null && _players[0].Top < _playground.ActualHeight - gy) //PO
                _players[0].GoDown();
        }
        public void MoveRight()
        {
            if (Gamerunning && _players[0] != null && _players[0].Left < _playground.ActualWidth - gx)

                _players[0].GoRight();
        }
        public void MoveLeft()
        {
            if (Gamerunning && _players[0] != null && _players[0].Left > 5) // X & Y start from left cornet with 0 , step is 10
                _players[0].GoLeft();
        }
        public void DoSpace()
        {
            if (Gamerunning && _players[0] != null)
                _players[0].DoJump();
        }
        int CreatureAlive;

        public async void SaveGame()
        {
            CreatureAlive = 0;
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i] != null)
                    CreatureAlive++;
            }

            string[] SaveArray = new string[CreatureAlive + 1];
            int CreatureCounter = 0;

            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i] != null)
                    SaveArray[CreatureCounter++] = _players[i].SavePosition();
            }
            SaveArray[CreatureCounter] = Colspeed.ToString(); // need to be 1 2 3

            StorageFolder doogefolder = ApplicationData.Current.LocalCacheFolder;
            StorageFile DoogeFile = await doogefolder.CreateFileAsync("DoogeSave.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteLinesAsync(DoogeFile, SaveArray);
            var Msg = new MessageDialog("Game Massage", "You have save the game\nYou can now leave and load it any time you want.");
            await Msg.ShowAsync();
        }

        public void LoadGame()
        {
            string FileLoc = @"C:\Users\meita\AppData\Local\Packages\3e8e9ce6-3d52-4a2f-a0b7-970a5f2070ac_sf0mw8hqd0tw0\LocalCache\DoogeSave.txt";
            List<string> Lines = File.ReadAllLines(FileLoc).ToList();
            _players = new Monsters[Lines.Count - 1];
            //int mcount = 0;
            string[] loadarrA = Lines[Lines.Count - 1].Split(',');
            int A = int.Parse(loadarrA[0]);
            Colspeed = A;
            //for (int i = 0; i < Lines.Count; i++)
            //{
            string[] loadarr = Lines[0].Split(',');
            if (A == 1) // easy mode
            {
                _a = 50; // Goodie size x and y
                _b = 60; // Baddie size x and y

            }
            else if (A == 2) // adv mode
            {
                _a = 50; // Goodie size x and y
                _b = 70; // Baddie size x and y

            }
            else if (A == 3) // expert mode
            {
                _a = 50; // Goodie size x and y
                _b = 80; // Baddie size x and y


            }
            _players[0] = new good(_a, _a, "ms-appx:///Assets/goodie.png", _playground, int.Parse(loadarr[1]), int.Parse(loadarr[0]));
            for (int j = 1; j < Lines.Count - 1; j++)
            {
                loadarr = Lines[j].Split(',');
                _players[j] = new bads(_b, _b, "ms-appx:///Assets/beddie.gif", _playground, int.Parse(loadarr[1]), int.Parse(loadarr[0]));
            }
            mainsong.Volume = 0;
            //}


        }
    }
}