// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// Description: The NavigationCommands class defines a standard set of commands that act on Content Navigation
//
//              See spec at : http://avalon/CoreUI/Specs%20%20Eventing%20and%20Commanding/CommandLibrarySpec.mht

using MS.Internal;

namespace System.Windows.Input
{
    /// <summary>
    /// NavigationCommands - Set of Standard Commands
    /// </summary>
    public static class NavigationCommands
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------
        #region Public Methods

        /// <summary>
        /// BrowserBack Command
        /// </summary>
        public static RoutedUICommand BrowseBack
        {
            get { return _EnsureCommand(CommandId.BrowseBack); }
        }

        /// <summary>
        /// BrowserForward Command
        /// </summary>
        public static RoutedUICommand BrowseForward
        {
            get { return _EnsureCommand(CommandId.BrowseForward); }
        }

        /// <summary>
        /// BrowseHome Command
        /// </summary>
        public static RoutedUICommand BrowseHome
        {
            get { return _EnsureCommand(CommandId.BrowseHome); }
        }

        /// <summary>
        /// BrowseStop Command
        /// </summary>
        public static RoutedUICommand BrowseStop
        {
            get { return _EnsureCommand(CommandId.BrowseStop); }
        }

        /// <summary>
        /// Refresh Command
        /// </summary>
        public static RoutedUICommand Refresh
        {
            get { return _EnsureCommand(CommandId.Refresh); }
        }

        /// <summary>
        /// Favorites Command
        /// </summary>
        public static RoutedUICommand Favorites
        {
            get { return _EnsureCommand(CommandId.Favorites); }
        }

        /// <summary>
        /// Search Command
        /// </summary>
        public static RoutedUICommand Search
        {
            get { return _EnsureCommand(CommandId.Search); }
        }


        /// <summary>
        /// IncreaseZoom Command
        /// </summary>
        public static RoutedUICommand IncreaseZoom
        {
            get { return _EnsureCommand(CommandId.IncreaseZoom); }
        }

        /// <summary>
        /// DecreaseZoom Command
        /// </summary>
        public static RoutedUICommand DecreaseZoom
        {
            get { return _EnsureCommand(CommandId.DecreaseZoom); }
        }

        /// <summary>
        /// Zoom Command
        /// </summary>
        public static RoutedUICommand Zoom
        {
            get { return _EnsureCommand(CommandId.Zoom); }
        }

        /// <summary>
        /// NextPage Command
        /// </summary>
        public static RoutedUICommand NextPage
        {
            get {return _EnsureCommand(CommandId.NextPage);}
        }

        /// <summary>
        /// PreviousPage Command
        /// </summary>
        public static RoutedUICommand PreviousPage
        {
            get {return _EnsureCommand(CommandId.PreviousPage);}
        }

        /// <summary>
        /// FirstPage Command
        /// </summary>
        public static RoutedUICommand FirstPage
        {
            get {return _EnsureCommand(CommandId.FirstPage);}
        }

        /// <summary>
        /// LastPage Command
        /// </summary>
        public static RoutedUICommand LastPage
        {
            get {return _EnsureCommand(CommandId.LastPage);}
        }

        /// <summary>
        /// GoToPage Command
        /// </summary>
        public static RoutedUICommand GoToPage
        {
            get {return _EnsureCommand(CommandId.GoToPage);}
        }

        /// <summary>
        /// NavigateJournal command.
        /// </summary>
        public static RoutedUICommand NavigateJournal
        {
            get { return _EnsureCommand(CommandId.NavigateJournal); }
        }

        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------
        #region Private Methods


        private static string GetPropertyName(CommandId commandId)
        {
            string propertyName = String.Empty;

            switch (commandId)
            {
                case CommandId.BrowseBack: propertyName = "BrowseBack"; break;
                case CommandId.BrowseForward: propertyName = "BrowseForward"; break;
                case CommandId.BrowseHome: propertyName = "BrowseHome"; break;
                case CommandId.BrowseStop: propertyName = "BrowseStop"; break;
                case CommandId.Refresh: propertyName = "Refresh"; break;
                case CommandId.Favorites: propertyName = "Favorites"; break;
                case CommandId.Search: propertyName = "Search"; break;
                case CommandId.IncreaseZoom: propertyName = "IncreaseZoom"; break;
                case CommandId.DecreaseZoom: propertyName = "DecreaseZoom"; break;
                case CommandId.Zoom: propertyName = "Zoom"; break;
                case CommandId.NextPage: propertyName = "NextPage"; break;
                case CommandId.PreviousPage: propertyName = "PreviousPage"; break;
                case CommandId.FirstPage: propertyName = "FirstPage"; break;
                case CommandId.LastPage: propertyName = "LastPage"; break;
                case CommandId.GoToPage: propertyName = "GoToPage"; break;
                case CommandId.NavigateJournal: propertyName = "NavigateJournal"; break;
            }
            return propertyName;
        }

