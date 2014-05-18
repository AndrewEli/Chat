﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ChatClient.ViewModels;

namespace ChatClient.Views
{
    /// <summary>
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow
    {
        public UserListWindow()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        /// <summary>
        /// I can't find a good way of doing this directly with MVVM and bindings.
        /// Succumbed to creating the click event in the code-behind and then delegating the work off to the viewmodel.
        /// </summary>
        /// <param name="sender">The textblock clicked on</param>
        /// <param name="e">Mouse events for the selected textblock</param>
        private void OnNewUserSelection(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                var userClicked = (StackPanel) sender;
                var participantId = (int) userClicked.Tag;
                var viewmodel = DataContext as UserListViewModel;

                if (viewmodel != null)
                {
                    UserListViewModel.StartNewSingleUserConversation(participantId);
                }
            }
        }
    }
}