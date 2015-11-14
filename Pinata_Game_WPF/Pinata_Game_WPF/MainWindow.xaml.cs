﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pinata_Game_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MAX_MISSED = 2;

        private Pinata pinata; // The Pinata object.
        private Bat bat; // The Bat object
        private DispatcherTimer timer; // the game timer.
        private bool isPaused; // is the game paused
        private bool isGameOver; // is the game over
        private int numMissed; // The number of misses the user has missed
        private int currentScore;
        private int highScore;

        public MainWindow()
        {
            InitializeComponent();
            pinata = new Pinata(this);
            bat = new Bat(this);
            timer = new DispatcherTimer();
            isGameOver = false;
            isPaused = false;
            numMissed = 0;
            currentScore = 0;
            highScore = 0;

            //  DispatcherTimer setup
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // if the game is not paused then, call the draw methods for our pinata
            // and our bat objects.
            if (!isPaused && !isGameOver)
            {
                bat.Draw();
                pinata.Draw();

                if (bat.IsCollision(pinata))
                {
                    //pinata.Hit(); Uncomment once collision method is working.
                    currentScore++;
                }

                if (pinata.NumberOfHits == 5)
                {
                    isGameOver = true;
                    GameOver();
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Pause Logic will go in here.
            if (e.Key == Key.P)
            {
                PauseGame();
            }

            if (e.Key == Key.Space)
            {
                bat.SwingBat();
            }

            if (e.Key == Key.H)
            {
                pinata.Hit();
                currentScore++;
            }
        }

        private void GameOver()
        {
            if (MessageBox.Show("Game Over, Would you like to play again", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
            }
        }

        private void PauseGame()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                lbl_pause.Visibility = Visibility.Visible;
                bg_background.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_pause.Visibility = Visibility.Hidden;
                bg_background.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateLabels()
        {
            lbl_currentScore.Content = "Score: " + currentScore;
            lbl_highscore.Content = "Highscore: " + highScore;
        }
    }
}