        internal static string GetUIText(byte commandId)
        {
            string uiText = String.Empty;

            switch ((CommandId)commandId)
            {
                case  CommandId.BrowseBack: uiText = SR.BrowseBackText; break;
                case  CommandId.BrowseForward: uiText = SR.BrowseForwardText; break;
                case  CommandId.BrowseHome: uiText = SR.BrowseHomeText; break;
                case  CommandId.BrowseStop: uiText = SR.BrowseStopText; break;
                case  CommandId.Refresh: uiText = SR.RefreshText; break;
                case  CommandId.Favorites: uiText = SR.FavoritesText; break;
                case  CommandId.Search: uiText = SR.SearchText; break;
                case  CommandId.IncreaseZoom: uiText = SR.IncreaseZoomText; break;
                case  CommandId.DecreaseZoom: uiText = SR.DecreaseZoomText; break;
                case  CommandId.Zoom: uiText = SR.ZoomText; break;
                case  CommandId.NextPage: uiText = SR.NextPageText; break;
                case  CommandId.PreviousPage: uiText = SR.PreviousPageText; break;
                case  CommandId.FirstPage: uiText = SR.FirstPageText; break;
                case  CommandId.LastPage: uiText = SR.LastPageText; break;
                case  CommandId.GoToPage: uiText = SR.GoToPageText; break;
                case  CommandId.NavigateJournal: uiText = SR.NavigateJournalText; break;
            }
            return uiText;
        }

        internal static InputGestureCollection LoadDefaultGestureFromResource(byte commandId)
        {
            InputGestureCollection gestures = new InputGestureCollection();

            //Standard Commands
            switch ((CommandId)commandId)
            {
                case  CommandId.BrowseBack:
                    KeyGesture.AddGesturesFromResourceStrings(
                        BrowseBackKey,
                        SR.BrowseBackKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.BrowseForward:
                    KeyGesture.AddGesturesFromResourceStrings(
                        BrowseForwardKey,
                        SR.BrowseForwardKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.BrowseHome:
                    KeyGesture.AddGesturesFromResourceStrings(
                        BrowseHomeKey,
                        SR.BrowseHomeKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.BrowseStop:
                    KeyGesture.AddGesturesFromResourceStrings(
                        BrowseStopKey,
                        SR.BrowseStopKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.Refresh:
                    KeyGesture.AddGesturesFromResourceStrings(
                        RefreshKey,
                        SR.RefreshKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.Favorites:
                    KeyGesture.AddGesturesFromResourceStrings(
                        FavoritesKey,
                        SR.FavoritesKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.Search:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SearchKey,
                        SR.SearchKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.IncreaseZoom:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.IncreaseZoomKey,
                        SR.IncreaseZoomKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.DecreaseZoom:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.DecreaseZoomKey,
                        SR.DecreaseZoomKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.Zoom:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.ZoomKey,
                        SR.ZoomKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.NextPage:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.NextPageKey,
                        SR.NextPageKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.PreviousPage:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.PreviousPageKey,
                        SR.PreviousPageKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.FirstPage:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.FirstPageKey,
                        SR.FirstPageKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.LastPage:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.LastPageKey,
                        SR.LastPageKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.GoToPage:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.GoToPageKey,
                        SR.GoToPageKeyDisplayString,
                        gestures);
                    break;
                case  CommandId.NavigateJournal:
                    KeyGesture.AddGesturesFromResourceStrings(
                        SR.NavigateJournalKey,
                        SR.NavigateJournalKeyDisplayString,
                        gestures);
                    break;
            }
            return gestures;
        }

        private static RoutedUICommand _EnsureCommand(CommandId idCommand)
        {
            if (idCommand >= 0 && idCommand < CommandId.Last)
            {
                lock (_internalCommands.SyncRoot)
                {
                    if (_internalCommands[(int)idCommand] == null)
                    {
                        RoutedUICommand newCommand = CommandLibraryHelper.CreateUICommand(
                                                            GetPropertyName(idCommand),
                                                            typeof(NavigationCommands), (byte)idCommand);

                        _internalCommands[(int)idCommand] = newCommand;
                    }
                }
                return _internalCommands[(int)idCommand];
            }
            return null;
        }
        #endregion Private Methods

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------
        #region Private Fields
        // these constants will go away in future, its just to index into the right one.
        private enum CommandId : byte
        {
            // Formatting
            BrowseBack = 1,
            BrowseForward = 2,
            BrowseHome = 3,
            BrowseStop = 4,
            Refresh = 5,
            Favorites = 6,
            Search = 7,
            IncreaseZoom = 8,
            DecreaseZoom = 9,
            Zoom = 10,
            NextPage = 11,
            PreviousPage = 12,
            FirstPage = 13,
            LastPage = 14,
            GoToPage = 15,
            NavigateJournal = 16,
            // Last
            Last = 17
        }

        private static RoutedUICommand[] _internalCommands = new RoutedUICommand[(int)CommandId.Last];
        #endregion Private Fields

        private const string BrowseBackKey = "Alt+Left;Backspace";
        private const string BrowseForwardKey = "Alt+Right;Shift+Backspace";
        private const string BrowseHomeKey = "Alt+Home;BrowserHome";
        private const string BrowseStopKey = "Alt+Esc;BrowserStop";
        private const string FavoritesKey = "Ctrl+I";
        private const string RefreshKey = "F5";
        private const string SearchKey = "F3";
    }
}
