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
    public abstract partial class PathSegment : Animatable
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
        public new PathSegment Clone()
        {
            return (PathSegment)base.Clone();
        }

        /// <summary>
        ///     Shadows inherited CloneCurrentValue() with a strongly typed
        ///     version for convenience.
        /// </summary>
        public new PathSegment CloneCurrentValue()
        {
            return (PathSegment)base.CloneCurrentValue();
        }




        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------




        #region Public Properties

        /// <summary>
        ///     IsStroked - bool.  Default value is true.
        /// </summary>
        public bool IsStroked
        {
            get
            {
                return (bool)GetValue(IsStrokedProperty);
            }
            set
            {
                SetValueInternal(IsStrokedProperty, BooleanBoxes.Box(value));
            }
        }

        /// <summary>
        ///     IsSmoothJoin - bool.  Default value is false.
        /// </summary>
        public bool IsSmoothJoin
        {
            get
            {
                return (bool)GetValue(IsSmoothJoinProperty);
            }
            set
            {
                SetValueInternal(IsSmoothJoinProperty, BooleanBoxes.Box(value));
            }
        }

        #endregion Public Properties

        //------------------------------------------------------
        //
        //  Protected Methods
        //
        //------------------------------------------------------

        #region Protected Methods





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
        ///     The DependencyProperty for the PathSegment.IsStroked property.
        /// </summary>
        public static readonly DependencyProperty IsStrokedProperty;
        /// <summary>
        ///     The DependencyProperty for the PathSegment.IsSmoothJoin property.
        /// </summary>
        public static readonly DependencyProperty IsSmoothJoinProperty;

        #endregion Dependency Properties

        //------------------------------------------------------
        //
        //  Internal Fields
        //
        //------------------------------------------------------

        #region Internal Fields





        internal const bool c_IsStroked = true;
        internal const bool c_IsSmoothJoin = false;

        #endregion Internal Fields



        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        static PathSegment()
        {
            // We check our static default fields which are of type Freezable
            // to make sure that they are not mutable, otherwise we will throw
            // if these get touched by more than one thread in the lifetime
            // of your app.


            // Initializations
            Type typeofThis = typeof(PathSegment);
            IsStrokedProperty =
                  RegisterProperty("IsStroked",
                                   typeof(bool),
                                   typeofThis,
                                   true,
                                   null,
                                   null,
                                   /* isIndependentlyAnimated  = */ false,
                                   /* coerceValueCallback */ null);
            IsSmoothJoinProperty =
                  RegisterProperty("IsSmoothJoin",
                                   typeof(bool),
                                   typeofThis,
                                   false,
                                   null,
                                   null,
                                   /* isIndependentlyAnimated  = */ false,
                                   /* coerceValueCallback */ null);
        }



        #endregion Constructors
    }
}
