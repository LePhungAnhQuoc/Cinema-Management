﻿#pragma checksum "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23A62697F81B1C77671444841B549BBABBBE22D7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AnhQuoc_WPF_C1_B1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace AnhQuoc_WPF_C1_B1 {
    
    
    /// <summary>
    /// ucDisplayListPoster
    /// </summary>
    public partial class ucDisplayListPoster : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AnhQuoc_WPF_C1_B1.ucDisplayListPoster ucContainer;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbMovies;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_WPF_C1_B1;component/usercontrols/cashier/ucdisplaylistposter.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ucContainer = ((AnhQuoc_WPF_C1_B1.ucDisplayListPoster)(target));
            
            #line 9 "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml"
            this.ucContainer.Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbMovies = ((System.Windows.Controls.ListBox)(target));
            
            #line 13 "..\..\..\..\UserControls\Cashier\ucDisplayListPoster.xaml"
            this.lbMovies.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbMovies_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

