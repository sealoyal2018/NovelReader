using HtmlAgilityPack;
using Novel.Update.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Novel.Update.Services {
    public class YSService {
        private readonly RestClient _restClient;
        private Dictionary<string, string> cookies = new Dictionary<string, string>();
        private List<UpdateFileInfo> fileInfos = new List<UpdateFileInfo>();

        public List<UpdateFileInfo> FileInfos {
            get {
                return fileInfos;
            }
        }

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
                    FileInfos.Add(new UpdateFileInfo { Path = path, Size = size, Name = name });
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取最新版本信息
        /// </summary>
        /// <returns></returns>
        public async Task<VersionInfo> GetLatestVersionInfoAsync() {
            var fileName = "latest.json";
            var fileInfo = FileInfos.Where(x => x.Name == fileName).FirstOrDefault();
            if (fileInfo is null)
                return null;
            var req = new RestRequest(fileInfo.Path, Method.GET);
            var res = await _restClient.ExecuteAsync(req);
            if (res.IsSuccessful) {
                var text = res.Content;
                var verInfo = JsonSerializer.Deserialize<VersionInfo>(text);
                return verInfo;
            }
            return null;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task DownloadUpdateFileAsync(string saveFilePath, Action<double> action) {
            var fileName = "latest.json";
            var updateFile = FileInfos.Where(x => x.Name != fileName).ToList();
            var count = updateFile.Count;
            var perMount = 1d / count;
            if (Directory.Exists(saveFilePath)) {
                Directory.Delete(saveFilePath, true);
            }
            Directory.CreateDirectory(saveFilePath);
            if (!saveFilePath.EndsWith("\\")) {
                saveFilePath += "\\";
            }

            foreach (var file in updateFile) {
                var arr = file.Name.Split('.');
                var name = arr[0];
                var nameBuilder = new StringBuilder();
                foreach (var ch in name) {
                    if ((int)ch > 127) {
                        nameBuilder.Append(Uri.EscapeUriString($"{ch}"));
                    } else {
                        nameBuilder.Append(ch);
                    }
                }
                var path = $"{file.Path.Substring(0, file.Path.LastIndexOf('/') + 1)}{nameBuilder}.{arr[1]}";
                var req = new RestRequest(path, Method.GET);
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
                            if (offset >= allSize) {
                                break;
                            }
                            action(1d * offset / allSize * perMount * 100);
                            await Task.Delay(5);
                        }
                    }
                    action(100d);
                }
            }
        }

    }
}
