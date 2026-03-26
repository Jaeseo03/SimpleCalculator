namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        double resultValue = 0;
        string operationPerformed = "";
        bool isOperationPerformed = false;
        bool isCalculationInside = false; // 결과가 나온 직후인지 확인하는 상태 변수

        public Form1()
        {
            InitializeComponent();
        }

        // 헬퍼 함수: 수식창의 마지막 숫자 덩어리에만 콤마를 적용
        private void ApplyCommaToLastOperand()
        {
            try
            {
                string[] parts = txtExpression.Text.Split(' ');
                if (parts.Length > 0)
                {
                    string lastPart = parts[parts.Length - 1].Replace(",", "");
                    if (double.TryParse(lastPart, out double val))
                    {
                        // 소수점 입력 중일 때는 콤마 포맷팅을 잠시 유보하거나 처리 로직이 복잡해지므로 정수 기준 포맷팅
                        parts[parts.Length - 1] = val.ToString("#,##0");
                        txtExpression.Text = string.Join(" ", parts);
                    }
                }
            }
            catch { }
        }

        // 1. 숫자 버튼 클릭 (btnNum0 ~ 9)
        private void nBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            txtResult.Clear(); // 셋째: 어떤 버튼을 누르든 결과창 비우기

            // 둘째: 결과 후에 숫자를 누르면 기존 결과 무시하고 새로 시작
            if (isCalculationInside)
            {
                txtExpression.Text = "0";
                isCalculationInside = false;
            }

            if (txtExpression.Text == "0") txtExpression.Clear();

            string text = txtExpression.Text;
            if (text.Length > 0)
            {
                char lastChar = text[text.Length - 1];
                if (!char.IsDigit(lastChar) && lastChar != ' ' && lastChar != '.' && lastChar != ',')
                {
                    txtExpression.Text += " ";
                }
            }

            txtExpression.Text += button.Text;

            // 첫째: 세 자리마다 콤마 추가
            ApplyCommaToLastOperand();
        }

        // 2. 사칙연산 버튼 클릭 (+, -, *, /)
        // 2. 사칙연산 버튼 클릭 (+, -, *, /)
        private void op_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                // ✅ 수정된 핵심 로직: 결과가 나온 직후 연산자를 누른 경우
                if (isCalculationInside)
                {
                    // 결과창에 있는 숫자(콤마 포함됨)를 수식창으로 옮기고 시작
                    txtExpression.Text = txtResult.Text;
                    isCalculationInside = false; // 상태 해제
                }

                // 현재 수식창의 마지막 숫자(또는 결과값)에서 콤마를 빼고 숫자로 인식
                string[] parts = txtExpression.Text.Split(' ');
                string lastOperand = parts[parts.Length - 1].Replace(",", "");

                if (double.TryParse(lastOperand, out resultValue))
                {
                    operationPerformed = button.Text;
                    isOperationPerformed = true;

                    // 결과창은 이제 비워줌 (셋째 요구사항: 버튼 누르면 사라짐)
                    txtResult.Clear();

                    // 수식창에 "결과값 연산자 " 형태로 이어 붙임
                    txtExpression.Text = lastOperand.ToString() + " " + operationPerformed + " ";

                    // 콤마 다시 적용 (결과값에도 콤마가 있었을 테니 다시 예쁘게 출력)
                    ApplyCommaToLastOperand();
                }
            }
            catch
            {
                // 숫자가 없는 상태에서 연산자 클릭 시 방어 로직
            }
        }

        // 3. 결과 버튼 클릭 (=)
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parts = txtExpression.Text.Split(' ');
                // 마지막 피연산자도 콤마 제거 후 변환
                double secondValue = double.Parse(parts[parts.Length - 1].Replace(",", ""));
                double finalResult = 0;

                switch (operationPerformed)
                {
                    case "+": finalResult = resultValue + secondValue; break;
                    case "-": finalResult = resultValue - secondValue; break;
                    case "*":
                    case "X": finalResult = resultValue * secondValue; break;
                    case "/":
                        if (secondValue != 0) finalResult = resultValue / secondValue;
                        else { MessageBox.Show("0으로 나눌 수 없습니다."); return; }
                        break;
                }

                // 결과창에 콤마 포맷 적용
                txtResult.Text = finalResult.ToString("#,##0");

                // 수식창에는 "=" 기호 안 뜨게 유지
                isCalculationInside = true;
                isOperationPerformed = true;
            }
            catch
            {
                MessageBox.Show("계산할 수 없는 수식입니다.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resultValue = 0;
            operationPerformed = "";
            isOperationPerformed = false;
            isCalculationInside = false;

            txtExpression.Text = "0";
            txtResult.Clear();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            if (txtExpression.Text.EndsWith(" ")) return;

            string[] parts = txtExpression.Text.Trim().Split(' ');
            if (parts.Length > 0)
            {
                txtExpression.Text = string.Join(" ", parts, 0, parts.Length - 1);

                if (!string.IsNullOrEmpty(txtExpression.Text))
                    txtExpression.Text += " ";
                else
                    txtExpression.Text = "0";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            if (txtExpression.Text.Length > 0 && txtExpression.Text != "0")
            {
                if (txtExpression.Text.EndsWith(" ")) return;

                txtExpression.Text = txtExpression.Text.Remove(txtExpression.Text.Length - 1);

                if (string.IsNullOrWhiteSpace(txtExpression.Text))
                    txtExpression.Text = "0";
                else
                    ApplyCommaToLastOperand(); // 지운 후에도 콤마 재배치
            }
        }
    }
}