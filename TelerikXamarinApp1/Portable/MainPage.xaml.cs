using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TelerikXamarinApp1.Portable
{
    public partial class MainPage : ContentPage
    {
        private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Device.RuntimePlatform == Device.Android)
            {
                if (this.Parent != null)
                {
                    if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                    {
                        this.lastInputMode = GetSoftInputMode();
                    }

                    SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);
                }
                else
                {
                    SetSoftInputMode(this.lastInputMode);
                }
            }
        }

        private static AndroidSpecific.WindowSoftInputModeAdjust GetSoftInputMode()
        {
            return AndroidSpecific.Application.GetWindowSoftInputModeAdjust(Application.Current);
        }

        private static void SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust inputMode)
        {
            AndroidSpecific.Application.SetWindowSoftInputModeAdjust(Application.Current, inputMode);
        }
    }
}
