namespace DataDeclaration
{
    #region Enum
    public enum ItemType
    {
        Recoverable,
        Sign
    }

    public enum ConditionType
    {
        Stamina,
        SlowTime,
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
        /// <summary>
        /// 상호작용 시 실행할 로직 구현
        /// </summary>
        public void OnInteract();
    }
    #endregion
}
