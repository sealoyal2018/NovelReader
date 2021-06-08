using Caliburn.Micro;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NovelReader.Installer.ViewModels {
    [Export(typeof(IShell))]
    public class ShellViewModel: Screen, IShell {
        private string installPath = @"G:\testinstall";

        public string InstallPath {
            get {
                return installPath;
            }

            set {
                installPath = value;
                NotifyOfPropertyChange(nameof(InstallPath));
            }
        }
       
        /// <summary>
        /// 将文件属性设置为“嵌入的资源”的文件拷贝到安装目录
        /// 这个方法有限制。你的文件名称和文件夹名称内不能带有符号“.”，因为是通过程序集名称来识别分隔的，程序集的分隔是用“.”，所以加了“.”就无法识别是文件夹名称还是文件名称。
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="sourceFile"></param>
        /// <param name="assemblyName"></param>
        public static void CopyEmbedFile(string targetPath, string sourceFile, string assemblyName) {
            Assembly assm = Assembly.GetExecutingAssembly();
            Stream stream = assm.GetManifestResourceStream(assemblyName);

            if (!Directory.Exists(targetPath + "/" + Path.GetDirectoryName(sourceFile))) {
                Directory.CreateDirectory(targetPath + "/" + Path.GetDirectoryName(sourceFile));
            }

            try {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                FileStream fs = new FileStream(targetPath + "/" + sourceFile, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
            } catch (Exception ex) {
            }

        }

        /// <summary>
        /// 安装
        /// </summary>
        public void Install() {
            var dirs = "NovelReader.Installer.Files";
            var d = Assembly.GetExecutingAssembly();
            var resourceStr = d.GetManifestResourceNames();
            int resourceCount = resourceStr.Length - 2;
            for (int i = 0; i < resourceStr.Length; i++) {
                string item = resourceStr[i];
                if (item.StartsWith(dirs)) {
                    var fileName = assemblyNameToDirection(item);
                    CopyEmbedFile(installPath, fileName, item);
                }
            }
        }

        /// <summary>
        /// 将程序集名称转路径和文件名
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public string assemblyNameToDirection(string assemblyName) {
            var  newAssemblyName = assemblyName.Substring("NovelReader.Installer.Files.".Length);
            var folders = new[] {
                "f.",
            };
            var path = newAssemblyName;
            foreach(var item in folders) {
                if (newAssemblyName.StartsWith(item)) {
                   var arr  = item.Split(".", StringSplitOptions.RemoveEmptyEntries).ToList();
                    arr.Add(newAssemblyName.Substring(item.Length));
                    path = string.Join("\\", arr);
                    break;
                }
            }
            return path;
        }
    }
}
