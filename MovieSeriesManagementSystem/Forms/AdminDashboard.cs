using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieSeriesManagementSystem.Forms
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveMovie removeMovie = new RemoveMovie();
            removeMovie.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMovie addMovie = new AddMovie();
            addMovie.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewRating rating = new ViewRating();
            rating.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchMovie searchMovie = new SearchMovie();
            searchMovie.Show();

        }
    }
}
