// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
// 
//
// Description: Vector3D collection partial class. 
//
//              See spec at http://avalon/medialayer/Specifications/Avalon3D%20API%20Spec.mht 


using System.IO;
using MS.Internal.Media;

namespace System.Windows.Media.Media3D
{
    public partial class Vector3DCollection
    {
        ///<summary>
        /// Deserialize this object from  BAML binary format.
        ///</summary>
        internal static object DeserializeFrom(BinaryReader reader)
        {
            // Get the size.
            uint count = reader.ReadUInt32() ; 
            
            Vector3DCollection collection = new Vector3DCollection( (int) count) ; 
            
            for ( uint i = 0; i < count ; i ++ ) 
            {
                Vector3D point = new Vector3D(
                                             XamlSerializationHelper.ReadDouble( reader ), 
                                             XamlSerializationHelper.ReadDouble( reader ) , 
                                             XamlSerializationHelper.ReadDouble( reader ) ) ; 

                collection.Add( point );                 
            }

            return collection ; 
        }
}
}
