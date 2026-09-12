using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class RemoveMovie : Form
    {
        public RemoveMovie()
        {
            InitializeComponent();
        }

        // =========================
        // TEXTBOX
        // =========================
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // =========================
        // CONFIRM BUTTON
        // =========================
        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();

            // Check empty field
            if (title == "")
            {
                MessageBox.Show(
                    "Please enter the Movie/Series title!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DbConnection db = new DbConnection();

                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    // =========================
                    // CHECK MOVIE EXISTS
                    // =========================

                    string checkQuery =
                        "SELECT COUNT(*) FROM Movies " +
                        "WHERE Title = @Title";

                    using (SqlCommand checkCmd =
                           new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@Title", title);

                        int count =
                            Convert.ToInt32(
                                checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            MessageBox.Show(
                                "Movie/Series not found!",
                                "Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }
                    }

                    // =========================
                    // DELETE MOVIE/SERIES
                    // =========================

                    string deleteQuery =
                        "DELETE FROM Movies " +
                        "WHERE Title = @Title";

                    using (SqlCommand deleteCmd =
                           new SqlCommand(deleteQuery, con))
                    {
                        deleteCmd.Parameters.AddWithValue(
                            "@Title", title);

                        int result =
                            deleteCmd.ExecuteNonQuery();

                        // =========================
                        // SUCCESS
                        // =========================

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Movie/Series Removed Successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            textBox1.Clear();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to remove Movie/Series!",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}