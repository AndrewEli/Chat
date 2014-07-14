﻿using System.Collections.Generic;
using System.Windows;
using ChatClient.ViewMediator;
using log4net;
using SharedClasses.Domain;

namespace ChatClient.ViewModels
{
    /// <summary>
    /// Holds the active conversation windows for the user. Can request to create a new conversation window, or change the status of a window.
    /// </summary>
    internal static class WindowManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (WindowManager));

        private static readonly Dictionary<int, WindowStatus> ConversationWindowStatusesIndexedByConversationId = new Dictionary<int, WindowStatus>();

        /// <summary>
        /// Creates a new chat window if the current chat window is closed.
        /// </summary>
        /// <param name="conversation">The conversation id of the chat window.</param>
        public static void CreateConversationWindow(Conversation conversation)
        {
            // Check if conversation window already exists
            if (GetWindowStatus(conversation.ConversationId) == WindowStatus.Closed)
            {
                Application.Current.Dispatcher.Invoke(
                    () => Mediator.Instance.SendMessage(ViewName.ChatWindow, new ChatWindowViewModel.ChatWindowViewModel(conversation)));

                SetWindowStatus(conversation.ConversationId, WindowStatus.Open);
                Log.DebugFormat("Window with conversation Id {0} has been created.", conversation.ConversationId);
            }
            else
            {
                Log.DebugFormat("Window with conversation Id {0} has already been created. Cannot create another.", conversation.ConversationId);
            }
        }

        /// <summary>
        /// Sets the current status of the conversation window, if no conversation is found, create one.
        /// </summary>
        /// <param name="conversationId">The conversation's ID relating to the window.</param>
        /// <param name="windowStatus">The status of the conversation window we want to set.</param>
        public static void SetWindowStatus(int conversationId, WindowStatus windowStatus)
        {
            if (!ConversationWindowStatusesIndexedByConversationId.ContainsKey(conversationId))
            {
                ConversationWindowStatusesIndexedByConversationId.Add(conversationId, windowStatus);
            }
            else
            {
                ConversationWindowStatusesIndexedByConversationId[conversationId] = windowStatus;
            }
        }

        /// <summary>
        /// Gets the current window status of the conversation window. If no status is found, return closed.
        /// </summary>
        /// <param name="conversationId">The conversation's ID being queried</param>
        /// <returns>The window status of the selected conversation window</returns>
        private static WindowStatus GetWindowStatus(int conversationId)
        {
            WindowStatus windowStatus;

            bool isFound = ConversationWindowStatusesIndexedByConversationId.TryGetValue(conversationId, out windowStatus);

            return isFound ? windowStatus : WindowStatus.Closed;
        }
    }
}