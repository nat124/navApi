using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class HighChartModel
    {
        public HighChartModel()
        {
            lang = new Lang();
            chart = new Chart();
            title = new Title();
            subtitle = new SubTitle();
            plotOptions = new PlotOption();
            xAxis = new List<XAxis>();
            yAxis = new List<YAxis>();
            legend = new Legend();
            series = new List<Series>();
            drilldown = new DrillDown();
            colors = new List<string>();

        }

        public Lang lang { get; set; }
        public List<string> colors { get; set; }
        public Chart chart { get; set; }
        public Title title { get; set; }
        public SubTitle subtitle { get; set; }
        public PlotOption plotOptions { get; set; }
        public List<XAxis> xAxis { get; set; }
        public List<YAxis> yAxis { get; set; }
        public Legend legend { get; set; }
        public List<Series> series { get; set; }
        public DrillDown drilldown { get; set; }
        public Credit credits { get; set; }
        public int MaxLength { get; set; }
        public decimal? Difference { get; set; }
    }

    public class PlotOption
    {
        public PlotOption()
        {
            pie = new Pie();
            series = new SeriesPlotOption();
        }
        public Pie pie { get; set; }
        public SeriesPlotOption series { get; set; }
    }
    public class SeriesPlotOption
    {
        public SeriesPlotOption()
        {
            dataLabels = new DataLabels();
        }
        public DataLabels dataLabels { get; set; }
    }
    public class Pie
    {
        public Pie()
        {
            allowPointSelect = true;
            cursor = "pointer";
            dataLabels = new DataLabels();
            showInLegend = true;
        }
        public bool allowPointSelect { get; set; }
        public string cursor { get; set; }
        public DataLabels dataLabels { get; set; }
        public bool showInLegend { get; set; }
    }
    public class DataLabels
    {
        public DataLabels()
        {
            enabled = true;

        }
        public bool enabled { get; set; }
        public string format { get; set; }
        public int rotation { get; set; }
        public int y { get; set; }
    }
    public class Lang
    {
        public Lang()
        {
            drillUpText = "Back";
        }
        public string drillUpText { get; set; }
    }

    public class DrillDown
    {
        public DrillDown()
        {
            series = new List<Series>();
        }
        public List<Series> series { get; set; }
    }

    public class DataDrill
    {
        public DataDrill()
        {
            data = new List<ArrayList>();
        }
        public string name { get; set; }
        public string id { get; set; }
        public List<ArrayList> data { get; set; }
    }

    public class Series
    {
        public Series()
        {
            data = new List<Data>();
            //dataLabels = new DataLabels();
        }
        public string name { get; set; }
        public string id { get; set; }
        public int yAxis { get; set; }
        public string type { get; set; }
        public List<Data> data { get; set; }
        //public DataLabels dataLabels { get; set; }
        public bool colorByPoint { get; set; }
    }

    public class Data
    {
        public Data()
        {
            drilldown = true;
        }
        public string name { get; set; }
        public decimal y { get; set; }
        public bool drilldown { get; set; }
    }

    public class Legend
    {
        public Legend()
        {
            layout = "horizontal"; // default
            itemDistance = 50;
            enabled = true;
            align = "center";
            //horizontalAlign = "bottom";
            //layout = "horizontal";
        }
        public int itemDistance { get; set; }
        public bool enabled { get; set; }
        public string align { get; set; }
        //public string labelFormat { get; set; }
        //public string horizontalAlign { get; set; } 
        public string layout { get; set; }
    }
    public class Label
    {
        public Label()
        {
            enabled = true;
        }
        public bool enabled { get; set; }
    }

    public class XAxis
    {
        public XAxis()
        {
            type = "category";
            title = new Title();
            labels = new Label();
        }
        public string type { get; set; }
        public Title title { get; set; }
        public Label labels { get; set; }
    }
    public class YAxis
    {
        public YAxis()
        {
            title = new Title();
        }
        public Title title { get; set; }
        public bool opposite { get; set; }
    }

    public class SubTitle
    {
        public SubTitle()
        {
            text = string.Empty;
        }
        public string text { get; set; }
    }

    public class Title
    {
        public Title()
        {
            text = string.Empty;
        }
        public string text { get; set; }
    }

    public class Chart
    {
        public Chart()
        {
            height = 200;
            width = 450;
            type = "line";
        }
        public string type { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }
    public class Credit
    {
        public Credit()
        {
            enabled = false;
        }
        public bool enabled { get; set; }
    }
}