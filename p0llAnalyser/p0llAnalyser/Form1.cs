using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using p0llAnalyser.objects;


namespace p0llAnalyser
{
    public partial class Form1 : Form
    {

        private String dirName;
        private List<Question> questions;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dirName = chooseDir();
            if (String.IsNullOrWhiteSpace(dirName))
            {
                Close();
            }
            loadJson(dirName);
            foreach(Question question in questions){
                Label label = new Label();
                
                label.Text = question.title;
                flowLayoutPanel1.Controls.Add(label);
            }
        }

        private String chooseDir()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ResultDatei|results_by_pr0p0ll";
            ofd.Title = "Pollergebniss auswählen";
            ofd.ShowDialog();

            if (String.IsNullOrWhiteSpace(ofd.FileName))
            {
                return "";
            }
            else
            {
                return Path.GetDirectoryName(ofd.FileName);
            }
        }

        private void loadJson(String dirName)
        {
            try
            {

        
            String jsonFile = Directory.GetFiles(dirName, "*.json").First();



            String json = File.ReadAllText(jsonFile);

            JObject jObject = JObject.Parse(json);
            Info info = jObject["info"].ToObject<Info>();

            questions = new List<Question>();

            int questionNr = 1;
            while (jObject["q" + questionNr] != null)
            {
                Question question = jObject["q" + questionNr].ToObject<Question>();
                int answerNr = 1;
                while (jObject["q" + questionNr]["a" + answerNr] != null)
                {
                    Answer answer = jObject["q" + questionNr]["a" + answerNr].ToObject<Answer>();
                    question.answers.Add(answer);
                    answerNr++;
                }
                questions.Add(question);
                questionNr++;

            }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
