using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(IDocument))]
    public class BeautifulViewModel : Screen, IDocument {
        private readonly NovelService _service;
        private BindableCollection<NovelInfo> novels;
        private NovelListViewModel novelList;
        private HelloViewModel helloViewModel;

        public string Name {
            get {
                return "唯美纯爱";
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

        public int Index {
            get {
                return 1;
            }
        }

        public BindableCollection<NovelInfo> Novels {
            get {
                return novels;
            }

            set {
                novels = value;
                NotifyOfPropertyChange(nameof(Novels));
            }
        }

        public NovelListViewModel NovelList {
            get {
                return novelList;
            }

            set {
                novelList = value;
                NotifyOfPropertyChange(nameof(NovelListViewModel));
            }
        }

        public HelloViewModel HelloViewModel {
            get {
                return helloViewModel;
            }

            set {
                helloViewModel = value;
            }
        }


        [ImportingConstructor]
        public BeautifulViewModel(NovelService service) {
            novels = new BindableCollection<NovelInfo>() {
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
                new NovelInfo {
                    Href = "/book/164873/",
                    ImageSource = "https://www.23qb.net/files/article/image/164/164873/164873s.jpg",
                    Type = "言情女生",
                    UpDateTime = "2021-05-30",
                    Title = "都市无敌战神",
                    Summary = "五年前，被陷害入狱！五年后，他荣耀归来，天下权势，尽握手中！我所失去的，终会千百倍的拿回来！…",
                    Status = "连载中",
                    WordCount = "4074653",
                    LastCharpter = new NovelCharpter {
                        Title = "第1736章 震惊遗落大陆",
                        Href = "/book/164873/84252144.html",
                    }
                },
            };
            novelList = new NovelListViewModel(novels);
            helloViewModel = new HelloViewModel();
            this._service = service;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetHotNovels(NovelType.Romance);
            this.Novels = new BindableCollection<NovelInfo>(ret);
            await  base.OnActivateAsync(cancellationToken);
        }
    }
}
