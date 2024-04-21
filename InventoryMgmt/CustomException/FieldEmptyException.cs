namespace InventoryMgmt.CustomException
{
    public class FieldEmptyException : Exception
    {
        public FieldEmptyException(string message) : base(message) { }
    }
}
