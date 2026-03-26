using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        double resultValue = 0;
        string operationPerformed = "";
        string fullExpression = "";
        bool isOperationPerformed = false;
        bool isEqualed = false;

        // 디자인 컬러 정의 (배경색을 살짝 밝게 조정)
        Color colorBg = Color.FromArgb(45, 48, 54);        // 메인 배경: 덜 어두운 검은색 (조정됨)
        Color colorDisplay = Color.FromArgb(55, 58, 64);   // 디스플레이: 배경보다 살짝 더 밝게 (조정됨)
        Color colorNumBtn = Color.FromArgb(65, 68, 75);    // 숫자 버튼: 가독성을 위해 상향 (조정됨)
        Color colorOpBtn = Color.FromArgb(0, 122, 204);    // 연산 버튼: 기존 유지 (파란색)
        Color colorAccent = Color.FromArgb(255, 120, 0);   // 강조 버튼: 기존 유지 (주황색)
        Color colorText = Color.FromArgb(240, 240, 240);   // 텍스트: 더 밝은 흰색계열로 변경
        Color colorAccentText = Color.FromArgb(80, 210, 255); // 결과 텍스트: 가독성 보강

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private Panel panelHeader = new Panel();

        public Form1()
        {
            InitializeComponent();
            ApplyPremiumDesign_V2();
        }

        private void ApplyPremiumDesign_V2()
        {
            // 1. 기본 폼 설정
            this.BackColor = colorBg;
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.None;

            // 2. 수식 입력창 설정 (상단)
            txtExpression.BackColor = colorDisplay;
            txtExpression.ForeColor = Color.DarkGray;
            txtExpression.BorderStyle = BorderStyle.None;
            txtExpression.Font = new Font("맑은 고딕", 14F, FontStyle.Regular);
            txtExpression.TextAlign = HorizontalAlignment.Right;
            // 여백 추가: 텍스트박스 내부가 아니라 컨트롤 자체의 여백 설정
            txtExpression.Margin = new Padding(0, 10, 20, 0);

            // 3. 결과 출력창 설정 (하단)
            txtResult.Multiline = true;
            txtResult.Height = 70; // 폰트 크기에 맞춰 높이를 조금 더 확보
            txtResult.BackColor = colorDisplay;
            txtResult.ForeColor = colorAccentText;
            txtResult.BorderStyle = BorderStyle.None;
            txtResult.Font = new Font("맑은 고딕", 30F, FontStyle.Bold); // 32pt에서 30pt로 살짝 조절 (안정감)
            txtResult.TextAlign = HorizontalAlignment.Right;
            txtResult.Margin = new Padding(0, 0, 20, 10); // 오른쪽과 아래에 여백

            // 4. 버튼 그리드 디자인
            if (tableLayoutPanel1 != null)
            {
                // 버튼 사이의 간격을 넓혀서 시원하게 만듦
                tableLayoutPanel1.Padding = new Padding(10);

                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if (c is Button btn)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Cursor = Cursors.Hand;
                        btn.Font = new Font("맑은 고딕", 16F, FontStyle.Bold); // 버튼 글씨도 살짝 조절
                        btn.ForeColor = Color.White;
                        btn.Margin = new Padding(5); // 버튼 간 간격 확보

                        // 버튼 색상 로직
                        if (char.IsDigit(btn.Text[0]) || btn.Text == ".") btn.BackColor = colorNumBtn;
                        else if (btn.Text == "=") btn.BackColor = colorAccent;
                        else btn.BackColor = colorOpBtn;

                        btn.Paint += Btn_Paint;
                        btn.MouseDown += (s, e) => { if (s is Button b) { b.BackColor = Color.White; b.Invalidate(); } };
                        btn.MouseUp += (s, e) =>
                        {
                            if (s is Button b)
                            {
                                if (char.IsDigit(b.Text[0]) || b.Text == ".") b.BackColor = colorNumBtn;
                                else if (b.Text == "=") b.BackColor = colorAccent;
                                else b.BackColor = colorOpBtn;
                                b.Invalidate();
                            }
                        };
                    }
                }
            }

            // 5. 헤더 패널
            panelHeader.Size = new Size(this.Width, 35);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.BackColor = colorBg;
            panelHeader.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); } };
            this.Controls.Add(panelHeader);

            // 6. 종료 버튼
            Button btnClose = new Button { Text = "×", Size = new Size(30, 30), Location = new Point(this.Width - 35, 3), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("맑은 고딕", 12, FontStyle.Bold) };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            panelHeader.Controls.Add(btnClose);
        }

        private void nBtn_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;

            // 만약 "="을 눌러 계산이 끝난 상태에서 숫자를 누르면, 수식창을 비우고 새로 시작
            if (isEqualed)
            {
                fullExpression = "";
                txtExpression.Text = "";
                txtResult.Text = "0";
                isEqualed = false;
            }

            fullExpression += button.Text;
            txtExpression.Text = fullExpression;

            if (isOperationPerformed || txtResult.Text == "0")
            {
                txtResult.Clear();
                isOperationPerformed = false;
            }
            txtResult.Text += button.Text;
        }

        private void op_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            if (fullExpression.Length > 0)
            {
                char lastChar = fullExpression[fullExpression.Length - 1];
                if ("+-×÷".Contains(lastChar))
                    fullExpression = fullExpression.Remove(fullExpression.Length - 1) + button.Text;
                else
                    fullExpression += button.Text;
                txtExpression.Text = fullExpression;
                isOperationPerformed = true;
                isEqualed = false;
            }
        }

        private void btnEqual_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fullExpression)) return;

                // 1. 계산을 위해 연산자 변환 (× -> *, ÷ -> /)
                string computeExpression = fullExpression.Replace("×", "*").Replace("÷", "/").Replace(",", "");

                // 2. 계산 수행
                var result = new System.Data.DataTable().Compute(computeExpression, null);
                double finalResult = Convert.ToDouble(result);

                // 3. [교수님 요청사항] 상단 입력창에 "2 + 5 = 7" 형태로 출력
                txtExpression.Text = $"{fullExpression} = {finalResult:#,##0.######}";

                // 4. 하단 결과창에도 결과값 표시
                txtResult.Text = finalResult.ToString("#,##0.######");

                // 5. 다음 계산을 위해 현재 결과값을 fullExpression에 저장 (연속 계산용)
                fullExpression = finalResult.ToString();
                isEqualed = true;
            }
            catch
            {
                MessageBox.Show("수식이 올바르지 않습니다.");
                btnClear_Click(this, EventArgs.Empty);
            }
        }

        // ✅ [수정] CE 버튼: 현재 입력 중인 숫자와 상단 수식의 마지막 숫자를 지웁니다.
        private void btnCE_Click(object? sender, EventArgs e)
        {
            // 1. 하단 결과창 초기화
            txtResult.Text = "0";

            // 2. 상단 수식(fullExpression)에서 마지막 숫자 부분 제거
            if (!string.IsNullOrEmpty(fullExpression))
            {
                // 연산자(+, -, ×, ÷)의 위치를 찾음
                char[] operators = { '+', '-', '×', '÷' };
                int lastOpIndex = fullExpression.LastIndexOfAny(operators);

                if (lastOpIndex != -1)
                {
                    // 연산자가 있다면 그 이후의 숫자만 잘라냄
                    fullExpression = fullExpression.Substring(0, lastOpIndex + 1);
                }
                else
                {
                    // 연산자가 없다면 전체가 숫자이므로 전체 삭제
                    fullExpression = "";
                }

                txtExpression.Text = fullExpression;
            }

            isOperationPerformed = false; // 다시 숫자를 입력할 수 있는 상태로 설정
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (isEqualed) return;
            if (fullExpression.Length > 0)
            {
                fullExpression = fullExpression.Remove(fullExpression.Length - 1);
                txtExpression.Text = fullExpression;
                if (txtResult.Text.Length > 0 && txtResult.Text != "0")
                {
                    txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
                    if (string.IsNullOrEmpty(txtResult.Text)) txtResult.Text = "0";
                }
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            fullExpression = "";
            txtExpression.Text = "";
            txtResult.Text = "0";
            isEqualed = false;
            isOperationPerformed = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                string key = (e.KeyCode >= Keys.NumPad0) ? (e.KeyCode - Keys.NumPad0).ToString() : (e.KeyCode - Keys.D0).ToString();
                nBtn_Click(new Button { Text = key }, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Add || (e.Shift && e.KeyCode == Keys.Oemplus)) ExecuteOpByText("+");
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) ExecuteOpByText("-");
            else if (e.KeyCode == Keys.Multiply) ExecuteOpByText("×");
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.OemQuestion) ExecuteOpByText("÷");
            else if (e.KeyCode == Keys.Enter) btnEqual_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Back) btnDelete_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape) btnClear_Click(this, EventArgs.Empty);
        }

        private void Btn_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button btn) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color baseColor = btn.BackColor;
            using (LinearGradientBrush brush = new LinearGradientBrush(btn.ClientRectangle, ControlPaint.Light(baseColor), baseColor, 90F))
                FillRoundedRectangle(e.Graphics, brush, 2, 2, btn.Width - 4, btn.Height - 4, 10);
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(btn.Text, btn.Font, new SolidBrush(btn.ForeColor), btn.ClientRectangle, sf);
        }

        public static void FillRoundedRectangle(Graphics graphics, Brush brush, int x, int y, int width, int height, int radius)
        {
            using (GraphicsPath path = GetRoundedRectanglePath(x, y, width, height, radius)) graphics.FillPath(brush, path);
        }

        public static GraphicsPath GetRoundedRectanglePath(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure(); return path;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        private void ExecuteOpByText(string op) { op_Click(new Button { Text = op }, EventArgs.Empty); }

        // ✅ [수정] +/- 버튼 대신 사용될 스마트 괄호 버튼 이벤트
        private void btnParenthesis_Click(object sender, EventArgs e)
        {
            if (isEqualed)
            {
                fullExpression = "";
                txtExpression.Text = "";
                txtResult.Text = "0";
                isEqualed = false;
            }

            // 1. 현재 수식에서 열린 괄호 '('와 닫힌 괄호 ')'의 개수를 셉니다.
            int openCount = fullExpression.Count(f => f == '(');
            int closeCount = fullExpression.Count(f => f == ')');

            // 2. 마지막 글자를 확인하여 상황에 맞는 괄호를 판단합니다.
            char lastChar = fullExpression.Length > 0 ? fullExpression[fullExpression.Length - 1] : '\0';

            // 닫는 괄호를 넣어야 하는 상황: 
            // 열린 괄호가 더 많고 + 마지막이 숫지이거나 닫는 괄호인 경우
            if (openCount > closeCount && (char.IsDigit(lastChar) || lastChar == ')'))
            {
                fullExpression += ")";
            }
            else
            {
                // 그 외의 경우는 모두 열기 괄호 "("
                // 만약 숫자 뒤에 바로 "("를 누르면 자동으로 "*"를 추가해주는 센스
                if (char.IsDigit(lastChar))
                {
                    fullExpression += "×(";
                }
                else
                {
                    fullExpression += "(";
                }
            }

            txtExpression.Text = fullExpression;
            isOperationPerformed = true; // 다음 숫자 입력을 위해 준비
        }
    }
}