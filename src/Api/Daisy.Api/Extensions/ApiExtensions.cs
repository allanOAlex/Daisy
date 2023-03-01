using Daisy.Shared.Extensions;

namespace Daisy.Api.Extensions
{
    public static class ApiExtensions
    {
        public static int ValidateDates(DateTime? start, DateTime? time, DateTime? end)
        {
            try
            {
                var validDates = ModelValidationsExtension.ValidateDates(start, time, end);
                return validDates;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception($"Error Validating Dates | {ex.Message}");
            }

        }

        
    }
}
