using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Clear();

            oFD1.InitialDirectory = "C:\\";
            oFD1.Title = "Open an Image File";
            oFD1.Filter = "JPEGS|*.jpg|GIFS|*.gif";

            var ofdResults = oFD1.ShowDialog();

            if (ofdResults == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                int num_of_files = oFD1.FileNames.Length;
                string[] aryFilePaths = new string[num_of_files];
                int counter = 0;

                foreach (string single_file in oFD1.FileNames)
                {
                    aryFilePaths[counter] = single_file;
                    imageList1.Images.Add(Image.FromFile(single_file));
                    counter++;
                }

                listView1.LargeImageList = imageList1;

                for (int i = 0; i < counter; i++)
                {
                    listView1.Items.Add(aryFilePaths[i], i);
                }
            }

            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string big_filename;

            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                big_filename = listView1.SelectedItems[i].Text;
                pictureBox1.Image = Image.FromFile(big_filename);
                panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width,
                                            pictureBox1.Image.Height);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap bmp_new = new
                Bitmap(Convert.ToInt32(pictureBox1.Image.Width / 2),
                Convert.ToInt32(pictureBox1.Image.Height / 2));
                Graphics gr = Graphics.FromImage(bmp_new);
                gr.DrawImage(bmp, 0, 0, bmp_new.Width, bmp_new.Height);
                pictureBox1.Image = bmp_new;

            }
        }
    }
}
