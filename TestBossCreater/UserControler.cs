using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

public class CreditsControl : UserControl
{
    private Timer timer;
    private int currentY;
    private string[] lines = new string[]
    {
         "Проект создан: TestBossCreater",
        "Разработчик: Valdemar",
        "UI: Microsoft MVP",
        "2025 © Все права защищены",
        "----------------------------",
        "       [---------]",
        "       |  O   O  |",
        "       |    ∆    |",
        "       |  \\___/ |",
        "       [_________]",
        "         /|   |\\",
        "        /_|___|_\\",
        "         /     \\",
        "                  ",
        "   Тимоха, ты чё творишь",
        "                  ",
        "  SWAGA присутсвует ",
        "               ",
        "       / \\ ",
        "      / _ \\ ",
        "     |.o '.| ",
        "     |'._.'| ",
        "     |     | ",
        "    ,'|  | |`. ",
        "   /  |  | |  \\ ",
        "   |,-'--|--'-.| ",
        "       🚀 ",
        "    STARTING...",
        "Special thanks:",
        " Coffee ☕",
        " Музыкальная паУза   ",
        "Баги? Не баги, а фичи!",
        "Падения — часть обучения.",
        "Ctrl+S — ",
        " лучший друг ",
        "разработчика.",
        "Все совпадения случайны. Или нет?",
        "           🤖            ",
        "Если ты видишь этот текст ",
        "— значит всё работает!",
        "Debug Mode:",
        " ON 🔧",
        "Ждём обновления ",
        "в версии 2.0 🚀",
        "----------------------------",
        "     Я всегда с вами         "

    };

    public int ScrollSpeed { get; set; } = 2;

    public CreditsControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.DoubleBuffered = true;
        this.BackColor = Color.Black;
        this.ForeColor = Color.White;
        this.Font = new Font("Arial", 16, FontStyle.Bold);

        timer = new Timer();
        timer.Interval = 50;
        timer.Tick += Timer_Tick;

        this.Resize += (s, e) => ResetPosition();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        int y = currentY;

        foreach (var line in lines)
        {
            SizeF size = e.Graphics.MeasureString(line, this.Font);
            float x = (this.Width - size.Width) / 2;
            e.Graphics.DrawString(line, this.Font, new SolidBrush(this.ForeColor), x, y);
            y += (int)size.Height + 10;
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        currentY -= ScrollSpeed;
        if (currentY + lines.Length * (Font.Height + 10) < 0)
        {
            ResetPosition();
        }
        this.Invalidate();
    }

    private void ResetPosition()
    {
        currentY = this.Height;
    }

    public void Start()
    {
        ResetPosition();
        timer.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }

    public void SetLines(string[] newLines)
    {
        lines = newLines;
        ResetPosition();
    }
}
