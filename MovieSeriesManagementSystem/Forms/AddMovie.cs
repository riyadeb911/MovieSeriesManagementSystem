using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using MovieSeriesManagementSystem.DataBase;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class AddMovie : Form
    {
        public AddMovie()
        {
            InitializeComponent();

            // Type ComboBox
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Movie");
            comboBox1.Items.Add("Series");
        }


        // =========================
        // CONFIRM BUTTON
        // =========================
        private void button1_Click_1(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();
            string type = comboBox1.Text.Trim();
            string genre = textBox2.Text.Trim();
            string ratingText = textBox3.Text.Trim();


            // Check empty fields
            if (title == "" || type == "" ||
                genre == "" || ratingText == "")
            {
                MessageBox.Show(
                    "Please fill all fields!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // Check rating
            if (!decimal.TryParse(ratingText, out decimal rating))
            {
                MessageBox.Show(
                    "Please enter a valid rating!",
                    "Invalid Rating",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // Rating must be between 0 and 10
            if (rating < 0 || rating > 10)
            {
                MessageBox.Show(
                    "Rating must be between 0 and 10!",
                    "Invalid Rating",
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
                    // CHECK DUPLICATE TITLE
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

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "This Movie/Series already exists!",
                                "Duplicate",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }


                    // =========================
                    // INSERT MOVIE/SERIES
                    // =========================

                    string query =
                        @"INSERT INTO Movies
                          (Title, Type, Genre, Rating)
                          VALUES
                          (@Title, @Type, @Genre, @Rating)";


                    using (SqlCommand cmd =
                           new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Title", title);

                        cmd.Parameters.AddWithValue(
                            "@Type", type);

                        cmd.Parameters.AddWithValue(
                            "@Genre", genre);

                        cmd.Parameters.AddWithValue(
                            "@Rating", rating);


                        int result = cmd.ExecuteNonQuery();


                        // =========================
                        // SUCCESS
                        // =========================

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Movie/Series Added Successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);


                            // Clear fields
                            textBox1.Clear();
                            comboBox1.SelectedIndex = -1;
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to add Movie/Series!",
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


        // =========================
        // EXISTING EVENT
        // =========================
        private void radioButton2_CheckedChanged(
            object sender, EventArgs e)
        {
        }
    }
}