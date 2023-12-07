using System.Collections.ObjectModel;
using DXMauiApp.Data;

namespace DXMauiApp.ViewModels; 

public class PostsViewModel : BaseViewModel
{
    public Command DeletePostCommand { get; }
    

    public PostsViewModel() {
        DeletePostCommand = new Command<Post>(DeletePost);
        ExecuteLoadItems();
        GetCurrentUser();
    }

    ObservableCollection<Post> _posts;
    public ObservableCollection<Post> Posts
    { 
        get => _posts; 
        set => SetProperty(ref _posts, value); 
    }

    ApplicationUser _currentUser;
    public ApplicationUser CurrentUser 
    { 
        get => _currentUser; 
        set => SetProperty(ref _currentUser, value); 
    }

    bool? _canDeletePosts;
    public bool CanDeletePosts
    {
        get
        {
            if (_canDeletePosts.HasValue)
                return _canDeletePosts.Value;

            UpdateCanDeletePostsAsync();
            return false;
            
        }
        set
        {
            _canDeletePosts = value;
            OnPropertyChanged();
        }
    }

    public async void DeletePost(Post post)
    {
        bool isDeleted = await DataStore.DeletePostAsync(post.PostId);
        if (!isDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Couldn't delete the post", "Ok");
        }
        else
        {
            Posts.Remove(post);
        }
    }
    public async void UpdateCanDeletePostsAsync()
    {
        CanDeletePosts = await DataStore.UserCanDeletePostAsync();
    }

    async void ExecuteLoadItems()
    {
        try
        {
            Posts = new ObservableCollection<Post>(await DataStore.GetItemsAsync(true));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    async void GetCurrentUser()
    {
        try
        {
            CurrentUser = await DataStore.CurrentUser();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}