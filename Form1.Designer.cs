namespace SimpleCalculator
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
            txtExpression = new TextBox();
            txtResult = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnEqual = new Button();
            btnDot = new Button();
            btnNum0 = new Button();
            btnSign = new Button();
            btnAdd = new Button();
            btnNum3 = new Button();
            btnNum2 = new Button();
            btnNum1 = new Button();
            btnSubtract = new Button();
            btnNum6 = new Button();
            btnNum5 = new Button();
            btnNum4 = new Button();
            btnMultiply = new Button();
            btnNum9 = new Button();
            btnNum8 = new Button();
            btnNum7 = new Button();
            btnDivide = new Button();
            btnCE = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lblTitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtExpression
            // 
            txtExpression.Location = new Point(61, 118);
            txtExpression.Name = "txtExpression";
            txtExpression.ReadOnly = true;
            txtExpression.Size = new Size(423, 27);
            txtExpression.TabIndex = 1;
            txtExpression.TextAlign = HorizontalAlignment.Right;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(61, 166);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(423, 27);
            txtResult.TabIndex = 2;
            txtResult.TextAlign = HorizontalAlignment.Right;
            txtResult.TextAlignChanged += txtResult_TextAlignChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(btnEqual, 3, 4);
            tableLayoutPanel1.Controls.Add(btnDot, 2, 4);
            tableLayoutPanel1.Controls.Add(btnNum0, 1, 4);
            tableLayoutPanel1.Controls.Add(btnSign, 0, 4);
            tableLayoutPanel1.Controls.Add(btnAdd, 3, 3);
            tableLayoutPanel1.Controls.Add(btnNum3, 2, 3);
            tableLayoutPanel1.Controls.Add(btnNum2, 1, 3);
            tableLayoutPanel1.Controls.Add(btnNum1, 0, 3);
            tableLayoutPanel1.Controls.Add(btnSubtract, 3, 2);
            tableLayoutPanel1.Controls.Add(btnNum6, 2, 2);
            tableLayoutPanel1.Controls.Add(btnNum5, 1, 2);
            tableLayoutPanel1.Controls.Add(btnNum4, 0, 2);
            tableLayoutPanel1.Controls.Add(btnMultiply, 3, 1);
            tableLayoutPanel1.Controls.Add(btnNum9, 2, 1);
            tableLayoutPanel1.Controls.Add(btnNum8, 1, 1);
            tableLayoutPanel1.Controls.Add(btnNum7, 0, 1);
            tableLayoutPanel1.Controls.Add(btnDivide, 3, 0);
            tableLayoutPanel1.Controls.Add(btnCE, 0, 0);
            tableLayoutPanel1.Controls.Add(btnDelete, 2, 0);
            tableLayoutPanel1.Controls.Add(btnClear, 1, 0);
            tableLayoutPanel1.Location = new Point(61, 220);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(423, 305);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btnEqual
            // 
            btnEqual.Location = new Point(318, 247);
            btnEqual.Name = "btnEqual";
            btnEqual.Size = new Size(99, 51);
            btnEqual.TabIndex = 19;
            btnEqual.Text = "=";
            btnEqual.UseVisualStyleBackColor = true;
            btnEqual.Click += btnEqual_Click;
            // 
            // btnDot
            // 
            btnDot.Location = new Point(213, 247);
            btnDot.Name = "btnDot";
            btnDot.Size = new Size(99, 55);
            btnDot.TabIndex = 18;
            btnDot.Text = ".";
            btnDot.UseVisualStyleBackColor = true;
            // 
            // btnNum0
            // 
            btnNum0.Location = new Point(108, 247);
            btnNum0.Name = "btnNum0";
            btnNum0.Size = new Size(99, 55);
            btnNum0.TabIndex = 17;
            btnNum0.Text = "0";
            btnNum0.UseVisualStyleBackColor = true;
            btnNum0.Click += nBtn_Click;
            // 
            // btnSign
            // 
            btnSign.Location = new Point(3, 247);
            btnSign.Name = "btnSign";
            btnSign.Size = new Size(99, 55);
            btnSign.TabIndex = 16;
            btnSign.Text = "+/-";
            btnSign.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(318, 186);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(99, 51);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnNum3
            // 
            btnNum3.Location = new Point(213, 186);
            btnNum3.Name = "btnNum3";
            btnNum3.Size = new Size(99, 55);
            btnNum3.TabIndex = 14;
            btnNum3.Text = "3";
            btnNum3.UseVisualStyleBackColor = true;
            btnNum3.Click += nBtn_Click;
            // 
            // btnNum2
            // 
            btnNum2.Location = new Point(108, 186);
            btnNum2.Name = "btnNum2";
            btnNum2.Size = new Size(99, 55);
            btnNum2.TabIndex = 13;
            btnNum2.Text = "2";
            btnNum2.UseVisualStyleBackColor = true;
            btnNum2.Click += nBtn_Click;
            // 
            // btnNum1
            // 
            btnNum1.Location = new Point(3, 186);
            btnNum1.Name = "btnNum1";
            btnNum1.Size = new Size(99, 55);
            btnNum1.TabIndex = 12;
            btnNum1.Text = "1";
            btnNum1.UseVisualStyleBackColor = true;
            btnNum1.Click += nBtn_Click;
            // 
            // btnSubtract
            // 
            btnSubtract.Location = new Point(318, 125);
            btnSubtract.Name = "btnSubtract";
            btnSubtract.Size = new Size(99, 51);
            btnSubtract.TabIndex = 11;
            btnSubtract.Text = "-";
            btnSubtract.UseVisualStyleBackColor = true;
            // 
            // btnNum6
            // 
            btnNum6.Location = new Point(213, 125);
            btnNum6.Name = "btnNum6";
            btnNum6.Size = new Size(99, 55);
            btnNum6.TabIndex = 10;
            btnNum6.Text = "6";
            btnNum6.UseVisualStyleBackColor = true;
            btnNum6.Click += nBtn_Click;
            // 
            // btnNum5
            // 
            btnNum5.Location = new Point(108, 125);
            btnNum5.Name = "btnNum5";
            btnNum5.Size = new Size(99, 55);
            btnNum5.TabIndex = 9;
            btnNum5.Text = "5";
            btnNum5.UseVisualStyleBackColor = true;
            btnNum5.Click += nBtn_Click;
            // 
            // btnNum4
            // 
            btnNum4.Location = new Point(3, 125);
            btnNum4.Name = "btnNum4";
            btnNum4.Size = new Size(99, 55);
            btnNum4.TabIndex = 8;
            btnNum4.Text = "4";
            btnNum4.UseVisualStyleBackColor = true;
            btnNum4.Click += nBtn_Click;
            // 
            // btnMultiply
            // 
            btnMultiply.Location = new Point(318, 64);
            btnMultiply.Name = "btnMultiply";
            btnMultiply.Size = new Size(99, 51);
            btnMultiply.TabIndex = 7;
            btnMultiply.Text = "X";
            btnMultiply.UseVisualStyleBackColor = true;
            // 
            // btnNum9
            // 
            btnNum9.Location = new Point(213, 64);
            btnNum9.Name = "btnNum9";
            btnNum9.Size = new Size(99, 55);
            btnNum9.TabIndex = 6;
            btnNum9.Text = "9";
            btnNum9.UseVisualStyleBackColor = true;
            btnNum9.Click += nBtn_Click;
            // 
            // btnNum8
            // 
            btnNum8.Location = new Point(108, 64);
            btnNum8.Name = "btnNum8";
            btnNum8.Size = new Size(99, 55);
            btnNum8.TabIndex = 5;
            btnNum8.Text = "8";
            btnNum8.UseVisualStyleBackColor = true;
            btnNum8.Click += nBtn_Click;
            // 
            // btnNum7
            // 
            btnNum7.Location = new Point(3, 64);
            btnNum7.Name = "btnNum7";
            btnNum7.Size = new Size(99, 55);
            btnNum7.TabIndex = 4;
            btnNum7.Text = "7";
            btnNum7.UseVisualStyleBackColor = true;
            btnNum7.Click += nBtn_Click;
            // 
            // btnDivide
            // 
            btnDivide.Location = new Point(318, 3);
            btnDivide.Name = "btnDivide";
            btnDivide.Size = new Size(99, 51);
            btnDivide.TabIndex = 3;
            btnDivide.Text = "/";
            btnDivide.UseVisualStyleBackColor = true;
            btnDivide.Click += button4_Click;
            // 
            // btnCE
            // 
            btnCE.Location = new Point(3, 3);
            btnCE.Name = "btnCE";
            btnCE.Size = new Size(99, 51);
            btnCE.TabIndex = 0;
            btnCE.Text = "CE";
            btnCE.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(213, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(99, 55);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "DEL";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(108, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(99, 55);
            btnClear.TabIndex = 2;
            btnClear.Text = "C";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 28.8000011F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblTitle.ForeColor = SystemColors.ActiveCaption;
            lblTitle.Location = new Point(65, 33);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(413, 66);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Simple Calculator";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 579);
            Controls.Add(lblTitle);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(txtResult);
            Controls.Add(txtExpression);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtExpression;
        private TextBox txtResult;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnDot;
        private Button btnNum0;
        private Button btnSign;
        private Button btnNum3;
        private Button btnNum2;
        private Button btnNum1;
        private Button btnNum6;
        private Button btnNum5;
        private Button btnNum4;
        private Button btnNum9;
        private Button btnNum8;
        private Button btnNum7;
        private Button btnClear;
        private Button btnDelete;
        private Button btnCE;
        private Label lblTitle;
        private Button btnEqual;
        private Button btnAdd;
        private Button btnSubtract;
        private Button btnMultiply;
        private Button btnDivide;
    }
}
