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

namespace MatrixAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IList<IItem> shapes;
        private Cursor? currentMovingShape;

        public MainWindow()
        {
            InitializeComponent();
            shapes = new List<IItem>();
            InitMovingShape();

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    var shape = new Item(paintCanvas);
                    shape.SetPosition(x, y);

                    shapes.Add(shape);
                }
            }
        }

        private void InitMovingShape()
        {
            currentMovingShape = new Cursor(paintCanvas);
        }

        private void SetMovingShapePosition(MouseEventArgs e)
        {
            var pos = e.GetPosition(paintCanvas);
            currentMovingShape.SetPosition(((int)pos.X / Item.zoomFactor), ((int)pos.Y / Item.zoomFactor));
        }

        private void paintCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            SetMovingShapePosition(e);
        }

        private void paintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentMovingShape == null) return;

            shapes.Add(currentMovingShape);
            InitMovingShape();
            SetMovingShapePosition(e);
        }

        private void paintCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void paintCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //isMouseDown = true;
        }

        private void paintCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //isMouseDown = false;
        }
    }

    public interface IItem
    {

    }

    public class Item : IItem
    {
        public static int zoomFactor = 20;
        private readonly Rectangle shape;

        public Item(Canvas canvas)
        {
            shape = new Rectangle { Width = zoomFactor, Height = zoomFactor, Fill = Brushes.Yellow };
            canvas.Children.Add(shape);
            SetPosition(0, 0);
        }


        public void SetPosition(int x, int y)
        {
            Canvas.SetLeft(shape, 1 + x * zoomFactor);
            Canvas.SetTop(shape, 1 + y * zoomFactor);
            shape.Width = zoomFactor - 2;
            shape.Height = zoomFactor - 2;
        }
    }

    public class Cursor : IItem
    {
        public static int zoomFactor = 20;
        private readonly Rectangle shape;

        public Cursor(Canvas canvas)
        {
            shape = new Rectangle 
            { 
                Width = zoomFactor, 
                Height = zoomFactor, 
                //Fill = Brushes.Yellow ,
                Stroke = Brushes.White
            };
            canvas.Children.Add(shape);
            SetPosition(0, 0);
        }


        public void SetPosition(int x, int y)
        {
            Canvas.SetLeft(shape, x * zoomFactor);
            Canvas.SetTop(shape, y * zoomFactor);
            shape.Width = zoomFactor;
            shape.Height = zoomFactor;
        }
    }
}

