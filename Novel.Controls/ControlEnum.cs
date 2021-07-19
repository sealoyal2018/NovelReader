using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Controls
{
    public enum EnumTrigger
    {
        /// <summary>
        /// 悬浮
        /// </summary>
        Hover,
        /// <summary>
        /// 点击
        /// </summary>
        Click,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom,
    }

    public enum EnumPlacementDirection
    {
        Left,
        Top,
        Right,
        Bottom,
    }
    public enum EnumPlacement
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左中
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 左下
        /// </summary>
        LeftCenter,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom,
        /// <summary>
        /// 右中
        /// </summary>
        RightCenter,
        /// <summary>
        /// 上左
        /// </summary>
        TopLeft,
        /// <summary>
        /// 上中
        /// </summary>
        TopCenter,
        /// <summary>
        /// 上右
        /// </summary>
        TopRight,
        /// <summary>
        /// 下左
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 下中
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 下右
        /// </summary>
        BottomRight,
    }
}
