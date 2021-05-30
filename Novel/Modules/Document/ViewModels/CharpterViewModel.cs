using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(CharpterViewModel))]
    [Export(typeof(IDocument))]
    public class CharpterViewModel : Screen, IDocument {
        /// <summary>
        /// 小说服务
        /// </summary>
        private readonly NovelService _service;

        /// <summary>
        /// 当前小说章节列表
        /// </summary>
        private BindableCollection<NovelCharpter> charpters;

        /// <summary>
        /// 当前小说信息
        /// </summary>
        private NovelInfo novel;

        public string Name {
            get {
                return "章节列表";
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
                return 7;
            }
        }

        public BindableCollection<NovelCharpter> Charpters {
            get {
                return charpters;
            }

            set {
                charpters = value;
                NotifyOfPropertyChange(nameof(Charpters));
            }
        }

        public NovelInfo Novel {
            get {
                return novel;
            }

            set {
                novel = value;
                NotifyOfPropertyChange(nameof(Novel));
            }
        }

        [ImportingConstructor]
        public CharpterViewModel(NovelService service) {
            this._service = service;
            novel = new NovelInfo {
                Author = "柒月甜" ,
                ImageSource = "https://www.23qb.net/files/article/image/146/146152/146152s.jpg",
                Title = "逆天神医妃",
                Status = "连载",
                Summary = "看着面前步步逼近的男人，萧沐凌屏住呼吸，却移不开那双被美色所迷的眼睛。",
                Type = "言情女生",
                UpDateTime = "2021-05-30",
            };
            charpters = new BindableCollection<NovelCharpter>() {
                new NovelCharpter{Href="/book/146152/57582682.html", Title="第1章 被预言的天才！"},
                new NovelCharpter{Href="/book/146152/57582683.html", Title="第2章 他，该由我来杀！"},
                new NovelCharpter{Href="/book/146152/57582686.html", Title="第3章 不生气，不着急"},
                new NovelCharpter{Href="/book/146152/57582687.html", Title="第4章 女儿奴"},
                new NovelCharpter{Href="/book/146152/57582688.html", Title="第5章 她，害怕他？"},
                new NovelCharpter{Href="/book/146152/57582689.html", Title="第6章 神秘空间"},
                new NovelCharpter{Href="/book/146152/57582690.html", Title="第7章 修灵！"},
                new NovelCharpter{Href="/book/146152/57582691.html", Title="第8章 最好是不要再遇到他！"},
                new NovelCharpter{Href="/book/146152/57582692.html", Title="第9章 只有他一个召唤师！"},
                new NovelCharpter{Href="/book/146152/57582693.html", Title="第10章 杀人灭口！"},
                new NovelCharpter{Href="/book/146152/57582694.html", Title="第11章 萧清，你怎么看？"},
                new NovelCharpter{Href="/book/146152/57582695.html", Title="第12章 杀了人还推给萧沐凌！"},
                new NovelCharpter{Href="/book/146152/57582696.html", Title="第13章 按照族规受罚！"},
                new NovelCharpter{Href="/book/146152/57582697.html", Title="第14章 只是杀几个人罢了"},
                new NovelCharpter{Href="/book/146152/57582698.html", Title="第15章 总要有一个开端！"},
                new NovelCharpter{Href="/book/146152/57582699.html", Title="第16章 她，可以召唤魔兽！"},
                new NovelCharpter{Href="/book/146152/57582700.html", Title="第17章 她，明明不是召唤师"},
                new NovelCharpter{Href="/book/146152/57582701.html", Title="第18章 天灾还是人为？"},
                new NovelCharpter{Href="/book/146152/57582702.html", Title="第19章 萧沐凌，她又跑！"},
                new NovelCharpter{Href="/book/146152/57582703.html", Title="第20章 上古神兽！"},
                new NovelCharpter{Href="/book/146152/57582704.html", Title="第21章 让他变成一堆骨头渣！"},
                new NovelCharpter{Href="/book/146152/57582705.html", Title="第22章 金瞳腾云马"},
                new NovelCharpter{Href="/book/146152/57582706.html", Title="第23章 传言，是真的！"},
                new NovelCharpter{Href="/book/146152/57582707.html", Title="第24章 第一次见！"},
                new NovelCharpter{Href="/book/146152/57582708.html", Title="第25章 我等你"},
            };
        }

        /// <summary>
        /// 窗口被激活事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task OnActivateAsync(CancellationToken cancellationToken) {
            return base.OnActivateAsync(cancellationToken);
        }

    }
}
