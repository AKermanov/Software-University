using System;

namespace Class_Box_Data
{
    class StartUp
    {
        static void Main(string[] args)
        {
			try
			{
				var lenght = double.Parse(Console.ReadLine());
				var width = double.Parse(Console.ReadLine());
				var height = double.Parse(Console.ReadLine());

				var box = new Box(lenght , width, height);
				Console.WriteLine($"Surface Area - {box.SurfaceArea()}");
				Console.WriteLine($"Lateral Surface Area - {box.LateralSurfaceArea()}");
				Console.WriteLine($"Volume - {box.Volume()}");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }
    }
}
