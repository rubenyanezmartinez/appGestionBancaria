// Charts for WinForms (c) 2017 MIT License <baltasarq@gmail.com>

namespace Graficos.UI {
    using System.Linq;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;

    /// <summary>
    /// Draws a simple chart.
    /// Note that Mono's implementation of WinForms Chart is incomplete.
    /// </summary>
    public class Chart: PictureBox {
        public enum ChartType { Lines, Bars, Guided };
        
        public Chart(int width, int height)
        {

            this.values = new List<List<int>>();
            this.legendValues = new List<string>();
            this.LegendValuesX = new List<string>();
            this.Width = width;
            this.Height = height;
            this.FrameWidth = 50;
            this.Type = ChartType.Lines;
            this.AxisPen = new Pen( Color.Black ) { Width = 10 };
            this.DataPen = new Pen( Color.Red ) { Width = 4 };
            this.DataFont = new Font( FontFamily.GenericMonospace, 12 );
            this.DataTextPen = new Pen(Color.Black) { Width = 1 };
            this.DataTextFont = new Font(FontFamily.GenericSansSerif, 10);
            this.LegendFont = new Font( FontFamily.GenericSansSerif, 12 );
            this.LegendPen = new Pen( Color.Navy );
            this.Colors = new List<Color>() { Color.Blue, Color.FromArgb(255 / 2, Color.Orange.R, Color.Orange.G, Color.Orange.B), Color.FromArgb(255 / 2, Color.Red.R, Color.Red.G, Color.Red.B), Color.FromArgb(255 / 2, Color.Green.R, Color.Green.G, Color.Green.B) };
        
            this.Build();
        }

        public Chart(int width, int height, List<Color> colors, ChartType type = ChartType.Lines)
        {

            this.values = new List<List<int>>();
            this.legendValues = new List<string>();
            this.LegendValuesX = new List<string>();
            this.Width = width;
            this.Height = height;
            this.FrameWidth = 50;
            this.Type = type;
            this.AxisPen = new Pen(Color.Black) { Width = 10 };
            this.DataPen = new Pen(Color.Red) { Width = 4 };
            this.DataFont = new Font(FontFamily.GenericMonospace, 12);
            this.DataTextPen = new Pen(Color.Black) { Width = 1 };
            this.DataTextFont = new Font(FontFamily.GenericSansSerif, 10);
            this.LegendFont = new Font(FontFamily.GenericSansSerif, 12);
            this.LegendPen = new Pen(Color.Navy);
            this.Colors = colors;
            this.Build();
        }

        /// <summary>
        /// Redraws the chart
        /// </summary>
        public void Draw()
        {
            
            this.grf.DrawRectangle(
                            new Pen( this.BackColor ),
                            new Rectangle( 0, 0, this.Width, this.Height ) );

            this.DrawAxis();
            if (this.Type == ChartType.Guided) this.DrawGuideLines();
            this.DrawData();
            this.DrawLegends();
            
        }
        
        private void DrawLegends()
        {
            StringFormat verticalDrawFmt = new StringFormat {
                FormatFlags = StringFormatFlags.DirectionVertical
            };
            int legendXWidth = (int) this.grf.MeasureString(
                                                        this.LegendX,
                                                        this.LegendFont ).Width;
            int legendYHeight = (int) this.grf.MeasureString(
                                                        this.LegendY,
                                                        this.LegendFont,
                                                        new Size( this.Width,
                                                                  this.Height ),
                                                        verticalDrawFmt ).Height;


            this.grf.DrawString(
                    this.LegendX,
                    this.DataTextFont,
                    this.DataTextPen.Brush,
                    new Point(
                        (this.Width - legendXWidth) / 2,
                        this.Height - (int)this.grf.MeasureString(this.LegendX, this.LegendFont).Height));

            this.grf.DrawString(
                    this.LegendY,
                    this.DataTextFont,
                    this.DataTextPen.Brush,
                    new Point( 
                        this.Type == ChartType.Guided ? 0 : this.FramedOrgPosition.X - ( this.FrameWidth / 2 ),
                        ( this.Height - legendYHeight ) / 2 ),
                    verticalDrawFmt );

            var legendsColorsMargin = 20;
            var marginBetwenLegends = 10;
            int squareSize = 10;
            for(int i = 0; i < this.legendValues.Count; i++)
            {

                int margin = i > 0 ? i * (squareSize + (int)this.grf.MeasureString(this.legendValues[i - 1], DataFont).Width) + marginBetwenLegends : 0;
                LegendPen.Color = this.Colors[i % Colors.Count];
                this.grf.FillRectangle(LegendPen.Brush, new Rectangle(this.FramedOrgPosition.X + margin, legendsColorsMargin, squareSize, squareSize));
                this.grf.DrawString(this.legendValues[i], DataFont, LegendPen.Brush, this.FramedOrgPosition.X + squareSize + margin, legendsColorsMargin);

            }

        } 
        


