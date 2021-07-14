using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Novel.Controls.TreeListView {
    public class TreeListView : ListView {

        public static readonly DependencyProperty RootProperty;
        public static readonly DependencyProperty ShowRootProperty;
        public static ResourceKey DefaultItemContainerStyleKey { get; private set; }

        public TreeListViewNode Root {
            get { return (TreeListViewNode)GetValue(RootProperty); }
            set { SetValue(RootProperty, value); }
        }

        public bool ShowRoot {
            get { return (bool)GetValue(ShowRootProperty); }
            set { SetValue(ShowRootProperty, value); }
        }


        static TreeListView() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata(typeof(TreeListView)));
            DefaultItemContainerStyleKey = new ComponentResourceKey(typeof(TreeListView), "DefaultItemContainerStyleKey");
            RootProperty = DependencyProperty.Register("Root", typeof(TreeListViewNode), typeof(TreeListView), new PropertyMetadata(null, OnRootChanged));
            ShowRootProperty = DependencyProperty.Register("ShowRoot", typeof(bool), typeof(TreeListView), new PropertyMetadata(true));
        }
        public TreeListView() {
            SetResourceReference(ItemContainerStyleProperty, DefaultItemContainerStyleKey);
        }
        protected override DependencyObject GetContainerForItemOverride() {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item) {
            return item is TreeListViewItem;
        }

        private static void OnRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is TreeListView tree) {
                tree.ItemsSource = tree.Root.ToList();
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            if (element is TreeListViewItem treeItem) {
                treeItem.Content = item;
            }
            base.PrepareContainerForItemOverride(element, item);
        }

        public void Reload() {
            if (Root is null)
                return;
            var list = Root.ToList();
            if (!ShowRoot && list.Any()) {
                list.RemoveAt(0);
                Root.IsExpanded = true;
                Margin = new Thickness(-15, 0, 0, 0);
            }
            ItemsSource = list;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);
            if (e.Property == ShowRootProperty || e.Property == RootProperty) {
                Reload();
            }
        }
    }
}
