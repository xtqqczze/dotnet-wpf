// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// 
// Description:
//              This class is used to filter assemblies as they are loaded into an application domain.
//              The intent is to bring the AppDomain down in the case that one of these is on a disallowed list
//              similar to the kill bit for Activex
//

using System;
using System.Windows;
using MS.Internal.PresentationFramework;
using System.Collections.Generic;
using MS.Win32;
using Microsoft.Win32;
using System.Security;
using System.Reflection;
using System.Text;
using MS.Internal.AppModel;
using MS.Internal;
using System.Windows.Resources;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace MS.Internal
{
    internal class AssemblyFilter
    {
        static AssemblyFilter()
        {
            _disallowedListExtracted = false;
            _assemblyList = new System.Collections.Generic.List<string>();
        }

        internal void FilterCallback(Object sender, AssemblyLoadEventArgs args)
        {
            /* This method no longer applies to .NET Core and should be removed. */
        }

        //appends assembly name with file version to generate a unique entry for the assembly lookup process
        private string AssemblyNameWithFileVersion(Assembly a)
        {
            FileVersionInfo fileVersionInfo;
            StringBuilder sb = new StringBuilder(a.FullName);

            fileVersionInfo = FileVersionInfo.GetVersionInfo(a.Location);
            if (fileVersionInfo != null && fileVersionInfo.ProductVersion != null)
            {
                sb.Append(FILEVERSION_STRING + fileVersionInfo.ProductVersion);
            }
            return ((sb.ToString()).ToLower(System.Globalization.CultureInfo.InvariantCulture)).Trim();
        }

        private bool AssemblyOnDisallowedList(String assemblyToCheck)
        {
            bool retVal = false;
            // if the list disallowed list is not populated populate it once
            if (_disallowedListExtracted == false)
            {
                // hit the registry one time and read 
                ExtractDisallowedRegistryList();
                _disallowedListExtracted = true;
            }
            if (_assemblyList.Contains(assemblyToCheck))
            {
                retVal = true;
            }
            return retVal;
        }

        private void ExtractDisallowedRegistryList()
        {
            string[] disallowedAssemblies;
            RegistryKey featureKey;

            // open the key and read the value
            featureKey = Registry.LocalMachine.OpenSubKey(KILL_BIT_REGISTRY_LOCATION);
            if (featureKey != null)
            {
                // Enumerate through all keys and populate dictionary
                disallowedAssemblies = featureKey.GetSubKeyNames();
                // iterate over this list and for each extract the APTCA_FLAG value and set it in the 
                // dictionary
                foreach (string assemblyName in disallowedAssemblies)
                {
                    featureKey = Registry.LocalMachine.OpenSubKey($@"{KILL_BIT_REGISTRY_LOCATION}\{assemblyName}");
                    object keyValue = featureKey.GetValue(SUBKEY_VALUE);
                    // if there exists a value and it is 1 add to hash table
                    if ((keyValue != null) && (int)(keyValue) == 1)
                    {
                        if (!_assemblyList.Contains(assemblyName))
                        {
                            _assemblyList.Add(assemblyName.ToLower(System.Globalization.CultureInfo.InvariantCulture).Trim());
                        }
                    }
                }
            }
        }

        static System.Collections.Generic.List<string> _assemblyList;

        static bool _disallowedListExtracted;

        static readonly object _lock = new object();

        private const string FILEVERSION_STRING = @", FileVersion=";
        // This is the location in the registry where all the keys are stored
        private const string KILL_BIT_REGISTRY_HIVE = @"HKEY_LOCAL_MACHINE\";
        private const string KILL_BIT_REGISTRY_LOCATION = @"Software\Microsoft\.NetFramework\policy\APTCA";
        private const string SUBKEY_VALUE = @"APTCA_FLAG";
    }
}
