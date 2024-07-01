using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.ObjectModel;

namespace Wordle.Model;
public class GameView 
{
    // defining variables 
    private int rowIndex = 0; 

    private int columnIndex = 0;

    private char[] correctAnswer;

    private List<string> words;
    public char[] KeyRow1 { get; }
    public char[] KeyRow2 { get; }
    public char[] KeyRow3 { get; }
    public ObservableCollection<Row> Rows { get; set; }
    public Row WordRow { get; set; }

    // default constructor
    public GameView()
    {
        Rows = new ObservableCollection<Row>();
        WordRow = new Row();
        WordRow.Boxes = new Box[5];

        // initializing the grid of game 
        for (int i = 0; i < 6; i++)
        {
            var wordRow = new Row();
            wordRow.Boxes = new Box[5];

            for (int j = 0; j < 5; j++)
            {
                wordRow.Boxes[j] = new Box();
            }

            Rows.Add(WordRow);
        }

        // words to randomly show in the game
        words = new List<string>
        {
            "Apple", "Table", "Chair", "House", "Clock", "River", "Happy", "Smile", "Music", "Dream"
        };

        Random random = new Random();
        correctAnswer = words[random.Next(words.Count)].ToUpper().ToCharArray();
         
        // initializing the keyboard variables
        KeyRow1 = "QWERTYUIOP".ToCharArray();
        KeyRow2 = "ASDFGHJKL".ToCharArray();
        KeyRow3 = "<ZXCVBNM>".ToCharArray();
    }

    public void EnterPressed()
    {
        if (columnIndex != 5)
            return;

        // check for the validity
        var correct = Validate(correctAnswer); 

        if (correct)
        {
            App.Current.MainPage.DisplayAlert("You Won!", "Congratulation.. \nPress okay to play again!", "Okay");
            Reset();
            return;
        }

        if (rowIndex == 5)
        {
            App.Current.MainPage.DisplayAlert("Game Over!", "Correct answer was "+ new string(correctAnswer) + ". \nPress okay to play again!", "Okay");
            Reset();
        }
        else
        {
            rowIndex++;
            columnIndex = 0;
            WordRow.Boxes = new Box[5];
        }
    }

    // function to set the values in the grid
    public void LetterClicked(char letter)
    {
        // '>' symbol here is used as an enter button
        if (letter == '>')
        {
            EnterPressed();
            return;
        }

        // '<' this symbol is used here as a backspace
        if (letter == '<')
        {
            if (columnIndex == 0)
                return;
            columnIndex--;
            Rows[rowIndex].Boxes[columnIndex].Input = ' ';

            return;
        }

        // donot do anything if index is 5
        if (columnIndex == 5)
        {
            return;
        }

        // setting the values 
        WordRow.Boxes[columnIndex] = new Box { Input = letter }; 
        Rows.RemoveAt(rowIndex);
        Rows.Insert(rowIndex, WordRow);
        columnIndex++;
    }

    // validating the inputs with correct answer
    public bool Validate(char[] correctAnswer)
    {
        int count = 0;

        for (int i = 0; i < 5; i++)
        {
            var letter = WordRow.Boxes[i];

            // conditions to change the colour

            if (letter.Input == correctAnswer[i])  // incase of correct character at correct place
            {
                letter.Colour = Colors.Green; // Green
                count++;
            }
            else if (correctAnswer.Contains(letter.Input)) // incase of correct character at wrong place 
            {
                letter.Colour = Color.FromArgb("#C2AA1F"); // Yellow
            }
            else   // incase of wrong character at wrong place 
            {
                letter.Colour = Colors.Gray; // Gray
            }
        }

        return count == 5;
    }

    //fucction to reset the game grid
    public void Reset()
    {
        Rows.Clear();
        WordRow = new Row();
        WordRow.Boxes = new Box[5];

        // initializing the grid of game i.e 6 rows & 5 cols
        for (int i = 0; i < 6; i++)
        {
            var wordRow = new Row();
            wordRow.Boxes = new Box[5];

            for (int j = 0; j < 5; j++)
            {
                wordRow.Boxes[j] = new Box();
            }

            Rows.Add(wordRow);
        }
        rowIndex = 0;
        columnIndex = 0;
        Random random = new Random();
        correctAnswer = words[random.Next(words.Count)].ToUpper().ToCharArray();

    }


}