        private void DrawData()
        {
            this.NormalizeData();
          
            for (int j = 0; j < this.values.Count; j++)
            {
               
                DataPen.Color = this.Colors[j % this.Colors.Count];
                int numValues = this.values[j].Count;
                var p = this.DataOrgPosition;
                int xGap = this.GraphWidth / (numValues + 1);
                int baseLine = this.DataOrgPosition.Y;


                
                for (int i = 0; i < numValues; ++i) {
                    string tag = this.values[j][i].ToString();
                    int tagWidth = (int)this.grf.MeasureString(
                                                            tag,
                                                            this.DataFont).Width;
                    var nextPoint = new Point(
                        p.X + xGap, baseLine - this.normalizedData[j][i]
                    );

                    if (this.Type == ChartType.Bars) {
                        p = new Point(nextPoint.X, baseLine);
                    }

                    this.grf.DrawLine(this.DataPen, p, nextPoint);
                    if(this.Type != ChartType.Guided) this.grf.DrawString(tag, this.DataFont, this.DataPen.Brush,new Point(nextPoint.X - tagWidth,nextPoint.Y));
                    p = nextPoint;
                }
            }
        }
        
        private void DrawAxis()
        {
            var maxList = new List<int>();
            this.values.ForEach((value) => maxList.Add(value.Max()));
            int maxValue = maxList.Max();

            var maxLegendSize = this.grf.MeasureString(maxValue.ToString(), DataFont).Width;

            var xPositionYAxis = FramedOrgPosition.X;
            if(this.Type == ChartType.Guided)
            {
                xPositionYAxis = System.Math.Min(this.FramedOrgPosition.X + (int)maxLegendSize / 2, (int)(DataOrgPosition.X - AxisPen.Width/2));
            }

            // Y axis
            this.grf.DrawLine( this.AxisPen,
                               new Point(xPositionYAxis, FramedOrgPosition.Y),
                               new Point(
                                        xPositionYAxis,
                                        this.FramedEndPosition.Y ) );
                                        
            // X axis
            this.grf.DrawLine( this.AxisPen,
                               new Point(
                                        this.FramedOrgPosition.X,
                                        (int)(this.DataOrgPosition.Y + AxisPen.Width/2) ),
                               new Point(this.FramedEndPosition.X, (int)(this.DataOrgPosition.Y + AxisPen.Width / 2)) );
        }

        private void DrawGuideLines()
        {
            NormalizeAxis();
            
            var maxList = new List<int>();
            this.values.ForEach((value) => maxList.Add(value.Max()));
            int maxValue = maxList.Max();
            var legendMargin = this.grf.MeasureString(maxValue.ToString(), DataFont).Width;
            Pen guidePen = new Pen(Color.Gray);
            foreach (KeyValuePair<string, int> pair in this.legendValuesY)
            {
                Point startPoint = new Point(this.FramedOrgPosition.X, this.DataOrgPosition.Y - pair.Value);
                Point endPoint = new Point(this.FramedEndPosition.X, this.DataOrgPosition.Y - pair.Value);
                this.grf.DrawLine(guidePen, startPoint, endPoint);
                this.grf.DrawString(pair.Key, this.DataTextFont, this.DataTextPen.Brush, this.grf.MeasureString(this.LegendY, LegendFont).Height, this.DataOrgPosition.Y - pair.Value );
            }


            int numValues = this.LegendValuesX.Count;

            int xGap = this.GraphWidth / (numValues + 1);

            

            for (int i = 1; i <= numValues; i++)
            {
                
                var startPoint = new Point(this.DataOrgPosition.X + xGap * i, this.FramedOrgPosition.Y);
                var endPoint = new Point(this.DataOrgPosition.X + xGap * i, this.FramedEndPosition.Y);
                this.grf.DrawLine(guidePen, startPoint, endPoint);
                
                this.grf.DrawString(LegendValuesX[i-1],
                                        DataTextFont, DataTextPen.Brush, 
                                        (this.DataOrgPosition.X + xGap * i) - this.grf.MeasureString(LegendValuesX[i-1], DataFont).Width / 2, 
                                        this.FramedEndPosition.Y);
            }



        }

        private void Build()
        {
			Bitmap bmp = new Bitmap( this.Width, this.Height );
			this.Image = bmp;
            this.grf = Graphics.FromImage( bmp );
        }

