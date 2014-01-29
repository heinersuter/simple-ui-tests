namespace HttpPostSender.View
{
    using System.Collections.ObjectModel;
    using Commons.Mvvm;

    public class FavoriteListViewModel : ViewModel
    {
        public ObservableCollection<FavoriteViewModel> Favorites
        {
            get { return BackingFields.GetValue(() => Favorites, () => new ObservableCollection<FavoriteViewModel>()); }
        }
    }
}
