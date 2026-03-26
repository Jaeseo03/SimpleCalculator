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
        private bool isDarkMode = true; // 기본값은 다크모드

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

        // 테마별 색상 정의 (배경, 디스플레이, 숫자버튼, 연산버튼, 텍스트)
        private void SetThemeColors()
        {
            if (isDarkMode)
            {
                colorBg = Color.FromArgb(32, 33, 36);
                colorDisplay = Color.FromArgb(45, 46, 50);
                colorNumBtn = Color.FromArgb(60, 64, 67);
                colorOpBtn = Color.FromArgb(0, 122, 204);
                // [수정] 다크모드에서 글자가 아주 잘 보이도록 순백색과 밝은 하늘색 적용
                colorText = Color.White;
                colorAccentText = Color.FromArgb(138, 180, 248);
            }
            else // 라이트 모드
            {
                colorBg = Color.FromArgb(241, 243, 244);
                colorDisplay = Color.White;
                colorNumBtn = Color.FromArgb(255, 255, 255);
                colorOpBtn = Color.FromArgb(232, 234, 237);
                colorText = Color.FromArgb(60, 64, 67);
                colorAccentText = Color.FromArgb(25, 103, 210);
            }
        }

        // 2. 부드러운 UI 적용 버전
        private void ApplyPremiumDesign_V2()
        {
            SetThemeColors();

            this.BackColor = colorBg;

            // 1. 입력창(수식) 디자인: 글씨 크기를 14pt로 키우고 색상 적용
            txtExpression.BackColor = colorDisplay;
            txtExpression.ForeColor = isDarkMode ? Color.FromArgb(200, 200, 200) : Color.Gray; // 수식은 약간 흐리게
            txtExpression.Font = new Font("맑은 고딕", 14F, FontStyle.Regular);

            // 2. 결과창 디자인: 글씨 크기를 28pt로 크게 키우고 강조색 적용
            txtResult.BackColor = colorDisplay;
            txtResult.ForeColor = colorAccentText;
            txtResult.Font = new Font("맑은 고딕", 28F, FontStyle.Bold);

            // 3. 테마 전환 버튼: 현재 모드 이름을 표시 (Dark/Light)
            if (btnThemeToggle != null)
            {
                btnThemeToggle.Text = isDarkMode ? "Dark" : "Light"; // 요청하신 대로 수정
                btnThemeToggle.BackColor = isDarkMode ? Color.FromArgb(60, 64, 67) : Color.FromArgb(225, 225, 225);
                btnThemeToggle.ForeColor = colorText;
                btnThemeToggle.FlatStyle = FlatStyle.Flat;
                btnThemeToggle.FlatAppearance.BorderSize = 0;
                btnThemeToggle.Cursor = Cursors.Hand;
            }

            // 버튼들 둥글게 그리기 (기존 코드 유지)
            if (tableLayoutPanel1 != null)
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if (c is Button btn)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Font = new Font("맑은 고딕", 16F, FontStyle.Bold);
                        btn.ForeColor = (isDarkMode || btn.Text == "=") ? Color.White : Color.Black;

                        if (char.IsDigit(btn.Text[0]) || btn.Text == ".") btn.BackColor = colorNumBtn;
                        else if (btn.Text == "=") btn.BackColor = colorAccent;
                        else btn.BackColor = colorOpBtn;

                        btn.Paint -= Btn_Paint;
                        btn.Paint += Btn_Paint;
                        btn.Invalidate();
                    }
                }
            }
        }

        // 디자인 창에서 버튼을 더블 클릭해서 이 이벤트를 연결해 주세요!
        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ApplyPremiumDesign_V2(); // 디자인 재적용
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

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // 안티앨리어싱 적용
            Color baseColor = btn.BackColor;

            using (SolidBrush brush = new SolidBrush(baseColor))
            {
                // 마지막 숫자 18이 둥글기 정도입니다. 더 부드럽게 조정했어요.
                FillRoundedRectangle(e.Graphics, brush, 1, 1, btn.Width - 3, btn.Height - 3, 18);
            }

            // 텍스트 그리기
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using (SolidBrush textBrush = new SolidBrush(btn.ForeColor))
            {
                e.Graphics.DrawString(btn.Text, btn.Font, textBrush, btn.ClientRectangle, sf);
            }
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
            // 마지막 두 숫자를 40, 40으로 높이면 전체 창이 더 부드러워집니다.
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
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