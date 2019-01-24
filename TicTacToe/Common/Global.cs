using System;
namespace TicTacToe
{
    static public class Constants
    {
        //image for win,lose,draw
        public const string IMG_DRAW = "face_indifferent.png";
        public const string IMG_WIN = "face_happy.png";
        public const string IMG_LOSE = "face_sad.png";

        //label message for win,lose,draw
        public const string MESSAGE_DRAW = "That's a draw!";
        public const string MESSAGE_WIN = "You win!";
        public const string MESSAGE_LOSE = "You lose!";

        public const string PLAY_START = "Start";
        public const string PLAY_AGAIN = "Play again";

        //button images
        public const string COLUMN_1_O = "o_1.png";
        public const string COLUMN_1_X = "x_1.png";

        public const string COLUMN_2_O = "o_2.png";
        public const string COLUMN_2_X = "x_2.png";

        public const string COLUMN_3_O = "o_3.png";
        public const string COLUMN_3_X = "x_3.png";

        //show turn info
        public const string Turn_Player = "Now it's your turn!";
        public const string Turn_AI = "Now it's mobile's turn!";
    }

    public class Global
    {
        //app properties setting
        public static string PROPERTY_PLAY_COUNT = "PLAY_COUNT";
        public static string PROPERTY_WIN_COUNT = "WIN_COUNT";
        public static string PROPERTY_LOST_COUNT = "LOST_COUNT";

        //player
        public enum Player
        {
            Empty = 0,
            Player1 = 1,
            Player2 = 2
        }

        //Winner position
        public enum WinnerPos
        {
            Horizontal_0 = 0, //Horizontal line1
            Horizontal_1 = 1, //Horizontal line2
            Horizontal_2 = 2, //Horizontal line3
            Vertical_0 = 3,   //Vertical line1
            Vertical_1 = 4,   //Vertical line2
            Vertical_2 = 5,   //Vertical line3
            Slash_LeftTop_To_RightButtom = 6, //slash left top to right buttom
            Slash_LeftButtom_To_Righttop = 7, //slash left buttom to right top
            None = 9          //no winner
        }

        //private double _dResultValue1 = 0.0;
        //private int _playCount1 = 0;

        //public static string aaa;

        //public double _dResultValue { get => _dResultValue1; set => _dResultValue1 = value; }

        //public int _playCount { get => _playCount1; set => _playCount1 = value; }

    }

}
