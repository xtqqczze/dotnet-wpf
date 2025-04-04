// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/***************************************************************************\
*
*
*  Class for Xaml markup extension for static resource references.
*
*
\***************************************************************************/
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Windows
{
    /// <summary>
    ///  Class for Xaml markup extension for ColorConvertedBitmap with non-embedded profile.
    /// </summary>
    [MarkupExtensionReturnType(typeof(ColorConvertedBitmap))]
    public class ColorConvertedBitmapExtension : MarkupExtension
    {
        /// <summary>
        ///  Constructor that takes no parameters
        /// </summary>
        public ColorConvertedBitmapExtension()
        {
        }
        
        /// <summary>
        ///  Constructor that takes the markup for a "{ColorConvertedBitmap image source.icc destination.icc}"
        /// </summary>
        public ColorConvertedBitmapExtension(
            object image) 
        {
            ArgumentNullException.ThrowIfNull(image);

            string[] tokens = ((string)image).Split(' ');

            foreach (string str in tokens)
            {
                if (str.Length > 0)
                {
                    if (_image == null)
                    {
                        _image = str;
                    }
                    else if (_sourceProfile == null)
                    {
                        _sourceProfile = str;
                    }
                    else if (_destinationProfile == null)
                    {
                        _destinationProfile = str;
                    }
                    else
                    {
                        throw new InvalidOperationException(SR.ColorConvertedBitmapExtensionSyntax);
                    }
                }
            }
        }
        
        /// <summary>
        ///  Return an object that should be set on the targetObject's targetProperty
        ///  for this markup extension.  For ColorConvertedBitmapExtension, this is the object found in
        ///  a resource dictionary in the current parent chain that is keyed by ResourceKey
        /// </summary>
        /// <returns>
        ///  The object to set on this property.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_image == null)
            {
                throw new InvalidOperationException(SR.ColorConvertedBitmapExtensionNoSourceImage);
            }
            if (_sourceProfile == null)
            {
                throw new InvalidOperationException(SR.ColorConvertedBitmapExtensionNoSourceProfile);
            }
            
            // [BreakingChange] 
            // (NullReferenceException in ColorConvertedBitmapExtension.ProvideValue)
            // We really should throw an ArgumentNullException here for serviceProvider.

            // Save away the BaseUri.
            IUriContext uriContext = serviceProvider.GetService(typeof(IUriContext)) as IUriContext;
            if( uriContext == null )
            {
                throw new InvalidOperationException(SR.Format(SR.MarkupExtensionNoContext, GetType().Name, "IUriContext" ));
            }
            _baseUri = uriContext.BaseUri;
            

            Uri imageUri = GetResolvedUri(_image);
            Uri sourceProfileUri = GetResolvedUri(_sourceProfile);
            Uri destinationProfileUri = GetResolvedUri(_destinationProfile);

            ColorContext sourceContext = new ColorContext(sourceProfileUri);
            ColorContext destinationContext = destinationProfileUri != null ?
                                                new ColorContext(destinationProfileUri) :
                                                new ColorContext(PixelFormats.Default);

            BitmapDecoder decoder = BitmapDecoder.Create(
                                                                imageUri,
                                                                BitmapCreateOptions.IgnoreColorProfile | BitmapCreateOptions.IgnoreImageCache,
                                                                BitmapCacheOption.None
                                                                );

            BitmapSource bitmap = decoder.Frames[0];
            FormatConvertedBitmap formatConverted = new FormatConvertedBitmap(bitmap, PixelFormats.Bgra32, null, 0.0);

            object result = formatConverted;

            try
            {
                ColorConvertedBitmap colorConverted = new ColorConvertedBitmap(formatConverted, sourceContext, destinationContext, PixelFormats.Bgra32);
                result= colorConverted;
            }
            catch (FileFormatException)
            {   // Gracefully ignore non-matching profile
                // If the file contains a bad color context, we catch the exception here
                // since color transform isn't possible
                // with the given color context.
            }

            return result;
        }

        private Uri GetResolvedUri(string uri)
        {
            if (uri == null)
            {
                return null;
            }

            return new Uri(_baseUri,uri);
        }

        private string _image;
        private string _sourceProfile;
        private Uri _baseUri;
        private string _destinationProfile;
    }
}

