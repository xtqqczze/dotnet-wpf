// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//
//
// Description: 3D positional light base class implementation.
//
//              See spec at http://avalon/medialayer/Specifications/Avalon3D%20API%20Spec.mht
//
//

namespace System.Windows.Media.Media3D
{
    /// <summary>
    ///     Positional lights have a position in space and project their light in all directions.
    ///     The falloff of the light is controlled by attenuation and range properties.
    /// </summary>
    public abstract partial class PointLightBase : Light
    {
        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        // Prevent 3rd parties from extending this abstract base class.
        internal PointLightBase() {}

        #endregion Constructors

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Events
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------
    }
}
