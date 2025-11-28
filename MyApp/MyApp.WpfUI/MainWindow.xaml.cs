using Microsoft.Extensions.DependencyInjection;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Data;
using MyApp.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace MyApp.WpfUI
{
    public partial class MainWindow : Window
    {
        private readonly GenericCrudService<User> _userService;
        private readonly GenericCrudService<Hobby> _hobbyService;

        private User? _selectedUser;

        public MainWindow()
        {
            InitializeComponent();

            _userService = App.Services.GetRequiredService<GenericCrudService<User>>();
            _hobbyService = App.Services.GetRequiredService<GenericCrudService<Hobby>>();

            LoadUsersAsync();
        }

        #region Users CRUD

        private async void LoadUsersAsync()
        {
            UserGrid.ItemsSource = await _userService.GetAllAsync();
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) || !int.TryParse(AgeBox.Text, out int age))
            {
                MessageBox.Show("Enter valid name and age.");
                return;
            }

            var user = new User { Name = NameBox.Text, Age = age, Id = Guid.NewGuid() };
            await _userService.AddAsync(user);

            NameBox.Clear();
            AgeBox.Clear();
            LoadUsersAsync();
        }

        private async void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is User user)
            {
                var dialog = new UpdateUserDialog(user.Name, user.Age) { Owner = this };
                if (dialog.ShowDialog() == true)
                {
                    user.Name = dialog.UpdatedName;
                    user.Age = dialog.UpdatedAge;
                    await _userService.UpdateAsync(user);
                    LoadUsersAsync();
                }
            }
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is User user)
            {
                await _userService.DeleteAsync(user);
                LoadUsersAsync();
            }
        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = UserGrid.SelectedItem as User;
            LoadHobbiesAsync();
        }

        #endregion

        #region Hobbies CRUD
        private async void LoadHobbiesAsync()
        {
            if (_selectedUser == null || _selectedUser.Id == Guid.Empty)
            {
                HobbyGrid.ItemsSource = null;
                return;
            }

            var allHobbies = await _hobbyService.GetAllAsync();

            if (allHobbies == null || allHobbies.Count == 0)
            {
                HobbyGrid.ItemsSource = null;
                return;
            }

            var cleaned = allHobbies.Where(h => h != null && h.UserId != null).ToList();

            if (_selectedUser == null)
            {
                HobbyGrid.ItemsSource = null;
                return;
            }

            var hobbiesForUser = cleaned
                .Where(h => h.UserId == _selectedUser.Id)
                .ToList();

            HobbyGrid.ItemsSource = hobbiesForUser;
        }
        private async void AddHobby_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Select a user first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(HobbyNameBox.Text))
            {
                MessageBox.Show("Enter hobby name.");
                return;
            }

            var hobby = new Hobby
            {
                Id = Guid.NewGuid(),
                Name = HobbyNameBox.Text,
                Description = HobbyDescBox.Text,
                ProficiencyLevel = HobbyLevelBox.Text,
                UserId = _selectedUser.Id
            };

            await _hobbyService.AddAsync(hobby);

            HobbyNameBox.Clear();
            HobbyDescBox.Clear();
            HobbyLevelBox.Clear();
            LoadHobbiesAsync();
        }
        private async void UpdateHobby_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Hobby hobby)
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox(
                    "Update Hobby Name:", "Edit Hobby", hobby.Name);
                if (!string.IsNullOrWhiteSpace(input))
                {
                    hobby.Name = input;
                    await _hobbyService.UpdateAsync(hobby);
                    LoadHobbiesAsync();
                }
            }
        }
        private async void DeleteHobby_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Hobby hobby)
            {
                await _hobbyService.DeleteAsync(hobby);
                LoadHobbiesAsync();
            }
        }

        #endregion

        #region Generic Sync Method
        private async Task SyncEntitiesAsync<T>(ISyncRepository<T> repo, string entityName) where T : BaseEntity
        {
            var unsynced = await repo.GetUnsyncedAsync();

            if (unsynced.Count == 0)
                return;

            using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7010/") };

            var request = new
            {
                EntityType = entityName,
                Data = unsynced 
            };

            var response = await client.PostAsJsonAsync("api/sync", request);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Failed to sync {entityName}: {await response.Content.ReadAsStringAsync()}");
                return;
            }

            await repo.MarkAsSyncedAsync(unsynced);
        }

        private async void SyncAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var coreAssembly = typeof(BaseEntity).Assembly;

                var entityTypes = coreAssembly.GetTypes()
                    .Where(t => typeof(BaseEntity).IsAssignableFrom(t)
                                && !t.IsAbstract
                                && t != typeof(BaseEntity))
                    .ToList();

                foreach (var entityType in entityTypes)
                {
                    string entityName = entityType.Name;

                    var repoType = typeof(ISyncRepository<>).MakeGenericType(entityType);
                    var repo = App.Services.GetService(repoType);

                    if (repo == null)
                    {
                        Console.WriteLine($"No SyncRepository registered for {entityName}");
                        continue;
                    }

                    await ((dynamic)this).SyncEntitiesAsync((dynamic)repo, entityName);
                }

                LoadUsersAsync();
                LoadHobbiesAsync();

                MessageBox.Show("All data synced successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sync failed: {ex.Message}");
            }
        }

        #endregion

        #region TextBox Focus Handlers

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Enter name..." || tb.Text == "0" || tb.Text == "Hobby Name..." || tb.Text == "Description..." || tb.Text == "Proficiency..."))
                tb.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "NameBox") tb.Text = "Enter name...";
                if (tb.Name == "AgeBox") tb.Text = "0";
                if (tb.Name == "HobbyNameBox") tb.Text = "Hobby Name...";
                if (tb.Name == "HobbyDescBox") tb.Text = "Description...";
                if (tb.Name == "HobbyLevelBox") tb.Text = "Proficiency...";
            }
        }

        #endregion
    }
}
