using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows;

namespace Novel.Framework.ViewModels {
    public class DialogViewModel : Screen {
        private double height=430d;
        private double width=550d;
        private string title="对话框";
        private Thickness padding = new(30);
        private bool showCancelButton = true;
        private bool showConfirmButton = true;
        private bool showApplyButton = false;
        private ResizeMode resizeMode = ResizeMode.NoResize;
        private DialogBase content;

        public double Height {
            get {
                return height;
            }

            set {
                height = value;
                NotifyOfPropertyChange(nameof(Height));
            }
        }

        public double Width {
            get {
                return width;
            }

            set {
                width = value;
                NotifyOfPropertyChange(nameof(Width));
            }
        }

        public string Title {
            get {
                return title;
            }

            set {
                title = value;
                NotifyOfPropertyChange(nameof(Title));
            }
        }

        public Thickness Padding {
            get {
                return padding;
            }

            set {
                padding = value;
                NotifyOfPropertyChange(nameof(Padding));
            }
        }

        public bool ShowCancelButton {
            get {
                return showCancelButton;
            }

            set {
                showCancelButton = value;
                NotifyOfPropertyChange(nameof(ShowCancelButton));
            }
        }

        public bool ShowConfirmButton {
            get {
                return showConfirmButton;
            }

            set {
                showConfirmButton = value;
                NotifyOfPropertyChange(nameof(ShowConfirmButton));
            }
        }

        public bool ShowApplyButton {
            get {
                return showApplyButton;
            }

            set {
                showApplyButton = value;
                NotifyOfPropertyChange(nameof(ShowApplyButton));
            }
        }

        public ResizeMode ResizeMode {
            get {
                return resizeMode;
            }

            set {
                resizeMode = value;
                NotifyOfPropertyChange(nameof(ResizeMode));
            }
        }

        public DialogBase Content {
            get {
                return content;
            }

            set {
                content = value;
                NotifyOfPropertyChange(nameof(Content));
            }
        }


        public Task ConfirmAsync() {
            return content.ConfirmAsync();
        }

        public Task CancelAsync() {
            return content.CancelAsync();
        }

        public Task ApplyAsync() {
            return content.ApplyAsync();
        }

    }
}
