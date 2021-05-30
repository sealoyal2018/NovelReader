using Caliburn.Micro;
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

        private readonly string _name;
        private readonly string _icon;
        private readonly bool _show;
        private readonly NovelService _service;
        private BindableCollection<Recommend> recommends;
        public string Name {
            get {
                return _name;
            }
        }

        public string Icon {
            get {
                return _icon;
            }
        }

        public bool Show {
            get {
                return _show;
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

        [ImportingConstructor]
        public RecommendViewModel(NovelService service) {
            this._name = "热门推荐";
            this._icon = "";
            this._show = true;

            recommends = new BindableCollection<Recommend>() {
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
                new Recommend {
                    Type = "玄幻奇幻",
                    Novels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210107/210107s.jpg",
                            Href = "/book/210107/",
                           Title = "阿姨，不是说好不努力了吗？",
                           Author= "作者：骨惑",
                           Summary = "下载客户端，查看完整作品简介。…"
                        },
                        new NovelInfo {
                            ImageSource = "https://www.23qb.net/files/article/image/210/210614/210614s.jpg",
                            Title = "开局签到回复术士的重启人生",
                            Href = "/book/210614/",
                            Author = "作者：梦寻三千度",
                            Summary = "【本书偏向脑洞，轻松】一场突如其来的大火烧烬了一切，却让白墨穿越到了回复术士的重启人生，并且获得了攻略系统，只要提升好感值，就可以获得各…",
                        }
                    },
                    OtherNovels = new System.Collections.Generic.List<NovelInfo> {
                        new NovelInfo{
                             Author= "葬道疯魔",
                            Type = "[言情女生]",
                            Href ="/book/210838/",
                            Title = "签到千年我成了人族圣皇",
                        },
                        new NovelInfo{
                            Author = "北方唐糖",
                            Type = "[豪门总裁]",
                            Href ="/book/4390/",
                            Title = "冷情前夫，前妻已改嫁",
                        },
                        new NovelInfo{
                            Author = "鱼龙服",
                            Type = "[言情女生]",
                            Href ="/book/201216/",
                            Title = "秦时明月之人宗门徒",
                        },
                        new NovelInfo{
                            Author = "杨三流",
                            Type = "[言情女生]",
                            Href ="/book/175532/",
                            Title = "暮霭沉沉楚天一阔",
                        },
                        new NovelInfo{
                            Author = "大赦天下",
                            Type = "[言情女生]",
                            Href ="/book/180066/",
                            Title = "被照美冥挖了出来",
                        },
                        new NovelInfo{
                            Author = "只为了吃",
                            Type = "[言情女生]",
                            Href ="/book/208176/",
                            Title = "吓人，群里竟都是古代大佬",
                        },
                        new NovelInfo{
                            Author = "梦箩",
                            Type = "[言情女生]",
                            Href ="/book/210738/",
                            Title = "霍格沃茨的魔法世界之旅",
                        },
                        new NovelInfo{
                            Author = "笙予",
                            Type = "[言情女生]",
                            Href ="/book/210737/",
                            Title = "武魂殿：开局觉醒佛祖武魂",
                        },
                        new NovelInfo{
                            Author = "海鲜质检员",
                            Type = "[言情女生]",
                            Href ="/book/211045/",
                            Title = "海域求生：从签到开始",
                        },
                        new NovelInfo{
                            Author = "冷石",
                            Type = "[言情女生]",
                            Href ="/book/8987/",
                            Title = "网游之风流骑士",
                        },
                    }
                },
            };
            this._service = service;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetRecommendNovel();
            Recommends = new BindableCollection<Recommend>(ret);
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
