// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
//
// This file was generated, please do not edit it directly.
//
// Please see MilCodeGen.html for more information.
//


namespace System.Windows
{
    internal static partial class ValidateEnums
    {
        /// <summary>
        ///     Returns whether or not an enumeration instance a valid value.
        ///     This method is designed to be used with ValidateValueCallback, and thus
        ///     matches it's prototype.
        /// </summary>
        /// <param name="valueObject">
        ///     Enumeration value to validate.
        /// </param>    
        /// <returns> 'true' if the enumeration contains a valid value, 'false' otherwise. </returns>
        public static bool IsTextDecorationLocationValid(object valueObject)
        {
            TextDecorationLocation value = (TextDecorationLocation) valueObject;

            return (value == TextDecorationLocation.Underline) || 
                   (value == TextDecorationLocation.OverLine) || 
                   (value == TextDecorationLocation.Strikethrough) || 
                   (value == TextDecorationLocation.Baseline);
        }
    }
}
