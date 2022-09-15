using TourSelector.Data.Models;

namespace TourSelector.Data
{
    public class TourService
    {
        private static readonly string[] tourNames = new[]
        {
            "Safari", "Whale Spotting", "Historical Monuments"
        };

        private TourRequestEmitter tourRequestEmitter = new TourRequestEmitter();

        public Task<string[]> GetTourNamesAsync()
        {
            return Task.FromResult(tourNames);
        }

        public void HandleTourRequest(TourRequest tourRequest)
        {
            tourRequestEmitter.Emit(tourRequest);
        }
    }
}
