﻿#pragma checksum "..\..\..\..\pages\modal\SampleTimeModal.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "864E6256F250DF1A1AF61921539851223F2A087A"
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
    /// SampleTimeModal
    /// </summary>
    public partial class SampleTimeModal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pharmacy.INST.DissolutionClient.pages.modal.SampleTimeModal STM_MW;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MainCanvas;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SampleTime_DataGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_SaveSampleTime;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_SaveSampleTime_Return;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label STM_SAMPLE_TIMES;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label STM_SAMPLE_POS_TIME;
        
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
            System.Uri resourceLocater = new System.Uri("/DissolutionClient;V1.0.0.0;component/pages/modal/sampletimemodal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
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
            this.STM_MW = ((Pharmacy.INST.DissolutionClient.pages.modal.SampleTimeModal)(target));
            
            #line 10 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
            this.STM_MW.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.SampleTime_DataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 12 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
            this.SampleTime_DataGrid.KeyDown += new System.Windows.Input.KeyEventHandler(this.SampleTime_DataGrid_KeyDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
            this.SampleTime_DataGrid.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.SampleTime_DataGrid_CellEditEnding);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Btn_SaveSampleTime = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
            this.Btn_SaveSampleTime.Click += new System.Windows.RoutedEventHandler(this.Btn_SaveSampleTime_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Btn_SaveSampleTime_Return = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\pages\modal\SampleTimeModal.xaml"
            this.Btn_SaveSampleTime_Return.Click += new System.Windows.RoutedEventHandler(this.Btn_SaveSampleTime_Return_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.STM_SAMPLE_TIMES = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.STM_SAMPLE_POS_TIME = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
