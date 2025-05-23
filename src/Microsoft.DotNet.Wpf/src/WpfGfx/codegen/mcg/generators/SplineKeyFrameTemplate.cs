// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


//---------------------------------------------------------------------------
//

//
// Description: This file contains the definition of template-based generation of 
//              the type-specific Spline keyframes (SplineByteKeyFrame, etc)
//              
//---------------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

using MS.Internal.MilCodeGen;
using MS.Internal.MilCodeGen.Runtime;
using MS.Internal.MilCodeGen.ResourceModel;
using MS.Internal.MilCodeGen.Helpers;

namespace MS.Internal.MilCodeGen.ResourceModel
{
    /// <summary>
    /// SplineKeyFrameTemplate: This class represents one instantiation of the SplineKeyFrame template.
    /// Due to a limitation of the build system, SplineKeyFrame classes are coalesced into one file
    /// per module.
    /// </summary>
    public class SplineKeyFrameTemplate: Template
    {
        private struct SplineKeyFrameTemplateInstance
        {
            public SplineKeyFrameTemplateInstance(
                string moduleName,
                string typeName
                )
            {
                ModuleName = moduleName;
                TypeName = typeName;
            }

            public string ModuleName;
            public string TypeName;
        }

        /// <summary>
        /// AddTemplateInstance - This is called by the code which parses the generation control.
        /// It is called on each TemplateInstance XMLNode encountered.
        /// </summary>
        public override void AddTemplateInstance(ResourceModel resourceModel, XmlNode node)
        {
            Instances.Add(new SplineKeyFrameTemplateInstance(
                ResourceModel.ToString(node, "ModuleName"),
                ResourceModel.ToString(node, "TypeName")));
        }

        public override void Go(ResourceModel resourceModel)
        {
            FileCodeSink csFile = null;
            string currentModuleName = null;

            foreach (SplineKeyFrameTemplateInstance instance in Instances)
            {
                string extraInterpolateArgs = "";

                //
                // If we've hit a new module we need to close off the current file
                // and make a new one.
                //
                if (instance.ModuleName != currentModuleName)
                {
                    currentModuleName = instance.ModuleName;
                    CloseFile(ref csFile);
                    ProcessNewModule(resourceModel, currentModuleName, ref csFile);
                }


                // AnimatedTypeHelpers.Interpolate has an extra parameter
                // for the Quaternion type
                if (instance.TypeName == "Quaternion")
                {
                    extraInterpolateArgs = ", UseShortestPath";
                }


                //
                // Write the typed class for the current instance
                //

                csFile.WriteBlock(
                    [[inline]]

                            /// <summary>
                            /// This class is used as part of a [[instance.TypeName]]KeyFrameCollection in
                            /// conjunction with a KeyFrame[[instance.TypeName]]Animation to animate a
                            /// [[instance.TypeName]] property value along a set of key frames.
                            ///
                            /// This [[instance.TypeName]]KeyFrame interpolates between the [[instance.TypeName]] Value of
                            /// the previous key frame and its own Value to produce its output value.
                            /// </summary>
                            public partial class Spline[[instance.TypeName]]KeyFrame : [[instance.TypeName]]KeyFrame
                            {
                                #region Constructors
                                
                                /// <summary>
                                /// Creates a new Spline[[instance.TypeName]]KeyFrame.
                                /// </summary>
                                public Spline[[instance.TypeName]]KeyFrame()
                                    : base()
                                {
                                }
                                
                                /// <summary>
                                /// Creates a new Spline[[instance.TypeName]]KeyFrame.
                                /// </summary>
                                public Spline[[instance.TypeName]]KeyFrame([[instance.TypeName]] value)
                                    : this()
                                {
                                    Value = value;
                                }
                                
                                /// <summary>
                                /// Creates a new Spline[[instance.TypeName]]KeyFrame.
                                /// </summary>
                                public Spline[[instance.TypeName]]KeyFrame([[instance.TypeName]] value, KeyTime keyTime)
                                    : this()
                                {
                                    Value = value;
                                    KeyTime = keyTime;
                                }
                                
                                /// <summary>
                                /// Creates a new Spline[[instance.TypeName]]KeyFrame.
                                /// </summary>
                                public Spline[[instance.TypeName]]KeyFrame([[instance.TypeName]] value, KeyTime keyTime, KeySpline keySpline)
                                    : this()
                                {
                                    ArgumentNullException.ThrowIfNull(keySpline);
                                                
                                    Value = value;
                                    KeyTime = keyTime;
                                    KeySpline = keySpline;
                                }
                                        
                                #endregion
                                
                                #region Freezable
                                
                                /// <summary>
                                /// Implementation of <see cref="System.Windows.Freezable.CreateInstanceCore">Freezable.CreateInstanceCore</see>.
                                /// </summary>
                                /// <returns>The new Freezable.</returns>
                                protected override Freezable CreateInstanceCore()
                                {
                                    return new  Spline[[instance.TypeName]]KeyFrame();
                                }

                                #endregion
                                
                                #region [[instance.TypeName]]KeyFrame
                                
                                /// <summary>
                                /// Implemented to linearly interpolate between the baseValue and the
                                /// Value of this KeyFrame using the keyFrameProgress.
                                /// </summary>
                                protected override [[instance.TypeName]] InterpolateValueCore([[instance.TypeName]] baseValue, double keyFrameProgress)
                                {
                                    if (keyFrameProgress == 0.0)
                                    {
                                        return baseValue;
                                    }
                                    else if (keyFrameProgress == 1.0)
                                    {
                                        return Value;
                                    }
                                    else
                                    {
                                        double splineProgress = KeySpline.GetSplineProgress(keyFrameProgress);
                                        
                                        return AnimatedTypeHelpers.Interpolate[[instance.TypeName]](baseValue, Value, splineProgress[[extraInterpolateArgs]]);
                                    }
                                }
                                
                                #endregion
                                
                                #region Public Properties
                                
                                /// <summary>
                                /// KeySpline Property
                                /// </summary>
                                public static readonly DependencyProperty KeySplineProperty =
                                    DependencyProperty.Register(
                                        "KeySpline",
                                        typeof(KeySpline),
                                        typeof(Spline[[instance.TypeName]]KeyFrame),
                                        new PropertyMetadata(new KeySpline()));

                                /// <summary>
                                /// The KeySpline defines the way that progress will be altered for this
                                /// key frame.
                                /// </summary>
                                public KeySpline KeySpline
                                {
                                    get
                                    {
                                        return (KeySpline)GetValue(KeySplineProperty);
                                    }
                                    set
                                    {
                                        ArgumentNullException.ThrowIfNull(value);
                                        SetValue(KeySplineProperty, value);
                                    }
                                }
                               
                                #endregion
                            }

                    [[/inline]]
                    );
                
            }

            // Done writing the last module; close the file
            CloseFile(ref csFile);
        }

