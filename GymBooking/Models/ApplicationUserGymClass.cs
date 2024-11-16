namespace GymBooking.Models
{
    public class ApplicationUserGymClass
    {
        //foreign keys
        public int GymClassId { get; set; }
        public string ApplicationUserId { get; set; }

        //navigational properties
        public ApplicationUser User { get; set; }
        public GymClass GymClass { get; set; }
    }
}