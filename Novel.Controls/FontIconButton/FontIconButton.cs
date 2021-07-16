using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Novel.Controls.FontIconButton {
    public class FontIconButton : Control {
        
        public event RoutedEventHandler Click {
            add {
                AddHandler(ClickEvent, value);
            }
            remove {
                RemoveHandler(ClickEvent, value);
            }
        }

        [Browsable(false), Category("Appearance"), ReadOnly(true)]
        public bool IsPressed {
            get { return (bool)GetValue(IsPressedProperty); }
            protected set { SetValue(IsPressedProperty, value); }
        }

        public string UnicodeText {
            get { return (string)GetValue(UnicodeTextProperty); }
            set { SetValue(UnicodeTextProperty, value); }
        }

        public bool ShowText {
            get { return (bool)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            var grid = GetTemplateChild("PART_PANEL") as Grid;
            grid.MouseLeftButtonDown += OnGridMouseLeftButtonDown;
            grid.MouseLeftButtonUp += OnGridMouseLeftButtonUp;
        }

        private void OnGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ReleaseMouseCapture();
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs() { RoutedEvent = ClickEvent, Source = this });
            SetIsPressed(false);
        }

        private void OnGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            CaptureMouse();
            e.Handled = true;
            SetIsPressed(true);
        }

        private void SetIsPressed(bool pressed) {
            if (pressed) {
                SetValue(IsPressedProperty, pressed);
            } else {
                ClearValue(IsPressedProperty);
            }
        }

        public static readonly DependencyProperty IsPressedProperty;
        public static readonly DependencyProperty UnicodeTextProperty;
        public static readonly RoutedEvent ClickEvent;
        public static readonly DependencyProperty ShowTextProperty;
        public static readonly DependencyProperty TextProperty;
        
        static FontIconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconButton), new FrameworkPropertyMetadata(typeof(FontIconButton)));

            IsPressedProperty = DependencyProperty.Register(nameof(IsPressed), typeof(bool), typeof(FontIconButton), new FrameworkPropertyMetadata(false));
            UnicodeTextProperty = DependencyProperty.Register(nameof(UnicodeText), typeof(string), typeof(FontIconButton), new PropertyMetadata(string.Empty));
            ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEvent), typeof(FontIconButton));
            ShowTextProperty = DependencyProperty.Register("ShowText", typeof(bool), typeof(FontIconButton), new PropertyMetadata(true));
            TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(FontIconButton), new PropertyMetadata(string.Empty));
        }

    }
}
