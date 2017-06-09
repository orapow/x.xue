using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace xx.tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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

        private void btn_trans_Click(object sender, EventArgs e)
        {
            var ms = Clipboard.GetData("Html Format") as MemoryStream;
            if (ms == null) return;
            rtb_cot.Text = System.Web.HttpUtility.HtmlDecode(removeHtml(Encoding.UTF8.GetString(ms.ToArray())));
            Clipboard.SetData(DataFormats.Text, rtb_cot.Text);
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
                }
                else return "";
            });
        }
    }
}
