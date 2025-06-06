// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//
// This file was generated, please do not edit it directly.
//
// Please see MilCodeGen.html for more information.
//

using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.Collections;
using MS.Utility;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Markup;
using System.Windows.Media.Converters;

namespace System.Windows.Media
{
    public sealed partial class QuadraticBezierSegment : PathSegment
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        #region Public Methods

        /// <summary>
        ///     Shadows inherited Clone() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new QuadraticBezierSegment Clone()
        {
            return (QuadraticBezierSegment)base.Clone();
        }

        /// <summary>
        ///     Shadows inherited CloneCurrentValue() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new QuadraticBezierSegment CloneCurrentValue()
        {
            return (QuadraticBezierSegment)base.CloneCurrentValue();
        }




        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------




        #region Public Properties

        /// <summary>
        ///     Point1 - Point.  Default value is new Point().
        /// </summary>
        public Point Point1
        {
            get
            {
                return (Point)GetValue(Point1Property);
            }
            set
            {
                SetValueInternal(Point1Property, value);
            }
        }

        /// <summary>
        ///     Point2 - Point.  Default value is new Point().
        /// </summary>
        public Point Point2
        {
            get
            {
                return (Point)GetValue(Point2Property);
            }
            set
            {
                SetValueInternal(Point2Property, value);
            }
        }

        #endregion Public Properties

        //------------------------------------------------------
        //
        //  Protected Methods
        //
        //------------------------------------------------------

        #region Protected Methods

        /// <summary>
        /// Implementation of <see cref="System.Windows.Freezable.CreateInstanceCore">Freezable.CreateInstanceCore</see>.
        /// </summary>
        /// <returns>The new Freezable.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new QuadraticBezierSegment();
        }



        #endregion ProtectedMethods

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods









        #endregion Internal Methods

        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        #region Internal Properties





        #endregion Internal Properties

        //------------------------------------------------------
        //
        //  Dependency Properties
        //
        //------------------------------------------------------

        #region Dependency Properties

        /// <summary>
        ///     The DependencyProperty for the QuadraticBezierSegment.Point1 property.
        /// </summary>
        public static readonly DependencyProperty Point1Property;
        /// <summary>
        ///     The DependencyProperty for the QuadraticBezierSegment.Point2 property.
        /// </summary>
        public static readonly DependencyProperty Point2Property;

        #endregion Dependency Properties

        //------------------------------------------------------
        //
        //  Internal Fields
        //
        //------------------------------------------------------

        #region Internal Fields





        internal static Point s_Point1 = new Point();
        internal static Point s_Point2 = new Point();

        #endregion Internal Fields



        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        static QuadraticBezierSegment()
        {
            // We check our static default fields which are of type Freezable
            // to make sure that they are not mutable, otherwise we will throw
            // if these get touched by more than one thread in the lifetime
            // of your app.


            // Initializations
            Type typeofThis = typeof(QuadraticBezierSegment);
            Point1Property =
                  RegisterProperty("Point1",
                                   typeof(Point),
                                   typeofThis,
                                   new Point(),
                                   null,
                                   null,
                                   /* isIndependentlyAnimated  = */ false,
                                   /* coerceValueCallback */ null);
            Point2Property =
                  RegisterProperty("Point2",
                                   typeof(Point),
                                   typeofThis,
                                   new Point(),
                                   null,
                                   null,
                                   /* isIndependentlyAnimated  = */ false,
                                   /* coerceValueCallback */ null);
        }



        #endregion Constructors
    }
}
