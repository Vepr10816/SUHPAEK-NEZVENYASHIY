#pragma checksum "..\..\EditUsers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EDCB7259D07F2A2782375FE24A3B150CAF0096D934A1B491316455F92B1DE6C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Carting;
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


namespace Carting {
    
    
    /// <summary>
    /// EditUsers
    /// </summary>
    public partial class EditUsers : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cmbEmail;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cmbFiltrPoRolyam;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cmbOtsortir;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPoisk;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pas;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pas2;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddNew;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\EditUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
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
            System.Uri resourceLocater = new System.Uri("/Carting;component/editusers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditUsers.xaml"
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
            this.cmbEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cmbFiltrPoRolyam = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\EditUsers.xaml"
            this.cmbFiltrPoRolyam.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.cmbFiltrPoRolyam_Pasting));
            
            #line default
            #line hidden
            
            #line 25 "..\..\EditUsers.xaml"
            this.cmbFiltrPoRolyam.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.cmbFiltrPoRolyam_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbOtsortir = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\EditUsers.xaml"
            this.cmbOtsortir.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.cmbOtsortir_Pasting));
            
            #line default
            #line hidden
            
            #line 26 "..\..\EditUsers.xaml"
            this.cmbOtsortir.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.cmbOtsortir_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbPoisk = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.pas = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\EditUsers.xaml"
            this.pas.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.pas_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.pas2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnAddNew = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\EditUsers.xaml"
            this.btnAddNew.Click += new System.Windows.RoutedEventHandler(this.btnAddNew_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\EditUsers.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

