namespace cw8_2.Entities
{
    public class Order : BaseEntity
    {
        public DateTime RegisterDate { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public bool IsPaid { get; set; } = false;
        public List<Product> products { get; set; } = new List<Product>();
    }
}
