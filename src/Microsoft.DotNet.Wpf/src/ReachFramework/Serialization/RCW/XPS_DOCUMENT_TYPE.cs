// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows.Xps.Serialization.RCW
{
    /// <summary>
    /// RCW for xpsobjectmodel.idl found in Windows SDK
    /// This is generated code with minor manual edits. 
    /// i.  Generate TLB
    ///      MIDL /TLB xpsobjectmodel.tlb xpsobjectmodel.IDL //xpsobjectmodel.IDL found in Windows SDK
    /// ii. Generate RCW in a DLL
    ///      TLBIMP xpsobjectmodel.tlb // Generates xpsobjectmodel.dll
    /// iii.Decompile the DLL and copy out the RCW by hand.
    ///      ILDASM xpsobjectmodel.dll
    /// </summary>

    internal enum XPS_DOCUMENT_TYPE
    {
        XPS_DOCUMENT_TYPE_UNSPECIFIED = 1,
        XPS_DOCUMENT_TYPE_XPS = 2,
        XPS_DOCUMENT_TYPE_OPENXPS = 3,
    }
}
