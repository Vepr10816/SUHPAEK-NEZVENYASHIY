#pragma checksum "..\..\PastRaceResults.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5B58F4DE51C5491C6A274FC664770AEEDFCBC25BDA9AB95358F6FD2033A31D27"
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
    /// PastRaceResults
    /// </summary>
    public partial class PastRaceResults : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridResult;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn pos;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtRacerAllCount;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtRacerFinishCount;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTime;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbEvent;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbTrackTye;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbGender;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbAge;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\PastRaceResults.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
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
            System.Uri resourceLocater = new System.Uri("/Carting;component/pastraceresults.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PastRaceResults.xaml"
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
            this.gridResult = ((System.Windows.Controls.DataGrid)(target));
            
            #line 36 "..\..\PastRaceResults.xaml"
            this.gridResult.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.gridResult_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.pos = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 3:
            this.txtRacerAllCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtRacerFinishCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.cmbEvent = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.cmbTrackTye = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.cmbGender = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.cmbAge = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\PastRaceResults.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.btnSearch_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

