using System;
using System.Collections.Generic;
using System.Text;

namespace Class_Box_Data
{
    class Box
    {private const string ERROR_MESSIGE = "{0} cannot be zero or negative.";
		private double lenght;
		private double width;
		private double height;

		public Box(double lenght, double width, double height)
		{
			this.Lenght = lenght;
			this.Width = width;
			this.Height = height;
		}
		public double Lenght
		{
			get { return this.lenght; }
			private set
			{
				ValidateSIdes(value, nameof(this.Lenght));

				this.lenght = value;
			}
		}
		public double Width
		{
			get { return this.width; }
			private set
			{
				ValidateSIdes(value, nameof(this.Width));
				this.width = value;
			}
		}
		public double Height
		{
			get { return this.height; }
			private set 
			{
				ValidateSIdes(value, nameof(this.Height));
				this.height = value; 
			}
		}

		public string SurfaceArea()
		{
			double result = 2 * (this.Lenght * this.Width + this.Lenght * this.Height + this.Width * this.Height);
			return $"{result:f2}";
		}
		public string LateralSurfaceArea()
		{
			double result = 2 * (this.Lenght * this.Height + this.Width * this.Height);
			return $"{result:f2}";
		}
		public string Volume()
		{
			double result = this.Lenght * this.Width * this.Height;
			return $"{result:f2}";
		}
		
		private void ValidateSIdes(double value,string messige)
		{
			if (value <= 0)
				{
					throw new InvalidOperationException(string.Format(ERROR_MESSIGE, messige));

				}
		}
	}
}
