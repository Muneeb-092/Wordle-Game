
using System.ComponentModel;

namespace Wordle.Model;

public class Box: INotifyPropertyChanged
{
    // defining variables
    private char _input {  get; set; }
    private Color _colour;

    public Box() {
        _colour = Colors.Black;
    }

    // input getter and setter
    public char Input
    {
        get { return _input; }
        set
        {
            if (_input != value)
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }
    }

    // color getter and setter
    public Color Colour
    {
        get { return _colour; }
        set
        {
            if (_colour != value)
            {
                _colour = value;
                OnPropertyChanged(nameof(Colour));
            }
        }
    }

    // property change event handler
    public event PropertyChangedEventHandler PropertyChanged;

    // function to update property on change
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
