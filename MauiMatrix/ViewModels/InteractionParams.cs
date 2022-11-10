namespace MauiMatrix.ViewModels;

public class InteractionParams
{
    public int X { get; set; }
    public int Y { get; set; }
}

//public class MyBehavior : Behavior<GraphicsView>
//{
//    protected override void OnAttachedTo(GraphicsView bindable)
//    {
//        base.OnAttachedTo(bindable);
//        // Perform setup
//        bindable.StartInteraction += StartInteraction;
//    }

//    protected override void OnDetachingFrom(GraphicsView bindable)
//    {
//        base.OnDetachingFrom(bindable);
//        // Perform clean up
//        bindable.StartInteraction -= StartInteraction;
//    }

//    // Behavior implementation
//    void StartInteraction(object sender, TouchEventArgs e)
//    {
//    }
//}