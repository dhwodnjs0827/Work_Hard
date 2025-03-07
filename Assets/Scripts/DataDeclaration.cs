namespace DataDeclaration
{
    #region Enum
    public enum ItemType
    {
        Consumable,
        Sign
    }

    public enum UIState
    {
        Main,
        Menu
    }
    #endregion
    
    #region Interface
    public interface IInteraction
    {
        public void OnInteract();
    }
    #endregion
}
