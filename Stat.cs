using System.ComponentModel.DataAnnotations;


namespace aspnetharjoitus
{
    public class Stat
    {
        [Key]
        public int Id { get; set; }
        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int CurrentLocationID { get; set; }
    }
}
