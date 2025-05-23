// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/***************************************************************************\
*
*
*  TargetType event setting class.
*
*
\***************************************************************************/
using System.ComponentModel; // EditorBrowsableAttribute

namespace System.Windows
{
    /// <summary>
    ///     TargetType event setting class.
    /// </summary>
    public class EventSetter : SetterBase
    {
        /// <summary>
        ///     EventSetter construction
        /// </summary>
        public EventSetter()
        {
        }

        /// <summary>
        ///     EventSetter construction
        /// </summary>
        public EventSetter(RoutedEvent routedEvent, Delegate handler)
        {
            ArgumentNullException.ThrowIfNull(routedEvent);
            ArgumentNullException.ThrowIfNull(handler);

            _event = routedEvent;
            _handler = handler;
        }
        
        /// <summary>
        ///    Event that is being set by this setter
        /// </summary>
        public RoutedEvent Event
        {
            get { return _event; }
            set 
            {
                ArgumentNullException.ThrowIfNull(value);

                CheckSealed();
                _event = value; 
            }
        }

        /// <summary>
        ///    Handler delegate that is being set by this setter
        /// </summary>
        [TypeConverter(typeof(System.Windows.Markup.EventSetterHandlerConverter))]
        public Delegate Handler
        {
            get { return _handler; }
            set 
            {
                ArgumentNullException.ThrowIfNull(value);

                CheckSealed();
                _handler = value; 
            }
        }

        /// <summary>
        ///     HandledEventsToo flag that is being set by this setter
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool HandledEventsToo
        {
            get { return _handledEventsToo; }
            set 
            { 
                CheckSealed();
                _handledEventsToo = value; 
            }
        }


        //
        //  Do the error checking that we can only do once all of the properties have been
        //  set, then call up to base.
        //
        
        internal override void Seal()
        {

            if (_event == null)
            {
                throw new ArgumentException(SR.Format(SR.NullPropertyIllegal, "EventSetter.Event"));
            }
            if (_handler == null)
            {
                throw new ArgumentException(SR.Format(SR.NullPropertyIllegal, "EventSetter.Handler"));
            }
            if (_handler.GetType() != _event.HandlerType)
            {
                throw new ArgumentException(SR.HandlerTypeIllegal);
            }

            base.Seal();
            
        }


        private RoutedEvent    _event;
        private Delegate         _handler;
        private bool             _handledEventsToo;
    }
    
}

