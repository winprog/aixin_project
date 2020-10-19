using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Usage()
        {
            System.Console.WriteLine("{0} <文件名称> <文件目录> <目标目录>", Process.GetCurrentProcess().ProcessName);
            
        }

        static bool FindAndCopy(string file, string src, string dst)
        {
            if (!Directory.Exists(src))
            {
                Console.WriteLine("源目录不存在：{0}", src);
                return false;
            }

            if (!Directory.Exists(dst))
            {
                Console.WriteLine("目标目录不存在：{0}", dst);
                return false;
            }

            foreach (string gf in Directory.GetFiles(src))
            {
                FileInfo f = new FileInfo(gf);
                if (f.Name.Equals(file))
                {
                    string srcFile = f.FullName;
                    string dstFile = srcFile.Replace('\\', '_').Replace(':', '_');
                    dstFile = Path.Combine(dst, dstFile);

                    Console.WriteLine("src file: {0}", srcFile);
                    Console.WriteLine("dst file: {0}", dstFile);

                    if (File.Exists(dstFile))
                    {
                        Console.WriteLine("目标文件已存在: {0}", dstFile);
                        continue;
                    }

                    File.Copy(srcFile, dstFile);
                }
            }

            foreach (string gd in Directory.GetDirectories(src))
            {
                if (!FindAndCopy(file, gd, dst))
                {
                    return false;
                }
                Console.WriteLine();
            }

            
            return true;
        }
   

        static void Main(string[] args)
        {
            if (args.Length != 3) {
                Usage();
                return;
            }

            string file = args[0];
            string srcDir = args[1];
            string dstDir = args[2];
            Console.WriteLine("copy {0} from {1} to {2}", file, srcDir, dstDir);
            Console.WriteLine();
            
            if (!FindAndCopy(file, srcDir, dstDir))
            {
                Console.WriteLine("复制发生错误");
            }
            else
            {
                Console.WriteLine("复制成功");
            }
        }
    }
}
