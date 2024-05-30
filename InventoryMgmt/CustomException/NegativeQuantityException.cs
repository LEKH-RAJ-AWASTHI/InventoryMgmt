namespace InventoryMgmt.CustomException
{
    public class NegativeQuantityException : Exception
    {
        public NegativeQuantityException(string message) : base(message)
        {
        }
    }
}
