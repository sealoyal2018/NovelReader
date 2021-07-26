using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Novel.Controls
{
    public class FontIconButton : ButtonBase {

        public string UnicodeText {
            get { return (string)GetValue(UnicodeTextProperty); }
            set { SetValue(UnicodeTextProperty, value); }
        }

        public bool ShowContent {
            get { return (bool)GetValue(ShowContentProperty); }
            set { SetValue(ShowContentProperty, value); }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            var grid = GetTemplateChild("PART_PANEL") as Grid;
            grid.MouseLeftButtonDown += OnGridMouseLeftButtonDown;
        }
        private void OnGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            RaiseEvent(new RoutedEventArgs() { RoutedEvent = ClickEvent, Source = this });
        }

        public static readonly DependencyProperty UnicodeTextProperty;
        public static readonly DependencyProperty ShowContentProperty;
        public static readonly DependencyProperty TextProperty;
        
        static FontIconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconButton), new FrameworkPropertyMetadata(typeof(FontIconButton)));
            UnicodeTextProperty = DependencyProperty.Register(nameof(UnicodeText), typeof(string), typeof(FontIconButton), new PropertyMetadata(string.Empty));
            ShowContentProperty = DependencyProperty.Register(nameof(ShowContent), typeof(bool), typeof(FontIconButton), new PropertyMetadata(true));
        }

    }
}
