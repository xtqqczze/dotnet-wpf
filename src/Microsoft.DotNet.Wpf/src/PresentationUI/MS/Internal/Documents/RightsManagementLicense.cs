// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace MS.Internal.Documents
{
    /// <summary>
    /// RightsManagementLicense is a structure that describes the RM grants given to a
    /// user.  The data in this class is critical for set, so only Critical code can
    /// modify the rights granted or the user to whom they are granted.
    /// </summary>
    internal sealed class RightsManagementLicense
    {
        #region Constructors (none)
        //------------------------------------------------------
        // Constructors (none)
        //------------------------------------------------------

        #endregion Constructors (none)

        #region Internal Methods
        //------------------------------------------------------
        // Internal Methods
        //------------------------------------------------------

        /// <summary>
        /// Adds a permission to the list of permissions saved in the license.
        /// </summary>
        /// <param name="permission">The permission to add.</param>
        internal void AddPermission(RightsManagementPermissions permission)
        {
            _userRights |= permission;
        }

        /// <summary>
        /// Checks whether the license grants the given permission.
        /// </summary>
        /// <param name="permission">The permission to check for</param>
        /// <returns>Whether or not the license grants the permission</returns>
        internal bool HasPermission(RightsManagementPermissions permission)
        {
            return (((_userRights & permission) == permission) && IsLicenseValid);
        }

        /// <summary>
        /// Converts the permissions granted to a DRX Policy class.
        /// </summary>
        /// <returns>The permissions granted, converted to a policy</returns>
        internal RightsManagementPolicy ConvertToPolicy()
        {
            return (RightsManagementPolicy)(_userRights);
        }

        #endregion Internal Methods

        #region Internal Properties
        //------------------------------------------------------
        // Internal Properties
        //------------------------------------------------------

        /// <summary>
        /// Checks the validity dates of the license to see if the license is valid.
        /// </summary>
        /// <returns>Whether or not the license is still valid.</returns>
        internal bool IsLicenseValid
        {
            get
            {
                bool valid = true;

                // Check both whether the "valid from" date is in the future or the
                // "valid until" date is in the past.  In either case the license is not
                // valid, and we must return false.

                if ((ValidFrom.CompareTo(DateTime.UtcNow) > 0) ||
                    (ValidUntil.CompareTo(DateTime.UtcNow) < 0))
                {
                    valid = false;
                }

                return valid;
            }
        }

        /// <summary>
        /// Gets or sets the date from when the license is valid, in the UTC
        /// time zone
        /// </summary>
        internal DateTime ValidFrom
        {
            get { return _validFrom; }

            set { _validFrom = value; }
        }

        /// <summary>
        /// Gets or sets the date until when the license is valid, in the UTC
        /// time zone
        /// </summary>
        internal DateTime ValidUntil
        {
            get { return _validUntil; }

            set { _validUntil = value; }
        }

        /// <summary>
        /// Gets or sets the user for whom this license applies
        /// </summary>
        internal RightsManagementUser LicensedUser
        {
            get { return _user; }

            set { _user = value; }
        }

        /// <summary>
        /// Gets or sets the permissions granted by this license
        /// </summary>
        internal RightsManagementPermissions LicensePermissions
        {
            get { return _userRights; }

            set { _userRights = value; }
        }

        /// <summary>
        /// Gets or sets the name of the person to contact for more rights than
        /// are currently granted to the user.
        /// </summary>
        internal string ReferralInfoName
        {
            get { return _referralInfoName; }

            set { _referralInfoName = value; }
        }

        /// <summary>
        /// Gets or sets a URI to contact for more rights than are currently
        /// granted to the user.
        /// </summary>
        internal Uri ReferralInfoUri
        {
            get { return _referralInfoUri; }

            set { _referralInfoUri = value; }
        }


        #endregion Internal Properties

        #region Private Fields
        //------------------------------------------------------
        // Private Fields
        //------------------------------------------------------

        private DateTime _validFrom;
        private DateTime _validUntil;
        private RightsManagementUser _user;
        private RightsManagementPermissions _userRights;

        /// <summary>
        /// The name of the person to contact for more rights.
        /// </summary>
        private string _referralInfoName;

        /// <summary>
        /// The URI to contact for more rights.
        /// </summary>
        private Uri _referralInfoUri;

        #endregion Private Fields
    }
}
