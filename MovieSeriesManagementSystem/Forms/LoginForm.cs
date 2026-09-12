using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        // =========================
        // LOGIN BUTTON
        // =========================
        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Empty field check
            if (username == "" || password == "")
            {
                MessageBox.Show(
                    "Please enter Username/Email and Password!");
                return;
            }

            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();


                    // =================================
                    // FIRST: CHECK ADMIN
                    // =================================

                    string adminQuery =
                        "SELECT COUNT(*) FROM Admin " +
                        "WHERE Username = @username " +
                        "AND Password = @password";

                    using (SqlCommand adminCmd =
                           new SqlCommand(adminQuery, con))
                    {
                        adminCmd.Parameters.AddWithValue(
                            "@username", username);

                        adminCmd.Parameters.AddWithValue(
                            "@password", password);

                        int adminCount =
                            Convert.ToInt32(adminCmd.ExecuteScalar());


                        if (adminCount > 0)
                        {
                            MessageBox.Show("Admin Login Successful!");

                            AdminDashboard adminDashboard =
                                new AdminDashboard();

                            adminDashboard.Show();

                            this.Hide();

                            return;
                        }
                    }


                    // =================================
                    // SECOND: CHECK USER
                    // =================================

                    string userQuery =
                        "SELECT COUNT(*) FROM Users " +
                        "WHERE Email = @email " +
                        "AND Password = @password";

                    using (SqlCommand userCmd =
                           new SqlCommand(userQuery, con))
                    {
                        userCmd.Parameters.AddWithValue(
                            "@email", username);

                        userCmd.Parameters.AddWithValue(
                            "@password", password);

                        int userCount =
                            Convert.ToInt32(userCmd.ExecuteScalar());


                        if (userCount > 0)
                        {
                            MessageBox.Show("User Login Successful!");

                            UserDashboard userDashboard =
                                new UserDashboard();

                            userDashboard.Show();

                            this.Hide();

                            return;
                        }
                    }


                    // =================================
                    // INVALID LOGIN
                    // =================================

                    MessageBox.Show(
                        "Invalid Username/Email or Password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Connection Error!\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================
        // SIGN UP BUTTON
        // =========================
        private void button2_Click_1(object sender, EventArgs e)
        {
            SignUpForm signupForm = new SignUpForm();

            signupForm.Show();

            this.Hide();
        }


        // =========================
        // LABEL
        // =========================
        private void label4_Click(object sender, EventArgs e)
        {
        }


        // =========================
        // PICTURE BOX
        // =========================
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}