        private void NormalizeAxis()
        {
            int maxHeight = this.DataOrgPosition.Y - this.FrameWidth;
            var maxList = new List<int>();
            this.values.ForEach((value) => maxList.Add(value.Max()));
            int maxValue = maxList.Max() == 0 ? 10 : maxList.Max();

            int numLegendsAxisY = maxValue > 6 ? Enumerable.Range(6, 20).Where(i => maxValue % i == 0).Min() : maxValue;
            
            double axisDiference = (double)maxValue / (double)numLegendsAxisY;
            this.legendValuesY = new Dictionary<string, int>();
            for (int i = 0; i <= numLegendsAxisY; i++)
            {
                var actualValue = maxValue - (i * axisDiference);
                var actualPosition = (actualValue * maxHeight) / maxValue;
                this.legendValuesY.Add((actualValue).ToString(), (int)(actualPosition));
            }
            
            
        }
        
        private void NormalizeData()
        {
            int maxHeight = this.DataOrgPosition.Y - this.FrameWidth;
            var maxList = new List<int>();
            this.values.ForEach((value) => maxList.Add(value.Max()));
            int maxValue = maxList.Max() == 0 ? 1 : maxList.Max();
            this.normalizedData = new int[this.values.Count][];
            for (int j = 0; j < this.values.Count; j++)
            {
                
                
                this.normalizedData[j] = this.values[j].ToArray();

                for (int i = 0; i < this.normalizedData[j].Length; ++i)
                {
                    this.normalizedData[j][i] =
                                        (this.values[j][i] * maxHeight) / maxValue;
                }

            }
        }

       

        /// <summary>
        /// Gets or sets the values used as data.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<IEnumerable<int>> Values {
            get {
                return this.values.ToArray();
            }
            set {
                this.values.Clear();
                foreach(IEnumerable<int> list in value)
                {
                    
                    this.values.Add(list as List<int>);
                }
                
            }
        }

        public Dictionary<string, IEnumerable<int>> LegendValues
        {
            get
            {
                Dictionary<string, IEnumerable<int>> toret = new Dictionary<string, IEnumerable<int>>();
                if (legendValues.Count > 0)
                {
                    
                    for (int i = 0; i < legendValues.Count; i++)
                    {
                        toret.Add(legendValues[i], values[i]);
                    }
                   
                }
                return toret;
            }
            set
            {
                this.values.Clear();
                this.legendValues.Clear();
                foreach (KeyValuePair<string, IEnumerable<int>> pair in value)
                {
                    this.legendValues.Add(pair.Key);
                    this.values.Add(pair.Value.ToList());
                }

            }
        }


        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Point"/>.</value>
        public Point DataOrgPosition {
            get {
                int margin = (int) ( this.AxisPen.Width * 2 );
                
                return new Point(
                    this.FramedOrgPosition.X + margin,
                    this.FramedEndPosition.Y - margin );
            }
        }
        
        /// <summary>
        /// Gets or sets the width of the frame around the chart.
        /// </summary>
        /// <value>The width of the frame.</value>
        public int FrameWidth {
            get; set;
        }
        
        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Point"/>.</value>
        public Point FramedOrgPosition {
            get {
                return new Point( this.FrameWidth, this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the framed end.
        /// </summary>
        /// <value>The end <see cref="Point"/>.</value>
        public Point FramedEndPosition {
            get {
                return new Point( this.Width - this.FrameWidth,
                                  this.Height - this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the width of the graph.
        /// </summary>
        /// <value>The width of the graph.</value>
        public int GraphWidth {
            get {
                return this.Width - ( this.FrameWidth * 2 );
            }
        }
        
        /// <summary>
        /// Gets or sets the pen used to draw the axis.
        /// </summary>
        /// <value>The axis <see cref="Pen"/>.</value>
        public Pen AxisPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the pen used to draw the data.
        /// </summary>
        /// <value>The data <see cref="Pen"/>.</value>
        public Pen DataPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the data font.
        /// </summary>
        /// <value>The data <see cref="Font"/>.</value>
        public Font DataFont {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the legend for the x axis.
        /// </summary>
        /// <value>The legend for axis x.</value>
        public string LegendX {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the legend for the y axis.
        /// </summary>
        /// <value>The legend for axis y.</value>
        public string LegendY {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the font for legends.
        /// </summary>
        /// <value>The <see cref="Font"/> for legends.</value>
        public Font LegendFont {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the pen for legends.
        /// </summary>
        /// <value>The <see cref="Pen"/> for legends.</value>
        public Pen LegendPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the type of the chart.
        /// </summary>
        /// <value>The <see cref="ChartType"/>.</value>
        public ChartType Type {
            get; set;
        }

        public List<Color> Colors
        {
            get;set;
        }

        public List<string> LegendValuesX
        {
            get;set;
        }

        //The color and font of all legends

        public Font DataTextFont
        {
            get;set;
        }

        public Pen DataTextPen
        {
            get;set;
        }
        
       
        private Graphics grf;
        private List<List<int>> values;
        //To show the type of the line with the color of the line
        private List<string> legendValues;
        private Dictionary<string, int> legendValuesY;
        private int[][] normalizedData;
    }
}
