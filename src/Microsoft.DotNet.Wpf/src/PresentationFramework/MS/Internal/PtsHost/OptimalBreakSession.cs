﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


//
// Description: OptimalBreakSession is unmanaged resouce handle to TextParagraphCache
//

using System.Windows.Media.TextFormatting;

namespace MS.Internal.PtsHost
{
    // ----------------------------------------------------------------------
    // Break record for line - holds decoration information
    // ----------------------------------------------------------------------
    internal sealed class OptimalBreakSession : UnmanagedHandle
    {
        // ------------------------------------------------------------------
        // Constructor.
        //
        //      PtsContext - Context
        //      TextParagraphCache - Contained line break
        // ------------------------------------------------------------------
        internal OptimalBreakSession(TextParagraph textParagraph, TextParaClient textParaClient, TextParagraphCache TextParagraphCache, OptimalTextSource optimalTextSource) : base(textParagraph.PtsContext)
        {
            _textParagraph = textParagraph;
            _textParaClient = textParaClient;
            _textParagraphCache = TextParagraphCache;
            _optimalTextSource = optimalTextSource;
        }

        /// <summary>
        /// Dispose the break session / paragraph cache
        /// </summary>
        public override void Dispose()
        {
            try
            {
                _textParagraphCache?.Dispose();

                _optimalTextSource?.Dispose();
            }
            finally
            {
                _textParagraphCache = null;
                _optimalTextSource = null;
            }

            base.Dispose();
        }

        #region Internal Properties

        internal TextParagraphCache TextParagraphCache { get { return _textParagraphCache; } }
        internal TextParagraph      TextParagraph { get { return _textParagraph; } }
        internal TextParaClient     TextParaClient { get { return _textParaClient; } }
        internal OptimalTextSource  OptimalTextSource      { get { return _optimalTextSource; } }

        #endregion Internal Properties


        #region Private Fields
        
        private TextParagraphCache _textParagraphCache;
        private TextParagraph      _textParagraph;
        private TextParaClient     _textParaClient;
        private OptimalTextSource  _optimalTextSource;

        #endregion Private Fields
    }


    // ----------------------------------------------------------------------
    // LineBreakpoint - Unmanaged handle for TextBreakpoint / optimal break session
    // ----------------------------------------------------------------------
    internal sealed class LineBreakpoint : UnmanagedHandle
    {
        // ------------------------------------------------------------------
        // Constructor.
        //
        //      PtsContext - Context
        //      TextBreakpoint - Contained breakpoint
        // ------------------------------------------------------------------
        internal LineBreakpoint(OptimalBreakSession optimalBreakSession, TextBreakpoint textBreakpoint) : base(optimalBreakSession.PtsContext)
        {
            _textBreakpoint = textBreakpoint;
            _optimalBreakSession = optimalBreakSession;
        }

        /// <summary>
        /// Dispose the text breakpoint
        /// </summary>
        public override void Dispose()
        {
            _textBreakpoint?.Dispose();

            base.Dispose();
        }

        #region Internal Properties

        internal OptimalBreakSession OptimalBreakSession { get { return _optimalBreakSession; } }


        #endregion Internal Properties


        #region Private Fields

        private TextBreakpoint _textBreakpoint;
        private OptimalBreakSession _optimalBreakSession;

        #endregion Private Fields
    }
}

