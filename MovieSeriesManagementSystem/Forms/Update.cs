using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        // =========================
        // BACK BUTTON
        // =========================
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // =========================
        // OLD TITLE
        // =========================
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // =========================
        // NEW TITLE
        // =========================
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // =========================
        // UPDATE BUTTON
        // =========================
        private void button1_Click(object sender, EventArgs e)
        {
            string oldTitle = textBox1.Text.Trim();
            string newTitle = textBox2.Text.Trim();

            // =========================
            // CHECK EMPTY FIELDS
            // =========================

            if (oldTitle == "" || newTitle == "")
            {
                MessageBox.Show(
                    "Please fill all fields!",
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
                    // CHECK OLD TITLE EXISTS
                    // =========================

                    string checkQuery =
                        "SELECT COUNT(*) FROM Movies " +
                        "WHERE Title = @OldTitle";

                    using (SqlCommand checkCmd =
                           new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@OldTitle", oldTitle);

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
                    // CHECK NEW TITLE DUPLICATE
                    // =========================

                    string duplicateQuery =
                        "SELECT COUNT(*) FROM Movies " +
                        "WHERE Title = @NewTitle " +
                        "AND Title <> @OldTitle";

                    using (SqlCommand duplicateCmd =
                           new SqlCommand(duplicateQuery, con))
                    {
                        duplicateCmd.Parameters.AddWithValue(
                            "@NewTitle", newTitle);

                        duplicateCmd.Parameters.AddWithValue(
                            "@OldTitle", oldTitle);

                        int count =
                            Convert.ToInt32(
                                duplicateCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "This Movie/Series title already exists!",
                                "Duplicate",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    // =========================
                    // UPDATE TITLE
                    // =========================

                    string updateQuery =
                        "UPDATE Movies " +
                        "SET Title = @NewTitle " +
                        "WHERE Title = @OldTitle";

                    using (SqlCommand updateCmd =
                           new SqlCommand(updateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue(
                            "@NewTitle", newTitle);

                        updateCmd.Parameters.AddWithValue(
                            "@OldTitle", oldTitle);

                        int result =
                            updateCmd.ExecuteNonQuery();

                        // =========================
                        // SUCCESS
                        // =========================

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Movie/Series Updated Successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to update Movie/Series!",
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
    }
}