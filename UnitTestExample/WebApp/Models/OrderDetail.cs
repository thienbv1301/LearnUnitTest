namespace WebApp.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int OrderCode { get; set; }
        public virtual Order Order { get; set; }
    }
}
