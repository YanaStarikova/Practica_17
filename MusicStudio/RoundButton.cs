using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundButton : Button //класс кнопки с загругленными краями
{
    protected override void OnPaint(PaintEventArgs pevent)
    {
        GraphicsPath path = new GraphicsPath();   // задаем графический путь с закругленными углами
        int radius = 20; // радиус закругления
        path.AddArc(0, 0, radius, radius, 180, 90); // верхний левый угол
        path.AddArc(Width - radius, 0, radius, radius, 270, 90); // верхний правый угол
        path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90); // нижний правый угол
        path.AddArc(0, Height - radius, radius, radius, 90, 90); // нижний левый угол
        path.CloseFigure();
        this.Region = new Region(path); // устанавливаем форму кнопки

        base.OnPaint(pevent);

        using (Pen pen = new Pen(Color.Purple))
        {
            pevent.Graphics.DrawPath(pen, path); // Рисуем границу
        }
    }
}
