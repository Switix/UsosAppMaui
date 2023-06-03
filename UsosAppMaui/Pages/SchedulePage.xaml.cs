using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.UI.Xaml.Controls;
using System.Xml;
using UsosAppMaui.Model.TimeTable;
using UsosAppMaui.Service;
using Border = Microsoft.Maui.Controls.Border;
using RowDefinition = Microsoft.Maui.Controls.RowDefinition;

namespace UsosAppMaui.Pages;

public partial class SchedulePage : ContentPage
{
	private UsosService usosService;
    private DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
    private List<Lesson> Lessons;
    public SchedulePage(UsosService usosService)
	{
		InitializeComponent();
		this.usosService = usosService;      
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

        GridSetup();
        
        Lessons =  usosService.getTimeTable(StartOfWeek.ToString("yyyy-MM-dd"));
        Week.Text = StartOfWeek.ToString("dd.MM.yyyy") + " - " + StartOfWeek.AddDays(6).ToString("dd.MM.yyyy");
        
        UpdateUI();
        

        
    }

    private void UpdateUI()
    {
        TimeTable.Clear();

        int timeNumber = 7;
        for (int i = 0; i < 64; i++)
        {
            Line gray = new Line { BackgroundColor = Color.Parse("#9f9f9f"), HeightRequest = 1 };
            Line darkGray = new Line { BackgroundColor = Color.Parse("#2d2d2d"), HeightRequest = 1 };

            if (i % 2 == 0)
            {
                TimeTable.SetColumn(gray, 1);
                TimeTable.SetColumnSpan(gray, 6);
                TimeTable.SetRow(gray, i);
                TimeTable.Children.Add(gray);
            }
            else
            {
                TimeTable.SetColumn(darkGray, 1);
                TimeTable.SetColumnSpan(darkGray, 6);
                TimeTable.SetRow(darkGray, i);
                TimeTable.Children.Add(darkGray);
            }

            if (i % 4 == 0)
            {
                Label label = new Label { Text = $"{timeNumber}:00", HorizontalOptions = LayoutOptions.Center };
                timeNumber++;
                TimeTable.SetRowSpan(label, 4);
                TimeTable.SetRow(label, i);
                TimeTable.Children.Add(label);
            }

        }

        foreach (var lesson in Lessons)
        {
            int size = (lesson.end_time - lesson.start_time).Minutes / 4;
            int row = (lesson.start_time.Hour * 60 + lesson.start_time.Minute - 7 * 60) / 15;
            int column = (int)lesson.start_time.DayOfWeek;
            string classType = lesson.name.pl.Split('-')[1].Trim();
            string classname = lesson.name.pl.Split('-')[0];
            Border border = new Border
            {
                BackgroundColor = Color.Parse("#dd696969"),

                Padding = Thickness.Zero,
                HeightRequest = 15 * size,
                Margin = new Thickness(0, 7.5, 0, 7.5),
                Content = new VerticalStackLayout
                {

                    new Label
                    {
                        Text = lesson.start_time.TimeOfDay + "-" + lesson.end_time.TimeOfDay,
                        Padding = Thickness.Zero,
                        Margin = new Thickness(10,0,0,0),
                        TextColor= Color.Parse("#FDFD96")
                    },
                    new Label
                    {
                        Text = classType,
                        Padding = Thickness.Zero,
                        Margin = new Thickness(10,0,0,0),
                    },
                    new Label
                    {
                        Text =  classname,
                        Padding = Thickness.Zero,
                        Margin = new Thickness(10,0,0,0),
                        TextColor= Color.Parse("#98fb98"),
                    }
                }
                
                
               
            };


            TimeTable.SetRowSpan(border, size);
            TimeTable.SetColumn(border, column);
            TimeTable.SetRow(border, row);
            TimeTable.Children.Add(border);
        }

        //set TimeLine
        Line CurrentTimeLine = new Line { BackgroundColor = Color.Parse("#aaff0000"), HeightRequest = 1 };
        var CurrentTimeRow = (DateTime.Now.Hour * 60 + DateTime.Now.Minute - 7 * 60)/15;
        if(CurrentTimeRow >= 0)
        {
            TimeTable.SetRow(CurrentTimeLine, CurrentTimeRow);
            TimeTable.SetColumnSpan(CurrentTimeLine, 6);
            TimeTable.Children.Add(CurrentTimeLine);
        }
        
      
    }

    private void GridSetup()
    {
        for (int i = 0; i < 64; i++)
        {
            RowDefinition row = new RowDefinition { Height = 15 };
            TimeTable.RowDefinitions.Add(row);
        } 
    }



    private void PreviousWeekBtn_Clicked(object sender, EventArgs e)
    {
        StartOfWeek = StartOfWeek.AddDays(-7);
        Week.Text = StartOfWeek.ToString("dd.MM.yyyy") + " - " + StartOfWeek.AddDays(6).ToString("dd.MM.yyyy");

        Lessons = usosService.getTimeTable(StartOfWeek.ToString("yyyy-MM-dd"));
        UpdateUI();
    }


    private void NextWeekBtn_Clicked(object sender, EventArgs e)
    {
        StartOfWeek = StartOfWeek.AddDays(7);
        Week.Text = StartOfWeek.ToString("dd.MM.yyyy") + " - " + StartOfWeek.AddDays(6).ToString("dd.MM.yyyy");

        Lessons = usosService.getTimeTable(StartOfWeek.ToString("yyyy-MM-dd"));
        UpdateUI();
    }

}