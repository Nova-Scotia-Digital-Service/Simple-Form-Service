﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleFormsService.Web.Public.Forms.Submission {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class StringResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SimpleFormsService.Web.Public.Forms.Submission.StringResource", typeof(StringResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All finished, back to start.
        /// </summary>
        public static string BackToStart {
            get {
                return ResourceManager.GetString("BackToStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Print a copy.
        /// </summary>
        public static string Btn_Print {
            get {
                return ResourceManager.GetString("Btn_Print", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save a copy.
        /// </summary>
        public static string Btn_Save {
            get {
                return ResourceManager.GetString("Btn_Save", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Submission successful.
        /// </summary>
        public static string PageTitle {
            get {
                return ResourceManager.GetString("PageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You may also wish to print or save a copy of this page for your files..
        /// </summary>
        public static string Save_Print {
            get {
                return ResourceManager.GetString("Save_Print", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your submission ID is: &lt;strong&gt;{0}&lt;/strong&gt;..
        /// </summary>
        public static string SubmissionID {
            get {
                return ResourceManager.GetString("SubmissionID", resourceCulture);
            }
        }
    }
}