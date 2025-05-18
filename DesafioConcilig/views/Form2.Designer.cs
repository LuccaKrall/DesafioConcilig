namespace DesafioConcilig.views
{
    partial class Form2
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
            label1 = new Label();
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            label3 = new Label();
            label2 = new Label();
            button2 = new Button();
            label4 = new Label();
            button5 = new Button();
            textBox1 = new TextBox();
            label5 = new Label();
            button6 = new Button();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Assertive;
            label1.Location = new Point(25, 83);
            label1.Name = "label1";
            label1.Size = new Size(142, 21);
            label1.TabIndex = 0;
            label1.Text = "Adicionar Arquivo";
            // 
            // button1
            // 
            button1.Image = Properties.Resources.open_folder;
            button1.ImageAlign = ContentAlignment.MiddleRight;
            button1.Location = new Point(166, 78);
            button1.Name = "button1";
            button1.Size = new Size(116, 34);
            button1.TabIndex = 1;
            button1.Text = "Selecionar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Image = Properties.Resources.database;
            button3.ImageAlign = ContentAlignment.MiddleRight;
            button3.Location = new Point(49, 188);
            button3.Name = "button3";
            button3.Size = new Size(203, 36);
            button3.TabIndex = 4;
            button3.Text = "Salvar Banco de Dados";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(68, 128);
            button4.Name = "button4";
            button4.Size = new Size(144, 23);
            button4.TabIndex = 5;
            button4.Text = "Salvar Arquivo";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(22, 164);
            label3.Name = "label3";
            label3.Size = new Size(259, 21);
            label3.TabIndex = 6;
            label3.Text = "Salvar Arquivo no banco de dados";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(438, 91);
            label2.Name = "label2";
            label2.Size = new Size(203, 21);
            label2.TabIndex = 7;
            label2.Text = "Consultar Contratos(Geral)";
            label2.Click += label2_Click_1;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.magnifying_glass;
            button2.ImageAlign = ContentAlignment.MiddleRight;
            button2.Location = new Point(489, 145);
            button2.Name = "button2";
            button2.Size = new Size(113, 40);
            button2.TabIndex = 8;
            button2.Text = "Consultar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(671, 91);
            label4.Name = "label4";
            label4.Size = new Size(230, 21);
            label4.TabIndex = 9;
            label4.Text = "Consultar contratos do cliente";
            // 
            // button5
            // 
            button5.Image = Properties.Resources.magnifying_glass;
            button5.ImageAlign = ContentAlignment.MiddleRight;
            button5.Location = new Point(734, 145);
            button5.Name = "button5";
            button5.Size = new Size(110, 40);
            button5.TabIndex = 10;
            button5.Text = "Consultar";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(734, 115);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(129, 23);
            textBox1.TabIndex = 12;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(306, 278);
            label5.Name = "label5";
            label5.Size = new Size(309, 21);
            label5.TabIndex = 13;
            label5.Text = "Controle de Contratos inseridos/Usuario ";
            label5.Click += label5_Click;
            // 
            // button6
            // 
            button6.Image = Properties.Resources.magnifying_glass;
            button6.ImageAlign = ContentAlignment.MiddleRight;
            button6.Location = new Point(407, 326);
            button6.Name = "button6";
            button6.Size = new Size(113, 40);
            button6.TabIndex = 14;
            button6.Text = "Consultar";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(319, 9);
            label6.Name = "label6";
            label6.Size = new Size(283, 40);
            label6.TabIndex = 15;
            label6.Text = "Area Adminstrativa";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(686, 118);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 16;
            label7.Text = "NOME";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(913, 388);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(button6);
            Controls.Add(label5);
            Controls.Add(textBox1);
            Controls.Add(button5);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button3;
        private Button button4;
        private Label label3;
        private Label label2;
        private Button button2;
        private Label label4;
        private Button button5;
        private TextBox textBox1;
        private Label label5;
        private Button button6;
        private Label label6;
        private Label label7;
    }
}