namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        // ✅ 수정 포인트 1: 변수를 클래스 내부로 이동 (필드 선언)
        double resultValue = 0;          // 첫 번째 숫자 저장용
        string operationPerformed = "";    // 어떤 연산인지 저장용
        bool isOperationPerformed = false; // 연산자 클릭 여부 확인용

        public Form1()
        {
            InitializeComponent();
        }

        private void txtResult_TextAlignChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void nBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // 위쪽 수식창이 "0"일 때만 지우고, 나머지는 계속 이어 붙임
            if (txtExpression.Text == "0")
            {
                txtExpression.Clear();
            }

            // 연산자 버튼을 누른 직후에 숫자를 누르는 경우라도 Clear하지 않고 
            // isOperationPerformed 상태만 해제해서 계속 이어 붙이게 함
            isOperationPerformed = false;

            txtExpression.Text += button.Text; // "5 +" 뒤에 "2"가 붙어서 "5 + 2"가 됨
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 현재까지 입력된 숫자를 저장 (지시서 3번: 피연산자 저장)
            resultValue = double.Parse(txtExpression.Text);
            operationPerformed = "+";
            isOperationPerformed = true;

            // "5" 뒤에 " + "를 붙여서 "5 + " 상태를 만듦
            txtExpression.Text += " + ";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            // 1. 전체 수식 "5 + 2"를 공백 기준으로 나눔
            string[] parts = txtExpression.Text.Split(' ');

            // 2. 가장 마지막에 있는 요소("2")를 숫자로 변환
            double secondValue = double.Parse(parts[parts.Length - 1]);

            // 3. 더하기 계산 수행 (지시서 로직)
            double finalResult = resultValue + secondValue;

            // 4. 결과값만 아래쪽 텍스트박스에 표시
            txtResult.Text = finalResult.ToString();

            isOperationPerformed = true;
        }
    }
}