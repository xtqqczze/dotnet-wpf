﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
    /// <summary>
    /// Class for Xaml markup extension for Arrays.
    /// </summary>
    [TypeForwardedFrom("PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    [ContentProperty("Items")]
    [MarkupExtensionReturnType(typeof(Array))]
    public class ArrayExtension : MarkupExtension
    {
        private readonly ArrayList _arrayList = new ArrayList();

        /// <summary>
        /// Constructor that takes no parameters. This creates an empty array.
        /// </summary>
        public ArrayExtension()
        {
        }

        /// <summary>
        /// Constructor that takes one parameter. This initializes the type of the array.
        /// </summary>
        public ArrayExtension(Type arrayType)
        {
            Type = arrayType ?? throw new ArgumentNullException(nameof(arrayType));
        }

        /// <summary>
        /// Constructor for writing
        /// </summary>
        /// <param name="elements">The array to write</param>
        public ArrayExtension(Array elements)
        {
            ArgumentNullException.ThrowIfNull(elements);

            _arrayList.AddRange(elements);
            Type = elements.GetType().GetElementType();
        }

        /// <summary>
        /// Called to Add an object as a new array item. This will append the
        /// object to the end of the array.
        /// </summary>
        /// <param name="value">Object to add to the end of the array.</param>
        public void AddChild(object value) => _arrayList.Add(value);

        /// <summary>
        /// Called to Add a text as a new array item. This will append the
        /// object to the end of the array.
        /// </summary>
        /// <param name="text">Text to Add to the end of the array.</param>
        public void AddText(string text) => AddChild(text);

        ///<summary>
        /// Get and set the type of array to be created when calling ProvideValue
        /// </summary>
        [ConstructorArgument("type")]
        public Type Type { get; set; }

        /// <summary>
        /// An IList accessor to the contents of the array
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IList Items => _arrayList;

        /// <summary>
        /// Return an array that is sized to the number of objects added to the ArrayExtension.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>The Array containing all the objects added to this extension.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type is null)
            {
                throw new InvalidOperationException(SR.MarkupExtensionArrayType);
            }

            try
            {
                return _arrayList.ToArray(Type);
            }
            catch (InvalidCastException)
            {
                // If an element was added to the ArrayExtension that does not agree with the
                // ArrayType, then an InvalidCastException will occur.
                // Generate a more meaningful error for this case.
                throw new InvalidOperationException(SR.Format(SR.MarkupExtensionArrayBadType, Type.Name));
            }
        }
    }
}
