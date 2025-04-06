namespace AMRS.Models
{
    /// <summary>
    /// Airport Model - Create / Search / Delete airport entry
    /// </summary>
    public class AirportsModel
    {
        //private AMRSDBContext context;
        public int portId {get; set;}
        public required string portName { get; set; }
        public required string portCity { get; set; }
        public required string portCountry { get; set; }
        public required string portZip { get; set; }        

    }
}
