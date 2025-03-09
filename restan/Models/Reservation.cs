namespace restan.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Pnumber { get; set; }
        public int Person {  get; set; }
        public DateTime Reservdate { get; set; }
        public string Time {  get; set; }
    }
}
