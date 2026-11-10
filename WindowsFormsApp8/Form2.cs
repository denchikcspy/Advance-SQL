using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp8.Entities;
using WindowsFormsApp8.Repos;

namespace WindowsFormsApp8
{
    public partial class Form2 : Form
    {
        private readonly string _connectionString = "server=localhost;port=3307;user=root;password=root;database=messenger";
        private readonly UserRepository _userRepository;

        public Form2()
        {
            InitializeComponent();
            _userRepository = new UserRepository(_connectionString);

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            try
            {
                List<User> list = _userRepository.GetAll();

                listBox1.Items.Clear();
                richTextBox1.Clear();

                foreach (var user in list)
                {
                    listBox1.Items.Add($"{user.Id}. {user.Name} | {user.Email} | ДН: {user.GetFormattedDate(user.BirthDay)}");
                    richTextBox1.AppendText($"{user.Id}. {user.Name} | {user.Email} | ДН: {user.GetFormattedDate(user.BirthDay)}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження списку: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateListBox();
        }
    }
}
