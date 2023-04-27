namespace SuperMarioArturoBros
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TIMER = new System.Windows.Forms.Timer(components);
            PCT_CANVAS = new PictureBox();
            COUNTDOWN = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)PCT_CANVAS).BeginInit();
            SuspendLayout();
            // 
            // TIMER
            // 
            TIMER.Enabled = true;
            TIMER.Interval = 25;
            TIMER.Tick += TIMER_Tick;
            // 
            // PCT_CANVAS
            // 
            PCT_CANVAS.BackColor = Color.Black;
            PCT_CANVAS.BorderStyle = BorderStyle.FixedSingle;
            PCT_CANVAS.Location = new Point(13, 14);
            PCT_CANVAS.Name = "PCT_CANVAS";
            PCT_CANVAS.Size = new Size(1039, 655);
            PCT_CANVAS.SizeMode = PictureBoxSizeMode.StretchImage;
            PCT_CANVAS.TabIndex = 0;
            PCT_CANVAS.TabStop = false;
            // 
            // COUNTDOWN
            // 
            COUNTDOWN.Enabled = true;
            COUNTDOWN.Interval = 1000;
            COUNTDOWN.Tick += COUNTDOWN_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(PCT_CANVAS);
            Name = "Form1";
            Text = "SuperMarioArturoBros";
            SizeChanged += Form1_SizeChanged;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)PCT_CANVAS).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer TIMER;
        private PictureBox PCT_CANVAS;
        private System.Windows.Forms.Timer COUNTDOWN;
    }
}