﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BizHawk.FreeEnterprise.Companion.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool KeyItemsDisplay {
            get {
                return ((bool)(this["KeyItemsDisplay"]));
            }
            set {
                this["KeyItemsDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool KeyItemsBorder {
            get {
                return ((bool)(this["KeyItemsBorder"]));
            }
            set {
                this["KeyItemsBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Text")]
        public global::BizHawk.FreeEnterprise.Companion.KeyItemStyle KeyItemsStyle {
            get {
                return ((global::BizHawk.FreeEnterprise.Companion.KeyItemStyle)(this["KeyItemsStyle"]));
            }
            set {
                this["KeyItemsStyle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool PartyDisplay {
            get {
                return ((bool)(this["PartyDisplay"]));
            }
            set {
                this["PartyDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool PartyBorder {
            get {
                return ((bool)(this["PartyBorder"]));
            }
            set {
                this["PartyBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Stand")]
        public global::BizHawk.FreeEnterprise.Companion.Sprites.Pose PartyPose {
            get {
                return ((global::BizHawk.FreeEnterprise.Companion.Sprites.Pose)(this["PartyPose"]));
            }
            set {
                this["PartyPose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ObjectivesDisplay {
            get {
                return ((bool)(this["ObjectivesDisplay"]));
            }
            set {
                this["ObjectivesDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ObjectivesBorder {
            get {
                return ((bool)(this["ObjectivesBorder"]));
            }
            set {
                this["ObjectivesBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DockOffset {
            get {
                return ((int)(this["DockOffset"]));
            }
            set {
                this["DockOffset"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Dock {
            get {
                return ((bool)(this["Dock"]));
            }
            set {
                this["Dock"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Right")]
        public global::BizHawk.FreeEnterprise.Companion.DockSide DockSide {
            get {
                return ((global::BizHawk.FreeEnterprise.Companion.DockSide)(this["DockSide"]));
            }
            set {
                this["DockSide"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool LocationsDisplay {
            get {
                return ((bool)(this["LocationsDisplay"]));
            }
            set {
                this["LocationsDisplay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool LocationsBorder {
            get {
                return ((bool)(this["LocationsBorder"]));
            }
            set {
                this["LocationsBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int RefreshEveryNFrames {
            get {
                return ((int)(this["RefreshEveryNFrames"]));
            }
            set {
                this["RefreshEveryNFrames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool PartyAnimate {
            get {
                return ((bool)(this["PartyAnimate"]));
            }
            set {
                this["PartyAnimate"] = value;
            }
        }
    }
}
