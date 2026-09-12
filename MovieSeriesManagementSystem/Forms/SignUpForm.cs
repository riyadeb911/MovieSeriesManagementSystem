using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }


        // =========================
        // SIGN UP BUTTON
        // =========================
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();

            // Empty field check
            if (name == "" || email == "" || password == "")
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    // Check if email already exists
                    string checkQuery =
                        "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                    using (SqlCommand checkCmd =
                           new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);

                        int count =
                            Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "This email is already registered!");

                            return;
                        }
                    }

                    // Insert new user
                    string query =
                        "INSERT INTO Users (Name, Email, Password) " +
                        "VALUES (@Name, @Email, @Password)";

                    using (SqlCommand cmd =
                           new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Registration Successful!");

                            // Go to Login Form
                            LoginForm loginForm = new LoginForm();
                            loginForm.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Registration Failed!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Error!\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================
        // BACK BUTTON
        // =========================
        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            this.Hide();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}