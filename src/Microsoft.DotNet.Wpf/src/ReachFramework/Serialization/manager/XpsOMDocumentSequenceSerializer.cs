// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows.Xps.Serialization
{
    /// <summary>
    /// Class defining common functionality required to
    /// serialize a Document Sequence.
    /// </summary>
    internal class XpsOMDocumentSequenceSerializer :
                   ReachSerializer
    {
        public
        XpsOMDocumentSequenceSerializer(
            PackageSerializationManager manager
            ) :
            base(manager)
        {
            ///
            /// Fail if manager is not XpsOMSerializationManager
            /// 
            _xpsOMSerializationManager = (XpsOMSerializationManager)manager;
        }

        internal
        override
        void
        PersistObjectData(
            SerializableObjectContext serializableObjectContext
            )
        {
            BeginPersistObjectData(serializableObjectContext);

            if (serializableObjectContext.IsComplexValue)
            {
                SerializeObjectCore(serializableObjectContext);
            }

            EndPersistObjectData();
        }

        internal
        void
        BeginPersistObjectData(
            SerializableObjectContext serializableObjectContext
            )
        {
            _xpsOMSerializationManager.RegisterDocumentSequenceStart();
            _xpsOMSerializationManager.EnsureXpsOMPackageWriter();
        }

        internal
        void
        EndPersistObjectData(
            )
        {
            _xpsOMSerializationManager.ReleaseXpsOMWriterForFixedDocumentSequence();

            //
            // Signal to any registered callers that the Sequence has been serialized
            //
            XpsSerializationProgressChangedEventArgs progressEvent =
            new XpsSerializationProgressChangedEventArgs(XpsWritingProgressChangeLevel.FixedDocumentSequenceWritingProgress,
                                                         0,
                                                         0,
                                                         null);

            
            _xpsOMSerializationManager.RegisterDocumentSequenceEnd();
            _xpsOMSerializationManager.OnXPSSerializationProgressChanged(progressEvent);
        }

        private XpsOMSerializationManager _xpsOMSerializationManager;
    };
}


