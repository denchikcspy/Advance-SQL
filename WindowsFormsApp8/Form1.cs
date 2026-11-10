using System;
using System.Windows.Forms;
using WindowsFormsApp8.Repos;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString = "server=localhost;port=3307;user=root;password=root;database=messenger";
        private readonly UserRepository _userRepository;

        public Form1()
        {
            InitializeComponent();
            _userRepository = new UserRepository(_connectionString);
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string passwordText = textBox3.Text.Trim();
            DateTime birthDay = dateTimePicker1.Value;

           
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordText))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля!");
                return;
            }

            
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Введіть коректний email (має містити '@' та '.')!");
                return;
            }

            
            if (passwordText.Length < 4)
            {
                MessageBox.Show("Пароль має містити щонайменше 4 цифри!");
                return;
            }

            int password;
            if (!int.TryParse(passwordText, out password))
            {
                MessageBox.Show("У базі даних поле password має тип int, тому пароль має складатися тільки з цифр!");
                return;
            }

            
            int age = DateTime.Now.Year - birthDay.Year;
            if (birthDay.Date > DateTime.Now.AddYears(-age).Date)
            {
                age--;
            }

            if (age < 18)
            {
                MessageBox.Show("Реєстрація дозволена лише користувачам, яким виповнилося 18 років!");
                return;
            }

            
            try
            {
                if (_userRepository.Create(name, email, password, birthDay) > 0)
                {
                    MessageBox.Show("Користувача успішно зареєстровано!");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Не вдалося зареєструвати користувача.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 usersForm = new Form2();
            usersForm.Show();
        }
    }
}
