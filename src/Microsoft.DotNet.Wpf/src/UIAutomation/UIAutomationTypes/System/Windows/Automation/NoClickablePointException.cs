// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// Description: Exception indicating that a clickable point could not be found

using System.Runtime.Serialization;

namespace System.Windows.Automation
{
    /// <summary>
    /// The exception that is thrown when an error occurs within a AutomationElement.GetClickablePoint.
    /// When the bounding rect is empty, has no width or heigth or the or the AutomationElement at that point
    /// is not the same is one it was called on
    /// </summary>  
    [Serializable]
#if (INTERNAL_COMPILE)
    internal class NoClickablePointException : Exception
#else
    public class NoClickablePointException : Exception
#endif
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NoClickablePointException() {}
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        public NoClickablePointException(String message) : base(message) {}
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public  NoClickablePointException(string message, Exception innerException) : base(message, innerException) {}
        
        /// <internalonly>
        /// Constructor for serialization
        /// </internalonly>
#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected NoClickablePointException(SerializationInfo info, StreamingContext context) : base(info, context) {}
#pragma warning restore SYSLIB0051 // Type or member is obsolete

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
#pragma warning disable CS0672 // Member overrides obsolete member
#pragma warning disable SYSLIB0051 // Type or member is obsolete
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
#pragma warning restore CS0672 // Member overrides obsolete member
    }
}

