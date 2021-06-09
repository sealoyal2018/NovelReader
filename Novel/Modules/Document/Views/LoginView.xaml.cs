using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Novel.Modules.Document.Views {
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl {
        public LoginView() {
            InitializeComponent();
        }


        /// <summary>
        /// 随机数
        /// </summary>
        private Random _random = new Random();

        //布局宽490 高210 显示宽430 高180
        //阵距4行8列 点之间的距离 X轴Y轴都是70
        /// <summary>
        /// 点信息阵距
        /// </summary>
        private PointInfo[,] _points = new PointInfo[8, 4];

        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer _timer;


        /// <summary>
        /// 初始化阵距
        /// </summary>
        private void Init() {
            //生成阵距的点
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 4; j++) {
                    double x = _random.Next(-11, 11);
                    double y = _random.Next(-6, 6);
                    _points[i, j] = new PointInfo() {
                        X = i * 70,
                        Y = j * 70,
                        SpeedX = x / 24,
                        SpeedY = y / 24,
                        DistanceX = _random.Next(35, 106),
                        DistanceY = _random.Next(20, 40),
                        MovedX = 0,
                        MovedY = 0,
                        PolygonInfoList = new List<PolygonInfo>()
                    };
                }
            }

            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;

            //上一行取2个点 下一行取1个点
            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 3; j++) {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i + 1, j].X, _points[i + 1, j].Y));
                    _points[i + 1, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromRgb(r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }

            //上一行取1个点 下一行取2个点
            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 3; j++) {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i, j + 1].X, _points[i, j + 1].Y));
                    _points[i, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromRgb(r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }
        }

        /// <summary>
        /// 设置颜色动画
        /// </summary>
        /// <param name="polygon">多边形</param>
        private void SetColorAnimation(UIElement polygon) {
            //颜色动画的时间 1-4秒随机
            Duration dur = new Duration(new TimeSpan(0, 0, _random.Next(1, 5)));
            //故事版
            Storyboard sb = new Storyboard() {
                Duration = dur
            };
            sb.Completed += (S, E) => //动画执行完成事件
            {
                //颜色动画完成之后 重新set一个颜色动画
                SetColorAnimation(polygon);
            };
            //颜色动画
            //颜色的RGB
            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;
            ColorAnimation ca = new ColorAnimation() {
                To = Color.FromRgb(r, g, b),
                Duration = dur
            };
            Storyboard.SetTarget(ca, polygon);
            Storyboard.SetTargetProperty(ca, new PropertyPath("Fill.Color"));
            sb.Children.Add(ca);
            sb.Begin(this);
        }

        /// <summary>
        /// 多边形变化动画
        /// </summary>
        void PolyAnimation(object sender, EventArgs e) {
            //不改变阵距最外边一层的点
            for (int i = 1; i < 7; i++) {
                for (int j = 1; j < 3; j++) {
                    PointInfo pointInfo = _points[i, j];
                    pointInfo.X += pointInfo.SpeedX;
                    pointInfo.Y += pointInfo.SpeedY;
                    pointInfo.MovedX += pointInfo.SpeedX;
                    pointInfo.MovedY += pointInfo.SpeedY;
                    if (pointInfo.MovedX >= pointInfo.DistanceX || pointInfo.MovedX <= -pointInfo.DistanceX) {
                        pointInfo.SpeedX = -pointInfo.SpeedX;
                        pointInfo.MovedX = 0;
                    }
                    if (pointInfo.MovedY >= pointInfo.DistanceY || pointInfo.MovedY <= -pointInfo.DistanceY) {
                        pointInfo.SpeedY = -pointInfo.SpeedY;
                        pointInfo.MovedY = 0;
                    }
                    //改变多边形的点
                    foreach (PolygonInfo pInfo in _points[i, j].PolygonInfoList) {
                        pInfo.PolygonRef.Points[pInfo.PointIndex] = new Point(pointInfo.X, pointInfo.Y);
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Init();
            //注册帧动画
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += new EventHandler(PolyAnimation);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 24);//一秒钟刷新24次
            _timer.Start();
        }
    }


    /// <summary>
    /// 阵距点信息
    /// </summary>
    internal class PointInfo {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// X轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedX { get; set; }

        /// <summary>
        /// Y轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedY { get; set; }

        /// <summary>
        /// X轴需要移动的距离
        /// </summary>
        public double DistanceX { get; set; }

        /// <summary>
        /// Y轴需要移动的距离
        /// </summary>
        public double DistanceY { get; set; }

        /// <summary>
        /// X轴已经移动的距离
        /// </summary>
        public double MovedX { get; set; }

        /// <summary>
        /// Y轴已经移动的距离
        /// </summary>
        public double MovedY { get; set; }

        /// <summary>
        /// 多边形信息列表
        /// </summary>
        public List<PolygonInfo> PolygonInfoList { get; set; }
    }

    /// <summary>
    /// 多边形信息
    /// </summary>
    internal class PolygonInfo {
        /// <summary>
        /// 对多边形的引用
        /// </summary>
        public Polygon PolygonRef { get; set; }

        /// <summary>
        /// 需要改变的点的索引
        /// </summary>
        public int PointIndex { get; set; }
    }
}
