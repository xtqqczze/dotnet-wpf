﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows
{
    /// <summary>
    /// Int32Rect - The primitive which represents an integer rectangle.
    /// </summary>
    public partial struct Int32Rect
    {
        /// <summary>
        /// Constructor which sets the initial values to the values of the parameters.
        /// </summary>
        public Int32Rect(Int32 x,
                    Int32 y,
                    Int32 width,
                    Int32 height)
        {
            _x    = x;
            _y     = y;
            _width   = width;
            _height  = height;
        }

        /// <summary>
        /// Empty - a static property which provides an Empty Int32Rectangle.
        /// </summary>
        public static Int32Rect Empty
        {
            get
            {
                return s_empty;
            }
        }

        /// <summary>
        /// Returns true if this Int32Rect is the Empty integer rectangle.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (_x == 0) && (_y == 0) && (_width == 0) && (_height == 0);
            }
        }

        /// <summary>
        /// Returns true if this Int32Rect has area.
        /// </summary>
        public bool HasArea
        {
            get
            {
                return _width > 0 && _height > 0;
            }
        }

        // Various places use an Int32Rect to specify a dirty rect for a
        // bitmap.  The logic for validation is centralized here.  Note that
        // we could do a much better job of validation, but compatibility
        // concerns prevent this until a side-by-side release.
        internal void ValidateForDirtyRect(string paramName, int width, int height)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(_x, paramName);
            ArgumentOutOfRangeException.ThrowIfNegative(_y, paramName);
            ArgumentOutOfRangeException.ThrowIfNegative(_width, paramName);
            ArgumentOutOfRangeException.ThrowIfNegative(_height, paramName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(_width, width, paramName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(_height, height, paramName);
        }

        private static readonly Int32Rect s_empty = new Int32Rect(0,0,0,0);
}
}
