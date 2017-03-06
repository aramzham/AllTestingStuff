namespace Dices
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.your_score = new System.Windows.Forms.TextBox();
            this.opponent_score = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Roll = new System.Windows.Forms.Button();
            this.yourTotal = new System.Windows.Forms.Label();
            this.opponentTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // your_score
            // 
            this.your_score.Location = new System.Drawing.Point(61, 50);
            this.your_score.Name = "your_score";
            this.your_score.Size = new System.Drawing.Size(68, 26);
            this.your_score.TabIndex = 0;
            // 
            // opponent_score
            // 
            this.opponent_score.Location = new System.Drawing.Point(397, 50);
            this.opponent_score.Name = "opponent_score";
            this.opponent_score.Size = new System.Drawing.Size(68, 26);
            this.opponent_score.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "You";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(393, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Opponent";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(205, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 109);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Roll
            // 
            this.Roll.Location = new System.Drawing.Point(142, 309);
            this.Roll.Name = "Roll";
            this.Roll.Size = new System.Drawing.Size(243, 55);
            this.Roll.TabIndex = 5;
            this.Roll.Text = "You roll";
            this.Roll.UseVisualStyleBackColor = true;
            this.Roll.Click += new System.EventHandler(this.Roll_Click);
            // 
            // yourTotal
            // 
            this.yourTotal.AutoSize = true;
            this.yourTotal.Location = new System.Drawing.Point(54, 143);
            this.yourTotal.Name = "yourTotal";
            this.yourTotal.Size = new System.Drawing.Size(51, 20);
            this.yourTotal.TabIndex = 6;
            this.yourTotal.Text = "label3";
            // 
            // opponentTotal
            // 
            this.opponentTotal.AutoSize = true;
            this.opponentTotal.Location = new System.Drawing.Point(393, 143);
            this.opponentTotal.Name = "opponentTotal";
            this.opponentTotal.Size = new System.Drawing.Size(51, 20);
            this.opponentTotal.TabIndex = 7;
            this.opponentTotal.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 410);
            this.Controls.Add(this.opponentTotal);
            this.Controls.Add(this.yourTotal);
            this.Controls.Add(this.Roll);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.opponent_score);
            this.Controls.Add(this.your_score);
            this.Name = "Form1";
            this.Text = "Dice";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox your_score;
        private System.Windows.Forms.TextBox opponent_score;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Roll;
        private System.Windows.Forms.Label yourTotal;
        private System.Windows.Forms.Label opponentTotal;
    }
}

