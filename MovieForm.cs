using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Playwright;
using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.Text;
using IMDbTop250Scraper.Services;
using IMDbTop250Scraper.Helpers;

namespace IMDbTop250Scraper
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Movie> allMovies = new List<Movie>();

        BindingList<Movie> movies = new BindingList<Movie>();


        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyLightTheme(this);

            dataGridView1.DataSource = movies;


            dataGridView1.Columns["Position"].HeaderText = "#";
            dataGridView1.Columns["Title"].HeaderText = "Название";
            dataGridView1.Columns["Rating"].HeaderText = "Рейтинг";
            dataGridView1.Columns["VoteCount"].HeaderText = "Голоса";
            dataGridView1.Columns["Year"].HeaderText = "Год";
            dataGridView1.Columns["Poster"].HeaderText = "Постер";
            dataGridView1.Columns["Link"].HeaderText = "Ссылка";

            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            lblStatus.Text = "Ожидание";
            lblProgress.Text = "...";

            tbxSearch.TextChanged += (s, e) => ApplyFilters();
            numMinRating.ValueChanged += (s, e) => ApplyFilters();
            numMinVotes.ValueChanged += (s, e) => ApplyFilters();
            minYear.ValueChanged += (s, e) => ApplyFilters();

            lblVotes.Text = "минимум голосов";
            lblRating.Text = "Минимальный рейтинг";
            lblYear.Text = "Минимальный год";
            lblUrl.Text = "Url";

            string[] fields =
            {
                "Position",
                "Title",
                "Rating",
                "Votes",
                "Year"
            };

            cbSort1.Items.AddRange(fields);
            cbSort2.Items.AddRange(fields);
            cbSort3.Items.AddRange(fields);

            cbDir1.Items.AddRange(new[] { "ASC", "DESC" });
            cbDir2.Items.AddRange(new[] { "ASC", "DESC" });
            cbDir3.Items.AddRange(new[] { "ASC", "DESC" });

            cbSort1.SelectedIndex = 2; // Rating
            cbDir1.SelectedIndex = 1;  // DESC

            cbSort2.SelectedIndex = 3; // Votes
            cbDir2.SelectedIndex = 1;

            cbSort3.SelectedIndex = 4; // Year
            cbDir3.SelectedIndex = 0;

            cbSort1.SelectedIndexChanged += (s, e) => ApplyFilters();
            cbSort2.SelectedIndexChanged += (s, e) => ApplyFilters();
            cbSort3.SelectedIndexChanged += (s, e) => ApplyFilters();

            cbDir1.SelectedIndexChanged += (s, e) => ApplyFilters();
            cbDir2.SelectedIndexChanged += (s, e) => ApplyFilters();
            cbDir3.SelectedIndexChanged += (s, e) => ApplyFilters();



        }

        private async void btnParse_Click(
                        object sender,
                        EventArgs e)
        {
            try
            {
                lblStatus.Text = "Парсинг...";

                btnParse.Enabled = false;

                movies.Clear();
                allMovies.Clear();

                var parser =
                    new ImdbParserService();

                var parsedMovies =
                    await parser.ParseAsync(
                        tbxUrl.Text);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = parsedMovies.Count;
                progressBar1.Value = 0;

                int parsedMoviesYet = 0;
                foreach(var movie in parsedMovies)
                {
                    movies.Add(movie);
                    allMovies.Add(movie);

                    parsedMoviesYet++;
                    lblProgress.Text = $"{parsedMoviesYet}/{parsedMovies.Count}";

                    progressBar1.Value++;
                }


                var db = new DatabaseService();

                db.SaveMovies(parsedMovies);

                var count = db.LoadMovies().Count;
                MessageBox.Show($"В БД сохранено: {count}");

                ApplyFilters();

                lblStatus.Text = "Готово";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnParse.Enabled = true;
            }
        }


        private void btnSave_Click(
        object sender,
        EventArgs e)
        {
            if(allMovies.Count == 0)
            {
                MessageBox.Show(
                    "Нет данных для сохранения");

                return;
            }

            try
            {
                var excelService =
                    new ExcelExportService();

                excelService.ExportMovies(
                    allMovies,
                    "films.xlsx");

                MessageBox.Show(
                    "Excel файл сохранён");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            var filtered = allMovies.Where(x => x.Title.Contains(tbxSearch.Text, StringComparison.OrdinalIgnoreCase));
        }

        private void ApplyFilters()
        {
            if(allMovies.Count == 0)
            {
                movies.Clear();
                return;
            }

            var filtered = allMovies.Where(movie =>
            {
                bool titleMatch =
                    string.IsNullOrWhiteSpace(tbxSearch.Text)
                    || movie.Title.Contains(
                        tbxSearch.Text,
                        StringComparison.OrdinalIgnoreCase);

                double rating = ParseHelpers.ParseRating(movie.Rating);

                bool ratingMatch =
                    rating >= (double)numMinRating.Value;

                int votes = ParseHelpers.ParseVotes(movie.VoteCount);

                bool votesMatch =
                    votes >= (int)numMinVotes.Value;

                int year = ParseHelpers.ParseYear(movie.Year);

                bool yearMatch =
                    minYear.Value == 0
                    || year >= (int)minYear.Value;

                return titleMatch &&
                       ratingMatch &&
                       votesMatch &&
                       yearMatch;
            }).ToList();



            filtered = ApplySorting(filtered);

            movies.Clear();

            foreach(var m in filtered)
                movies.Add(m);
        }

        private List<Movie> ApplySorting(List<Movie> filtered)
        {
            IOrderedEnumerable<Movie>? ordered = null;

            var sorts = new[]
            {
        (Field: cbSort1.Text, Dir: cbDir1.Text),
        (Field: cbSort2.Text, Dir: cbDir2.Text),
        (Field: cbSort3.Text, Dir: cbDir3.Text)
    };

            foreach(var sort in sorts)
            {
                if(string.IsNullOrWhiteSpace(sort.Field))
                    continue;

                bool desc = sort.Dir == "DESC";

                if(ordered == null)
                {
                    ordered = sort.Field switch
                    {
                        "Position" => desc
                            ? filtered.OrderByDescending(x => x.Position)
                            : filtered.OrderBy(x => x.Position),

                        "Title" => desc
                            ? filtered.OrderByDescending(x => x.Title)
                            : filtered.OrderBy(x => x.Title),

                        "Rating" => desc
                            ? filtered.OrderByDescending(x => ParseHelpers.ParseRating(x.Rating))
                            : filtered.OrderBy(x => ParseHelpers.ParseRating(x.Rating)),

                        "Votes" => desc
                            ? filtered.OrderByDescending(x => ParseHelpers.ParseVotes(x.VoteCount))
                            : filtered.OrderBy(x => ParseHelpers.ParseVotes(x.VoteCount)),

                        "Year" => desc
                            ? filtered.OrderByDescending(x => ParseHelpers.ParseYear(x.Year))
                            : filtered.OrderBy(x => ParseHelpers.ParseYear(x.Year)),

                        _ => filtered.OrderBy(x => x.Position)
                    };
                }
                else
                {
                    ordered = sort.Field switch
                    {
                        "Position" => desc
                            ? ordered.ThenByDescending(x => x.Position)
                            : ordered.ThenBy(x => x.Position),

                        "Title" => desc
                            ? ordered.ThenByDescending(x => x.Title)
                            : ordered.ThenBy(x => x.Title),

                        "Rating" => desc
                            ? ordered.ThenByDescending(x => ParseHelpers.ParseRating(x.Rating))
                            : ordered.ThenBy(x => ParseHelpers.ParseRating(x.Rating)),

                        "Votes" => desc
                            ? ordered.ThenByDescending(x => ParseHelpers.ParseVotes(x.VoteCount))
                            : ordered.ThenBy(x => ParseHelpers.ParseVotes(x.VoteCount)),

                        "Year" => desc
                            ? ordered.ThenByDescending(x => ParseHelpers.ParseYear(x.Year))
                            : ordered.ThenBy(x => ParseHelpers.ParseYear(x.Year)),

                        _ => ordered
                    };
                }
            }

            return ordered?.ToList() ?? filtered;
        }

        private void btnLoadDb_Click_1(object sender, EventArgs e)
        {
            try
            {
                var db = new DatabaseService();

                var savedMovies = db.LoadMovies();

                MessageBox.Show($"Найдено: {savedMovies.Count}");

                if(savedMovies.Count == 0)
                {
                    MessageBox.Show("База пустая");
                    return;
                }

                allMovies = savedMovies;

                movies.Clear();

                foreach(var movie in savedMovies)
                    movies.Add(movie);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = movies;

                lblStatus.Text =
                    $"Загружено {savedMovies.Count} фильмов";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClearDb_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
               "Очистить базу?",
               "Подтверждение",
               MessageBoxButtons.YesNo);

            if(result != DialogResult.Yes)
                return;

            try
            {
                var db = new DatabaseService();

                db.ClearMovies();

                allMovies.Clear();
                movies.Clear();

                dataGridView1.Refresh();

                lblStatus.Text = "База очищена";

                MessageBox.Show("База очищена");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ApplyDarkTheme(Control parent)
        {
            // Цвет формы
            parent.BackColor = Color.FromArgb(30, 30, 30);
            parent.ForeColor = Color.White;

            foreach(Control control in parent.Controls)
            {
                switch(control)
                {
                    case Button btn:
                    btn.BackColor = Color.FromArgb(45, 45, 48);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                    break;

                    case TextBox tb:
                    tb.BackColor = Color.FromArgb(40, 40, 40);
                    tb.ForeColor = Color.White;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                    case Label lbl:
                    lbl.ForeColor = Color.White;
                    break;

                    case NumericUpDown num:
                    num.BackColor = Color.FromArgb(40, 40, 40);
                    num.ForeColor = Color.White;
                    break;

                    case ComboBox cb:
                    cb.BackColor = Color.FromArgb(40, 40, 40);
                    cb.ForeColor = Color.White;
                    cb.FlatStyle = FlatStyle.Flat;
                    break;

                    case DataGridView dgv:
                    dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
                    dgv.GridColor = Color.Gray;

                    dgv.EnableHeadersVisualStyles = false;

                    dgv.ColumnHeadersDefaultCellStyle.BackColor =
                        Color.FromArgb(45, 45, 48);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                        Color.White;

                    dgv.DefaultCellStyle.BackColor =
                        Color.FromArgb(35, 35, 35);
                    dgv.DefaultCellStyle.ForeColor =
                        Color.White;

                    dgv.DefaultCellStyle.SelectionBackColor =
                        Color.FromArgb(70, 70, 70);

                    dgv.RowHeadersDefaultCellStyle.BackColor =
                        Color.FromArgb(45, 45, 48);
                    dgv.RowHeadersDefaultCellStyle.ForeColor =
                        Color.White;
                    break;
                }

                // Рекурсивно для вложенных контролов
                if(control.HasChildren)
                    ApplyDarkTheme(control);
            }
        }

        
        private void ApplyLightTheme(Control parent)
        {
            // Цвет формы
            parent.BackColor = SystemColors.Control;
            parent.ForeColor = Color.Black;

            foreach(Control control in parent.Controls)
            {
                switch(control)
                {
                    case Button btn:
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                    btn.FlatStyle = FlatStyle.Standard;
                    break;

                    case TextBox tb:
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                    case Label lbl:
                    lbl.ForeColor = Color.Black;
                    break;

                    case NumericUpDown num:
                    num.BackColor = Color.White;
                    num.ForeColor = Color.Black;
                    break;

                    case ComboBox cb:
                    cb.BackColor = Color.White;
                    cb.ForeColor = Color.Black;
                    cb.FlatStyle = FlatStyle.Standard;
                    break;

                    case DataGridView dgv:
                    dgv.BackgroundColor = Color.White;
                    dgv.GridColor = Color.LightGray;

                    dgv.EnableHeadersVisualStyles = true;

                    dgv.ColumnHeadersDefaultCellStyle.BackColor =
                        SystemColors.Control;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                        Color.Black;

                    dgv.DefaultCellStyle.BackColor =
                        Color.White;
                    dgv.DefaultCellStyle.ForeColor =
                        Color.Black;

                    dgv.DefaultCellStyle.SelectionBackColor =
                        SystemColors.Highlight;
                    dgv.DefaultCellStyle.SelectionForeColor =
                        Color.White;

                    dgv.RowHeadersDefaultCellStyle.BackColor =
                        SystemColors.Control;
                    dgv.RowHeadersDefaultCellStyle.ForeColor =
                        Color.Black;
                    break;
                }

                // Рекурсивно для вложенных контролов
                if(control.HasChildren)
                    ApplyLightTheme(control);
            }
        }
        int themeChanger = 0;
        private void btnDarkTheme_Click(object sender, EventArgs e)
        {
            themeChanger++;
            if(themeChanger % 2 == 1)
            {
                ApplyDarkTheme(this);
            }
            else
            {
                ApplyLightTheme(this);
            }

        }
    }
}

