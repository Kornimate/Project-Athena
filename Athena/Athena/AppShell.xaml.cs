using Athena.Views;

namespace Athena
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ReadWordsPage), typeof(ReadWordsPage));
            Routing.RegisterRoute(nameof(FlashCardsPage), typeof(FlashCardsPage));
        }
    }
}
