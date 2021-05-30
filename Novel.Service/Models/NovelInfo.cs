namespace Novel.Service.Models {
    /// <summary>
    /// 小说信息
    /// </summary>
    public class NovelInfo {

        /// <summary>
        /// 小说链接
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 展示图
        /// </summary>
        public string ImageSource { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string UpDateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 最新章节信息
        /// </summary>
        public NovelCharpter LastCharpter { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        public string WordCount { get; set; }
    }
}
