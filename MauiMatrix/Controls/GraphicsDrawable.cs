using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MauiMatrix.ViewModels;
using Microsoft.Maui.Graphics;

namespace MauiMatrix.Controls;

public class GraphicsDrawable : View, IDrawable
{
    public GraphicsDrawable()
    {
//        Items.CollectionChanged += Items_CollectionChanged;
    }

//    private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
//    {
//        var p = Parent;
//    }

    public ObservableCollection<MatrixRect> Items
    {
        get { return (ObservableCollection<MatrixRect>)GetValue(ItemsProperty); }
        set
        {
            SetValue(ItemsProperty, value);
        }
    }

    public static BindableProperty ItemsProperty =
        BindableProperty.Create(nameof(Items),
        typeof(ObservableCollection<MatrixRect>),
        typeof(GraphicsDrawable),
        new ObservableCollection<MatrixRect>(), propertyChanged: async (bindable, oldValue, newValue) =>
        {
            var chartView = ((GraphicsDrawable)bindable);
            chartView.Items = (ObservableCollection<MatrixRect>)newValue;
        });

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromRgb(127, 63, 63);
        canvas.FillRectangle(dirtyRect);
        if (Items != null)
        {
            foreach (var r in Items)
            {
                canvas.StrokeColor = r.Color;
                canvas.StrokeSize = 4;
                canvas.DrawRectangle(r.X * 10, r.Y * 10, 10, 10);
            }
        }
    }
}
