using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiPhotos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBoxDoubleClick(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            var openFileWindow = new OpenFileDialog();
            if (openFileWindow.ShowDialog() == DialogResult.OK)
            {

                HttpClient webApiClient = new HttpClient();
                webApiClient.BaseAddress = new Uri("http://89.67.144.106:9150");
                var pictureBytes = File.ReadAllBytes(openFileWindow.FileName);
                var pictureString = Convert.ToBase64String(pictureBytes);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Picture() { Id = pictureBox.Name, Content = pictureString });
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                webApiClient.PostAsync("api/Picture", content);
            }

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1.Image = GetPictureFor("p1");
            p2.Image = GetPictureFor("p2");
            p3.Image = GetPictureFor("p3");
            p4.Image = GetPictureFor("p4");
            p5.Image = GetPictureFor("p5");
            p6.Image = GetPictureFor("p6");
        }

        private Image GetPictureFor(string value)
        {
            HttpClient webApiClient = new HttpClient();
            webApiClient.BaseAddress = new Uri("http://89.67.144.106:9150");
            var pictureString = webApiClient.GetStringAsync("api/Picture/" + value).Result;
            var pictureObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Picture>(pictureString);
            if (pictureObj == null)
            {
                return null;
            }
            var pictureContent = pictureObj.Content;
            var pictureBytes = Convert.FromBase64String(pictureContent);
            MemoryStream ms = new MemoryStream(pictureBytes);
            ms.Position = 0;
            return Image.FromStream(ms);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }

    public  class Picture
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
