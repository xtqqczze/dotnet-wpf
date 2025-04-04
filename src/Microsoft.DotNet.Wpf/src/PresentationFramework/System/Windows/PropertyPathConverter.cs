﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
// Description: Contains converter for creating PropertyPath from string
//              and saving PropertyPath to string
//

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Windows.Markup;
using MS.Internal.Data;

namespace System.Windows
{
    /// <summary>
    /// PropertyPathConverter - Converter class for converting instances of other types
    /// to and from PropertyPath instances.
    /// </summary>
    public sealed class PropertyPathConverter: TypeConverter
    {
        //-------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------

#region Public Methods

        /// <summary>
        /// CanConvertFrom - Returns whether or not this class can convert from a given type.
        /// </summary>
        /// <returns>
        /// bool - True if this converter can convert from the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="sourceType"> The Type being queried for support. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type sourceType)
        {
            // We can only handle strings
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// CanConvertTo - Returns whether or not this class can convert to a given type.
        /// </summary>
        /// <returns>
        /// bool - True if this converter can convert to the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="destinationType"> The Type being queried for support. </param>
        public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type destinationType)
        {
            // We can convert to a string.
            if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ConvertFrom - Attempt to convert to a PropertyPath from the given object
        /// </summary>
        /// <returns>
        /// The PropertyPath which was constructed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the example object is not null and is not a valid type
        /// which can be converted to a PropertyPath.
        /// </exception>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="cultureInfo"> The CultureInfo which is respected when converting. </param>
        /// <param name="source"> The object to convert to a PropertyPath. </param>
        public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext,
                                           CultureInfo cultureInfo,
                                           object source)
        {
            ArgumentNullException.ThrowIfNull(source);

            if (source is string)
            {
                return new PropertyPath((string)source, typeDescriptorContext);
            }

            throw new ArgumentException(SR.Format(SR.CannotConvertType, source.GetType().FullName, typeof(PropertyPath)));
        }

        /// <summary>
        /// ConvertTo - Attempt to convert a PropertyPath to the given type
        /// </summary>
        /// <returns>
        /// The object which was constructed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the example object is not null and is not a Brush,
        /// or if the destinationType isn't one of the valid destination types.
        /// </exception>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call.
        ///  If this is null, then no namespace prefixes will be included.</param>
        /// <param name="cultureInfo"> The CultureInfo which is respected when converting. </param>
        /// <param name="value"> The PropertyPath to convert. </param>
        /// <param name="destinationType">The type to which to convert the PropertyPath instance. </param>
        public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext,
                                         CultureInfo cultureInfo,
                                         object value,
                                         Type destinationType)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(destinationType);

            if (destinationType != typeof(String))
            {
                throw new ArgumentException(SR.Format(SR.CannotConvertType, typeof(PropertyPath), destinationType.FullName));
            }

            PropertyPath path = value as PropertyPath;
            if (path == null)
            {
                throw new ArgumentException(SR.Format(SR.UnexpectedParameterType, value.GetType(), typeof(PropertyPath)), nameof(value));
            }

            if (path.PathParameters.Count == 0)
            {
                // if the path didn't use paramaters, just write it out as it is
                return path.Path;
            }
            else
            {
                // if the path used parameters, convert them to (NamespacePrefix:OwnerType.DependencyPropertyName) syntax
                string originalPath = path.Path;
                Collection<object> parameters = path.PathParameters;
                XamlDesignerSerializationManager manager = typeDescriptorContext == null ?
                                                                null :
                                                                typeDescriptorContext.GetService(typeof(XamlDesignerSerializationManager)) as XamlDesignerSerializationManager;
                ValueSerializer typeSerializer = null;
                IValueSerializerContext serializerContext = null;
                if (manager == null)
                {
                    serializerContext = typeDescriptorContext as IValueSerializerContext;
                    if (serializerContext != null)
                    {
                        typeSerializer = ValueSerializer.GetSerializerFor(typeof(Type), serializerContext);
                    }
                }

                StringBuilder builder = new StringBuilder();

                int start = 0;
                for (int i=0; i<originalPath.Length; ++i)
                {
                    // look for (n)
                    if (originalPath[i] == '(')
                    {
                        int j;
                        for (j=i+1; j<originalPath.Length; ++j)
                        {
                            if (originalPath[j] == ')')
                                break;
                        }

                        int index;
                        if (Int32.TryParse( originalPath.AsSpan(i+1, j-i-1),
                                            NumberStyles.Integer,
                                            TypeConverterHelper.InvariantEnglishUS.NumberFormat,
                                            out index))
                        {
                            // found (n). Write out the path so far, including the opening (
                            builder.Append(originalPath.AsSpan(start, i-start+1));

                            object pathPart = parameters[index];

                            // get the owner type and name of the accessor
                            DependencyProperty dp;
                            PropertyInfo pi;
                            PropertyDescriptor pd;
                            DynamicObjectAccessor doa;
                            PropertyPath.DowncastAccessor(pathPart, out dp, out pi, out pd, out doa);

                            Type type;         // the accessor's ownerType, or type of indexer parameter
                            string name;        // the accessor's propertyName, or string value of indexer parameter

                            if (dp != null)
                            {
                                type = dp.OwnerType;
                                name = dp.Name;
                            }
                            else if (pi != null)
                            {
                                type = pi.DeclaringType;
                                name = pi.Name;
                            }
                            else if (pd != null)
                            {
                                type = pd.ComponentType;
                                name = pd.Name;
                            }
                            else if (doa != null)
                            {
                                type = doa.OwnerType;
                                name = doa.PropertyName;
                            }
                            else
                            {
                                // pathPart is an Indexer Parameter
                                type = pathPart.GetType();
                                name = null;
                            }

                            // write out the type of the accessor or index parameter
                            if (typeSerializer != null)
                            {
                                builder.Append(typeSerializer.ConvertToString(type, serializerContext));
                            }
                            else
                            {
                                // Need the prefix here
                                string prefix = null;
                                if (prefix != null && prefix != string.Empty)
                                {
                                    builder.Append(prefix);
                                    builder.Append(':');
                                }
                                builder.Append(type.Name);
                            }

                            if (name != null)
                            {
                                // write out the accessor name
                                builder.Append('.');
                                builder.Append(name);
                                // write out the closing )
                                builder.Append(')');
                            }
                            else
                            {
                                // write out the ) that closes the parameter's type
                                builder.Append(')');

                                name = pathPart as string;
                                if (name == null)
                                {
                                    // convert the parameter into string
                                    TypeConverter converter = TypeDescriptor.GetConverter(type);
                                    if (converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
                                    {
                                        try
                                        {
                                            name = converter.ConvertToString(pathPart);
                                        }
                                        catch (NotSupportedException)
                                        {
                                        }
                                    }
                                }

                                // write out the parameter's value string
                                builder.Append(name);
                            }

                            // resume after the (n)
                            i = j;
                            start = j+1;
                        }
                    }
                }

                if (start < originalPath.Length)
                {
                    builder.Append(originalPath.AsSpan(start));
                }

                return builder.ToString();
            }

        }
#endregion

    }
}
