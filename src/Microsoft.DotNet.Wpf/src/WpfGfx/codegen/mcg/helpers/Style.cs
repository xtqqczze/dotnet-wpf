// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


//------------------------------------------------------------------------------
//

//
// Description: Common coding style templates
//

namespace MS.Internal.MilCodeGen.Helpers
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;

    using MS.Internal.MilCodeGen.Runtime;

    public class Style : GeneratorMethods
    {
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        #region Public Methods
        public static void WriteFileHeader(
            FileCodeSink codeSink)
        {
            string ext = Path.GetExtension(codeSink.FileName).ToLower();

            if (ext == ".cpp" || ext == ".h" || ext == ".inl")
            {
                codeSink.WriteBlock(
                    [[inline]]
                        // Licensed to the .NET Foundation under one or more agreements.
                        // The .NET Foundation licenses this file to you under the MIT license.
                        
                        
                        //---------------------------------------------------------------------------
                        
                        //
                        // This file is automatically generated.  Please do not edit it directly.
                        //
                        // File name: [[codeSink.FileName]]
                        //---------------------------------------------------------------------------
                    [[/inline]]
                    );

                if (ext == ".h")
                {
                    codeSink.WriteBlock(
                        [[inline]]
                            #pragma once
                        [[/inline]]
                        );
                }
            }
            else if (ext == ".def")
            {
                codeSink.WriteBlock(
                    [[inline]]
                        ;;---------------------------------------------------------------------------
                        ;; Licensed to the .NET Foundation under one or more agreements.
                        ;; The .NET Foundation licenses this file to you under the MIT license.
                        ;;
                        ;; This file is automatically generated.  Please do not edit it directly.
                        ;;
                        ;; File name: [[codeSink.FileName]]
                        ;;---------------------------------------------------------------------------
                    [[/inline]]
                    );
            }
            else
            {
                Debug.Fail("Style.WriteFileHeader - Unknown file extension: '" + ext + "'");
            }
        }

        public static void WriteIncludePrecomp(CodeSink codeSink)
        {
            codeSink.WriteBlock(
                [[inline]]
                    #include "precomp.hpp"
                [[/inline]]
                );
        }
        #endregion Public Methods


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


    }
}




