using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace gameBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int turn = 1;
        int clickCnt = 20;


        public MainPage()
        {
            this.InitializeComponent();
            CreatGameBoard();

           ApplicationDataContainer localSetting = ApplicationData.Current.LocalSettings;
            try
            {

                //clickCnt = Convert.ToInt32(localSetting.Values["Value"]);
                sumtotal = Convert.ToInt32(localSetting.Values["Value1"]);

            }
            catch (Exception e)
            {

               clickCnt = 20;
            }

        }//end of MainPage()


        TextBlock txt = new TextBlock();
        TextBlock txt1 = new TextBlock();
        TextBlock txt2 = new TextBlock();
        TextBlock txt3 = new TextBlock();



        private void CreatGameBoard()
        {

            Grid square = new Grid();
            square.Background = new SolidColorBrush(Colors.Gray);

            square.Width = 400;
            square.Height = 400;
            square.BorderBrush = new SolidColorBrush(Colors.Black);
            square.BorderThickness = new Thickness(3);


            for (int i = 0; i < 6; i++)
            {
                square.RowDefinitions.Add(new RowDefinition());
            }//end of for loop for Row

            for (int i = 0; i < 5; i++)
            {
                square.ColumnDefinitions.Add(new ColumnDefinition());
            }//end of for loop for col

            Ellipse balls;
            //adding balls to the gameboard
            for (int y = 2; y <= 6; y++)
            {
                for (int u = 0; u < 5; u++)
                {

                    balls = new Ellipse();
                    balls.Width = 40;
                    balls.Height = 40;

                    balls.Fill = new SolidColorBrush(Colors.Orange);
                    balls.Tapped += Balls_Tapped;
                    balls.SetValue(Grid.RowProperty, y);
                    balls.SetValue(Grid.ColumnProperty, u);
                    square.Children.Add(balls);

                    balls.Name = "balls" + y;
                }//end of for(int u = 0; u < 5; u++) 
            }//end of for(int y = 0; y < 5; y++) 

            gameBoard.Children.Add(square);



            Button nextRow = new Button();
            nextRow.Content = "Start/Next Row";
            nextRow.Tapped += nextRow_Tapped;
            nextRow.Background = new SolidColorBrush(Colors.Green);
            nextRow.VerticalAlignment = VerticalAlignment.Top;
            nextRow.Margin = new Thickness(0, 5, 0, 0);
            nextRow.SetValue(Grid.ColumnSpanProperty, 2);
            square.Children.Add(nextRow);



            txt.Name = "Score";
            txt.Text = sumtotal.ToString();
            txt.VerticalAlignment = VerticalAlignment.Top;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.Margin = new Thickness(0, 5, 0, 0);
            txt.SetValue(Grid.ColumnProperty, 2);
            txt.SetValue(Grid.ColumnSpanProperty, 1);
            square.Children.Add(txt);
            txt.Foreground = new SolidColorBrush(Colors.Blue);



            txt1.Name = "Scores";
            txt1.Text = "Scores :";
            txt1.VerticalAlignment = VerticalAlignment.Top;
            txt1.HorizontalAlignment = HorizontalAlignment.Center;
            txt1.Margin = new Thickness(0, 5, 0, 0);
            txt1.SetValue(Grid.ColumnProperty, 1);
            txt1.SetValue(Grid.ColumnSpanProperty, 2);
            square.Children.Add(txt1);

            txt2.Name = "Rounds";
            txt2.Text = clickCnt.ToString();
            txt2.VerticalAlignment = VerticalAlignment.Top;
            txt2.HorizontalAlignment = HorizontalAlignment.Center;
            txt2.Margin = new Thickness(0, 5, 0, 0);
            txt2.SetValue(Grid.ColumnProperty, 4);
            txt2.SetValue(Grid.ColumnSpanProperty, 3);
            square.Children.Add(txt2);
            txt2.Foreground = new SolidColorBrush(Colors.Blue);


            txt3.Name = "Round";
            txt3.Text = "Balls Left: ";
            txt3.VerticalAlignment = VerticalAlignment.Top;
            txt3.HorizontalAlignment = HorizontalAlignment.Center;
            txt3.Margin = new Thickness(0, 5, 0, 0);
            txt3.SetValue(Grid.ColumnProperty, 3);
            txt3.SetValue(Grid.ColumnSpanProperty, 3);
            square.Children.Add(txt3);



            TextBlock myText = new TextBlock();
            myText.SetValue(Grid.RowProperty, 1);
            square.Children.Add(myText);
            myText.Text = "Black=0 Yellow=5 Green=33 Blue=45 Red=-55 White=10.";
            myText.SetValue(Grid.ColumnSpanProperty, 5);
            myText.Foreground = new SolidColorBrush(Colors.Orange);


            Button restart = new Button();
            restart.Content = "Play Again";
            myText.SetValue(Grid.RowProperty, 1);
            myText.SetValue(Grid.ColumnSpanProperty, 5);
            restart.Tapped += restart_Tapped;
            restart.Background = new SolidColorBrush(Colors.Green);
            gameBoard.Children.Add(restart);

         
         }//end of createGameBoar


       //to restart the game by taking you to the main page
        private void restart_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
          
            ApplicationDataContainer localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values["Value1"] = 0;
            this.Frame.Navigate(typeof(MainPage));
            
        }

        private void nextRow_Tapped(object sender, TappedRoutedEventArgs e)
        {

            turn++;
   
        }

        int blackBall = 0;
        int yellowBall = 5;
        int greenBall = 33;
        int blueBall = 45;
        int redBall = -55;
        int whiteBall = 10;
        int sumtotal = 0;


        private void Balls_Tapped(object sender, TappedRoutedEventArgs e)
        {
            clickCnt--;
            ApplicationDataContainer localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values["Value"] = clickCnt;

            Ellipse currentBall = (Ellipse)sender;
            string name = currentBall.Name;
            string ballspos = name.Substring(5, 1);
            int ballspos1 = Convert.ToInt32(ballspos);


            if (ballspos1 == turn)
            {

                if (clickCnt >= 0)
                {
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/laser.mp3"));
                    mediaPlayer.Play();

                    txt2.Text = clickCnt.ToString();

                    Random rnd = new Random();
                    int changeColor = rnd.Next(1, 7);

                    switch (changeColor)
                    {
                        case 1:
                            currentBall.Fill = new SolidColorBrush(Colors.Black);
                            sumtotal += blackBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;
                        case 2:
                            currentBall.Fill = new SolidColorBrush(Colors.Yellow);
                            sumtotal += yellowBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;
                        case 3:
                            currentBall.Fill = new SolidColorBrush(Colors.Green);
                            sumtotal += greenBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;
                        case 4:
                            currentBall.Fill = new SolidColorBrush(Colors.Blue);
                            sumtotal += blueBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;
                        case 5:
                            currentBall.Fill = new SolidColorBrush(Colors.Red);
                            sumtotal += redBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;
                        case 6:
                            currentBall.Fill = new SolidColorBrush(Colors.White);
                            sumtotal += whiteBall;
                            txt.Text = sumtotal.ToString();
                            localSetting.Values["Value1"] = sumtotal;
                            break;

                    }//end of switch

                }//end of clickCnt
            }//(ballspos1 == turn)



        }//end of Balls_Tapped



    }
}
