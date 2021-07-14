using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Novel.Controls.TreeListView {
    public class TreeListViewNode : Control {

        public static readonly RoutedEvent ExpandedChangedEvent;
        private string text;
        private TreeListViewCollection<TreeListViewNode> children;
        private TreeListViewNode nodeParent;
        private bool isExpanded;

        /// <summary>
        /// 当前节点的子节点
        /// </summary>
        public TreeListViewCollection<TreeListViewNode> Children {
            get {
                return children;
            }

            set {
                children = value;
            }
        }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text {
            get {
                return text;
            }

            set {
                text = value;
            }
        }

        /// <summary>
        /// 是否还有子节点, 用于控制伸缩图标
        /// </summary>
        public bool HasItem => Children.Any();

        /// <summary>
        /// 当前节点的父节点
        /// </summary>
        public TreeListViewNode NodeParent {
            get {
                return nodeParent;
            }

            set {
                nodeParent = value;
            }
        }

        /// <summary>
        /// 伸缩事件, 用于出来伸缩界面显示
        /// </summary>
        public event RoutedEventHandler ExpandedChanged {
            add {
                AddHandler(ExpandedChangedEvent, value);
            }
            remove {
                RemoveHandler(ExpandedChangedEvent, value);
            }
        }

        /// <summary>
        /// 当前节点深度
        /// </summary>
        public int Level {
            get {
                if (NodeParent is null)
                    return 0;
                return NodeParent.Level + 1;
            }
        }

        /// <summary>
        /// 当前节点边距
        /// </summary>
        public Thickness LevelPadding {
            get {
                return new Thickness(15 * Level, 0, 0, 0);
            }
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded {
            get {
                return isExpanded;
            }

            set {
                if (isExpanded != value) {
                    isExpanded = value;
                    if (isExpanded) {
                        LoadingChildren();
                    } else {
                        CollapsingChildren();
                    }
                    OnExpandedChanged();
                }
            }
        }

        static TreeListViewNode() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListViewNode), new FrameworkPropertyMetadata(typeof(TreeListViewNode)));
            ExpandedChangedEvent = EventManager.RegisterRoutedEvent(nameof(ExpandedChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeListViewNode));
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parent"></param>
        public TreeListViewNode(string text, TreeListViewNode parent = null) {
            this.text = text;
            nodeParent = parent;
            children = new TreeListViewCollection<TreeListViewNode>();
            
        }

        /// <summary>
        /// 生成界面显示列表
        /// </summary>
        /// <returns></returns>
        internal List<TreeListViewNode> ToList() {
            var ret = new List<TreeListViewNode>();
            ret.Add(this);
            if (IsExpanded && Children != null && Children.Any()) {
                foreach (var child in Children) {
                    var data = child.ToList();
                    ret.AddRange(data);
                }
            }
            return ret;
        }

        /// <summary>
        /// 伸缩完成事件
        /// </summary>
        private void OnExpandedChanged() {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ExpandedChangedEvent, this);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// 加载子节点
        /// </summary>
        protected virtual void LoadingChildren() {

        }

        /// <summary>
        /// 收起子节点
        /// </summary>
        protected virtual void CollapsingChildren() {

        }

        protected virtual void ActiveItem() {
            
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            ActiveItem();
        }
    }
}
