using Wordle.Model;

namespace Wordle
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        GameView gameView;
        public MainPage(GameView gameView)
        {
            InitializeComponent();
            BindingContext = gameView;
            this.gameView = gameView;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is char letter)
            {
                gameView.LetterClicked(letter);
            }
            
        }
    }

}
