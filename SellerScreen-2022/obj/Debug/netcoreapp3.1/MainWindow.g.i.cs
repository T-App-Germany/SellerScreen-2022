﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53F7EB51961AADA6C2940530884FA91BFE89D0C2"
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
using SourceChord.FluentWPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : SourceChord.FluentWPF.AcrylicWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SellerScreen_2022.MainWindow acrylicWindow;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ModernWpf.Controls.NavigationView MainNavView;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ModernWpf.Controls.Frame ContentFrame;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AppTitleBar;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LicensUserTxt;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TitleBar;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image WindowIconImg;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WindowTitleTxt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SellerScreen-2022;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.acrylicWindow = ((SellerScreen_2022.MainWindow)(target));
            
            #line 11 "..\..\..\MainWindow.xaml"
            this.acrylicWindow.SizeChanged += new System.Windows.SizeChangedEventHandler(this.acrylicWindow_SizeChanged);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\MainWindow.xaml"
            this.acrylicWindow.AddHandler(ModernWpf.ThemeManager.ActualThemeChangedEvent, new System.Windows.RoutedEventHandler(this.Window_ActualThemeChanged));
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainNavView = ((ModernWpf.Controls.NavigationView)(target));
            
            #line 104 "..\..\..\MainWindow.xaml"
            this.MainNavView.PaneOpening += new ModernWpf.TypedEventHandler<ModernWpf.Controls.NavigationView, object>(this.MainNavView_PaneOpening);
            
            #line default
            #line hidden
            
            #line 104 "..\..\..\MainWindow.xaml"
            this.MainNavView.PaneClosing += new ModernWpf.TypedEventHandler<ModernWpf.Controls.NavigationView, ModernWpf.Controls.NavigationViewPaneClosingEventArgs>(this.MainNavView_PaneClosing);
            
            #line default
            #line hidden
            
            #line 104 "..\..\..\MainWindow.xaml"
            this.MainNavView.SelectionChanged += new ModernWpf.TypedEventHandler<ModernWpf.Controls.NavigationView, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs>(this.MainNavView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ContentFrame = ((ModernWpf.Controls.Frame)(target));
            return;
            case 4:
            this.AppTitleBar = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.LicensUserTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.TitleBar = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.WindowIconImg = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.WindowTitleTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

