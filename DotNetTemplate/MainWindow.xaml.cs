// MainWindow.xaml.cs


using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NETTemplate.Controls;
using NETTemplate.Services;

namespace NETTemplate
{
    public class MainWindow : CustomWindow
    {
        private readonly CustomListView _listView;
        private readonly ItemService _itemService;
        private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=56U@@6Vg$999tH4sG3mNYEdHVUk@3yBkaHtu3r4g9!EVWM4C^25984692RBSbJF&;Database=dotnettemplatedb";

        public MainWindow() : base("My .NET UI App")
        {
            _listView = new CustomListView { Width = 300, Height = 400 };
            _itemService = new ItemService(ConnectionString);

            LoadItems();

            ConfigureButtons();

            AddControl(_listView, 1, 0);
        }

        private void ConfigureButtons()
        {
            var clickButton = new CustomButton("Click Me", 100, 30);
            clickButton.SetClickAction((s, e) => MessageBox.Show("Button Clicked!", "Info"));
            AddControl(clickButton, 0, 0);

            var addItemButton = new CustomButton("Add Item", 100, 30);
            addItemButton.SetClickAction((s, e) => ShowDialog(new InputDialog("Enter Item Name", "Add Item"), AddItem));
            AddControl(addItemButton, 0, 1);

            var updateItemButton = new CustomButton("Update Item", 100, 30);
            updateItemButton.SetClickAction((s, e) => ShowDialogForUpdate());
            AddControl(updateItemButton, 0, 2);

            var deleteItemButton = new CustomButton("Delete Item", 100, 30);
            deleteItemButton.SetClickAction((s, e) => ShowDeleteDialog());
            AddControl(deleteItemButton, 0, 3);

            var refreshButton = new CustomButton("Refresh", 100, 30);
            refreshButton.SetClickAction((s, e) => LoadItems());
            AddControl(refreshButton, 0, 4);
        }

        private void ShowDialog(InputDialog dialog, Action<string> onConfirm)
        {
            if (dialog.ShowDialog() == true)
            {
                string itemName = dialog.ItemName;
                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    onConfirm(itemName);
                }
            }
        }

        private void ShowDialogForUpdate()
        {
            if (_listView.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to update.", "Warning");
                return;
            }

            if (_listView.SelectedItem is ListViewItem selectedItem)
            {
                string oldName = selectedItem.Content.ToString();
                ShowDialog(new InputDialog("Enter New Item Name", "Update Item", oldName), newName =>
                {
                    if (newName != oldName)
                    {
                        try
                        {
                            _itemService.UpdateItem(oldName, newName);
                            _listView.Items.Remove(_listView.SelectedItem);
                            _listView.AddItem(newName);
                            MessageBox.Show("Item updated successfully!", "Success");
                        }
                        catch (Npgsql.NpgsqlException ex)
                        {
                            MessageBox.Show($"Error updating item: {ex.Message}", "Error");
                        }
                    }
                });
            }
        }

        private void ShowDeleteDialog()
        {
            if (_listView.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.", "Warning");
                return;
            }

            if (_listView.SelectedItem is ListViewItem selectedItem)
            {
                string itemName = selectedItem.Content.ToString();
                if (MessageBox.Show($"Are you sure you want to delete '{itemName}'?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _itemService.DeleteItem(itemName);
                        _listView.Items.Remove(_listView.SelectedItem);
                        MessageBox.Show("Item deleted successfully!", "Success");
                    }
                    catch (Npgsql.NpgsqlException ex)
                    {
                        MessageBox.Show($"Error deleting item: {ex.Message}", "Error");
                    }
                }
            }
        }

        private void AddItem(string itemName)
        {
            try
            {
                _itemService.SaveItem(itemName);
                _listView.AddItem(itemName);
                MessageBox.Show("Item added successfully!", "Success");
            }
            catch (Npgsql.NpgsqlException ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error");
            }
        }

        private void LoadItems()
        {
            _listView.Items.Clear();
            try
            {
                var items = _itemService.GetAllItems();
                foreach (var item in items)
                {
                    _listView.AddItem(item);
                }
            }
            catch (Npgsql.NpgsqlException ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error");
            }
        }
    }
}