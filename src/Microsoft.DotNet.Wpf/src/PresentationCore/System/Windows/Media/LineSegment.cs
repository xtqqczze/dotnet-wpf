// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//

namespace System.Windows.Media
{
    /// <summary>
    /// LineSegment
    /// </summary>
    public sealed partial class LineSegment : PathSegment
    {
        #region Constructors

        /// <summary>
        ///
        /// </summary>
        public LineSegment() 
        {
        }

        /// <summary>
        ///
        /// </summary>
        public LineSegment(Point point, bool isStroked)
        {
            Point = point;
            IsStroked = isStroked;
        }

        // Internal constructor supporting smooth joins between segments
        internal LineSegment(Point point, bool isStroked, bool isSmoothJoin)
        {
            Point = point;
            IsStroked = isStroked;
            IsSmoothJoin = isSmoothJoin;
        }

        #endregion

        #region AddToFigure
        internal override void AddToFigure(
            Matrix matrix,          // The transformation matrid
            PathFigure figure,      // The figure to add to
            ref Point current)      // Out: Segment endpoint, not transformed
        {
            current = Point;

            if (matrix.IsIdentity)
            {
                figure.Segments.Add(this);
            }
            else
            {
                Point pt = current;
                pt *= matrix;
                figure.Segments.Add(new LineSegment(pt, IsStroked, IsSmoothJoin));
            }
        }
        #endregion

        /// <summary>
        /// SerializeData - Serialize the contents of this Segment to the provided context.
        /// </summary>
        internal override void SerializeData(StreamGeometryContext ctx)
        {
            ctx.LineTo(Point, IsStroked, IsSmoothJoin);
        }
        
        internal override bool IsCurved()
        {
            return false;
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string 
        /// and IFormatProvider passed in.  
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            return "L" + ((IFormattable)Point).ToString(format, provider);
        }
    }
}

