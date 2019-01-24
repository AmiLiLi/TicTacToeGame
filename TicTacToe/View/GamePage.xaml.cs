using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace TicTacToe.View
{
    /// <summary>
    /// Game page.
    /// </summary>
    public partial class GamePage : ContentPage
    {
        private bool gameStarted { set; get; } = false;
        private bool GameOver { set; get; } = false;

        //play turn initialize: true: player, false: AI player
        private bool PlayTurn { set; get; }  
        private int PlayCount { set; get; }
        private int PlayTurnCount { set; get; } = 0;

        private Global.Player TheWinner { set; get; } = Global.Player.Empty;
        private Global.WinnerPos TheWinnerPosition { set; get; } = Global.WinnerPos.None;

        private int[,] DataArray = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; 

        public GamePage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "back");
            //NavigationPage.SetTitleIcon(this, "Logo.png");

            gameStarted = false;
            this.InitForm();

        }


        /// <summary>
        /// Ons the back button pressed.
        /// </summary>
        /// <returns><c>true</c>, if back button pressed was oned, <c>false</c> otherwise.</returns>
        protected override bool OnBackButtonPressed()
        {

            UserDialogs.Instance.ConfirmAsync("Are you sure you want to leave your current game?", "Leave game").ContinueWith((exit) =>
            {
                if (exit.Result)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                    });
                }
            });

            return true;//return base.OnBackButtonPressed();
        }

        void DoneButton_Clicked(object sender, System.EventArgs e)
        {
            //remove the page current in the top of the Navigation stack
            Application.Current.MainPage.Navigation.PopAsync();

        }

        void PlayButton_Clicked(object sender, System.EventArgs e)
        {
            if (!PlayTurn)
            {
                return;
            }

            Button currentButton = (Button)sender;

            BtnClickEvent(currentButton);

            if (!PlayTurn)
            {
                DoAIPlay();
            }

        }

        /// <summary>
        /// Buttons the click event.
        /// </summary>
        /// <param name="button">Current button.</param>
        void BtnClickEvent(Button button)
        {
            PlayTurnCount++;

            ShowAnimation(button);

            //set button image
            string btnImageName = GetImageAndUpdateDataArray(button.AutomationId);
            button.Image = (Xamarin.Forms.FileImageSource)ImageSource.FromFile(btnImageName);

            //check winner
            bool isWin = this.CheckPlayWinner();
            if (isWin)
            {
                GameOver = true;
                HighLightBtn(TheWinnerPosition);
                Console.WriteLine("TheWinner = [" + TheWinner + "]" + " TheWinnerPosition = [" + TheWinnerPosition + "]");

                //reset button disenable
                this.EnableButtons(false);
                //play,win time count up 
                this.PlayCountUp();
                //set info
                this.ShowWinInfo();
                //play turn label
                //PlayTurnLabel.Text = string.Empty;

            }
            else
            {
                //change player
                PlayTurn = !PlayTurn;
                //play turn label
                //PlayTurnLabel.Text = PlayTurn ? Constants.Turn_Player : Constants.Turn_AI;

                //disabling current button
                button.IsEnabled = false;

                //game over
                if (PlayTurnCount == DataArray.Length)
                {
                    GameOver = true;
                    //set info
                    this.ShowWinInfo();
                    //play,win time count up 
                    this.PlayCountUp();
                }
            }

        }

        /// <summary>
        /// Sets the match result info.
        /// </summary>
        void ShowWinInfo()
        {
            switch (TheWinner)
            {
                case Global.Player.Player1:
                    WinImage.Source = (Xamarin.Forms.FileImageSource)ImageSource.FromFile(Constants.IMG_WIN);
                    WinLabel.Text = Constants.MESSAGE_WIN;
                    break;
                case Global.Player.Player2:
                    WinImage.Source = (Xamarin.Forms.FileImageSource)ImageSource.FromFile(Constants.IMG_LOSE);
                    WinLabel.Text = Constants.MESSAGE_LOSE;
                    break;
                default:
                    WinImage.Source = (Xamarin.Forms.FileImageSource)ImageSource.FromFile(Constants.IMG_DRAW);
                    WinLabel.Text = Constants.MESSAGE_DRAW;
                    break;
            }
        }


        /// <summary>
        /// Plaies the count up.
        /// </summary>
        void PlayCountUp()
        {
            Application.Current.Properties[Global.PROPERTY_PLAY_COUNT] = (int)Application.Current.Properties[Global.PROPERTY_PLAY_COUNT] + 1;

            if (TheWinner == Global.Player.Player1)
            {
                Application.Current.Properties[Global.PROPERTY_WIN_COUNT] = (int)Application.Current.Properties[Global.PROPERTY_WIN_COUNT] + 1;
            }
            else if (TheWinner == Global.Player.Player2)
            {
                Application.Current.Properties[Global.PROPERTY_LOST_COUNT] = (int)Application.Current.Properties[Global.PROPERTY_LOST_COUNT] + 1;
            }

        }


        /// <summary>
        /// When button is clicked, get the button's image file
        /// and update data array.
        /// </summary>
        /// <returns>The button image name and set array info.</returns>
        /// <param name="btnName">Button name.</param>
        string GetImageAndUpdateDataArray(string btnName)
        {
            String retName = Constants.IMG_LOSE;

            switch (btnName)
            {
                case "button1":
                    if (PlayTurn)
                    {
                        DataArray[0, 0] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_1_O;
                    }
                    else
                    {
                        DataArray[0, 0] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_1_X;
                    }
                    break;
                case "button4":
                    if (PlayTurn)
                    {
                        DataArray[1, 0] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_1_O;
                    }
                    else
                    {
                        DataArray[1, 0] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_1_X;
                    }
                    break;
                case "button7":
                    if (PlayTurn)
                    {
                        DataArray[2, 0] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_1_O;
                    }
                    else
                    {
                        DataArray[2, 0] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_1_X;
                    }
                    break;
                case "button2":
                    if (PlayTurn)
                    {
                        DataArray[0, 1] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_2_O;
                    }
                    else
                    {
                        DataArray[0, 1] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_2_X;
                    }
                    break;
                case "button5":
                    if (PlayTurn)
                    {
                        DataArray[1, 1] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_2_O;
                    }
                    else
                    {
                        DataArray[1, 1] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_2_X;
                    }
                    break;
                case "button8":
                    if (PlayTurn)
                    {
                        DataArray[2, 1] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_2_O;
                    }
                    else
                    {
                        DataArray[2, 1] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_2_X;
                    }
                    break;
                case "button3":
                    if (PlayTurn)
                    {
                        DataArray[0, 2] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_3_O;
                    }
                    else
                    {
                        DataArray[0, 2] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_3_X;
                    }
                    break;
                case "button6":
                    if (PlayTurn)
                    {
                        DataArray[1, 2] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_3_O;
                    }
                    else
                    {
                        DataArray[1, 2] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_3_X;
                    }
                    break;
                case "button9":
                    if (PlayTurn)
                    {
                        DataArray[2, 2] = (int)Global.Player.Player1;
                        retName = Constants.COLUMN_3_O;
                    }
                    else
                    {
                        DataArray[2, 2] = (int)Global.Player.Player2;
                        retName = Constants.COLUMN_3_X;
                    }
                    break;
                default:
                    Console.WriteLine("Something is wrong.");
                    retName = Constants.IMG_LOSE;
                    break;

            }

            return retName;

        }

        /// <summary>
        /// Resets the game button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void ResetGameButton_Clicked(object sender, System.EventArgs e)
        {
            if (!GameOver && gameStarted)
            {
                var result = await UserDialogs.Instance.ConfirmAsync(
                    "The game isn't over yet, are you sure you want to restart the game?",
                    "Start a new game?");
                if (!result) return;
            }

            gameStarted = true;

            this.InitForm();

            if (!PlayTurn) DoAIPlay();

        }

        /// <summary>
        /// initialize Form
        /// </summary>
        void InitForm()
        {

            if (gameStarted)
            {
                ButtonRestart.Text = Constants.PLAY_AGAIN;
                this.EnableButtons(true);
            }
            else
            {
                ButtonRestart.Text = Constants.PLAY_START;
                this.EnableButtons(false);
            }

            //play count
            PlayCount = (int)Application.Current.Properties[Global.PROPERTY_PLAY_COUNT];
            //get play turn 
            PlayTurn = Convert.ToBoolean(PlayCount % 2);
            //play turn label
            //PlayTurnLabel.Text = PlayTurn ? Constants.Turn_Player : Constants.Turn_AI;


            //clear button image
            button1.Image = null;
            button2.Image = null;
            button3.Image = null;
            button4.Image = null;
            button5.Image = null;
            button6.Image = null;
            button7.Image = null;
            button8.Image = null;
            button9.Image = null;

            //clear win info 
            WinImage.Source = string.Empty;
            WinLabel.Text = string.Empty;

            //clear array
            DataArray = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            GameOver = false;
            PlayTurnCount = 0;

        }

        /// <summary>
        /// initial buttons for restarting a new play
        /// </summary>
        /// <param name="isEnabled">If set to <c>true</c> is enabled.</param>
        void EnableButtons(bool isEnabled)
        {
            button1.IsEnabled = isEnabled;
            button2.IsEnabled = isEnabled;
            button3.IsEnabled = isEnabled;
            button4.IsEnabled = isEnabled;
            button5.IsEnabled = isEnabled;
            button6.IsEnabled = isEnabled;
            button7.IsEnabled = isEnabled;
            button8.IsEnabled = isEnabled;
            button9.IsEnabled = isEnabled;

        }

        /// <summary>
        /// Checks the play winner.
        /// </summary>
        /// <returns><c>true</c>, if play winner was checked, <c>false</c> otherwise.</returns>
        private bool CheckPlayWinner()
        {
            //check Horizontal line
            for (int i = 0; i < DataArray.GetLength(0); i++)
            {
                if (DataArray[i, 0] == DataArray[i, 1] &&
                    DataArray[i, 0] == DataArray[i, 2] &&
                    DataArray[i, 0] != (int)Global.Player.Empty)
                {

                    TheWinner = (Global.Player)DataArray[i, 0];
                    TheWinnerPosition = (Global.WinnerPos)i;
                    return true;
                }
            }

            //check Vertical line
            for (int j = 0; j < DataArray.GetLength(1); j++)
            {
                if (DataArray[0, j] == DataArray[1, j] &&
                    DataArray[0, j] == DataArray[2, j] &&
                    DataArray[0, j] != (int)Global.Player.Empty)
                {

                    TheWinner = (Global.Player)DataArray[0, j];
                    TheWinnerPosition = (Global.WinnerPos)(j + 3);
                    return true;
                }
            }

            //check slash line  Slash_LeftTop_To_RightButtom
            if (DataArray[0, 0] == DataArray[1, 1] &&
                DataArray[0, 0] == DataArray[2, 2] &&
                DataArray[0, 0] != (int)Global.Player.Empty)
            {
                TheWinner = (Global.Player)DataArray[0, 0];
                TheWinnerPosition = Global.WinnerPos.Slash_LeftTop_To_RightButtom;
                return true;
            }
            //check slash line  Slash_LeftButtom_To_Righttop
            else if (DataArray[2, 0] == DataArray[1, 1] &&
                     DataArray[2, 0] == DataArray[0, 2] &&
                     DataArray[2, 0] != (int)Global.Player.Empty)
            {
                TheWinner = (Global.Player)DataArray[2, 0];
                TheWinnerPosition = Global.WinnerPos.Slash_LeftButtom_To_Righttop;
                return true;
            }
            else
            {
                TheWinner = Global.Player.Empty;
                TheWinnerPosition = Global.WinnerPos.None;
                return false;
            }

        }

        /// <summary>
        /// Gets the success button names.
        /// </summary>
        /// <param name="pos">Position.</param>
        void HighLightBtn(Global.WinnerPos pos)
        {
            switch (pos)
            {
                case Global.WinnerPos.Horizontal_0:
                    ShowAnimation(button1, button2, button3);
                    break;
                case Global.WinnerPos.Horizontal_1:
                    ShowAnimation(button4, button5, button6);
                    break;
                case Global.WinnerPos.Horizontal_2:
                    ShowAnimation(button7, button8, button9);
                    break;
                case Global.WinnerPos.Vertical_0:
                    ShowAnimation(button1, button4, button7);
                    break;
                case Global.WinnerPos.Vertical_1:
                    ShowAnimation(button2, button5, button8);
                    break;
                case Global.WinnerPos.Vertical_2:
                    ShowAnimation(button3, button6, button9);
                    break;
                case Global.WinnerPos.Slash_LeftTop_To_RightButtom:
                    ShowAnimation(button1, button5, button9);
                    break;
                case Global.WinnerPos.Slash_LeftButtom_To_Righttop:
                    ShowAnimation(button7, button5, button3);
                    break;
                default:
                    Console.WriteLine("Something is wrong in getSuccessBtnNames()");
                    break;
            }
        }

        /// <summary>
        /// AI's turn, select the best way to set chess automatically.
        /// </summary>
        void DoAIPlay()
        {
            bool AIHasPlayed = false;

            int[,] CombineData =
            {
                {DataArray[0,0], DataArray[0,1],DataArray[0,2]},
                {DataArray[1,0],DataArray[1,1],DataArray[1,2]},
                {DataArray[2,0],DataArray[2,1],DataArray[2,2]},
                {DataArray[0,0],DataArray[1,0],DataArray[2,0]},
                {DataArray[0,1],DataArray[1,1],DataArray[2,1]},
                {DataArray[0,2],DataArray[1,2],DataArray[2,2]},
                {DataArray[0,0],DataArray[1,1],DataArray[2,2]},
                {DataArray[2,0],DataArray[1,1],DataArray[0,2]}
            };

            string btnName = string.Empty;

            //priority for checking player2(AI player)
            btnName = GetBestPosForAIPlay(CombineData, (int)Global.Player.Player2, 2);
            if (btnName != string.Empty)
            {
                AIClick(btnName);
                AIHasPlayed = true;
            }

            //next check if player1(user) nearly become success
            if (!AIHasPlayed)
            {
                btnName = GetBestPosForAIPlay(CombineData, (int)Global.Player.Player1, 2);
                if (btnName != string.Empty)
                {
                    AIClick(btnName);
                    AIHasPlayed = true;
                }
            }

            //next check if there is a chess on one line for player2(AI player)
            if (!AIHasPlayed)
            {
                btnName = GetBestPosForAIPlay(CombineData, (int)Global.Player.Player2, 1);
                if (btnName != string.Empty)
                {
                    AIClick(btnName);
                    AIHasPlayed = true;
                }
            }

            //next check if there is a chess on one line for player1(user)
            if (!AIHasPlayed)
            {
                btnName = GetBestPosForAIPlay(CombineData, (int)Global.Player.Player1, 1);
                if (btnName != string.Empty)
                {
                    AIClick(btnName);
                    AIHasPlayed = true;
                }
            }


            //otherwise...
            if (!AIHasPlayed)
            {
                btnName = GetBestPosForAIPlay(CombineData, (int)Global.Player.Empty, 0);
                if (btnName != string.Empty)
                {
                    AIClick(btnName);
                    AIHasPlayed = true;
                }
            }

        }

        /// <summary>
        /// AI click the button.
        /// </summary>
        /// <param name="btnName">Button name.</param>
        void AIClick(string btnName)
        {
            Button myButton;

            myButton = GetButtonObj(btnName);
            if (myButton != null)
            {
                BtnClickEvent(myButton);
            }
        }

        /// <summary>
        /// Gets the best position for AI play.
        /// </summary>
        /// <returns>The best position for ai.</returns>
        /// <param name="combiArray">Combi array.</param>
        /// <param name="player">Player.</param>
        /// <param name="checkCount">Check count.</param>
        string GetBestPosForAIPlay(int[,] combiArray, int player, int checkCount)
        {
            int count = 0;
            string retRow = String.Empty;
            List<int> lineList = new List<int>();
            List<string> btnList = new List<string>();

            for (int i = 0; i < combiArray.GetLength(0); i++)
            {
                for (int j = 0; j < combiArray.GetLength(1); j++)
                {
                    if (combiArray[i, j] == player)
                    {
                        count++;
                    }

                }

                if (checkCount == 0)
                {
                    if (count >= checkCount)
                    {
                        lineList.Add(i);
                    }
                }
                else
                {
                    if (count == checkCount)
                    {
                        lineList.Add(i);
                    }
                }

                count = 0;
            }

            count = 0;
            string btnName = string.Empty;
            for (int cnt=0; cnt<lineList.Count; cnt++)
            {
                //check if there is a empty button on a line
                for (int j = 0; j < combiArray.GetLength(1); j++)
                {
                    if (combiArray[lineList[cnt], j] == (int)Global.Player.Empty)
                    {
                        count++;
                    }

                }

                if (count >0)
                {
                    btnName = GetButtonName(lineList[cnt]);
                    if (btnName != string.Empty)
                    {
                        btnList.Add(btnName);
                    }
                }
            }

            // get random line number
            if (btnList.Count != 0)
            {
                Random random = new Random();
                retRow = btnList[random.Next(btnList.Count)];
            }

            return retRow;

        }

        /// <summary>
        /// Gets the name of the button.
        /// </summary>
        /// <returns>The button name.</returns>
        /// <param name="line">Line.</param>
        string GetButtonName(int line)
        {
            string btnName = string.Empty;

            switch (line)
            {
                case 0:
                    btnName = EmptyButtonName(new int[3, 2] { { 0, 0 }, { 0, 1 }, { 0, 2 } });
                    break;
                case 1:
                    btnName = EmptyButtonName(new int[3, 2] { { 1, 0 }, { 1, 1 }, { 1, 2 } });
                    break;
                case 2:
                    btnName = EmptyButtonName(new int[3, 2] { { 2, 0 }, { 2, 1 }, { 2, 2 } });
                    break;
                case 3:
                    btnName = EmptyButtonName(new int[3, 2] { { 0, 0 }, { 1, 0 }, { 2, 0 } });
                    break;
                case 4:
                    btnName = EmptyButtonName(new int[3, 2] { { 0, 1 }, { 1, 1 }, { 2, 1 } });
                    break;
                case 5:
                    btnName = EmptyButtonName(new int[3, 2] { { 0, 2 }, { 1, 2 }, { 2, 2 } });
                    break;
                case 6:
                    btnName = EmptyButtonName(new int[3, 2] { { 0, 0 }, { 1, 1 }, { 2, 2 } });
                    break;
                case 7:
                    btnName = EmptyButtonName(new int[3, 2] { { 2, 0 }, { 1, 1 }, { 0, 2 } });
                    break;
                default:
                    break;

            }
            return btnName;
        }

        /// <summary>
        /// Empties the name of the button.
        /// </summary>
        /// <returns>The button name.</returns>
        /// <param name="array">Array.</param>
        string EmptyButtonName(int[,] array)
        {
            string retName = string.Empty;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (DataArray[array[i, 0], array[i, 1]] == (int)Global.Player.Empty)
                {
                    retName = "button" + Convert.ToString(array[i, 0] * 3 + array[i, 1] + 1);
                    break;
                }
            }

            return retName;
        }

        /// <summary>
        /// Gets the button object.
        /// </summary>
        /// <returns>The button object.</returns>
        /// <param name="name">Name.</param>
        Button GetButtonObj(string name)
        {
            switch (name)
            {
                case "button1":
                    return button1;
                case "button2":
                    return button2;
                case "button3":
                    return button3;
                case "button4":
                    return button4;
                case "button5":
                    return button5;
                case "button6":
                    return button6;
                case "button7":
                    return button7;
                case "button8":
                    return button8;
                case "button9":
                    return button9;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Shows the animation.
        /// </summary>
        /// <param name="myButton">My button.</param>
        void ShowAnimation(Button myButton)
        {
            var myAnimation = new Animation();
            var scaleDownAnimation1 = new Animation(v => myButton.Scale = v, 1, 0.8, Easing.SpringIn);
            var scaleUpAnimation = new Animation(v => myButton.Scale = v, 0.8, 1.1);
            var scaleDownAnimation2 = new Animation(v => myButton.Scale = v, 1.1, 1, Easing.SpringOut);
            myAnimation.Add(0, 0.3, scaleDownAnimation1);
            myAnimation.Add(0.3, 0.6, scaleUpAnimation);
            myAnimation.Add(0.6, 1, scaleDownAnimation2);
            myAnimation.Commit(this, 
                               "ButtonClickAnimation", 
                               1, 1500, null);
        }

        /// <summary>
        /// Sets the animation for win line three buttons.
        /// </summary>
        /// <param name="b1">B1.</param>
        /// <param name="b2">B2.</param>
        /// <param name="b3">B3.</param>
        async void ShowAnimation(Button b1, Button b2, Button b3)
        {
            uint duration = 800;
            await Task.WhenAll(
                b1.ScaleTo(0.7, duration),
                b2.ScaleTo(0.7, duration),
                b3.ScaleTo(0.7, duration)
            );

            await Task.WhenAll(
                b1.ScaleTo(1.3, duration),
                b2.ScaleTo(1.3, duration),
                b3.ScaleTo(1.3, duration)
            );

            await Task.WhenAll(
                b1.ScaleTo(1, duration),
                b2.ScaleTo(1, duration),
                b3.ScaleTo(1, duration)
            );
        }

    }
}
