using Novel.Service;
using System;
using System.Threading.Tasks;

namespace Novel.Test {
    class Program {
        static async Task Main(string[] args) {
            var service = new NovelService();
            //await service.GetCharpters("/book/146152/");
            await service.GetNovelContent("/book/146152/84253769.html");
            //var ret = await service.Register(new NovelLib.Models.Register { Email = "efklswerjddf@qq.com", Password = "abck1234", UserName = "asbklkjowerzxcvk" });
            //var ret = await service.Login(new NovelLib.Models.Login { Password = "abc123456", UserName = "sealoyal" });
            //var ret = await service.test();
            //var res = await service.Search();
            //Console.WriteLine($"标题\t作者\t状态");
            //foreach (var item in res) {
            //    Console.WriteLine($"{item.Title}\t{item.Author}\t{item.Status}");
            //}
            //await service.GetRecommendNovel();
            //var ret = await service.GetRomances(NovelLib.Models.NovelType.MartialArts);
        }
    }
}
