using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(IDocument))]
    public class RecommendViewModel : Screen, IDocument {
        private readonly NovelService _service;
        private readonly CharpterViewModel _charpterViewModel;
        private BindableCollection<Recommend> recommends;

        public string Name {
            get {
                return "热门推荐";
            }
        }

        public string Icon {
            get {
                return string.Empty;
            }
        }

        public bool Show {
            get {
                return true;
            }
        }

        public BindableCollection<Recommend> Recommends {
            get {
                return recommends;
            }

            set {
                recommends = value;
                NotifyOfPropertyChange(nameof(Recommends));
            }
        }

        public int Index {
            get {
                return 0;
            }
        }

        [ImportingConstructor]
        public RecommendViewModel(NovelService service, CharpterViewModel charpterViewModel) {
            recommends = new BindableCollection<Recommend>();
            this._service = service;
            this._charpterViewModel = charpterViewModel;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetRecommendNovel();
            Recommends = new BindableCollection<Recommend>(ret);
            await base.OnActivateAsync(cancellationToken);
        }
      
        /// <summary>
        /// 跳转到章节列表
        /// </summary>
        /// <param name="novel"></param>
        public void ToCharpterOfLeft(Recommend recommend) {
            var novel = recommend.Novels[0];
            this._charpterViewModel.Novel = novel;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._charpterViewModel;
        }

        /// <summary>
        /// 跳转到章节列表
        /// </summary>
        /// <param name="novel"></param>
        public void ToCharpterOfRight(Recommend recommend) {
            var novel = recommend.Novels[1];
            this._charpterViewModel.Novel = novel;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._charpterViewModel;
        }

        /// <summary>
        /// 跳转到章节内容
        /// </summary>
        /// <param name="novel"></param>
        public void ToCharpter(NovelInfo novel) {
            this._charpterViewModel.Novel = novel;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._charpterViewModel;
        }
    }
}
