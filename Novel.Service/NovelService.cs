using HtmlAgilityPack;
using Novel.Service.Models;
using RestSharp;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Novel.Service {
    /// <summary>
    /// 铅笔小说网站数据接口
    /// </summary>
    [Export(typeof(NovelService))]
    public class NovelService {

        /// <summary>
        /// html 文档解析器
        /// </summary>
        private readonly HtmlDocument _htmlDocument;

        /// <summary>
        /// 登录后的cookie
        /// </summary>
        private List<string> cookies;

        /// <summary>
        /// RestSharp 对象
        /// </summary>
        private readonly RestClient _restClient;

        /// <summary>
        /// 构建 NovelService 对象
        /// </summary>
        public NovelService() {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _htmlDocument = new HtmlDocument();
            cookies = new List<string>();
            _restClient = new RestClient("https://www.23qb.net/");
            _restClient.AddDefaultHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36 Edg/90.0.818.66");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="register">注册相关信息</param>
        /// <returns></returns>
        public async Task<bool> Register(Register register) {
            var registerUrl = "/register.php?do=submit";
            var req = new RestRequest(registerUrl, Method.POST);
            var data = $"username={register.UserName}&password={register.Password}&repassword={register.Password}&email={register.Email}&action=newuser&submit=%CC%E1+%BD%BB+%D7%A2+%B2%E1";
            req.AddParameter("application/x-www-form-urlencoded", data, ParameterType.RequestBody);
            var res = await _restClient.ExecuteAsync(req);
            var content = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            return content.Contains("注册成功");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login">登录相关信息</param>
        /// <returns></returns>
        public async Task<bool> Login(Login login) {
            var loginUrl = "/login.php?do=submit";
            var req = new RestRequest(loginUrl, Method.POST);
            var data = $"username={login.UserName}&password={login.Password}&usecookie=2592000&action=login&submit={EncodeGBK("登录")}";
            req.AddParameter("application/x-www-form-urlencoded", data, ParameterType.RequestBody);
            var res = await _restClient.ExecuteAsync(req);
            var content = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            if (content.Contains("登录成功")) {
                cookies.Clear();
                foreach (var cookie in res.Cookies) {
                    cookies.Add($"{cookie.Name}={cookie.Value}");
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 搜索小说
        /// </summary>
        /// <param name="keyword">名称</param>
        /// <param name="page">页码</param>
        /// <returns>返回小说列表</returns>
        public async Task<List<NovelInfo>> Search(string keyword = "重生", int page = 0) {
            var ret = new List<NovelInfo>();
            var req = new RestRequest("/search2.php", Method.POST);
            req.AddHeader("cookie", "PHPSESSID=a2hf580qsmbfhq81j4hliiqv7q; jieqiUserInfo=jieqiUserId%3D881695%2CjieqiUserUname%3Dsealoyal%2CjieqiUserName%3Dsealoyal%2CjieqiUserGroup%3D3%2CjieqiUserGroupName%3D%C6%D5%CD%A8%BB%E1%D4%B1%2CjieqiUserVip%3D0%2CjieqiUserHonorId%3D%2CjieqiUserHonor%3D%CA%E9%BC%EB%2CjieqiUserPassword%3D0659c7992e268962384eb17fafe88364%2CjieqiUserUname_un%3Dsealoyal%2CjieqiUserName_un%3Dsealoyal%2CjieqiUserHonor_un%3D%26%23x4E66%3B%26%23x8327%3B%2CjieqiUserGroupName_un%3D%26%23x666E%3B%26%23x901A%3B%26%23x4F1A%3B%26%23x5458%3B%2CjieqiUserLogin%3D1622211550; jieqiVisitInfo=jieqiUserLogin%3D1622211550%2CjieqiUserId%3D881695");
            req.AddParameter("application/x-www-form-urlencoded", $"searchkey={EncodeGBK(keyword)}&searchtype=all", ParameterType.RequestBody);
            var res = await _restClient.ExecuteAsync(req);
            if (res.IsSuccessful) {
                var content = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
                #region 解析 html 文档, 获取小说相关信息
                _htmlDocument.LoadHtml(content);
                var siteBoxDiv = _htmlDocument.GetElementbyId("sitebox");
                if (siteBoxDiv is null) {
                    return ret;
                }
                var dlNodes = siteBoxDiv.SelectNodes("dl");
                if (dlNodes is null) {
                    return ret;
                }
                foreach (var dlNode in dlNodes) {
                    var dtNode = dlNode.SelectSingleNode("dt");
                    var aNode = dtNode.SelectSingleNode("a");
                    var href = aNode.GetAttributeValue("href", string.Empty);
                    var imgNode = aNode.SelectSingleNode("img");
                    var imgSource = imgNode.GetAttributeValue("_src", string.Empty);
                    var spanNode = dtNode.SelectSingleNode("span");
                    var typeStr = spanNode.InnerText;

                    var ddNodes = dlNode.SelectNodes("dd");
                    var count = ddNodes.Count;
                    var time = ddNodes[0].SelectSingleNode(".//span").InnerText;
                    var title = ddNodes[0].SelectSingleNode(".//a").InnerText;
                    //ddNodes[0].SelectSingleNode("a").GetAttributeValue("href", string.Empty); 小说链接, 前面已获取

                    var spanNodes = ddNodes[1].SelectNodes("span");
                    var author = spanNodes[0].InnerText;
                    var status = spanNodes[1].InnerText;
                    var wordCount = spanNodes[2].InnerText;

                    var summary = ddNodes[2].InnerText;
                    var charpterNode = ddNodes[3].SelectSingleNode("a");
                    var charpterTitle = charpterNode.InnerText;
                    var charpterHref = charpterNode.GetAttributeValue("href", string.Empty);

                    ret.Add(new NovelInfo {
                        Author = author,
                        Href = href,
                        ImageSource = imgSource,
                        LastCharpter = new NovelCharpter {
                            Href = charpterHref,
                            Title = charpterTitle,
                        },
                        Summary = summary,
                        Title = title,
                        UpDateTime = time,
                        Status = status
                    });
                }
                #endregion
            }

            return ret;
        }

        /// <summary>
        /// 将中文字符编码成 gb2312
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string EncodeGBK(string data) {
            var res = new StringBuilder();
            byte[] byStr = Encoding.GetEncoding("GB2312").GetBytes(data);
            foreach (var item in byStr) {
                res.Append("%" + item.ToString("X2"));
            }
            return res.ToString();
        }

        /// <summary>
        /// 获取首页推荐小说
        /// </summary>
        /// <returns></returns>
        public async Task<List<Recommend>> GetRecommendNovel() {
            var ret = new List<Recommend>();
            var req = new RestRequest("/", Method.GET);
            var res = await _restClient.ExecuteAsync(req);
            if (!res.IsSuccessful)
                return ret;
            var html = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            _htmlDocument.LoadHtml(html);
            var mainDiv = _htmlDocument.GetElementbyId("main");
            var div = mainDiv.SelectNodes("./div");
            var recomDivs = div.Where(x => {
                var classAttributes = x.GetAttributeValue("class", string.Empty);
                return classAttributes.Contains("left") || classAttributes.Contains("right");
            });
            foreach (var recomDiv in recomDivs) {
                var recomemd = new Recommend();
                recomemd.Type = recomDiv.SelectSingleNode("./div[@class='tabstit']").InnerText.Trim();
                var dlDivs = recomDiv.SelectNodes(".//dl");
                foreach (var dlDiv in dlDivs) {
                    var novel = new NovelInfo();
                    novel.Href = dlDiv.SelectSingleNode(".//dt/a").GetAttributeValue("href", string.Empty);
                    novel.ImageSource = dlDiv.SelectSingleNode(".//dt/a/img").GetAttributeValue("_src", string.Empty);
                    var ddNodes = dlDiv.SelectNodes(".//dd");
                    novel.Title = ddNodes[0].InnerText;
                    novel.Author = ddNodes[1].InnerText;
                    novel.Summary = ddNodes[2].InnerText;
                    recomemd.Novels.Add(novel);
                }
                var lis = recomDiv.SelectNodes(".//ul/li");
                foreach (var li in lis) {
                    var novel = new NovelInfo();
                    novel.Author = li.SelectSingleNode("em").InnerText;
                    novel.Type = li.SelectSingleNode("span").InnerText;
                    var aNode = li.SelectSingleNode("a");
                    novel.Href = aNode.GetAttributeValue("href", string.Empty);
                    novel.Title = aNode.InnerText;
                    recomemd.OtherNovels.Add(novel);
                }
                ret.Add(recomemd);
            }
            return ret;
        }

        /// <summary>
        /// 获取其他小说列表(言情, 玄幻, 都市, 武侠, 唯美..)
        /// </summary>
        /// <returns>小说类型</returns>
        public async Task<List<NovelInfo>> GetHotNovels(NovelType type = NovelType.Romance) {
            var ret = new List<NovelInfo>();
            var addres = "/yanqing";
            switch (type) {
                case NovelType.Romance:
                    addres = "/yanqing";
                    break;
                case NovelType.Fantasy:
                    addres = "/xuanhuan";
                    break;
                case NovelType.Urban:
                    addres = "/dushi";
                    break;
                case NovelType.MartialArts:
                    addres = "/wuxia";
                    break;
                case NovelType.Beautiful:
                    addres = "/danmei";
                    break;
                default:
                    break;
            }

            var req = new RestRequest(addres, Method.GET);
            var res = await _restClient.ExecuteAsync(req);
            if (!res.IsSuccessful)
                return ret;
            var html = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            return GetNovelsFromSiteboxDiv(html);
        }

        /// <summary>
        /// 存放在 id=sitebox 的 div 下获取小说信息模板
        /// </summary>
        /// <param name="html">包含 id=sitebox div的 html 数据</param>
        /// <returns></returns>
        private List<NovelInfo> GetNovelsFromSiteboxDiv(string html) {
            var ret = new List<NovelInfo>();
            _htmlDocument.LoadHtml(html);
            var siteBoxDiv = _htmlDocument.GetElementbyId("sitebox");
            if (siteBoxDiv is null) {
                return ret;
            }
            var dlNodes = siteBoxDiv.SelectNodes("dl");
            if (dlNodes is null) {
                return ret;
            }
            foreach (var dlNode in dlNodes) {
                var novel = new NovelInfo();
                var dtNode = dlNode.SelectSingleNode("dt");
                var aNode = dtNode.SelectSingleNode("a");
                novel.Href = aNode.GetAttributeValue("href", string.Empty);
                var imgNode = aNode.SelectSingleNode("img");
                novel.ImageSource = imgNode.GetAttributeValue("_src", string.Empty);
                var spanNode = dtNode.SelectSingleNode("span");
                novel.Type = spanNode.InnerText;

                var ddNodes = dlNode.SelectNodes("dd");
                novel.UpDateTime = ddNodes[0].SelectSingleNode(".//span").InnerText;
                novel.Title = ddNodes[0].SelectSingleNode(".//a").InnerText;

                var spanNodes = ddNodes[1].SelectNodes("span");
                novel.Author = spanNodes[0].InnerText;
                novel.Status = spanNodes[1].InnerText;
                novel.WordCount = spanNodes[2].InnerText;

                novel.Summary = ddNodes[2].InnerText;
                var charpterNode = ddNodes[3].SelectSingleNode("a");
                var charpter = new NovelCharpter();
                charpter.Title = charpterNode.InnerText;
                charpter.Href = charpterNode.GetAttributeValue("href", string.Empty);
                novel.LastCharpter = charpter;
                ret.Add(novel);
            }
            return ret;
        }

        /// <summary>
        /// 获取特定小说章节
        /// </summary>
        public async Task<List<NovelCharpter>> GetCharpters(string url) {
            var ret = new List<NovelCharpter>();
            var req = new RestRequest(url, Method.GET);
            if(this.cookies.Count > 0)
                req.AddHeader("cookie", string.Join(';', cookies));
            var res = await this._restClient.ExecuteAsync(req);
            if (!res.IsSuccessful) {
                return ret;
            }
            var html = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            _htmlDocument.LoadHtml(html);
            // 所有章节
            var ul = _htmlDocument.GetElementbyId("chapterList");
            var aElements = ul.SelectNodes(".//li/a");
            foreach(var ele in aElements) {
                var href = ele.GetAttributeValue("href", string.Empty);
                var title = ele.InnerText;
                ret.Add(new NovelCharpter { Href = href, Title = title });
            }
            // 最新章节 -- 后续可用
            //var recentlyCharpter = "";
            //var newlist = _htmlDocument.GetElementbyId("newlist");
            //var tooltip = newlist.SelectSingleNode(".//div/h2").InnerText;
            //ul = newlist.SelectSingleNode(".//ul");
            //aElements = ul.SelectNodes(".//li/a");
            //foreach(var ele in aElements) {
            //    var href = ele.GetAttributeValue("href", string.Empty);
            //    var title = ele.InnerText;
            //}
            return ret;
        }

        /// <summary>
        /// 获取小说内容
        /// </summary>
        public async Task<NovelContent> GetNovelContent(string url) {
            var ret = new NovelContent();
            var req = new RestRequest(url, Method.GET);
            if (this.cookies.Count > 0)
                req.AddHeader("cookie", string.Join(';', cookies));
            var res = await this._restClient.ExecuteAsync(req);
            if (!res.IsSuccessful) {
                return ret;
            }
            var html = HttpUtility.UrlDecode(res.RawBytes, Encoding.GetEncoding("gbk"));
            _htmlDocument.LoadHtml(html);
            var mainDiv = _htmlDocument.GetElementbyId("mlfy_main_text");
            ret.Title = mainDiv.SelectSingleNode(".//h1").InnerText;
            var contentDiv = _htmlDocument.GetElementbyId("TextContent");
            var pElements = contentDiv.SelectNodes(".//p");
            var elements = pElements.Take(pElements.Count - 2);
            var contentBuilder = new StringBuilder();
            foreach(var p in elements) {
                contentBuilder.AppendLine(p.InnerText);
            }
            ret.Content = contentBuilder.ToString();
            return ret;
        }
    }
}
