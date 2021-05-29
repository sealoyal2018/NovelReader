using System.Collections.Generic;

namespace Novel.Service.Models {

    /// <summary>
    /// 首页推荐列表
    /// </summary>
    public class Recommend {
        /// <summary>
        /// 推荐的类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// 小说列表
        /// </summary>
        public List<NovelInfo> Novels { get; set; } = new List<NovelInfo>();

        /// <summary>
        /// 其他推荐信息
        /// </summary>
        public List<NovelInfo> OtherNovels { get; set; } = new List<NovelInfo>();
    }
}
