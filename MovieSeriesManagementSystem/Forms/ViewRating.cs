using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class ViewRating : Form
    {
        public ViewRating()
        {
            InitializeComponent();

            LoadRatings();
        }

        // =========================
        // LOAD RATINGS
        // =========================
        private void LoadRatings()
        {
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
                            Rating
                        FROM Movies
                        ORDER BY Rating DESC";

                    using (SqlCommand cmd =
                           new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter =
                               new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            dataGridView1.DataSource = table;
                        }
                    }
                }

                // =========================
                // DATAGRIDVIEW SETTINGS
                // =========================

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
                    "Database Error!\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // DATAGRIDVIEW CLICK
        // =========================
        private void dataGridView1_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {

        }
    }
}