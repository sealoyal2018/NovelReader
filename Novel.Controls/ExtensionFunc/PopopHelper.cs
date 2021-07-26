using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Novel.Controls
{
    public class PopopHelper
    {
        public static DependencyObject GetPopupPlacementTarget(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(PopupPlacementTargetProperty);
        }

        public static void SetPopupPlacementTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(PopupPlacementTargetProperty, value);
        }

        public static readonly DependencyProperty PopupPlacementTargetProperty =
            DependencyProperty.RegisterAttached("PopupPlacementTarget", typeof(DependencyObject), typeof(PopopHelper), new PropertyMetadata(null, OnPopupPlacementTargetChanged));

        private static void OnPopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                Window w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != w)
                {
                    //让Popup随着窗体的移动而移动
                    w.LocationChanged += delegate
                    {
                        UpdatePosition(pop);
                    };
                    //让Popup随着窗体的Size改变而移动位置
                    w.SizeChanged += delegate
                    {
                        UpdatePosition(pop);
                    };
                }
            }
        }

        private static void UpdatePosition(Popup pop)
        {
            var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            try
            {
                mi.Invoke(pop, null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
