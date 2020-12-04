using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaintApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private enum MyShape
        {
            Line, Ellipse, Rectangle
        }

        private MyShape CurrentShape = MyShape.Line;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentShape = MyShape.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentShape = MyShape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentShape = MyShape.Rectangle;
        }


        Point start;
        Point end;


        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

            switch (CurrentShape)
            {
                case MyShape.Line:
                    DrawLine();
                    break;
                case MyShape.Ellipse:
                    DrawEllipse();
                    break;
                case MyShape.Rectangle:
                    DrawRectangle();
                    break;
                default:
                    return;

            }

        } 

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {                
                end = e.GetPosition(this);
            }
        }

        private void DrawLine()
        {
            Line NewLine = new Line()
            {
                Stroke = Brushes.Blue,
                X1 = start.X,
                Y1 = start.Y-50,
                X2 = end.X,
                Y2 = end.Y-50,

            };


            MyCanvas.Children.Add(NewLine);

        }
       private void DrawEllipse()
        {
            Ellipse newEllipse = new Ellipse() 
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness=2,
                Height = 10,
                Width=10,
            };

            if (end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.Width = end.X - start.X;
            }
            else
            {
                newEllipse.SetValue(Canvas.LeftProperty, end.X);
                newEllipse.Width = start.X - end.X;
            }



            if (end.Y>= start.Y)
            {
                newEllipse.SetValue(Canvas.TopProperty, start.Y-50);
                newEllipse.Height = end.Y- start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y-50);
                newEllipse.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newEllipse);

        }
        private void DrawRectangle()
        {

            Rectangle newRectangle = new Rectangle()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 2,
               
            };

            if (end.X >= start.X)
            {
                newRectangle.SetValue(Canvas.LeftProperty, start.X);
                newRectangle.Width = end.X - start.X;
            }
            else
            {
                newRectangle.SetValue(Canvas.LeftProperty, end.X);
                newRectangle.Width = start.X - end.X;
            }



            if (end.Y >= start.Y)
            {
                newRectangle.SetValue(Canvas.TopProperty, start.Y - 50);
                newRectangle.Height = end.Y - start.Y;
            }
            else
            {
                newRectangle.SetValue(Canvas.TopProperty, end.Y - 50);
                newRectangle.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newRectangle);



        }

        private double scale = 1;
        private void MyCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var position = e.GetPosition(element);
            var matrix = Matrix.Identity;

            scale = Math.Max(e.Delta > 0 ? scale * 1.1 : scale / 1.1, 1.0);
            matrix.ScaleAt(scale, scale, position.X, position.Y-50);

            ((MatrixTransform)element.LayoutTransform).Matrix = matrix;
        }
    }
}
