﻿#pragma checksum "..\..\..\..\pages\modal\TempSettingModal.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7DDF072F076EEE5BCFBDD7611F7F1A0A022C7ED8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.pages.modal;
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
using com.ccg.GeckoKit.ctrl;


namespace Pharmacy.INST.DissolutionClient.pages.modal {
    
    
    /// <summary>
    /// TempSettingModal
    /// </summary>
    public partial class TempSettingModal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\pages\modal\TempSettingModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pharmacy.INST.DissolutionClient.pages.modal.TempSettingModal TSM_MW;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\pages\modal\TempSettingModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_TSM_TB_TEMPSETTING;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\pages\modal\TempSettingModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TSM_TB_TEMPSETTING;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\pages\modal\TempSettingModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TSM_TB_STARTHEAT;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\pages\modal\TempSettingModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TSM_TB_ENDHEAT;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DissolutionClient;V1.0.0.0;component/pages/modal/tempsettingmodal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\pages\modal\TempSettingModal.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TSM_MW = ((Pharmacy.INST.DissolutionClient.pages.modal.TempSettingModal)(target));
            
            #line 10 "..\..\..\..\pages\modal\TempSettingModal.xaml"
            this.TSM_MW.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LB_TSM_TB_TEMPSETTING = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TSM_TB_TEMPSETTING = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TSM_TB_STARTHEAT = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\pages\modal\TempSettingModal.xaml"
            this.TSM_TB_STARTHEAT.Click += new System.Windows.RoutedEventHandler(this.TSM_TB_STARTHEAT_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TSM_TB_ENDHEAT = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\pages\modal\TempSettingModal.xaml"
            this.TSM_TB_ENDHEAT.Click += new System.Windows.RoutedEventHandler(this.TSM_TB_ENDHEAT_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
