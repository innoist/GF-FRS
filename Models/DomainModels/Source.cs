namespace FRS.Models.DomainModels
{
    public class Source
    {
        public byte Value { get; set; }
        public string Name { get; set; }
        public byte StatusId { get; set; }

        public virtual Status Status { get; set; }
    }
}
