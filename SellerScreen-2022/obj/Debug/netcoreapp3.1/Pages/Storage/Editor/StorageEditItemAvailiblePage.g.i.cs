﻿#pragma checksum "..\..\..\..\..\..\Pages\Storage\Editor\StorageEditItemAvailiblePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73601F65B0FADE9DE0D5375BADF67BDC895CC3F3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
using SellerScreen_2022;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SellerScreen_2022 {
    
    
    /// <summary>
    /// StorageEditItemAvailiblePage
    /// </summary>
    public partial class StorageEditItemAvailiblePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\..\Pages\Storage\Editor\StorageEditItemAvailiblePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AvailibleTxt;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\..\Pages\Storage\Editor\StorageEditItemAvailiblePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ModernWpf.Controls.NumberBox AddBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SellerScreen-2022;component/pages/storage/editor/storageedititemavailiblepage.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Pages\Storage\Editor\StorageEditItemAvailiblePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AvailibleTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.AddBox = ((ModernWpf.Controls.NumberBox)(target));
            
            #line 23 "..\..\..\..\..\..\Pages\Storage\Editor\StorageEditItemAvailiblePage.xaml"
            this.AddBox.ValueChanged += new ModernWpf.TypedEventHandler<ModernWpf.Controls.NumberBox, ModernWpf.Controls.NumberBoxValueChangedEventArgs>(this.NumberBox_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

