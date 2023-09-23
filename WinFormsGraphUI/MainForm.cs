using System.Security;
using DOAN_LTDT_2023;
namespace WinFormsApp2
{
    public partial class MainForm : Form
    {
        private OpenFileDialog openFileDialog1;
        private GraphAnalysis graph;
        public MainForm()
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "*.txt",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file",
                InitialDirectory = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory()))
            };

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog1.FileName;
                    textBoxFilePath.Text = filePath;
                    graph = new GraphAnalysis(filePath);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (graph == null)
            {
                MessageBox.Show($"Bạn chưa tải tệp đồ thị");
                return;
            }

            List<string> listOutput = graph.PrintGraphInfor();
            foreach (string output in listOutput)
            {
                listBox1.Items.Add(output);
            }
        }
    }
}