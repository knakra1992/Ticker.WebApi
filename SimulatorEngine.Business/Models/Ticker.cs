namespace SimulatorEngine.Business.Models
{
	public class Ticker
	{
		public string? TickerId { get; set; }

		public string Symbol { get; set; }

		public decimal Price { get; set; }

		public string Source { get; set; }
	}
}

