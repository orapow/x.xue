using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fi = new FileInfo(ofd.FileName);
                var html = File.ReadAllText(ofd.FileName, Encoding.GetEncoding("gb2312"));
                html = html.Substring(html.IndexOf("<body")).Replace("&nbsp;", " ");
                richTextBox1.Text = new Regex("</?(!--)?(\\w+)[^>]*/?>").Replace(html, o =>
                {
                    if (o.Groups[2].Value == "table") return o.Groups[0].Value;
                    if (o.Groups[2].Value == "p") return o.Groups[0].Value;
                    if (o.Groups[2].Value != "img") return "";
                    var m = new Regex("src=\"([^\"]+)\"").Match(o.Groups[0].Value);
                    return new Regex("src=\"([^\"]+)\"").Replace(o.Groups[0].Value, "src='" + ImageToBase64(fi.DirectoryName + "\\" + m.Groups[1].Value) + "'");
                });
            }
        }
        public static string ImageToBase64(string file)
        {
            file = file.Replace("file:///", "");
            if (!File.Exists(file)) return "";
            var img = Image.FromFile(file);
            try
            {
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                img.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ms = Clipboard.GetData("Html Format") as MemoryStream;
            if (ms == null) return;
            richTextBox1.Text = removeHtml(Encoding.UTF8.GetString(ms.ToArray()));
        }

        string removeHtml(string html)
        {
            var igs = "table|tr|td".Split('|');
            html = html.Substring(html.IndexOf("<body")).Replace("&nbsp;", "");
            html = new Regex("<!--[^-]+-->").Replace(html, "");
            return new Regex("</?(!--)?(\\w+)[^>]*/?>").Replace(html, o =>
            {
                if (igs.Contains(o.Groups[2].Value)) return o.Groups[0].Value;
                if (o.Groups[2].Value == "img")
                {
                    var reg = new Regex("src=\"([^\"]+)\"");
                    return reg.Replace(o.Groups[0].Value, m => { return "src='data:image/png;base64," + ImageToBase64(m.Groups[1].Value) + "'"; });
                } //return o.Groups[0].Value.Replace(new Regex("src=\"([^\"]+)\""), ImageToBase64(new Regex("src=\"([^\"]+)\"").Match(o.Groups[0].Value).Groups[1].Value)) //return "[图片：" + ImageToBase64(new Regex("src=\"([^\"]+)\"").Match(o.Groups[0].Value).Groups[1].Value) + "]";
                else return "";
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Html, richTextBox1.Text);
        }
    }
}
