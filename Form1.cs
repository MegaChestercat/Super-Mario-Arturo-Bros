namespace SuperMarioArturoBros
{
    public partial class Form1 : Form
    {

        Map map;
        private static Rectangle src, dst;

        string score;
        bool right, left, dinoJump;
        int elapsed;


        public Form1()
        {
            InitializeComponent();
        }

        private void Init()
        {
            map = new Map();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }
    }
}