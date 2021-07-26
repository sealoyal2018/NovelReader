using Novel.Update.Models;
using Novel.Update.Services;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Novel.TestProj {
    public class Tests {

        [Test]
        public void Test1() {
            VersionInfo info = new VersionInfo {
                LatestVersion = "0.3.1",
                Summary = new System.Collections.Generic.List<string> {
                    "1. 全新界面",
                    "2. 添加退出登录",
                    "3. 修复已知 bug 若干"
                },
                Token = "75554BBFD2B72BB772D90E459DB59548",
                UpdateTime = new DateTime(2021, 7, 20, 22, 50, 00),
                OldVersionInfo = new System.Collections.Generic.List<VersionInfo> {
                    new VersionInfo {
                        LatestVersion = "0.2.0.210610",
                        Summary = new System.Collections.Generic.List<string> {
                            "1.添加登录功能",
                            "2.添加更新功能",
                            "3.修复部分已知bug"
                        },
                        Token = "AB11CD635C564C9A138F07B6B0E6D34A",
                        UpdateTime = new DateTime(2021, 6,10, 22,11,48),
                    }
                }
            };
            var data = System.Text.Json.JsonSerializer.Serialize(info, new JsonSerializerOptions() { 
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All),
            });
            using (var fs = new StreamWriter(new FileStream("./latest.json", FileMode.OpenOrCreate, FileAccess.Write))) {
                fs.Write(data);
            }
        }

        [Test]
        public void Test2() {
            using (var fs = new StreamReader(new FileStream("./latest.json", FileMode.Open, FileAccess.Read))) {
                var data = fs.ReadToEnd();
                var info = JsonSerializer.Deserialize<VersionInfo>(data);
            }
        }

        [Test]
        public async Task Test3() {
            await service.GetLatestVersionInfoAsync();
        }

        private YSService service;
        [SetUp]
        public async Task Setup() {
            service = new YSService();
            await service.InitServiceAsync();
        }

        [Test]
        public async Task DownloadTest() {
            await service.DownloadUpdateFileAsync((x)=> {
                Debug.WriteLine(x);
            });
        }

    }
}