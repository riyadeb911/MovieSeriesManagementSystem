using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();

            LoadAllMovies();
        }

        // LOAD ALL MOVIES
      
        private void LoadAllMovies()
        {
            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection connection = db.GetConnection())
                {
                    connection.Open();

                    string query = @"SELECT MovieId, Title, Type, Genre, ReleaseYear, Rating
                                     FROM Movies
                                     ORDER BY Title";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                }

                // DataGridView settings
                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        
        // SEARCH BUTTON
        
        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();

            
            if (string.IsNullOrEmpty(searchText))
            {
                LoadAllMovies();
                return;
            }

            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection connection = db.GetConnection())
                {
                    connection.Open();

                    string query = @"SELECT MovieId, Title, Type, Genre, ReleaseYear, Rating
                                     FROM Movies
                                     WHERE Title LIKE @Title
                                     ORDER BY Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@Title",
                            "%" + searchText + "%"
                        );

                        using (SqlDataAdapter adapter =
                               new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            dataGridView1.DataSource = table;

                            if (table.Rows.Count == 0)
                            {
                                MessageBox.Show(
                                    "Movie/Series not found!",
                                    "Search Result",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );

                                
                                LoadAllMovies();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        
        // SEARCH TEXTBOX
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        // DATAGRIDVIEW CLICK
        
        private void dataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // এখানে কিছু করার দরকার নেই
        }

        
        // LOGOUT BUTTON
        
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // UserDashboard hide
                this.Hide();

                // LoginForm open
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
            
        }
    }
}