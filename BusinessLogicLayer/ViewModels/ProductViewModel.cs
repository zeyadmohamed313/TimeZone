namespace BusinessLogicLayer.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public byte[] Img { get; set; }
    }
}
