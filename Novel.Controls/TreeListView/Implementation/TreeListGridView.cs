using System.Windows;
using System.Windows.Controls;

namespace Novel.Controls {
    public class TreeListGridView : GridView {
        public static ResourceKey ItemContainerStyle { get; private set; }
        static TreeListGridView() {
            ItemContainerStyle = new ComponentResourceKey(typeof(TreeListView), "TreeListGridViewItemContainerStyleKey");
        }

        protected override object ItemContainerDefaultStyleKey {
            get {
                return ItemContainerStyle;
            }
        }
    }
}
