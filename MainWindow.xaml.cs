using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaYP02._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private List<Student> students;

        public MainWindow()
        {
            InitializeComponent();
            students = new List<Student>();
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(StudentIdTextBox.Text);
            double grade = double.Parse(GradeTextBox.Text);

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                student = new Student(id);
                students.Add(student);
            }
            student.Grades.Add(grade);
        }

        private void DrawChartButton_Click(object sender, RoutedEventArgs e)
        {
            ChartCanvas.Children.Clear();

            if (students.Count == 0) return;

            // Получаем максимальную среднюю оценку
            double maxGrade = students.Max(s => s.GetAverageGrade());
            double minGrade = students.Min(s => s.GetAverageGrade());

            double range = maxGrade - minGrade;

            // Рисуем ось Y
            Line yAxis = new Line
            {
                X1 = 30,
                Y1 = 10,
                X2 = 30,
                Y2 = ChartCanvas.Height - 30,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            ChartCanvas.Children.Add(yAxis);

            // Рисуем ось X
            Line xAxis = new Line
            {
                X1 = 30,
                Y1 = ChartCanvas.Height - 30,
                X2 = ChartCanvas.Width - 10,
                Y2 = ChartCanvas.Height - 30,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            ChartCanvas.Children.Add(xAxis);

            // Сортируем студентов по ID для корректного расположения на графике
            var sortedStudents = students.OrderBy(s => s.Id).ToList();

            // Смещение для первой точки
            const double offsetX = 50; // изменение значения для смещения вправо

            for (int i = 0; i < sortedStudents.Count; i++)
            {
                var student = sortedStudents[i];
                double average = student.GetAverageGrade();

                double y = ChartCanvas.Height - 30 - ((average - minGrade) / range * (ChartCanvas.Height - 60));
                double x = (i * 50) + offsetX; // Смещаем X вправо

                // Рисуем точку
                Ellipse point = new Ellipse
                {
                    Fill = Brushes.Blue,
                    Width = 8,
                    Height = 8
                };
                Canvas.SetLeft(point, x - point.Width / 2); // Центрируем точку по оси X
                Canvas.SetTop(point, y - point.Height / 2); // Центрируем точку по оси Y
                ChartCanvas.Children.Add(point);

                // Если это не первая точка, рисуем линию до текущей
                if (i > 0)
                {
                    Line line = new Line
                    {
                        X1 = (i - 1) * 50 + offsetX, // Используем смещение
                        Y1 = ChartCanvas.Height - 30 - ((sortedStudents[i - 1].GetAverageGrade() - minGrade) / range * (ChartCanvas.Height - 60)),
                        X2 = x,
                        Y2 = y,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2
                    };
                    ChartCanvas.Children.Add(line);
                }
            }

            // Находим лучшего и худшего студента
            var bestStudent = sortedStudents.OrderByDescending(s => s.GetAverageGrade()).FirstOrDefault();
            var worstStudent = sortedStudents.OrderBy(s => s.GetAverageGrade()).FirstOrDefault();

            BestStudentTextBlock.Text = $"Лучший студент: ID {bestStudent.Id}, Средняя оценка: {bestStudent.GetAverageGrade():0.00}";
            WorstStudentTextBlock.Text = $"Худший студент: ID {worstStudent.Id}, Средняя оценка: {worstStudent.GetAverageGrade():0.00}";
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данный проект с целью отобржения графика успеваемости студентов выполнил Политов Сергей, группа ИСП-41","Информация о проектк", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FIO_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Руководителем и проверяющим учебной практики является преподователь Афанасьев Дмитрий Александрович", "Руководитель учебной практики", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}