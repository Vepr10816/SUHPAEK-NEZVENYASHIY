﻿#pragma checksum "..\..\EventRegisterPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B14A9F078FA85620924BD02CD968D428FF3E404E64785AE52EEA83142A7ECB83"
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
    /// EventRegisterPage
    /// </summary>
    public partial class EventRegisterPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel EventTypeCBs;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox One;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Two;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Three;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CharityCB;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SumTB;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ComplectsRBs;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton OptionA;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton OptionB;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton OptionC;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\EventRegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AllSumLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/Carting;component/eventregisterpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EventRegisterPage.xaml"
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
            this.EventTypeCBs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.One = ((System.Windows.Controls.CheckBox)(target));
            
            #line 26 "..\..\EventRegisterPage.xaml"
            this.One.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 26 "..\..\EventRegisterPage.xaml"
            this.One.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Two = ((System.Windows.Controls.CheckBox)(target));
            
            #line 27 "..\..\EventRegisterPage.xaml"
            this.Two.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\EventRegisterPage.xaml"
            this.Two.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Three = ((System.Windows.Controls.CheckBox)(target));
            
            #line 28 "..\..\EventRegisterPage.xaml"
            this.Three.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 28 "..\..\EventRegisterPage.xaml"
            this.Three.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CharityCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.SumTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\EventRegisterPage.xaml"
            this.SumTB.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tb1_PreviewTextNum);
            
            #line default
            #line hidden
            
            #line 38 "..\..\EventRegisterPage.xaml"
            this.SumTB.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.tb1_PastingNum));
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 41 "..\..\EventRegisterPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RegistrationButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 42 "..\..\EventRegisterPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ComplectsRBs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.OptionA = ((System.Windows.Controls.RadioButton)(target));
            
            #line 55 "..\..\EventRegisterPage.xaml"
            this.OptionA.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 55 "..\..\EventRegisterPage.xaml"
            this.OptionA.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.OptionB = ((System.Windows.Controls.RadioButton)(target));
            
            #line 56 "..\..\EventRegisterPage.xaml"
            this.OptionB.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 56 "..\..\EventRegisterPage.xaml"
            this.OptionB.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.OptionC = ((System.Windows.Controls.RadioButton)(target));
            
            #line 57 "..\..\EventRegisterPage.xaml"
            this.OptionC.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            
            #line 57 "..\..\EventRegisterPage.xaml"
            this.OptionC.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_CheckedUnchecked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.AllSumLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

