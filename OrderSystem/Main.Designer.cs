namespace RestaurantOrderSystem
{
    partial class Main
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRice = new System.Windows.Forms.Button();
            this.btnSoup = new System.Windows.Forms.Button();
            this.btnNoodle = new System.Windows.Forms.Button();
            this.btnDrink = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRice);
            this.groupBox1.Controls.Add(this.btnSoup);
            this.groupBox1.Controls.Add(this.btnNoodle);
            this.groupBox1.Controls.Add(this.btnDrink);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(193, 419);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // btnRice
            // 
            this.btnRice.Location = new System.Drawing.Point(0, 260);
            this.btnRice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRice.Name = "btnRice";
            this.btnRice.Size = new System.Drawing.Size(193, 37);
            this.btnRice.TabIndex = 3;
            this.btnRice.Text = "Rice";
            this.btnRice.UseVisualStyleBackColor = true;
            this.btnRice.Click += new System.EventHandler(this.btnRice_Click);
            // 
            // btnSoup
            // 
            this.btnSoup.Location = new System.Drawing.Point(0, 215);
            this.btnSoup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSoup.Name = "btnSoup";
            this.btnSoup.Size = new System.Drawing.Size(193, 37);
            this.btnSoup.TabIndex = 2;
            this.btnSoup.Text = "Soup";
            this.btnSoup.UseVisualStyleBackColor = true;
            this.btnSoup.Click += new System.EventHandler(this.btnSoup_Click);
            // 
            // btnNoodle
            // 
            this.btnNoodle.Location = new System.Drawing.Point(0, 169);
            this.btnNoodle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNoodle.Name = "btnNoodle";
            this.btnNoodle.Size = new System.Drawing.Size(193, 37);
            this.btnNoodle.TabIndex = 1;
            this.btnNoodle.Text = "Noodle";
            this.btnNoodle.UseVisualStyleBackColor = true;
            this.btnNoodle.Click += new System.EventHandler(this.btnNoodle_Click);
            // 
            // btnDrink
            // 
            this.btnDrink.Location = new System.Drawing.Point(0, 124);
            this.btnDrink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDrink.Name = "btnDrink";
            this.btnDrink.Size = new System.Drawing.Size(193, 37);
            this.btnDrink.TabIndex = 0;
            this.btnDrink.Text = "Drink";
            this.btnDrink.UseVisualStyleBackColor = true;
            this.btnDrink.Click += new System.EventHandler(this.btnDrink_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(201, 57);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(583, 300);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Text", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(257, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "W3 Restaurant";
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(576, 19);
            this.btnOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(100, 31);
            this.btnOrders.TabIndex = 3;
            this.btnOrders.Text = "My Orders";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(684, 19);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(100, 31);
            this.btnStaff.TabIndex = 4;
            this.btnStaff.Text = "Staff";
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(340, 365);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(336, 53);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 435);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnStaff);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRice;
        private System.Windows.Forms.Button btnSoup;
        private System.Windows.Forms.Button btnNoodle;
        private System.Windows.Forms.Button btnDrink;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Button btnAdd;
    }
}

