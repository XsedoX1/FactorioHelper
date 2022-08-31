﻿using Factorio.Items;
using Factorio.Logic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Factorio.Pages
{


    public sealed partial class AddItemPage : Page
    {

        public AddItemPage()
        {
            InitializeComponent();

        }

        private void Go_Back(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            AddItemPageListViewController.ListOfAvailableItems = MainPageListViewController.ListOfItems;
            AvailableItemsComboBox.ItemsSource = AddItemPageListViewController.ListOfAvailableItems;
            AddedIngredients.ItemsSource = AddItemPageListViewController.ListOfIngredients;

            if (e.Parameter is Item)
            {
                Item item = e.Parameter as Item;


                foreach (var ingredient in item.Ingredients)
                {
                    AddItemPageListViewController.ListOfIngredients.Add(ingredient);
                    AddItemPageListViewController.ListOfAvailableItems.Remove(ingredient.Item);
                }

                NameBox.Text = item.Name;
                TimeBox.Text = item.TimeToCraft.ToString();
                AmountCraftedBox.Text = item.AmountCrafted.ToString();
                AddEditItemButton.Content = "Edit item";
            }

        }


        private void Add_button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (!int.TryParse((ItemAmountBox.Text), out int AmountNeededInt) || string.IsNullOrEmpty(ItemAmountBox.Text))
            {
                IngredientAmountBoxTip.IsOpen = true;
            }
            else if (!double.TryParse(TimeBox.Text, out double time) || string.IsNullOrEmpty(TimeBox.Text))
            {
                TimeBoxTip.IsOpen = true;
            }
            else if (AvailableItemsComboBox.SelectedIndex == -1)
            {
                ComboBoxTip.IsOpen = true;
            }
            else
            {
                AddItemPageListViewController.Add_Button(AvailableItemsComboBox.SelectedItem as Item,
                                AmountNeededInt,
                                time);
                ItemAmountBox.Text = "";
            }
        }


        private void Remove_Ingredient_Button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            Ingredient ingredient = button.DataContext as Ingredient;
            if (ingredient == null) return;
            AddItemPageListViewController.Remove_Ingredient_Button(ingredient);
        }

        private void Add_Whole_Item_Button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                NameBoxTip.IsOpen = true;
            }
            else if (!int.TryParse((AmountCraftedBox.Text), out int AmountCraftedInt) || string.IsNullOrEmpty(AmountCraftedBox.Text))
            {
                AmountCraftedBoxTip.IsOpen = true;
            }
            else if (!double.TryParse(TimeBox.Text, out double time) || string.IsNullOrEmpty(TimeBox.Text))
            {
                TimeBoxTip.IsOpen = true;
            }
            else
            {
                Item item = new Item(NameBox.Text, time, AmountCraftedInt, AddItemPageListViewController.ListOfIngredients);

                MainPageListViewController.AddEdit_Whole_Item(item);

                this.Frame.GoBack();

            }
        }
    }
}