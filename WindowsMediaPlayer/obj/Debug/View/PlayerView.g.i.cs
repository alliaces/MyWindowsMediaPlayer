﻿#pragma checksum "..\..\..\View\PlayerView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "194F1B9C0ADFA302B03B782D84229364"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GalaSoft.MvvmLight.Command;
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
using System.Windows.Interactivity;
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
using WindowsMediaPlayer;


namespace WindowsMediaPlayer {
    
    
    /// <summary>
    /// PlayerView
    /// </summary>
    public partial class PlayerView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 567 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image backgroundForDrop;
        
        #line default
        #line hidden
        
        
        #line 577 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelVolume;
        
        #line default
        #line hidden
        
        
        #line 589 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPlay;
        
        #line default
        #line hidden
        
        
        #line 594 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTime;
        
        #line default
        #line hidden
        
        
        #line 595 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderTime;
        
        #line default
        #line hidden
        
        
        #line 602 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonStop;
        
        #line default
        #line hidden
        
        
        #line 603 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton buttonForward;
        
        #line default
        #line hidden
        
        
        #line 604 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton buttonRewind;
        
        #line default
        #line hidden
        
        
        #line 605 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonNext;
        
        #line default
        #line hidden
        
        
        #line 610 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPrev;
        
        #line default
        #line hidden
        
        
        #line 615 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonShuffle;
        
        #line default
        #line hidden
        
        
        #line 623 "..\..\..\View\PlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonRepeat;
        
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
            System.Uri resourceLocater = new System.Uri("/WindowsMediaPlayer;component/view/playerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\PlayerView.xaml"
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
            this.backgroundForDrop = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.labelVolume = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.buttonPlay = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.labelTime = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.sliderTime = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.buttonStop = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.buttonForward = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 8:
            this.buttonRewind = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            return;
            case 9:
            this.buttonNext = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.buttonPrev = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.buttonShuffle = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.buttonRepeat = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
