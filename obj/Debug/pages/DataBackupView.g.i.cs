﻿#pragma checksum "..\..\..\pages\DataBackupView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BFDAE95B57CE48E49BAB0FAFEC1DD79EF46BB9B6"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Pharmacy.INST.DissolutionClient.pages;
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


namespace Pharmacy.INST.DissolutionClient.pages {
    
    
    /// <summary>
    /// DataBackupView
    /// </summary>
    public partial class DataBackupView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DBV_TB_BACKUPDIR;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DBV_BTN_BACKUP_EXPLORER;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DBV_BTN_BACKUP;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DBV_TB_RESTOREFILE;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DBV_BTN_RESTORE_EXPLORER;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\pages\DataBackupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DBV_BTN_RESTORE;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DissolutionClient;component/pages/databackupview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pages\DataBackupView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\pages\DataBackupView.xaml"
            ((Pharmacy.INST.DissolutionClient.pages.DataBackupView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DBV_TB_BACKUPDIR = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.DBV_BTN_BACKUP_EXPLORER = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\pages\DataBackupView.xaml"
            this.DBV_BTN_BACKUP_EXPLORER.Click += new System.Windows.RoutedEventHandler(this.DBV_BTN_BACKUP_EXPLORER_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DBV_BTN_BACKUP = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\pages\DataBackupView.xaml"
            this.DBV_BTN_BACKUP.Click += new System.Windows.RoutedEventHandler(this.DBV_BTN_BACKUP_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DBV_TB_RESTOREFILE = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.DBV_BTN_RESTORE_EXPLORER = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\pages\DataBackupView.xaml"
            this.DBV_BTN_RESTORE_EXPLORER.Click += new System.Windows.RoutedEventHandler(this.DBV_BTN_RESTORE_EXPLORER_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DBV_BTN_RESTORE = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\pages\DataBackupView.xaml"
            this.DBV_BTN_RESTORE.Click += new System.Windows.RoutedEventHandler(this.DBV_BTN_RESTORE_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

