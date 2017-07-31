namespace Sandbox
{
    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
