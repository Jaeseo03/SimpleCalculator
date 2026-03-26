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

        // 1. 숫자 버튼 클릭 (btnNum0 ~ 9)
        private void nBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // 결과가 나온 직후(isCalculationInside) 숫자를 누르면
            // 결과창의 값을 수식창으로 가져와서 뒤에 숫자를 붙임
            if (isCalculationInside)
            {
                txtExpression.Text = txtResult.Text;
                isCalculationInside = false; // 상태 해제
            }

            if (txtExpression.Text == "0") txtExpression.Clear();

            txtExpression.Text += button.Text;
        }

        // 2. 사칙연산 버튼 클릭 (+, -, *, /)
        private void op_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                // 결과가 나온 직후 연산자를 누르면 결과값을 첫 번째 피연산자로 사용
                if (isCalculationInside)
                {
                    txtExpression.Text = txtResult.Text;
                    isCalculationInside = false;
                }

                string[] parts = txtExpression.Text.Split(' ');
                resultValue = double.Parse(parts[parts.Length - 1]);

                operationPerformed = button.Text;
                isOperationPerformed = true;

                // 수식창에 연산자 이어 붙이기
                txtExpression.Text += " " + operationPerformed + " ";
            }
            catch { }
        }

        // 3. 결과 버튼 클릭 (=)
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parts = txtExpression.Text.Split(' ');
                double secondValue = double.Parse(parts[parts.Length - 1]);
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

                // 결과값 표시 (아래창에만)
                txtResult.Text = finalResult.ToString();

                // 수식창(위쪽)은 건드리지 않거나, 
                // 원하신 대로 "=" 기호 없이 그대로 둡니다.

                isCalculationInside = true; // "결과가 나왔음" 상태 표시
                isOperationPerformed = true;
            }
            catch
            {
                MessageBox.Show("계산할 수 없는 수식입니다.");
            }
        }
    }
}