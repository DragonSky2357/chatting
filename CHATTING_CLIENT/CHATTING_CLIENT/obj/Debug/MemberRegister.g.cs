﻿#pragma checksum "..\..\MemberRegister.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C479D9A486E0690CDFBCAC9CB5F0149ADCD53A7CFBCA0297ED882A18DC8AB9AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using CHATTING_CLIENT;
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


namespace CHATTING_CLIENT {
    
    
    /// <summary>
    /// MemberRegister
    /// </summary>
    public partial class MemberRegister : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDText;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PasswordText;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PasswordCheckText;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PasswordReCheck;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnMen;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnWomen;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmergencyEmail;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FirstPhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RestPhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\MemberRegister.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSend;
        
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
            System.Uri resourceLocater = new System.Uri("/CHATTING_CLIENT;component/memberregister.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MemberRegister.xaml"
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
            
            #line 8 "..\..\MemberRegister.xaml"
            ((CHATTING_CLIENT.MemberRegister)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ScreenMove);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 11 "..\..\MemberRegister.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Minimize_Window);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 12 "..\..\MemberRegister.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Maximize_Window);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 13 "..\..\MemberRegister.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_Window);
            
            #line default
            #line hidden
            return;
            case 5:
            this.IDText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.PasswordText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.PasswordCheckText = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\MemberRegister.xaml"
            this.PasswordCheckText.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordCheckText_KeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PasswordReCheck = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.BtnMen = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.BtnWomen = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.EmergencyEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.FirstPhoneNumber = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 14:
            this.RestPhoneNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.BtnSend = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

