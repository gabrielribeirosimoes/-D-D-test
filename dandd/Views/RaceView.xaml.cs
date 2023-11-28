using dandd.ViewModels;

namespace dandd.Views;

public partial class RaceView : ContentPage
{
    public RaceView()
    {
        InitializeComponent();
        BindingContext = new RaceViewModel();
    }
}