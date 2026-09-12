using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class SearchMovie : Form
    {
        public SearchMovie()
        {
            InitializeComponent();
        }

        private void SearchMovie_Load(object sender, EventArgs e)
        {
            // DataGridView settings
            DataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            DataGridView1.ReadOnly = true;

            DataGridView1.AllowUserToAddRows = false;

            DataGridView1.AllowUserToDeleteRows = false;

            DataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            DataGridView1.MultiSelect = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();

            // TextBox empty হলে DataGridView clear হবে
            if (title == "")
            {
                DataGridView1.DataSource = null;
                return;
            }

            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT 
                            MovieId,
                            Title,
                            Type,
                            Genre,
                            ReleaseYear,
                            Rating
                        FROM Movies
                        WHERE Title LIKE @Title
                        ORDER BY Title";

                    using (SqlCommand cmd =
                           new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Title",
                            "%" + title + "%");

                        using (SqlDataAdapter adapter =
                               new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            DataGridView1.DataSource = table;

                            // No result
                            if (table.Rows.Count == 0)
                            {
                                DataGridView1.DataSource = null;

                                MessageBox.Show(
                                    "Movie/Series not found!",
                                    "Search Result",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
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

        private void DataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {

        }
    }
}