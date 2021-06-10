using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Novel.Update.Service {
    public class YSService {
        private readonly RestClient _restClient;
        private Dictionary<string, string> cookies = new Dictionary<string, string>();
        private List<UpdateFileInfo> fileInfos = new List<UpdateFileInfo>();

        public YSService() {
            _restClient = new RestClient();
            _restClient.AddDefaultHeader("Referer", "Mozilla /5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0");
            _restClient.AddDefaultHeader("User-Agent", "http://cc.ys168.com/f_ht/ajcx/000ht.html?bbh=1139");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitServiceAsync() {
            var req = new RestRequest("http://sealoyal.ys168.com/", Method.GET);
            var res = await _restClient.ExecuteAsync(req);
            if (res.IsSuccessful) {
                foreach (var item in res.Cookies) {
                    cookies.Add(item.Name, item.Value);
                }
                await GetSoftInfoAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> GetSoftInfoAsync() {
            var req = new RestRequest("http://cc.ys168.com/f_ht/ajcx/wj.aspx?cz=dq&jsq=0&mlbh=1942814&wjpx=1&_dlmc=sealoyal&_dlmm=", Method.GET);
            foreach (var item in cookies) {
                req.AddCookie(item.Key, item.Value);
            }
            var res = await _restClient.ExecuteAsync(req);
            if (res.IsSuccessful) {
                HtmlDocument doc = new HtmlDocument();
                var text = res.Content;
                var index = text.IndexOf("<li");
                var html = text.Substring(index);
                doc.LoadHtml($"<ul>{html}</ul>");
                var lis = doc.DocumentNode.SelectNodes(".//li");
                foreach (var item in lis) {
                    var aElem = item.SelectSingleNode(".//a");
                    var iElem = item.SelectSingleNode(".//i");
                    var path = aElem.GetAttributeValue("href", string.Empty);
                    var name = aElem.InnerText;
                    var size = iElem.InnerText;
                    fileInfos.Add(new UpdateFileInfo { Path = path, Size = size, Name = name });
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取最新版本信息
        /// </summary>
        /// <returns></returns>
        public async Task GetLatestVersionInfoAsync() {
            var fileName = "latest.json";
            var fileInfo = fileInfos.Where(x => x.Name == fileName).FirstOrDefault();
            if (fileInfo is null)
                return;
            var req = new RestRequest(fileInfo.Path, Method.GET);
            var res = await _restClient.ExecuteAsync(req);
            if (res.IsSuccessful) {
                var text = res.Content;
                var verInfo = JsonSerializer.Deserialize<VersionInfo>(text);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task DownloadUpdateFileAsync(Action<double> action) {
            var fileName = "latest.json";
            var updateFile = fileInfos.Where(x => x.Name != fileName).ToList();
            var count = updateFile.Count;
            var perMount = 1d / count;
            var saveFilePath = "./";
            foreach (var file in updateFile) {
                var req = new RestRequest("http://ys-g.ys168.com/618077323/216922314/SKhVstp645H36744MN58f/%E5%8F%96%E8%89%B2.zip", Method.GET);
                var res = await _restClient.ExecuteAsync(req);
                if (res.IsSuccessful) {
                    var allSize = res.ContentLength;
                    using (var fs = File.Create($"{saveFilePath}{file.Name}")) {
                        var offset = 0;
                        var length = 1024;
                        while (true) {
                            if (allSize < length + offset) {
                                length = (int)allSize - offset;
                            }
                            fs.Write(res.RawBytes, offset, length);
                            offset += length;
                            if(offset >= allSize) {
                                break;
                            }
                            action(1d*offset/allSize*perMount);
                        }
                    }
                }
            }
            action(100d);
        }

    }
}
