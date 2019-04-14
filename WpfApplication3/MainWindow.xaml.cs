using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private enum MyShape
        {
            Line, Ellipse, Rectangle
        }

        private MyShape currShape;

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Rectangle;
        }

        Point start;
        Point end;

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        
            start = e.GetPosition(this);
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
         
            switch (currShape)
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
            Line newLine = new Line()
            {
                Stroke = Brushes.Blue,
                X1 = start.X,
                Y1 = start.Y-70,
                X2 = end.X,
                Y2 = end.Y -70
            };
            MyCanvas.Children.Add(newLine);
        }

 
        private void DrawEllipse()
        {
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = Brushes.Black,
         
                StrokeThickness = 1,
         
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

            if (end.Y >= start.Y)
            {
             
                newEllipse.SetValue(Canvas.TopProperty, start.Y - 50);
                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y - 50);
                newEllipse.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newEllipse);
        }


        private void DrawRectangle()
        {
            Rectangle newRectangle = new Rectangle()
            {
                Stroke = Brushes.Red,
               
                StrokeThickness = 1
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
    









    private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
         
            var radiobutton = sender as RadioButton;

            
            string radioBPressed = radiobutton.Content.ToString();

   
            if (radioBPressed == "Draw")
            {
                this.DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
            else if (radioBPressed == "Erase")
            {
                this.DrawingCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
            else if (radioBPressed == "Select")
            {
                this.DrawingCanvas.EditingMode = InkCanvasEditingMode.Select;
            }
           
        }
       
        
        //private void DrawPanel_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if ((int)e.Key >= 35 && (int)e.Key <= 68)
        //    {
        //        switch ((int)e.Key)
        //        {
        //            case 35:
        //                strokeAttr.Width = 2;
        //                strokeAttr.Height = 2;
        //                break;
        //            case 36:
        //                strokeAttr.Width = 4;
        //                strokeAttr.Height = 4;
        //                break;
        //            case 37:
        //                strokeAttr.Width = 6;
        //                strokeAttr.Height = 6;
        //                break;
        //            case 38:
        //                strokeAttr.Width = 8;
        //                strokeAttr.Height = 8;
        //                break;
        //            case 39:
        //                strokeAttr.Width = 10;
        //                strokeAttr.Height = 10;
        //                break;
        //            case 40:
        //                strokeAttr.Width = 12;
        //                strokeAttr.Height = 12;
        //                break;
        //            case 41:
        //                strokeAttr.Width = 14;
        //                strokeAttr.Height = 14;
        //                break;
        //            case 42:
        //                strokeAttr.Width = 16;
        //                strokeAttr.Height = 16;
        //                break;
        //            case 43:
        //                strokeAttr.Width = 18;
        //                strokeAttr.Height = 18;
        //                break;
        //            case 45:
        //                strokeAttr.Color = (Color)ColorConverter.ConvertFromString("Blue");
        //                break;
        //            case 50:
        //                strokeAttr.Color = (Color)ColorConverter.ConvertFromString("Green");
        //                break;
        //            case 68:
        //                strokeAttr.Color = (Color)ColorConverter.ConvertFromString("Yellow");
        //                break;
        //        }
        //    }
        //}

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("MyPicture.bin", FileMode.Create))
            {
                this.DrawingCanvas.Strokes.Save(fs);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("MyPicture.bin", FileMode.Open, FileAccess.Read))
            {
                StrokeCollection sc = new StrokeCollection(fs);
                this.DrawingCanvas.Strokes = sc;
            }
        }

        private void EllipseButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RectangleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LineButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DrawButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}