        // Creates a new file for the module and writes the the beginning of the file (comment header, 
        // using statements, namespace declaration, etc
        private void ProcessNewModule(ResourceModel resourceModel, string moduleName, ref FileCodeSink csFile)
        {
            string fileName = "SplineKeyFrames.cs";
            string path = null;
            string fullPath = null;
            string moduleReference = null;

            //
            // Create a new file
            //

            path = "src\\" + moduleName + "\\System\\Windows\\Media\\Animation\\Generated";
            fullPath = Path.Combine(resourceModel.OutputDirectory, path);

            // Duplicate AnimatedTypeHelpers class across Core/Framework causes name conflicts,
            // requiring that they be split across two namespaces.
            switch (moduleName)
            {
                case @"PresentationCore":
                    moduleReference = "using MS.Internal.PresentationCore;";
                    break;
                case "PresentationFramework":
                    moduleReference = "using MS.Internal.PresentationFramework;";
                    break;
            }

            csFile = new FileCodeSink(fullPath, fileName, true /* Create dir if necessary */);

            //
            //  Write the file preamble
            //

            csFile.WriteBlock(
                [[inline]]
                    [[Helpers.ManagedStyle.WriteFileHeader(fileName)]]

                    using MS.Internal;

                    using System.Collections;
                    using System.ComponentModel;
                    using System.Windows.Media;
                    using System.Windows.Media.Media3D;

                    [[moduleReference]]

                    namespace System.Windows.Media.Animation
                    {
                [[/inline]]
                );

        }

        private void CloseFile(ref FileCodeSink csFile)
        {
            if (csFile != null)
            {
                // Write the closing brace of the namespace block
                csFile.WriteBlock("}");
                csFile.Dispose();
                csFile = null;
            }
        }

        private List<SplineKeyFrameTemplateInstance> Instances = new List<SplineKeyFrameTemplateInstance>();
    }
}


