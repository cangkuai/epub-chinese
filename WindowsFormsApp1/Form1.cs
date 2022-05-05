using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public String[] filepath,filedir;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "epub文件(*.epub)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 filepath= dialog.FileNames;
                textBox1.Text = "";
                foreach(string file in filepath)
                {
                    textBox1.Text = textBox1.Text + file+"\n";
                }

            }
            }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string file in filepath)
            {
                textBox2.Text = textBox2.Text + "开始处理" + file + "\r\n";
                textBox2.Text = textBox2.Text + "开始解压" + file + "\r\n";
                try
                {
                    ZipFile.ExtractToDirectory(file, @".\reader");
                    textBox2.Text = textBox2.Text + "解压" + file + "成功" + "\r\n";
                }
                catch (Exception ex)
                {
                    textBox2.Text = textBox2.Text + "解压" + file + "失败" + "\r\n" + "原因：" + ex.Message;
                    break;
                }
                DirectoryInfo root = new DirectoryInfo(@".\reader\OEBPS\Text");
                foreach (FileInfo f in root.GetFiles())
                {
                    string name = f.Name;
                    string fullName = f.FullName;
                    textBox2.Text = textBox2.Text + "开始读取" + name + "\r\n";
                    try
                    {
                        StreamReader sr = new StreamReader(fullName);
                        textBox2.Text = textBox2.Text + "读取" + name + "成功" + "\r\n";
                        textBox2.Text = textBox2.Text + "开始转换" + name + "\r\n";
                        string ftext = sr.ReadToEnd();
                        sr.Close();
                        string strSimple;
                        if (radioButton2.Checked == true)
                        {
                            strSimple = Microsoft.VisualBasic.Strings.StrConv(ftext, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese);
                        }
                        else
                        {
                            strSimple= Microsoft.VisualBasic.Strings.StrConv(ftext, Microsoft.VisualBasic.VbStrConv.TraditionalChinese);
                        }
                        textBox2.Text = textBox2.Text + "转换" + name + "成功" + "\r\n";
                        textBox2.Text = textBox2.Text + "开始写入" + name + "\r\n";
                        StreamWriter sw = new StreamWriter(fullName);
                        sw.Write(strSimple);
                        textBox2.Text = textBox2.Text + "写入" + name + "成功" + "\r\n";
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        textBox2.Text = textBox2.Text + "处理" + name + "失败" + "\r\n" + "原因：" + ex.Message;
                        return;
                    }

                }
                try
                {
                    StreamReader sr = new StreamReader(@".\reader\OEBPS\toc.ncx");
                    textBox2.Text = textBox2.Text + "读取配置文件成功" + "\r\n";
                    textBox2.Text = textBox2.Text + "开始转换配置文件\r\n";
                    string ftext = sr.ReadToEnd();
                    sr.Close();
                    string strSimple;
                    if (radioButton2.Checked == true)
                    {
                        strSimple = Microsoft.VisualBasic.Strings.StrConv(ftext, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese);
                    }
                    else
                    {
                        strSimple = Microsoft.VisualBasic.Strings.StrConv(ftext, Microsoft.VisualBasic.VbStrConv.TraditionalChinese);
                    }
                    textBox2.Text = textBox2.Text + "转换配置文件成功" + "\r\n";
                    textBox2.Text = textBox2.Text + "开始写入配置文件\r\n";
                    StreamWriter sw = new StreamWriter(@".\reader\OEBPS\toc.ncx");
                    sw.Write(strSimple);
                    textBox2.Text = textBox2.Text + "写入配置文件成功" + "\r\n";
                    sw.Close();
                }
                catch(Exception ex)
                {
                    textBox2.Text = textBox2.Text + "处理配置文件失败" + "\r\n" + "原因：" + ex.Message;
                    return;
                }
                try
                {
                    textBox2.Text = textBox2.Text + "开始创建" + file + "的输出文件" + "\r\n";
                    ZipFile.CreateFromDirectory(@".\reader", Path.GetDirectoryName(file) +@"\new "+Path.GetFileName(file));

                }
                catch (Exception ex)
                {
                    textBox2.Text = textBox2.Text + "处理" + file + "失败" + "\r\n" + "原因：" + ex.Message;
                    break;
                }
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(@".\reader");
                    dir.Delete(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("删除临时文件错误：" + ex.Message);
                    return;
                }

                textBox2.Text = textBox2.Text + "处理" + file +"成功"+ "\r\n";
            }
        }
    }
}
