#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A76D0A026E503E75656527D722A43967BFC36963CC637FFCE516BB773F56B744"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project2;
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


namespace Project2 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewport3D viewport1;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.PerspectiveCamera kamera;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.ModelVisual3D MyModel;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.GeometryModel3D Top;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.ModelVisual3D MyModel2;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.GeometryModel3D Top2;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbrg;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbst;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton vodavoda;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton vodnormal;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox linesshow;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox inactiveLines;
        
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
            System.Uri resourceLocater = new System.Uri("/Project2;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.viewport1 = ((System.Windows.Controls.Viewport3D)(target));
            
            #line 11 "..\..\MainWindow.xaml"
            this.viewport1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.viewport1_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\MainWindow.xaml"
            this.viewport1.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.viewport1_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 13 "..\..\MainWindow.xaml"
            this.viewport1.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.viewport1_MouseUp);
            
            #line default
            #line hidden
            
            #line 14 "..\..\MainWindow.xaml"
            this.viewport1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.viewport1_MouseDown);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            this.viewport1.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ToleTips_MouseRightDown);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MainWindow.xaml"
            this.viewport1.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.viewport1_MouseRightButtonUp);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.viewport1.MouseMove += new System.Windows.Input.MouseEventHandler(this.viewport1_MouseMove);
            
            #line default
            #line hidden
            
            #line 18 "..\..\MainWindow.xaml"
            this.viewport1.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.viewport1_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.kamera = ((System.Windows.Media.Media3D.PerspectiveCamera)(target));
            return;
            case 3:
            this.MyModel = ((System.Windows.Media.Media3D.ModelVisual3D)(target));
            return;
            case 4:
            this.Top = ((System.Windows.Media.Media3D.GeometryModel3D)(target));
            return;
            case 5:
            this.MyModel2 = ((System.Windows.Media.Media3D.ModelVisual3D)(target));
            return;
            case 6:
            this.Top2 = ((System.Windows.Media.Media3D.GeometryModel3D)(target));
            return;
            case 7:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\MainWindow.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.Load_Data);
            
            #line default
            #line hidden
            return;
            case 8:
            this.rbrg = ((System.Windows.Controls.RadioButton)(target));
            
            #line 78 "..\..\MainWindow.xaml"
            this.rbrg.Checked += new System.Windows.RoutedEventHandler(this.rbrg_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.rbst = ((System.Windows.Controls.RadioButton)(target));
            
            #line 81 "..\..\MainWindow.xaml"
            this.rbst.Checked += new System.Windows.RoutedEventHandler(this.rbst_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.vodavoda = ((System.Windows.Controls.RadioButton)(target));
            
            #line 87 "..\..\MainWindow.xaml"
            this.vodavoda.Checked += new System.Windows.RoutedEventHandler(this.linesRB_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.vodnormal = ((System.Windows.Controls.RadioButton)(target));
            
            #line 90 "..\..\MainWindow.xaml"
            this.vodnormal.Checked += new System.Windows.RoutedEventHandler(this.linesRBD_Checked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.linesshow = ((System.Windows.Controls.CheckBox)(target));
            
            #line 98 "..\..\MainWindow.xaml"
            this.linesshow.Unchecked += new System.Windows.RoutedEventHandler(this.showlines);
            
            #line default
            #line hidden
            
            #line 98 "..\..\MainWindow.xaml"
            this.linesshow.Checked += new System.Windows.RoutedEventHandler(this.showlines);
            
            #line default
            #line hidden
            return;
            case 13:
            this.inactiveLines = ((System.Windows.Controls.CheckBox)(target));
            
            #line 106 "..\..\MainWindow.xaml"
            this.inactiveLines.Unchecked += new System.Windows.RoutedEventHandler(this.hidelines);
            
            #line default
            #line hidden
            
            #line 106 "..\..\MainWindow.xaml"
            this.inactiveLines.Checked += new System.Windows.RoutedEventHandler(this.hidelines);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

